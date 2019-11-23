using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint1.ItemClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint1.ItemEnemyClasses
{
    class BossEnemyCharacter : EnemyCharacter
    {


        public BossEnemyCharacter(Texture2D[] texture, Point[] rowsAndColumns, MoveParameters moveParameters) : base(texture, rowsAndColumns, moveParameters)
        {
            moveParameters.SetVelocity(3.5f,0);
        }

        public override void Update(float timeOfFrame)
        {
            currentSprite.Update(timeOfFrame);

        }
        public override void MarioCollide(bool specialCase)
        {

        }
        public override void BlockCollide(bool isBottom)
        {
            if (isBottom)
            {
                Parameters.SetVelocity(Math.Abs(Parameters.Velocity.X), 0);
            }
            else
            {

            }
        }
    }
}
