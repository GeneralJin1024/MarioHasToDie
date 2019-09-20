using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Sprites;

namespace Sprint0.BlockClasses
{
    class BrickBlockSprite : Bricks
    {
        private float[] destroyedBrickPosX;
        private float[] destroyedBrickPosY;
        private Vector2 dPos;
        public BrickBlockSprite(Sprint0 game, Vector2 pos, ArrayList items) : base(game.Content.Load<Texture2D>("BlockSprites/mario-brick-blocks"), pos, new Point(4, 1), 1, BrickType.Normal, items)
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

                destroyedBrickPosX[0] = bPosition.X - dPos.X; destroyedBrickPosX[1] = bPosition.X - 2 * dPos.X;
                destroyedBrickPosX[2] = bPosition.X + dPos.X; destroyedBrickPosX[3] = bPosition.X + 2 * dPos.X;

                for (int i = 0; i < 4; i++)
                    destroyedBrickPosY[i] = bPosition.Y + dPos.Y;
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
                    spriteBatch.Draw(SpriteSheets, new Vector2(destroyedBrickPosX[index], destroyedBrickPosY[index]), new Rectangle(0, 0, frameSize.X / 2, frameSize.Y / 2), Color.White);
            }
            else
            {
                base.Draw(spriteBatch, location, isLeft);
            }
            
        }
    }
}
