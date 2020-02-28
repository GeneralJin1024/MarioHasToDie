using Microsoft.Xna.Framework;
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
            private Point positionOffset;
            private Vector2 spriteSpeed;
            public EnemySprite(Texture2D texture, Vector2 location, Point sheetSize) : base(texture, sheetSize, new MoveParameters())
            {
                //initialize the location
                spriteSpeed = new Vector2(0, 0);
                Parameters.SetPosition(location.X, location.Y);
                Parameters.SetVelocity(0, 0);
            }
            public override void Draw(SpriteBatch spriteBatch)
            {
                base.Draw(spriteBatch);
            }
        }
    }
}