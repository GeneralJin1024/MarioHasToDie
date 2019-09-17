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
        public bool containItems;
        private List<ISprite> items;
        private bool isBumping;
        private int MinY, MaxY;
        private float[] destroyedBrickPosX;
        private float[] destroyedBrickPosY;
        public Bricks(Texture2D sheet, Vector2 pos, Point rowAndColumn, int totalFrame, BrickState state, List<ISprite> itemList) 
            : base(sheet, pos, rowAndColumn,totalFrame)
        {
            items = itemList;
            containItems = itemList.Count != 0 ? true : false;
            bState = state;
            isBumping = false;
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
            SpriteSheets = Sprint0.BlockTextures[3];
        }
        public void ChangeToDestroyed()
        {
            bState = BrickState.destroyed;
            destroyedBrickPosX = new float[4];
            destroyedBrickPosY = new float[4];
        }
        public void Bumping()
        {
            isBumping = true;
            if (bState == BrickState.bitem)
                ShowItem();
            MinY = (int)bPosition.Y;
            MaxY = (int)bPosition.Y + frameSize.Y;
        }
        public override void Update(GameTime gameTime)
        {
            if (bState == BrickState.destroyed)
            {
                Vector2 dPos = new Vector2(0, 0);
                Point positionOffset = new Point(1, 5);
                Vector2 spriteSpeed = new Vector2(10.0f, 50.0f);
                dPos.X += positionOffset.X != 0 ? spriteSpeed.X * (float)gameTime.ElapsedGameTime.TotalSeconds : 0;
                dPos.Y += positionOffset.Y != 0 ? spriteSpeed.Y * (float)gameTime.ElapsedGameTime.TotalSeconds : 0;

                destroyedBrickPosX[0] = bPosition.X - dPos.X; destroyedBrickPosX[1] = bPosition.X - dPos.X;
                destroyedBrickPosX[2] = bPosition.X + dPos.X; destroyedBrickPosX[3] = bPosition.X + dPos.X;

                for (int i = 0; i < 4; i++)
                    destroyedBrickPosY[i] = bPosition.Y + dPos.Y;
            }
            else if (!isBumping)
            {
                base.Update(gameTime);
            }
            else
            {
                Point positionOffset = new Point(0, 1);
                Vector2 spriteSpeed = new Vector2(50.0f, 50.0f);
                bPosition.Y += positionOffset.Y != 0 ? spriteSpeed.Y * (float)gameTime.ElapsedGameTime.TotalSeconds : 0;
                if (bPosition.Y > MaxY)
                {
                    spriteSpeed.Y *= -1;
                    bPosition.Y = MaxY;
                }
                else if (bPosition.Y < MinY)
                {
                    isBumping = false;
                    bPosition.Y = MinY;
                }
            }
        }
        public override void Draw(SpriteBatch spriteBatch, Vector2 location, bool isLeft)
        {
            if (bState != BrickState.bHidden && bState != BrickState.destroyed && !isBumping)
            {
                base.Draw(spriteBatch, location, isLeft);
            }
            if (isBumping)
            {
                base.Draw(spriteBatch, bPosition, isLeft);
            }
            if (bState == BrickState.destroyed)
            {
                for (int index = 0; index < 4; index++)
                    spriteBatch.Draw(SpriteSheets, new Vector2(destroyedBrickPosX[index], destroyedBrickPosY[index]), new Rectangle(0, 0, frameSize.X / 2, frameSize.Y / 2), Color.White);
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
