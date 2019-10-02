using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint1.MarioClasses;

namespace Sprint1.CollideDetection
{
    public class CollidePair
    {
        private ICharacter Character;
        private MarioCharacter Mario;
        private float xTime = 0, yTime = 0;
        public float Time { get; private set; }

        public CollidePair(MarioCharacter mario, ICharacter character)
        {
            Mario = mario;
            Character = character;
        }

        public void GetFirstContactTime()
        {
            Vector2 marioMin = Mario.GetMinPosition();
            Vector2 marioMax = Mario.GetMaxPosition();
            Vector2 characterMin = Character.GetMinPosition();
            Vector2 characterMax = Character.GetMaxPosition();

            #region OverLap Axel
            if (Mario.Parameters.Velocity.X > 0)
            {
                xTime = (characterMin.X - marioMax.X) / Mario.Parameters.Velocity.X;
            }
            else if (Mario.Parameters.Velocity.X < 0)
            {
                xTime = (marioMin.X - characterMax.X) / -Mario.Parameters.Velocity.X;
            }
            if (Mario.Parameters.Velocity.Y > 0)
            {
                yTime = (characterMin.Y - marioMax.Y) / Mario.Parameters.Velocity.Y;
            }
            else if (Mario.Parameters.Velocity.Y < 0)
            {
                yTime = (marioMin.Y - characterMax.Y) / -Mario.Parameters.Velocity.Y;
            }
            #endregion
            //Console.WriteLine("character = (" + characterMin.X + " , " + characterMax.Y + ")");
            //Console.WriteLine("Mario Position1:  (" + marioMin.X + " , " + marioMin.Y + ")" + "   rightX = " + marioMax.X);

            #region Intersect Time
            if (xTime <= 0 && yTime <= 0)
                Time = 0;
            else if (yTime > 0 && (xTime <= 0 || (xTime > 0 && yTime >= xTime)))
            {
                marioMax.X += yTime * Mario.Parameters.Velocity.X;
                marioMin.X += yTime * Mario.Parameters.Velocity.X;
                if ((characterMin.X <= marioMax.X && marioMax.X <= characterMax.X) || (characterMax.X >= marioMin.X && marioMin.X >= characterMin.X))
                    Time = yTime;
                else
                    Time = 0;
            }//(xTime <= 0 && yTime > 0)
            else if (xTime > 0 && (yTime <= 0 || (yTime > 0 && xTime > yTime)))
            {
                marioMax.Y += xTime * Mario.Parameters.Velocity.Y;
                marioMin.Y += xTime * Mario.Parameters.Velocity.Y;
                if ((characterMin.Y <= marioMax.Y && marioMax.Y <= characterMax.Y) || (characterMax.Y >= marioMin.Y && marioMin.Y >= characterMin.Y))
                    Time = xTime;
                else
                    Time = 0;
            }//(xTime > 0 && yTime <= 0)
            else //xTime > 0 and yTime > 0
            {
                if (xTime > yTime)
                {

                }
            }
            #endregion
        }

        public void Collide()
        {
            if (Time == yTime)
            {
                if (Mario.Parameters.Velocity.Y > 0)
                    Character.MarioCollideTop(Mario);
                else
                    Character.MarioCollideBottom(Mario);
            }
            else
            {
                if (Mario.Parameters.Velocity.X > 0)
                    Character.MarioCollideLeft(Mario);
                else
                    Character.MarioCollideRight(Mario);
            }
        }
    }
}
