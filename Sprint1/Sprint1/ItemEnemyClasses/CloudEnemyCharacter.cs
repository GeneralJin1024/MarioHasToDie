﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint1.ItemClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint1.ItemEnemyClasses
{
    class CloudEnemyCharacter : EnemyCharacter
    {
        public CloudEnemyCharacter(Texture2D[] texture, Point[] rowsAndColumns, MoveParameters moveParameters) : base(texture, rowsAndColumns, moveParameters)
        {
            Parameters.SetVelocity(0, 0);
            if (Parameters.Position.Y > 200)
                Parameters.SetPosition(Parameters.Position.X, 100);
            Parameters.HasGravity = false;
            Type = Sprint1Main.CharacterType.Enemy;
        }

        public override void Update(float timeOfFrame)
        {           
        }

        public override void MarioCollide(bool specialCase)
        {
            currentSprite = diedEnemy;
        }

    }
}
