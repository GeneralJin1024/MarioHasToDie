using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Sprint0.Sprites
{
    namespace Sprint0.Sprites
    {
        class EnemySprite : AnimatedSprite
        {
            private Vector2 Location;
            public EnemySprite(Texture2D texture, Vector2 location, Point sheetSize) : base(texture, sheetSize)
            {
                Location = location;
            }
            public override void Draw(SpriteBatch spriteBatch, Vector2 location, bool isLeft)
            {
                base.Draw(spriteBatch, Location, isLeft);
            }
        }
    }
}