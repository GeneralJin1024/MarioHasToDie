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
        bHidden, bempty, bcoin, bstar, qcoin, qitem, qlife, used, destroyed
    }

    //public for genernate blocks
    class Bricks : Blocks
    {
        public BrickState bState;
        protected ISprite blockSprite;
     
        private bool isBumping;
        private int MinY, MaxY;
        public Bricks(Texture2D sheet, Vector2 pos, Point rowAndColumn, int totalFrame, BrickState brickState, bool isHidden) : base(sheet, pos, rowAndColumn,totalFrame, isHidden)
        {
            bState = brickState;
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
        public void ChangeToBrick()
        {
            bState = BrickState.bempty;
            SpriteSheets = Sprint0.BlockTextures[0];
        }
        public void ChangeToUsed()
        {
            bState = BrickState.used;
            SpriteSheets = Sprint0.BlockTextures[3];
        }
        public void ChangeToDestroyed()
        {
            bState = BrickState.destroyed; 
        }
        public void Bumping()
        {
            isBumping = true;
            MinY = (int)bPosition.Y;
            MaxY = (int)bPosition.Y + frameSize.Y;
        }
        public override void Update(GameTime gameTime)
        {
            if (bState == BrickState.destroyed)
            {

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
        public void Hit(bool isBig, ISprite sprite)
        {
            throw new NotImplementedException();
        }
    }
}
