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
    class PlantEnemyCharacter:EnemyCharacter
    {
        private float maxHeight;
        private float minHeight;

        public PlantEnemyCharacter(Texture2D[] texture, Point[] rowsAndColumns, MoveParameters moveParameters) : base(texture, rowsAndColumns, moveParameters)
        {
            maxHeight = moveParameters.Position.Y-30F;
            minHeight = moveParameters.Position.Y;
            Type = Sprint1Main.CharacterType.PlantEnemy;


        }

        public override void Update(float timeOfFrame)
        {
                
            
            if (Parameters.Velocity.Y >0)
            {
                if (Parameters.Position.Y >= minHeight)
                {
                    Parameters.SetVelocity(0, 0);
                }
            }
            else if(Parameters.Velocity.Y < 0)
            {
                if (Parameters.Position.Y <= maxHeight)
                {
                    Parameters.SetVelocity(0, 2f);
                }
            }
                
                currentSprite.Update(timeOfFrame);
        }
        public void Trigger()
        {
            Parameters.SetVelocity(0, -2f);
        }
    }
}
