using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint1
{
    class AnimatedSprite : ISprite
    {
        public Texture2D SpriteSheets { get; set; }

        public MoveParameters Parameters { get; set; }

        private readonly float YAcceleration;
        private Point RowsAndColumns;
        private int ActionFrame;
        private int TimeSinceLastFrame;
        private readonly int MillisecondsPerFrame;
        public AnimatedSprite(Texture2D spriteSheet, Point rowAndColumn, MoveParameters parameters)
        {
            //set sprite sheets which can be changed from outside.
            SpriteSheets = spriteSheet;
            //let program know how many frames this sheet has
            RowsAndColumns = rowAndColumn;
            //start from the first frame
            ActionFrame = 0;
            TimeSinceLastFrame = 0;
            MillisecondsPerFrame = 100;
            Parameters = parameters;
            YAcceleration = 0;
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
                Parameters.UpdatePositionAndVelocity(YAcceleration);
                CheckBoundary();
            }
        }

        //public virtual void Update(float timeOfFrame)
        //{
        //    //TimeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
        //    Parameters.TimeOfFrame += timeOfFrame;
        //    if (Parameters.TimeOfFrame == MillisecondsPerFrame)
        //    {
        //        Parameters.TimeOfFrame = 0;
        //        ActionFrame += 1;
        //        if (ActionFrame >= RowsAndColumns.Y)     // Upper Limit Check
        //        {
        //            ActionFrame = 0;
        //        }
        //        //CheckBoundary();
        //    }
        //    Parameters.UpdatePositionAndVelocity(YAcceleration, timeOfFrame);
        //    Vector2 checkedPosition = Sprint1.CheckBoundary(new Vector2(Parameters.Position.X, Parameters.Position.Y - GetHeightAndWidth().X),
        //        GetHeightAndWidth());
        //    Parameters.SetPosition(checkedPosition.X, checkedPosition.Y + GetHeightAndWidth().X);

        //}

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (!Parameters.IsHidden)
            {
                //get frame's height and width
                float frameWidth = GetHeightAndWidth().Y;
                float frameHeight = GetHeightAndWidth().X;

                //get the frame that will be drawn in this update.
                Rectangle sourceRectangle = new Rectangle((int)(ActionFrame * frameWidth), 0,
                    (int)frameWidth, (int)frameHeight);
                //set the position the frame will be drawn
                Rectangle destinationRectangle = new Rectangle((int)Parameters.Position.X,
                    (int)(Parameters.Position.Y - frameHeight), (int)frameWidth, (int)frameHeight);

                if (Parameters.IsLeft)
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
        }

        public Vector2 GetHeightAndWidth()
        {
            return new Vector2((float)SpriteSheets.Height / RowsAndColumns.X, (float)SpriteSheets.Width / RowsAndColumns.Y);
        }

        private void CheckBoundary()
        {
            float x = Parameters.Position.X;
            float y = Parameters.Position.Y;
            float upBoundary = GetHeightAndWidth().X;
            float rightBoundary = 800 - GetHeightAndWidth().Y;
            x = (x <= 0) ? 0 : x;
            x = (x >= rightBoundary) ? rightBoundary : x;
            y = (y <= upBoundary) ? upBoundary : y;
            y = (y >= 500) ? 500 : y;
            Parameters.SetPosition(x, y);
        }
    }
}
