using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint1.Sprites;

namespace Sprint1.BlockClasses
{
    class BrickBlockSprite : Bricks
    {
        private float[] destroyedBrickPosX;
        private float[] destroyedBrickPosY;
        private Vector2 dPos;
        public BrickBlockSprite(Texture2D texture, Vector2 pos, ArrayList items) : base(texture, pos, new Point(4, 1), 1, BrickType.BNormal, items)
        {
            destroyedBrickPosX = new float[4];
            destroyedBrickPosY = new float[4];
        }
        public override void Update(GameTime gameTime)
        {
            if (bType == BrickType.Destroyed)
            {
                dPos.X += positionOffset.X != 0 ? spriteSpeed.X * (float)gameTime.ElapsedGameTime.TotalSeconds : 0;
                dPos.Y += positionOffset.Y != 0 ? spriteSpeed.Y * (float)gameTime.ElapsedGameTime.TotalSeconds : 0;

                destroyedBrickPosX[0] = Position.X - dPos.X; destroyedBrickPosX[1] = Position.X - 2 * dPos.X;
                destroyedBrickPosX[2] = Position.X + dPos.X; destroyedBrickPosX[3] = Position.X + 2 * dPos.X;

                for (int i = 0; i < 4; i++)
                    destroyedBrickPosY[i] = Position.Y + dPos.Y;
            }
            else
            {
                base.Update(gameTime);
            }
        }
        public override void Draw(SpriteBatch spriteBatch, Vector2 location, bool isLeft)
        {
            if (bType == BrickType.Destroyed)
            {
                for (int index = 0; index < 4; index++)
                    spriteBatch.Draw(SpriteSheets, new Vector2(destroyedBrickPosX[index], destroyedBrickPosY[index]), new Rectangle(0, 0, FrameSize.X / 2, FrameSize.Y / 2), Color.White);
            }
            else
            {
                base.Draw(spriteBatch, location, isLeft);
            }
            
        }
    }
}
