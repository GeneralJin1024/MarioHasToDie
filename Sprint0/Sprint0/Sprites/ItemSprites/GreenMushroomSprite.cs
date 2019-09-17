using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.ComponentModel;
using Sprint0;

namespace sprint0.Item
{
    class GreenMushroomSprite : AnimatedSprite
    {
        private Vector2 location;
        public GreenMushroomSprite(Texture2D texture) : base(texture, new Point(1, 1))
        {
            location = new Vector2(300, 100);
        }
        public override void Draw(SpriteBatch spriteBatch, Vector2 Location, bool isLeft)
        {
            base.Draw(spriteBatch, location, isLeft);
        }
    }
}