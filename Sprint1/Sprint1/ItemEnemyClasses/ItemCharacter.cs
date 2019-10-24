
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
        private float bumpHeight;
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
                Parameters.SetPosition(0, Parameters.Position.Y - (positionOffset.Y != 0 ? Parameters.Velocity.Y * timeOfFrame : 0));
                //if item is at the height, set bump to false.
                if (Parameters.Position.Y < bumpHeight)
                {
                    isBump = false;
                    Parameters.SetPosition(Parameters.Position.X, bumpHeight);
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

        public void Bumping(Vector2 startP, float minY, Vector2 blockSpeed)
        {
            //set the item's position to startP, and set the bump height and speed
            positionOffset = new Point(0, 1);
            Parameters.SetVelocity(0, blockSpeed.Y * 2);
            Parameters.SetPosition(startP.X, startP.Y);
            bumpHeight = minY;
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
