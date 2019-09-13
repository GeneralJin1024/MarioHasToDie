using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0
{
    class QuestionBlockSprite : ISprite
    {
        private Vector2 spritePos;
        private SpriteBatch spriteBatch;
        private static Texture2D spriteFrame;
        private static Point frameSize { get; set; }
        private static Point sheetSize { get; set; }
        private int timeSinceLastFrame { get; set; }
        private int millisecondsPerFrame { get; set; }
        private Point frameOrigin { get; set; }

        private Point currentFrame { get; set; }
        private int animationFrame { get; set; }

        public QuestionBlockSprite(Vector2 myPosition, SpriteBatch myBatch, Texture2D f)
        {
            this.spritePos = new Vector2(0, 0);
            this.spritePos.X = myPosition.X;
            this.spritePos.Y = myPosition.Y;

            this.spriteBatch = myBatch;
            spriteFrame = f;

            sheetSize = new Point(4, 3);
            frameSize = new Point(spriteFrame.Width / sheetSize.X, spriteFrame.Height / sheetSize.Y);
            this.timeSinceLastFrame = 0;
            this.millisecondsPerFrame = 500;
            this.currentFrame = new Point(0, 0);
            this.animationFrame = 0;
            this.frameOrigin = new Point(0, 0);

        }
        public bool Visibility { get; set; }

        public void Draw()
        {
            this.spriteBatch.Draw(spriteFrame, this.spritePos, new Rectangle(frameOrigin.X + currentFrame.X * frameSize.X, frameOrigin.Y + currentFrame.Y * frameSize.Y, frameSize.X, frameSize.Y), Color.White);
        }

        public void SwitchVisibility()
        {
            //nothing to do
        }

        public void Update(GraphicsDevice graphicsDevice, GameTime gameTime)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                timeSinceLastFrame -= millisecondsPerFrame;

                animationFrame += 1;
                if (animationFrame >= sheetSize.X)
                {
                    animationFrame = 0;
                }

                currentFrame = new Point(animationFrame % sheetSize.X, animationFrame / sheetSize.X);
            }
        }
    }
}
