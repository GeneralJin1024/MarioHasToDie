using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0
{
    interface ISprite
    {
        Texture2D SpriteSheets { get; set; }

        void Update(GameTime gameTime);

        void Draw(SpriteBatch spriteBatch, Vector2 location, bool isLeft);

    }
}
