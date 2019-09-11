using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0
{
    class MarioRunningSprite : ISprite
    {
        private static SpriteBatch spriteBatch;
        private static Texture2D frame;
        private int moveDirectionX = -1;
        private static Point frameSize { get; set; }
        private static Point sheetSize { get; set; }

        private Vector2 spritePosition;
        private int timeSinceLastFrame { get; set; }
        private int millisecondsPerFrame { get; set; }
        private Point frameOrigin { get; set; }

        private Point currentFrame { get; set; }
        private int animationFrame { get; set; }
        public bool Visibility { get; set; }
        static public void Init(SpriteBatch batch, Texture2D f)
        {
            spriteBatch = batch;
            frame = f;
            sheetSize = new Point(4, 1);
            frameSize = new Point(frame.Width / sheetSize.X, frame.Height / sheetSize.Y);
        }
        public MarioRunningSprite(Vector2 position)
        {
            Visibility = false;
            this.spritePosition = new Vector2(0, 0);
            this.spritePosition.X = position.X;
            this.spritePosition.Y = position.Y;
            timeSinceLastFrame = 0;
            millisecondsPerFrame = 500;
            currentFrame = new Point(0, 0);
            animationFrame = 0;
            frameOrigin = new Point(0, 0);
        }
        

        public void Draw()
        {
            spriteBatch.Draw(frame, this.spritePosition,
                new Rectangle(frameOrigin.X + currentFrame.X * frameSize.X, frameOrigin.Y + currentFrame.Y * frameSize.Y, frameSize.X, frameSize.Y), Color.White);
        }

        public void Update(GraphicsDevice graphicsDevice, GameTime gameTime)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                timeSinceLastFrame -= millisecondsPerFrame;

                animationFrame += 1;
                if (animationFrame >= sheetSize.X * sheetSize.Y)
                {
                    animationFrame = 0;
                }

                currentFrame = new Point(animationFrame % sheetSize.X, animationFrame / sheetSize.X);
                this.spritePosition.X += this.moveDirectionX * 10;
                if (this.spritePosition.X <= graphicsDevice.Viewport.Width / 4 + 10 || this.spritePosition.X >= graphicsDevice.Viewport.Width - 10)
                    this.moveDirectionX *= -1;
            }      
        }

        public void SwitchVisibility()
        {
            this.Visibility = !this.Visibility;
        }
    }
}
