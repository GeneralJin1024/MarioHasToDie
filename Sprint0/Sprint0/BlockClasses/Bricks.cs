using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.FactoryClasses;
using Sprint0.Sprites;

namespace Sprint0.BlockClasses
{
    public enum BrickType
    {
        Hidden, BNormal, QNormal, Used, Destroyed
    }

    //public for genernate blocks
    class Bricks : Blocks
    {
        public BrickType bType;
        private readonly IBlockStates[] bStates;
        public IBlockStates currentbState;
        private Vector2 bPosition;
        private bool IsBumping;
        private bool containItems;
        private ArrayList items;
        private ArrayList shownItems;
        private int MinY, MaxY;
        protected Point positionOffset = new Point(1, 1);
        protected Vector2 spriteSpeed = new Vector2(50.0f, 200.0f);
        public Bricks(Texture2D sheet, Vector2 pos, Point rowAndColumn, int totalFrame, BrickType type, ArrayList itemList) 
            : base(sheet, pos, rowAndColumn,totalFrame)
        {
            items = itemList;
            shownItems = new ArrayList { };
            containItems = itemList.Count != 0 ? true : false;
            bPosition = pos;
            bStates = new IBlockStates[4] { new HiddenState(), new NormalState(), new BumpingState(), new UsedOrDestroyedState() };
            bType = type;
            currentbState = GenerateCurrentState();
            IsBumping = false;
        }

        private IBlockStates GenerateCurrentState()
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
        #region CommandReciver
        public void ChangeToBrick()
        {
            bType = BrickType.BNormal;
            SpriteSheets = BlockFactory.BlockTextures[0];
            //currentbState = GenerateCurrentState(); for key-map specific test 
        }
        private void ChangeToUsed()
        {
            bType = BrickType.Used;
            base.ResizeFrame(BlockFactory.BlockTextures[2], new Point(4, 1), 1);          
            currentbState = GenerateCurrentState();
        }
        public void ChangeToDestroyed()
        {
            bType = BrickType.Destroyed;
            currentbState = GenerateCurrentState();
        }
        public void Bumping()
        {
            IsBumping = true;
            currentbState = bStates[2];
            MinY = (int)bPosition.Y - FrameSize.Y;
            MaxY = (int)bPosition.Y;
        }
        #endregion
        public override void Update(GameTime gameTime)
        {
            foreach (AnimatedSprite sprite in shownItems)
                sprite.Update(gameTime);
            base.Update(gameTime);   
            if (IsBumping)
            {                
                bPosition.Y -= positionOffset.Y != 0 ? spriteSpeed.Y * (float)gameTime.ElapsedGameTime.TotalSeconds : 0;
                if (bPosition.Y < MinY)
                {
                    if (containItems)
                    {
                        ItemSprite item = GenerateItems();
                        shownItems.Add(item);
                        item.bumping(bPosition, bPosition.Y - 3 * FrameSize.Y, spriteSpeed);
                        RemoveItem();
                    }
                    spriteSpeed.Y *= -1;
                    bPosition.Y = MinY;
                }
                if (bPosition.Y > MaxY)
                {
                    IsBumping = false;
                    currentbState = GenerateCurrentState();
                    spriteSpeed.Y *= -1;
                    bPosition.Y = MaxY;
                }
            }
        }
        public override void Draw(SpriteBatch spriteBatch, Vector2 location, bool isLeft)
        {
            foreach (AnimatedSprite sprite in shownItems)
                sprite.Draw(spriteBatch, sprite.Position, isLeft);
            if (bType != BrickType.Hidden && bType != BrickType.Destroyed)
            {
                base.Draw(spriteBatch, bPosition, isLeft);
            }
        }
        #region Methods Interact Items
        public void RemoveItem()
        {
            items.RemoveAt(0);
            if (items.Count == 0)
            {
                containItems = false;
                ChangeToUsed();
            }
        }
        #endregion
        private ItemSprite GenerateItems()
        {
            switch (items[0])
            {
                case "redMushroom":
                    return ItemFactory.Instance.getRedMushroom();
                case "greenMushroom":
                    return ItemFactory.Instance.getGreenMushroom();
                case "star":
                    return ItemFactory.Instance.getStar();
                case "flower":
                    return ItemFactory.Instance.getFlower();
                default:
                    return ItemFactory.Instance.getCoin();
            }
        }
    }
}
