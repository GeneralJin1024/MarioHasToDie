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
    class JumpEnemyCharacter : EnemyCharacter
    {

        public JumpEnemyCharacter(Texture2D[] texture, Point[] rowsAndColumns, MoveParameters moveParameters) : base(texture, rowsAndColumns, moveParameters)
        {
        }

        public override void Update(float timeOfFrame)
        {
            if (Parameters.Velocity.Y == 0)
            {
                if(Math.Abs(Sprint1Main.Game.Scene.Mario.GetMinPosition.X - Parameters.Position.X) <= 50 && 
                    Sprint1Main.Game.Scene.Mario.Parameters.Velocity.Y != 0 )
                {
                    Parameters.SetVelocity(0,-16f);
                    //jump mode sprite
                    currentSprite = diedEnemy;
                }
            }
            else
            {
                currentSprite = liveEnemy;
            }
            currentSprite.Update(timeOfFrame);
        }
        public override void MarioCollide(bool specialCase)
        {
        }
    }
}
