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
        private readonly ICharacter Character;
        private readonly MarioCharacter Mario;
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
            //Get mario's left up coner position and right down coner position.
            Vector2 marioMin = Mario.GetMinPosition();
            Vector2 marioMax = Mario.GetMaxPosition();
            //Get object's left up coner position and right down coner position.
            Vector2 characterMin = Character.GetMinPosition();
            Vector2 characterMax = Character.GetMaxPosition();
            //If both mario and the object are moving, we need to use relative velocity instead of mario's velocity.
            relativeVelocity = Mario.Parameters.Velocity;
            relativeVelocity.X -= Character.Parameters.Velocity.X;
            relativeVelocity.Y -= Character.Parameters.Velocity.Y;

            #region OverLap Axel
            if (relativeVelocity.X > 0)
            {
                /*
                 * If velocity is right, then mario can collide with obejct only when its right side is
                 * on the left of objects left side. In other words, their difference must be positive.
                 */
                xTime = (characterMin.X - marioMax.X) / relativeVelocity.X;
            }
            else if (Mario.Parameters.Velocity.X < 0)
            {
                /*
                 * If moving left, then mario can collide with object only when its left side is on the right of object's right side.
                 */
                xTime = (marioMin.X - characterMax.X) / -relativeVelocity.X;
            }
            if (Mario.Parameters.Velocity.Y > 0)
            {
                //If moving down, then mario can collide with object only when its bottom is upon object's top.
                yTime = (characterMin.Y - marioMax.Y) / relativeVelocity.Y;
            }
            else if (Mario.Parameters.Velocity.Y < 0)
            {
                //If moving down, then mario can collide with object only when its top is upon object's bottom.
                yTime = (marioMin.Y - characterMax.Y) / -relativeVelocity.Y;
            }
            #endregion
            //Here, I leave three lines of code used to check values in the future.
            //Console.WriteLine("characterMin = " + characterMin + "     characterMax = " + characterMax);
            //Console.WriteLine("Mario PositionMin:  " + marioMin + "     marioMax = " + marioMax);
            //Console.WriteLine("xTime = " + xTime + "    yTime  = " + yTime);

            #region Intersect Time
            if (xTime < 0 && yTime < 0)
                Time = -2; // if both x, y time are negative, then mario is leaving this object
            else if (yTime >= 0 && yTime >= xTime) //Mario must collide with object from top or bottom.
            {
                marioMax.X += yTime * relativeVelocity.X;
                marioMin.X += yTime * relativeVelocity.X;
                // check whether the mario still has a part that is between the left and right side of object after yTime.
                if ((characterMin.X <= marioMin.X && marioMin.X <= characterMax.X) || (characterMin.X >= marioMin.X && marioMax.X >= characterMin.X))
                    Time = yTime;
                else
                    Time = -2; // no, the mario didn't collide
            }
            else if (xTime >= 0 && xTime > yTime) //Mario must collide with object from left or right.
            {
                marioMax.Y += xTime * relativeVelocity.Y;
                marioMin.Y += xTime * relativeVelocity.Y;
                // check whether the mario still has a part that is between the top and bottom of object after yTime.
                if ((marioMin.Y >= characterMin.Y && marioMin.Y <= characterMax.Y) || (characterMin.Y >= marioMin.Y && marioMax.Y >= characterMin.Y))
                    Time = xTime;
                else
                    Time = -2;// no, the mario didn't collide
            }
            #endregion
        }

        public void Collide()
        {
            // tell mario what he collide with by check object's type and tell the object mario collide with it.
            switch (Character.Type)
            {
                case Sprint1Main.CharacterType.Block:
                    Character.MarioCollide((Time == yTime) && (relativeVelocity.Y < 0));
                    Mario.CollideWithBlock();
                    break;
                case Sprint1Main.CharacterType.Enemy:
                    Character.MarioCollide((Time == yTime) && (relativeVelocity.Y > 0));
                    Mario.CollideWithEnemy((Time == yTime) && (relativeVelocity.Y > 0));
                    break;
                case Sprint1Main.CharacterType.DiedEnemy:
                    Mario.CollideWithBlock();
                    break;
                case Sprint1Main.CharacterType.Flower:
                    Character.MarioCollide(true);
                    Mario.CollideWithFlower();
                    break;
                case Sprint1Main.CharacterType.Mushroom:
                    Character.MarioCollide(true);
                    Mario.CollideWithMushRoom();
                    break;
                case Sprint1Main.CharacterType.Coin:
                    Character.MarioCollide(true);
                    break;
                case Sprint1Main.CharacterType.Star:
                    Character.MarioCollide(true);
                    break;
                case Sprint1Main.CharacterType.Pipe:
                    Mario.CollideWithBlock();
                    break;
                default: break;
            }
        }
    }
}
