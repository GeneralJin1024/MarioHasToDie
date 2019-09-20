﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Sprites;

namespace Sprint0.BlockClasses
{

    class HiddenBlockSprite : Bricks
    {
        public HiddenBlockSprite(Texture2D texture, Vector2 pos, ArrayList items) : base(texture, pos, new Point(4, 1), 1, BrickType.Hidden, items)
        {
        }
    }
}
