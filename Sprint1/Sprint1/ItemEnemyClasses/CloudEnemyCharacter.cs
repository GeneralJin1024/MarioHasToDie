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
    class CloudEnemyCharacter : EnemyCharacter
    {
        private int attackTime;
        private readonly int maxAttackTime=60;
        public CloudEnemyCharacter(Texture2D[] texture, Point[] rowsAndColumns, MoveParameters moveParameters) : base(texture, rowsAndColumns, moveParameters)
        {
            Parameters.SetVelocity(0, 0);
            Parameters.HasGravity = false;
            //Type = Sprint1Main.CharacterType.DiedEnemy;
            Type = Sprint1Main.CharacterType.Enemy;
            attackTime = 0;
            maxAttackTime = 60;
        }

        public override void Update(float timeOfFrame)
        {
            //if (Type== Sprint1Main.CharacterType.DiedEnemy&& Math.Abs(Sprint1Main.Game.Scene.Mario.GetMinPosition.X - Parameters.Position.X) <= 70 && Math.Abs(Sprint1Main.Game.Scene.Mario.GetMinPosition.Y - Parameters.Position.Y) <= 70)
            //{
            //    Type= Sprint1Main.CharacterType.Enemy;

            //    //diedEnemy sprite is the attack mode of clouds
            //    currentSprite = diedEnemy;
            //}else if(Type == Sprint1Main.CharacterType.Enemy)
            //{
            //    attackTime++;
            //    if (attackTime >= maxAttackTime)
            //    {
            //        Type = Sprint1Main.CharacterType.DiedEnemy;
            //        attackTime = 0;
            //    }
            //}
            //Sprint1Main.Game.Scene.Mario
            
        }

        public override void MarioCollide(bool specialCase)
        {
            currentSprite = diedEnemy;
        }

    }
}
