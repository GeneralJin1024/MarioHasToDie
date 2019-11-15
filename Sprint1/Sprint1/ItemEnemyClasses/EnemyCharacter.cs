
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Sprint1.MarioClasses;
using Sprint1.Sprites;
using System;

namespace Sprint1.ItemClasses
{
   class EnemyCharacter : ICharacter
    {

        public Sprint1Main.CharacterType Type { get; set; } = Sprint1Main.CharacterType.Enemy;
        public Vector2 GetHeightAndWidth { get { return currentSprite.GetHeightAndWidth; } }
        public Vector2 GetMaxPosition { get { return new Vector2(Parameters.Position.X + currentSprite.GetHeightAndWidth.Y, Parameters.Position.Y); } }
        public Vector2 GetMinPosition { get { return new Vector2(Parameters.Position.X, Parameters.Position.Y - currentSprite.GetHeightAndWidth.X); } }
        readonly protected ISprite liveEnemy;
        readonly protected ISprite diedEnemy;
        private int disappear;
        private int disappearTimer;
        protected ISprite currentSprite;
        public MoveParameters Parameters { get; }
        public EnemyCharacter(Texture2D[] texture, Point[] rowsAndColumns, MoveParameters moveParameters)
        {
            Parameters = moveParameters;
            Parameters.IsLeft = Parameters.Position.X >= Sprint1Main.Game.Scene.Mario.GetMinPosition.X;
            Parameters.SetVelocity(2, 0);
            liveEnemy = new AnimatedSprite(texture[0], rowsAndColumns[0], Parameters);
            diedEnemy = new AnimatedSprite(texture[1], rowsAndColumns[1], Parameters);
            currentSprite = liveEnemy;
            disappear = 100;
        }


        public virtual void Update(float timeOfFrame) {
            if(Type== Sprint1Main.CharacterType.Enemy) {
                currentSprite.Update(timeOfFrame);
                if (Parameters.Position.Y >= 500) { Parameters.IsHidden = true; }
            }
            else
            {
                disappearTimer++;
                if (disappearTimer == disappear)
                {
                    Parameters.IsHidden = true;
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            currentSprite.Draw(spriteBatch);

        }

        //public Vector2 GetMaxPosition()
        //{
        //    return new Vector2(Parameters.Position.X + currentSprite.GetHeightAndWidth.Y, Parameters.Position.Y);
        //}
        //public Vector2 GetMinPosition()
        //{
        //    return new Vector2(Parameters.Position.X, Parameters.Position.Y - currentSprite.GetHeightAndWidth.X);
        //}
        

        public virtual void MarioCollide(bool specialCase)
        {
            if (specialCase)
            {
                if (Type == Sprint1Main.CharacterType.Enemy)
                    Sprint1Main.Point += 100;
                Parameters.SetVelocity(0, 0); //stop moving.
                Type = Sprint1Main.CharacterType.DiedEnemy;
                currentSprite = diedEnemy;
            }
        }
        public virtual void BlockCollide(bool isBottom)
        {
            if (isBottom)
            {
                Parameters.SetVelocity(Math.Abs(Parameters.Velocity.X), 0);
            }
            else
            {
                Parameters.IsLeft = !Parameters.IsLeft; //转向
                Parameters.SetVelocity(Math.Abs(Parameters.Velocity.X), Parameters.Velocity.Y); //速度重设
            }
        }
        //public Vector2 GetHeightAndWidth()
        //{
        //    return currentSprite.GetHeightAndWidth;
        //}

    }
}
