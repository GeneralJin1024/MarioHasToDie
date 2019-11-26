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
    class PlantEnemyCharacter : EnemyCharacter
    {
        private readonly float maxHeight;
        private readonly float minHeight;
        private float ClockToWait;
        private bool Appear;
        private bool ReadyToTrigger;

        public PlantEnemyCharacter(Texture2D[] texture, Point[] rowsAndColumns, MoveParameters moveParameters) : base(texture, rowsAndColumns, moveParameters)
        {
            maxHeight = moveParameters.Position.Y - 30F;
            minHeight = moveParameters.Position.Y;
            Parameters.SetVelocity(0, 0);
            Type = Sprint1Main.CharacterType.Enemy;
            Parameters.HasGravity = false;
            ClockToWait = 0;
            Appear = false; ReadyToTrigger = true;
        }

        public override void Update(float timeOfFrame)
        {
            if (Parameters.Velocity.Y == 0)
            {
                ClockToWait += timeOfFrame;
                if(Math.Abs(Sprint1Main.Game.Scene.Mario.GetMinPosition.X - Parameters.Position.X) <= 100 
                    && ClockToWait >= 20 && !Appear && ReadyToTrigger)
                {
                    ClockToWait = 0; ReadyToTrigger = false;
                    Parameters.SetVelocity(0, -2f); Appear = true;
                }else if(Appear && ClockToWait >= 20)
                {
                    ClockToWait = 0; Parameters.SetVelocity(0, 2);
                }
            }
            else if (Parameters.Velocity.Y > 0)
            {
                if (Parameters.Position.Y >= minHeight)
                {
                    Parameters.SetVelocity(0, 0); Appear = false;
                }
            }
            else if (Parameters.Velocity.Y < 0)
            {
                if (Sprint1Main.Game.Scene.Mario.IsDied())
                    Parameters.SetVelocity(0, 2);
                if (Parameters.Position.Y <= maxHeight)
                {
                    Parameters.SetVelocity(0, 0);
                }
            }
            ReadyToTrigger = Math.Abs(Sprint1Main.Game.Scene.Mario.GetMinPosition.X - Parameters.Position.X) > 100 || ReadyToTrigger;
            currentSprite.Update(timeOfFrame);
        }
        public override void MarioCollide(bool specialCase)
        {
            if (specialCase)
            {
                Type = Sprint1Main.CharacterType.DiedEnemy;
                Parameters.IsHidden = true;
            }
        }
    }
}
