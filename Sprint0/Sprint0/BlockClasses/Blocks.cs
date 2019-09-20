using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0.BlockClasses
{
    
    public class Blocks : ISprite
    {
        public Texture2D SpriteSheets { get; set; }
        public Vector2 Position
        {
            get
            {
                return bPosition;
            }
        }        
        //protected bool isHidden;
        protected Vector2 bPosition;

        protected int totalFrame;
        protected Point frameSize { get; set; }
        private Point sheetSize { get; set; }
        private int timeSinceLastFrame { get; set; }
        private int millisecondsPerFrame { get; set; }
        private Point frameOrigin { get; set; }

        private Point currentFrame { get; set; }
        private int animationFrame { get; set; }

        public Blocks(Texture2D sheet, Vector2 pos, Point rowAndCol, int totFrame)
        {
            ResizeFrame(sheet, rowAndCol, totFrame);
            this.bPosition = pos;
        }
        public virtual void Draw(SpriteBatch spriteBatch, Vector2 location, bool isLeft)
        {

            spriteBatch.Draw(SpriteSheets, location, new Rectangle(frameOrigin.X + currentFrame.X * frameSize.X, frameOrigin.Y + currentFrame.Y * frameSize.Y, frameSize.X, frameSize.Y), Color.White);
           
        }

        public Vector2 GetHeightAndWidth()
        {
            return new Vector2(frameSize.X, frameSize.Y);
        }

        protected void ResizeFrame(Texture2D sheet, Point rowAndCol, int totFrame)
        {
            SpriteSheets = sheet;
            sheetSize = rowAndCol;
            totalFrame = totFrame;
            frameSize = new Point(SpriteSheets.Width / sheetSize.X, SpriteSheets.Height / sheetSize.Y);
            this.timeSinceLastFrame = 0;
            this.millisecondsPerFrame = 500;
            this.currentFrame = new Point(0, 0);
            this.animationFrame = 0;
            this.frameOrigin = new Point(0, 0);
        }

        public virtual void Update(GameTime gameTime)
        {
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
