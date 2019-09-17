﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.ComponentModel;
using Sprint0;

namespace sprint0.Enemy
{
    class GreenkoopaSprite : AnimatedPlayerSprite
    {
        public GreenkoopaSprite(Texture2D texture) : base(texture, new Point(1, 2))
        {
        }
        public override void Draw(SpriteBatch spriteBatch, Vector2 Location, bool isLeft)
        {
            base.Draw(spriteBatch, new Vector2(100,150), !isLeft);
        }
    }
}