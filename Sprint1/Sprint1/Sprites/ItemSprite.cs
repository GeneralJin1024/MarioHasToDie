using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sprint1;


namespace Sprint1.Sprites
{
     class ItemSprite : AnimatedSprite
    {
        private Vector2 Location;
        private bool isBump;
        private float bumpHeight;
        private Point positionOffset;
        private Vector2 spriteSpeed;
        public ItemSprite(Texture2D texture, Vector2 location, Point sheetSize) : base(texture, sheetSize, new MoveParameters())
        {
            //initialize
            spriteSpeed = new Vector2(0, 0);
            Location=location;
            isBump = false;
        }
        public void Bumping(Vector2 startP, float minY, Vector2 blockSpeed)
        {
            //set the item's position to startP, and set the bump height and speed
            positionOffset = new Point(0, 1);
            spriteSpeed.Y = blockSpeed.Y * 2;
            Location = startP;
            bumpHeight = minY;
            isBump = true;
        }
        public override void Update(float timeOfFrame)
        {
            base.Update(timeOfFrame);
            //if item need bump, increase the location
            if (isBump)
            {
                //WARNING:Check next Sprint.
                Location.Y -= positionOffset.Y != 0 ? spriteSpeed.Y * timeOfFrame : 0;
                //if item is at the height, set bump to false.
                if (Location.Y < bumpHeight)
                {
                    isBump = false;
                    Location.Y = bumpHeight;
                }
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }


    }
}