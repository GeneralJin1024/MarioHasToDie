﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint1.MarioClasses;
using Sprint1.ItemClasses;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint1.ItemClasses
{
    class PipeCharacter: ItemCharacter
    {
        public PipeCharacter(Texture2D texture, Point rowsAndColunms, Vector2 location)
            : base(texture, rowsAndColunms, location)
        {

        }

        public override void MarioCollideBottom(MarioCharacter mario)
        {
          

        }

        public override void MarioCollideLeft(MarioCharacter mario)
        {
           

        }

        public override void MarioCollideRight(MarioCharacter mario)
        {
           

        }

        public override void MarioCollideTop(MarioCharacter mario)
        {
           

        }
    }
}