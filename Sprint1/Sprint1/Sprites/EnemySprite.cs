﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Sprint1.Sprites
{
    namespace Sprint1.Sprites
    {
        class EnemySprite : AnimatedSprite
        {
            private Vector2 Location;
            public EnemySprite(Texture2D texture, Vector2 location, Point sheetSize) : base(texture, sheetSize, new MoveParameters())
            {
                //initialize the location
                Location = location;
            }
            public override void Draw(SpriteBatch spriteBatch)
            {
                base.Draw(spriteBatch);
            }
        }
    }
}