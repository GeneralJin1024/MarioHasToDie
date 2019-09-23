using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0.BlockClasses
{
    
    class Blocks : ISprite
    {
        public Texture2D SpriteSheets { get; set; }
        //protected bool isHidden;
        private Vector2 bPosition;

        protected int totalFrame { get; set; }
        protected Point FrameSize { get; set; }
        private Point sheetSize { get; set; }
        private int timeSinceLastFrame { get; set; }
        private int millisecondsPerFrame { get; set; }
        private Point frameOrigin { get; set; }

        private Point currentFrame { get; set; }
        private int animationFrame { get; set; }
        public Vector2 Position { get => bPosition; }

        public Blocks(Texture2D sheet, Vector2 pos, Point rowAndCol, int totFrame)
        {
            ResizeFrame(sheet, rowAndCol, totFrame);
            this.bPosition = pos;
        }
        public virtual void Draw(SpriteBatch spriteBatch, Vector2 location, bool isLeft)
        {
            if (spriteBatch == null)
            {
                throw new ArgumentNullException(nameof(spriteBatch));
            }
            spriteBatch.Draw(SpriteSheets, location, new Rectangle(frameOrigin.X + currentFrame.X * FrameSize.X, frameOrigin.Y + currentFrame.Y * FrameSize.Y, FrameSize.X, FrameSize.Y), Color.White);
           
        }

        public Vector2 GetHeightAndWidth()
        {
            return new Vector2(FrameSize.X, FrameSize.Y);
        }

        protected void ResizeFrame(Texture2D sheet, Point rowAndCol, int totFrame)
        {
            if (sheet == null)
            {
                throw new ArgumentNullException(nameof(sheet));
            }
            SpriteSheets = sheet;
            sheetSize = rowAndCol;
            totalFrame = totFrame;
            FrameSize = new Point(SpriteSheets.Width / sheetSize.X, SpriteSheets.Height / sheetSize.Y);
            this.timeSinceLastFrame = 0;
            this.millisecondsPerFrame = 500;
            this.currentFrame = new Point(0, 0);
            this.animationFrame = 0;
            this.frameOrigin = new Point(0, 0);
        }

        public virtual void Update(GameTime gameTime)
        {
            if (gameTime == null)
            {
                throw new ArgumentNullException(nameof(gameTime));
            }
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                timeSinceLastFrame -= millisecondsPerFrame;

                animationFrame += 1;
                if (animationFrame >= totalFrame)
                {
                    animationFrame = 0;
                }

                currentFrame = new Point(animationFrame % sheetSize.X, animationFrame / sheetSize.X);
            }
        }
    }
}
