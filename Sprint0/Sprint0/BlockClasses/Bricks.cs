using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Sprites;

namespace Sprint0.BlockClasses
{
    public enum BrickType
    {
        Hidden, Normal, Used, Destroyed
    }

    //public for genernate blocks
    class Bricks : Blocks
    {
        public BrickType bType;
        private readonly IBrickStates[] bStates;
        public IBrickStates currentbState;
        public bool IsBumping { get; private set; }
        public bool containItems { get; private set; }     
        private List<ItemSprite> items;
        private int MinY, MaxY;
        protected Point positionOffset = new Point(1, 1);
        protected Vector2 spriteSpeed = new Vector2(50.0f, 200.0f);
        public Bricks(Texture2D sheet, Vector2 pos, Point rowAndColumn, int totalFrame, BrickType type, List<ItemSprite> itemList) 
            : base(sheet, pos, rowAndColumn,totalFrame)
        {
            items = itemList;
            containItems = itemList.Count != 0 ? true : false;
            bStates = new IBrickStates[4] { new HiddenState(), new NormalState(), new BumpingState(), new UsedOrDestroyedState() };
            bType = type;
            currentbState = GenerateCurrentState();
            IsBumping = false;
        }

        private IBrickStates GenerateCurrentState()
        {
            switch (bType)
            {
                case BrickType.Hidden :
                    return bStates[0];
                case BrickType.Used:
                case BrickType.Destroyed:
                    return bStates[3];
                default:
                    return bStates[1];
            }
        }
        /*
         *public void Draw(SpriteBatch spriteBatch, Vector2 location, bool isLeft)
          {
              blockSprite.Draw(spriteBatch, location, isLeft);
          }

          public void Update(GameTime gameTime)
          {
              blockSprite.Update(gameTime);
          }
          */
        #region FromHiddenToBrick
        public void ChangeToBrick()
        {
            bType = BrickType.Normal;
            SpriteSheets = Sprint0.BlockTextures[0];
            currentbState = bStates[1];
        }
        #endregion
        private void ChangeToUsed()
        {
            bType = BrickType.Used;
            SpriteSheets = Sprint0.BlockTextures[2];
            currentbState = bStates[3];
            totalFrame = 1;
        }
        public void ChangeToDestroyed()
        {
            bType = BrickType.Destroyed;
            currentbState = bStates[3];
        }
        public void Bumping()
        {
            IsBumping = true;
            currentbState = bStates[2];
            if (containItems)
                ShowItem();
            MinY = ((int)bPosition.Y - frameSize.Y)/2;
            MaxY = (int)bPosition.Y;
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);   
            if (IsBumping)
            {                
                bPosition.Y -= positionOffset.Y != 0 ? spriteSpeed.Y * (float)gameTime.ElapsedGameTime.TotalSeconds : 0;
                if (bPosition.Y < MinY)
                {
                    spriteSpeed.Y *= -1;
                    bPosition.Y = MinY;
                }
                if (bPosition.Y > MaxY)
                {
                    IsBumping = false;
                    spriteSpeed.Y *= -1;
                    bPosition.Y = MaxY;
                }
            }
        }
        public override void Draw(SpriteBatch spriteBatch, Vector2 location, bool isLeft)
        {
            if (bType != BrickType.Hidden && bType != BrickType.Destroyed && !IsBumping)
            {
                base.Draw(spriteBatch, location, isLeft);
            }
            if (IsBumping)
            {
                base.Draw(spriteBatch, bPosition, isLeft);
            }
        }
        private void ShowItem()
        {
            ItemSprite item = items.First();
            items.RemoveAt(0);
            item.bumping(this.Position, this.Position.Y + frameSize.Y, spriteSpeed);
            if (items.Count == 0)
            {
                containItems = false;
                ChangeToUsed();
            }
        }

        
    }
}
