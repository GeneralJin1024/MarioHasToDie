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
            moveParameters.SetVelocity(2.0f,0);
            currentSprite.FrameFreeze = true;
        }

        public override void Update(float timeOfFrame)
        {
            if (Sprint1Main.Game.Mario.Parameters.IsLeft)
            {
                currentSprite.ResizeFrame(currentSprite.SpriteSheets, new Point(1, 2), 0);
                currentSprite.Parameters.SetVelocity(0, 0);
            }
            else
            {
                currentSprite.ResizeFrame(currentSprite.SpriteSheets, new Point(1, 2), 1);
                currentSprite.Parameters.SetVelocity(4.0f, 0);
            }
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
        }
    }
}
