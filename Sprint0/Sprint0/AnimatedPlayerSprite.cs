using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint0
{
    class AnimatedPlayerSprite : ISprite
    {
        public Texture2D SpriteSheets { get; set; }

        public Vector2 Position
        {
            get
            {
                return this.location;
            }
        }

        private Point RowsAndColumns;
        private int ActionFrame;
        private int TimeSinceLastFrame;
        private int MillisecondsPerFrame;
        private Vector2 location;
        public AnimatedPlayerSprite(Texture2D spriteSheet, Point rowAndColumn)
        {
            SpriteSheets = spriteSheet;
            RowsAndColumns = rowAndColumn;
            ActionFrame = 0;
            TimeSinceLastFrame = 0;
            MillisecondsPerFrame = 200;
        }

        public void Update(GameTime gameTime)
        {
            TimeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (TimeSinceLastFrame > MillisecondsPerFrame)
            {
                TimeSinceLastFrame -= MillisecondsPerFrame;

                ActionFrame += 1;
                if (ActionFrame >= RowsAndColumns.Y)     // Upper Limit Check
                {
                    ActionFrame = 0;
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, Vector2 Location, bool isLeft)
        {
            float frameWidth = (float)SpriteSheets.Width / RowsAndColumns.Y;
            float frameHeight = (float)SpriteSheets.Height / RowsAndColumns.X;

            Rectangle sourceRectangle = new Rectangle((int)(ActionFrame * frameWidth), 0,
                (int)frameWidth, (int)frameHeight);
            Rectangle destinationRectangle = new Rectangle((int)Location.X,
                (int)Location.Y, (int)frameWidth, (int)frameHeight);

            this.location = new Vector2(destinationRectangle.X, destinationRectangle.Y);
            if (isLeft)
            {
                spriteBatch.Draw(SpriteSheets, destinationRectangle, sourceRectangle,
                    Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
            }
            else
            {
                spriteBatch.Draw(SpriteSheets, destinationRectangle, sourceRectangle,
                    Color.White);
            }
        }

        public Vector2 GetHeightAndWidth()
        {
            return new Vector2((float)SpriteSheets.Height / RowsAndColumns.X, (float)SpriteSheets.Width / RowsAndColumns.Y);
        }
    }
}
