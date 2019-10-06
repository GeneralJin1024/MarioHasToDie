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
        private float xTime = -2, yTime = -2;
        private Vector2 relativeVelocity;
        public float Time { get; private set; }

        public CollidePair(MarioCharacter mario, ICharacter character)
        {
            Mario = mario;
            Character = character;
            relativeVelocity = new Vector2();
        }

        public void GetFirstContactTime()
        {
            Vector2 marioMin = Mario.GetMinPosition();
            Vector2 marioMax = Mario.GetMaxPosition();
            Vector2 characterMin = Character.GetMinPosition();
            Vector2 characterMax = Character.GetMaxPosition();
            relativeVelocity = Mario.Parameters.Velocity;
            relativeVelocity.X -= Character.Parameters.Velocity.X;
            relativeVelocity.Y -= Character.Parameters.Velocity.Y;

            #region OverLap Axel
            if (relativeVelocity.X > 0)
            {
                xTime = (characterMin.X - marioMax.X) / relativeVelocity.X;
            }
            else if (Mario.Parameters.Velocity.X < 0)
            {
                xTime = (marioMin.X - characterMax.X) / -relativeVelocity.X;
            }
            if (Mario.Parameters.Velocity.Y > 0)
            {
                yTime = (characterMin.Y - marioMax.Y) / relativeVelocity.Y;
            }
            else if (Mario.Parameters.Velocity.Y < 0)
            {
                yTime = (marioMin.Y - characterMax.Y) / -relativeVelocity.Y;
            }
            #endregion
            //Console.WriteLine("characterMin = " + characterMin + "     characterMax = " + characterMax);
            //Console.WriteLine("Mario PositionMin:  " + marioMin + "     marioMax = " + marioMax);
            //Console.WriteLine("xTime = " + xTime + "    yTime  = " + yTime);

            #region Intersect Time
            if (xTime < 0 && yTime < 0) //(xTime <= 0 && yTime <= 0)
                Time = 2;
            else if (yTime >= 0 && yTime >= xTime) //(yTime >= 0 && (xTime < 0 || (xTime >= 0 && yTime >= xTime)))
            {
                marioMax.X += yTime * relativeVelocity.X;
                marioMin.X += yTime * relativeVelocity.X;
                //((characterMin.X <= marioMax.X && marioMax.X <= characterMax.X) ||(characterMax.X >= marioMin.X && marioMin.X >= characterMin.X))
                if ((characterMin.X <= marioMin.X && marioMin.X <= characterMax.X) || (characterMin.X >= marioMin.X && marioMax.X >= characterMin.X))
                    Time = yTime;
                else
                    Time = -2;
            }//(xTime <= 0 && yTime > 0)//(yTime > 0 && (xTime < 0 || (xTime > 0 && yTime >= xTime)))
            else if (xTime >= 0 && xTime > yTime) //(xTime >= 0 && (yTime < 0 || (yTime >= 0 && xTime > yTime)))
            {
                marioMax.Y += xTime * relativeVelocity.Y;
                marioMin.Y += xTime * relativeVelocity.Y;
                //((characterMin.Y <= marioMax.Y && marioMax.Y <= characterMax.Y) || (characterMax.Y >= marioMin.Y && marioMin.Y >= characterMin.Y))
                if ((marioMin.Y >= characterMin.Y && marioMin.Y <= characterMax.Y) || (characterMin.Y >= marioMin.Y && marioMax.Y >= characterMin.Y))
                    Time = xTime;
                else
                    Time = -2;
            }//(xTime > 0 && yTime <= 0)//(xTime > 0 && (yTime <= 0 || (yTime > 0 && xTime > yTime)))
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
                if (relativeVelocity.Y > 0)
                    Character.MarioCollideTop(Mario);
                else
                    Character.MarioCollideBottom(Mario);
            }
            else
            {
                if (relativeVelocity.X > 0)
                    Character.MarioCollideLeft(Mario);
                else
                    Character.MarioCollideRight(Mario);
            }
        }
    }
}
