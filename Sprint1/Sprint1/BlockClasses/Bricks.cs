using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint1.FactoryClasses;
using Sprint1.Sprites;

namespace Sprint1.BlockClasses
{
   
    //public for genernate blocks
    class Bricks : Blocks
    {
        private readonly IBlockStates[] bStates;
        public IBlockStates currentbState;
        private Vector2 bPosition;
        private bool IsBumping;
        private bool containItems;
        private readonly ArrayList items;
        private readonly ArrayList shownItems;
        private int MinY, MaxY;
        protected Point positionOffset = new Point(1, 1);
        protected Vector2 spriteSpeed = new Vector2(50.0f, 200.0f);
        public Bricks(Texture2D sheet, Vector2 pos, Point rowAndColumn, int totalFrame, BlockType type, ArrayList itemList) 
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
                case BlockType.Hidden :
                    return bStates[0];
                case BlockType.Used:
                case BlockType.Destroyed:
                    return bStates[3];
                default:
                    return bStates[1];
            }
        }
        #region CommandReciver
        public void ChangeToBrick()
        {
            bType = BlockType.BNormal;
            SpriteSheets = BlockFactory.BlockTextures[0];
            //currentbState = GenerateCurrentState(); for key-map specific test 
        }
        private void ChangeToUsed()
        {
            bType = BlockType.Used;
            base.ResizeFrame(BlockFactory.BlockTextures[2], new Point(4, 1), 1);          
            currentbState = GenerateCurrentState();
        }
        public void ChangeToDestroyed()
        {
            bType = BlockType.Destroyed;
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
                        item.Bumping(bPosition, bPosition.Y - 3 * FrameSize.Y, spriteSpeed);
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
        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (AnimatedSprite sprite in shownItems)
                sprite.Draw(spriteBatch);
            if (bType != BlockType.Hidden && bType != BlockType.Destroyed)
            {
                base.Draw(spriteBatch);
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
                    return ItemFactory.Instance.GetRedMushroom();
                case "greenMushroom":
                    return ItemFactory.Instance.GetGreenMushroom();
                case "star":
                    return ItemFactory.Instance.GetStar();
                case "flower":
                    return ItemFactory.Instance.GetFlower();
                default:
                    return ItemFactory.Instance.GetCoin();
            }
        }
    }
}
