using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0.BlockClasses
{
    public enum BrickState
    {
        bHidden, bempty, bitem, used, destroyed
    }

    //public for genernate blocks
    class Bricks : Blocks
    {
        public BrickState bState;
        public bool IsBumping { get; private set; }
        public bool containItems;
        private List<ISprite> items;
        private int MinY, MaxY;
        private float[] destroyedBrickPosX;
        private float[] destroyedBrickPosY;
        protected Point positionOffset = new Point(1, 5);
        protected Vector2 spriteSpeed = new Vector2(50.0f, 200.0f);
        private Vector2 dPos;
        public Bricks(Texture2D sheet, Vector2 pos, Point rowAndColumn, int totalFrame, BrickState state, List<ISprite> itemList) 
            : base(sheet, pos, rowAndColumn,totalFrame)
        {
            items = itemList;
            containItems = itemList.Count != 0 ? true : false;
            bState = state;
            IsBumping = false;
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
            bState = GetBrickState(items);
            SpriteSheets = Sprint0.BlockTextures[0];
        }
        private BrickState GetBrickState(List<ISprite> itemList)
        {
            if (itemList.Count != 0)
            {
                return BrickState.bitem;
            } else
            {
                return BrickState.bempty;
            }
        }
        #endregion

        public void ChangeToUsed()
        {
            bState = BrickState.used;
            SpriteSheets = Sprint0.BlockTextures[2];
        }
        public void ChangeToDestroyed()
        {
            bState = BrickState.destroyed;
        }
        public void Bumping()
        {
            IsBumping = true;
            if (bState == BrickState.bitem)
                ShowItem();
            MinY = (int)bPosition.Y - frameSize.Y;
            MaxY = (int)bPosition.Y;
        }
        public override void Update(GameTime gameTime)
        {
            if (!IsBumping)
            {
                base.Update(gameTime);
            }
            else
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
            if (bState != BrickState.bHidden && bState != BrickState.destroyed && !IsBumping)
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
            items.RemoveAt(0);
            if (items.Count == 0)
            {
                containItems = false;
                this.ChangeToUsed();
            }
        }

        
    }
}
