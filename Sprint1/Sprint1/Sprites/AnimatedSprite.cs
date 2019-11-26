using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint1
{
    public class AnimatedSprite : ISprite
    {
        public Texture2D SpriteSheets { get; set; }

        /*
         * If an object has multiple action state such as Mario, each of its sprites should always have some same parameters.
         * For example, velocity, position, facing direction and whether it exist.
         * Store all this values in Parameters to keep all Sprites has the same value without using for loop to update each one.
         */
        public MoveParameters Parameters { get; set; }
        public Vector2 GetHeightAndWidth {
            get { return new Vector2((float)SpriteSheets.Height / RowsAndColumns.X, (float)SpriteSheets.Width / RowsAndColumns.Y); }
        }
        private Point RowsAndColumns;
        private int ActionFrame;
        private readonly int MillisecondsPerFrame;
        public Boolean FrameFreeze { get; set; }
        public AnimatedSprite(Texture2D spriteSheet, Point rowAndColumn, MoveParameters parameters)
        {
            ResizeFrame(spriteSheet, rowAndColumn, 0);
            MillisecondsPerFrame = 2;
            Parameters = parameters;
            FrameFreeze = false; //default value
        }

        public virtual void Update(float timeOfFrame)
        {
            /*
             * One frame is a standard time for frame update. If we want the collision update velocity be different from 
             * frame update velocity, we can directly change the value of MillisecondsPerFrame
             */
            if (!FrameFreeze)
                Parameters.TimeOfFrame += timeOfFrame;
            if (Parameters.TimeOfFrame >= MillisecondsPerFrame)
            {
                Parameters.TimeOfFrame = 0;
                ActionFrame += 1;
                if (ActionFrame >= RowsAndColumns.Y)     // Upper Limit Check
                {
                    ActionFrame = 0;
                }
            }
            if (Parameters.InScreen) // objects will not update when it is out of screen
            {
                Parameters.UpdatePositionAndVelocity(timeOfFrame);
                //check boundary after each update to make sure the whole object are in the screen.
                Vector2 checkedPosition = LevelLoader.Stage.CheckBoundary(new Vector2(Parameters.Position.X, Parameters.Position.Y - GetHeightAndWidth.X),
                    GetHeightAndWidth);
                //use the checkedPosition as real position.
                int x = (int)checkedPosition.X;
                if (x > checkedPosition.X) { x--; }
                int y = (int)checkedPosition.Y;
                if (y > checkedPosition.Y) { y -= 1; }
                Parameters.SetPosition(x, y + GetHeightAndWidth.X);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (spriteBatch is null)
                throw new ArgumentNullException(nameof(spriteBatch));
            if (!Parameters.IsHidden && !Parameters.Flash)
            {
                //get frame's height and width
                float frameWidth = GetHeightAndWidth.Y;
                float frameHeight = GetHeightAndWidth.X;

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
                        Parameters.GetColor, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
                }
                else
                {
                    spriteBatch.Draw(SpriteSheets, destinationRectangle, sourceRectangle,
                        Parameters.GetColor);
                }
            }
        }

        public void ResizeFrame(Texture2D spriteSheet, Point rowAndColumn, int actionFrame)
        {
            //set sprite sheets which can be changed from outside.
            SpriteSheets = spriteSheet;
            //let program know how many frames this sheet has
            RowsAndColumns = rowAndColumn;
            //start from the first frame
            ActionFrame = actionFrame;
        }
    }
}
