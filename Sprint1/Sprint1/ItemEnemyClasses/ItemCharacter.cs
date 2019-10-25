
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Sprint1.MarioClasses;
using Sprint1.Sprites;
using System;
using Sprint1.LevelLoader;

namespace Sprint1.ItemClasses
{
    abstract class ItemCharacter : ICharacter
    {


        public abstract Sprint1Main.CharacterType Type { get; set; }

        private bool isBump;
        private float bumpHigh;
        private float bumpLow;
        protected Vector2 spriteSpeed;
        private Point positionOffset;
        protected readonly ItemSprite item;
        public MoveParameters Parameters { get; }
        private MoveParameters InitialParameter;
        public ItemCharacter(Texture2D texture, Point rowsAndColumns, Vector2 location)
        {
            item = new ItemSprite(texture, location, rowsAndColumns);
            Parameters = item.Parameters;
            InitialParameter = new MoveParameters(false);
            Scene.CopyDataOfParameter(Parameters, InitialParameter);
            isBump = false;
        }

        public void Update(float timeOfFrame)
        {
            item.Update(timeOfFrame);
            if (isBump)
            {
                //WARNING:Check next Sprint.
                Parameters.SetPosition(Parameters.Position.X, Parameters.Position.Y - (positionOffset.Y != 0 ? spriteSpeed.Y * timeOfFrame/10 : 0));
                //if item is at the height, set bump to false.
                if (Parameters.Position.Y < bumpHigh)
                {
                    Parameters.SetPosition(Parameters.Position.X, bumpHigh);
                    //Parameters.HasGravity = true;
                    spriteSpeed.Y *= -1;
                }
                if (Parameters.Position.Y > bumpLow)
                {
                    isBump = false;
                    Parameters.SetPosition(Parameters.Position.X, bumpLow);
                    Parameters.SetVelocity(5, 0);
                    spriteSpeed.Y = 0;
                    positionOffset.Y = 0;
                }
            }
            if (Parameters.Position.Y >= Stage.Boundary.X || Parameters.Position.Y >= Stage.Boundary.Y)
                Parameters.IsHidden = true;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            //Console.WriteLine(item.Parameters.IsHidden + "   Position = " + Parameters.Position);
            item.Draw(spriteBatch);
        }
        public Vector2 GetMaxPosition()
        {
            return new Vector2(Parameters.Position.X + item.GetHeightAndWidth().Y, Parameters.Position.Y);
        }
        public Vector2 GetMinPosition()
        {
            return new Vector2(Parameters.Position.X, Parameters.Position.Y - item.GetHeightAndWidth().X);
        }

        public void Bumping(Vector2 startP, float minY, float maxY, Vector2 blockSpeed)
        {
            //set the item's position to startP, and set the bump height and speed
            positionOffset = new Point(0, 1);
            spriteSpeed = new Vector2(0, blockSpeed.Y * 1.2f);
            Parameters.SetPosition(startP.X, startP.Y);
            Parameters.IsHidden = false;
            bumpHigh = minY;
            bumpLow = maxY;
            isBump = true;
        }

        public abstract void MarioCollide(bool specialCase);
        public abstract Vector2 GetHeightAndWidth();
        public virtual void BlockCollide(bool isBottom)
        {
            if (isBottom)
                Parameters.SetVelocity(Math.Abs(Parameters.Velocity.X), 0);
            else
            {
                Parameters.SetVelocity(0, 0);
                Parameters.HasGravity = false;
            }
        }
    }
}
