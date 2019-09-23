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

        protected int TotalFrame { get; set; }
        protected Point FrameSize { get; set; }
        private Point SheetSize { get; set; }
        private int TimeSinceLastFrame { get; set; }
        private int MillisecondsPerFrame { get; set; }
        private Point FrameOrigin { get; set; }

        private Point CurrentFrame { get; set; }
        private int AnimationFrame { get; set; }
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
            spriteBatch.Draw(SpriteSheets, location, new Rectangle(FrameOrigin.X + CurrentFrame.X * FrameSize.X, FrameOrigin.Y + CurrentFrame.Y * FrameSize.Y, FrameSize.X, FrameSize.Y), Color.White);
           
        }

        public Vector2 GetHeightAndWidth()
        {
            return new Vector2(FrameSize.X, FrameSize.Y);
        }

        protected void ResizeFrame(Texture2D sheet, Point rowAndCol, int totFrame)
        {
            SpriteSheets = sheet ?? throw new ArgumentNullException(nameof(sheet));
            SheetSize = rowAndCol;
            TotalFrame = totFrame;
            FrameSize = new Point(SpriteSheets.Width / SheetSize.X, SpriteSheets.Height / SheetSize.Y);
            this.TimeSinceLastFrame = 0;
            this.MillisecondsPerFrame = 500;
            this.CurrentFrame = new Point(0, 0);
            this.AnimationFrame = 0;
            this.FrameOrigin = new Point(0, 0);
        }

        public virtual void Update(GameTime gameTime)
        {
            if (gameTime == null)
            {
                throw new ArgumentNullException(nameof(gameTime));
            }
            TimeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (TimeSinceLastFrame > MillisecondsPerFrame)
            {
                TimeSinceLastFrame -= MillisecondsPerFrame;

                AnimationFrame += 1;
                if (AnimationFrame >= TotalFrame)
                {
                    AnimationFrame = 0;
                }

                CurrentFrame = new Point(AnimationFrame % SheetSize.X, AnimationFrame / SheetSize.X);
            }
        }
    }
}
