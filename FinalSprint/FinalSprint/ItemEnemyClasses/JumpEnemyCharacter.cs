using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FinalSprint.ItemClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalSprint.ItemEnemyClasses
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
                if(Math.Abs(Sprint5Main.Game.Scene.Mario.GetMinPosition.X - Parameters.Position.X) <= 50 && 
                    Sprint5Main.Game.Scene.Mario.Parameters.Velocity.Y != 0 )
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
