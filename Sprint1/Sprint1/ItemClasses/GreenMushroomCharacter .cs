using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint1.MarioClasses;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint1.ItemClasses
{
    class GreenMushroomCharacter : ItemCharacter
    {
        public GreenMushroomCharacter(Texture2D texture, Point rowsAndColunms, Vector2 location)
            : base(texture, rowsAndColunms, location)
        {

        }

        public override void MarioCollideBottom(MarioCharacter mario)
        {
            Parameters.IsHidden = true;
            mario.CollideWithMushRoom();

        }

        public override void MarioCollideLeft(MarioCharacter mario)
        {
            Parameters.IsHidden = true;
            mario.CollideWithMushRoom();

        }

        public override void MarioCollideRight(MarioCharacter mario)
        {
            Parameters.IsHidden = true;
            mario.CollideWithMushRoom();

        }

        public override void MarioCollideTop(MarioCharacter mario)
        {
            Parameters.IsHidden = true;
            mario.CollideWithMushRoom();
            

        }
    }
}
