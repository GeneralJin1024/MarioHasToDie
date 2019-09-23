using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint0
{
    class AnimatedSprite : ISprite
    {
        public Texture2D SpriteSheets { get; set; }

        public Vector2 Position
        {
            get
            {
                return Location;
            }
        }

        private Point RowsAndColumns;
        private int ActionFrame;
        private int TimeSinceLastFrame;
        private readonly int MillisecondsPerFrame;
        private Vector2 Location;
        public AnimatedSprite(Texture2D spriteSheet, Point rowAndColumn)
        {
            //set sprite sheets which can be changed from outside.
            SpriteSheets = spriteSheet;
            //let program know how many frames this sheet has
            RowsAndColumns = rowAndColumn;
            //start from the first frame
            ActionFrame = 0;
            TimeSinceLastFrame = 0;
            MillisecondsPerFrame = 200;
        }

        public virtual void Update(GameTime gameTime)
        {
            TimeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            //change a fram per Milliseconds (ms)
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

        public virtual void Draw(SpriteBatch spriteBatch, Vector2 location, bool isLeft)
        {
            //get frame's height and width
            float frameWidth = (float)SpriteSheets.Width / RowsAndColumns.Y;
            float frameHeight = (float)SpriteSheets.Height / RowsAndColumns.X;

            //get the frame that will be drawn in this update.
            Rectangle sourceRectangle = new Rectangle((int)(ActionFrame * frameWidth), 0,
                (int)frameWidth, (int)frameHeight);
            //set the position the frame will be drawn
            Rectangle destinationRectangle = new Rectangle((int)Location.X,
                (int)Location.Y, (int)frameWidth, (int)frameHeight);

            Location = location;
            if (isLeft)
            {
                //flip the frame if the target point to left.
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
