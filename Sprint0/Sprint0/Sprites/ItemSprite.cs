using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sprint0;


namespace Sprint0.Sprites
{
     class ItemSprite : AnimatedSprite
    {
        private Vector2 Location;
        private bool isBump;
        private float bumpHeight;
        private Point positionOffset;
        private Vector2 spriteSpeed;
        public ItemSprite(Texture2D texture, Vector2 location, Point sheetSize) : base(texture, sheetSize)
        {
            spriteSpeed = new Vector2(0, 0);
            Location=location;
            isBump = false;
        }
        public void bumping(Vector2 startP, float minY, Vector2 blockSpeed)
        {
            positionOffset = new Point(0, 1);
            spriteSpeed.Y = blockSpeed.Y * 2;
            Location = startP;
            bumpHeight = minY;
            isBump = true;
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (isBump&&bumpHeight > 0)
            {
                Location.Y -= positionOffset.Y != 0 ? spriteSpeed.Y * (float)gameTime.ElapsedGameTime.TotalSeconds : 0;
                if (Location.Y < bumpHeight)
                {
                    isBump = false;
                    Location.Y = bumpHeight;
                }
            }
        }
        public override void Draw(SpriteBatch spriteBatch, Vector2 location, bool isLeft)
        {
            base.Draw(spriteBatch, Location, isLeft);
        }


    }
}