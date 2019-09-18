using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sprint0;


namespace Sprint0.Sprites
{
     class ItemSprite : AnimatedSprite
    {
        private Vector2 Location;
        private bool isBump;
        private float bumpHeight;
        public ItemSprite(Texture2D texture, Vector2 location, Point sheetSize) : base(texture, sheetSize)
        {
            Location=location;
            isBump = false;
        }
        public void bumping(Vector2 startP, float height)
        {
            Location = startP;
            bumpHeight = height;
            isBump = true;
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (isBump&&bumpHeight > 0)
            {
                Location.Y--;
                bumpHeight--;
                if (bumpHeight <= 0)
                {
                    isBump = false;
                }
            }
        }
        public override void Draw(SpriteBatch spriteBatch, Vector2 location, bool isLeft)
        {
            base.Draw(spriteBatch, Location, isLeft);
        }


    }
}