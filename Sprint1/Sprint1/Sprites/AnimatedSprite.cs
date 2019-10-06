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
        private readonly int MillisecondsPerFrame;
        public AnimatedSprite(Texture2D spriteSheet, Point rowAndColumn, MoveParameters parameters)
        {
            //set sprite sheets which can be changed from outside.
            SpriteSheets = spriteSheet;
            //let program know how many frames this sheet has
            RowsAndColumns = rowAndColumn;
            //start from the first frame
            ActionFrame = 0;
            MillisecondsPerFrame = 1;
            Parameters = parameters;
            YAcceleration = 0;
        }

        public virtual void Update(float timeOfFrame)
        {
            Parameters.TimeOfFrame += timeOfFrame;
            if (Parameters.TimeOfFrame == MillisecondsPerFrame)
            {
                Parameters.TimeOfFrame = 0;
                ActionFrame += 1;
                if (ActionFrame >= RowsAndColumns.Y)     // Upper Limit Check
                {
                    ActionFrame = 0;
                }
            }
            Parameters.UpdatePositionAndVelocity(YAcceleration, timeOfFrame);
            Vector2 checkedPosition = Sprint1Main.CheckBoundary(new Vector2(Parameters.Position.X, Parameters.Position.Y - GetHeightAndWidth().X),
                GetHeightAndWidth());
            Parameters.SetPosition(checkedPosition.X, checkedPosition.Y + GetHeightAndWidth().X);
        }

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
    }
}
