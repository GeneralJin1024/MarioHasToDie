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
        public ItemSprite(Texture2D texture, Vector2 location, Point sheetSize) : base(texture, sheetSize, new MoveParameters(false))
        {
            //initialize
            Parameters.SetPosition(location.X, location.Y);
            Parameters.SetVelocity(0, 0);
        }        
        public override void Update(float timeOfFrame)
        {
            base.Update(timeOfFrame);
            //if item need bump, increase the location
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }


    }
}