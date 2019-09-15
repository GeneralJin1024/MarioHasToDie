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
        private Vector2 myPos;
        private static Texture2D mySheet;
        private static Point frameSize { get; set; }
        private static Point sheetSize { get; set; }
        private int timeSinceLastFrame { get; set; }
        private int millisecondsPerFrame { get; set; }
        private Point frameOrigin { get; set; }

        private Point currentFrame { get; set; }
        private int animationFrame { get; set; }
        public Texture2D SpriteSheets { get; set; }

        public QuestionBlockSprite()
        {
            mySheet = SpriteSheets;
            sheetSize = new Point(4, 3);
            frameSize = new Point(mySheet.Width / sheetSize.X, mySheet.Height / sheetSize.Y);
            this.timeSinceLastFrame = 0;
            this.millisecondsPerFrame = 500;
            this.currentFrame = new Point(0, 0);
            this.animationFrame = 0;
            this.frameOrigin = new Point(0, 0);
        }

        public void Update(GameTime gameTime)
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

        public void Draw(SpriteBatch spriteBatch, Vector2 location, bool isLeft)
        {
            this.myPos.X = location.X;
            this.myPos.Y = location.Y;
            spriteBatch.Draw(mySheet, this.myPos, new Rectangle(frameOrigin.X + currentFrame.X * frameSize.X, frameOrigin.Y + currentFrame.Y * frameSize.Y, frameSize.X, frameSize.Y), Color.White);
        }

        public Vector2 GetHeightAndWidth()
        {
            throw new NotImplementedException();
        }
    }
}
