using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.ComponentModel;
using Sprint0;
using System.Collections;

namespace sprint0.Background
{
    class BackgroundSprite : ISprite
    {
        public Vector2 Position { get; set; }
        protected ArrayList Location;
        public Texture2D SpriteSheets { get; set; }

        void Update(GameTime gameTime) { }
        public BackgroundSprite(Texture2D texture)
        {
            SpriteSheets = texture;
            Location = new ArrayList();
        }
        public Vector2 GetHeightAndWidth()
        {
            return new Vector2((float)SpriteSheets.Height, (float)SpriteSheets.Width);
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 location, bool isLeft)
        {
            foreach (Vector2 vector in Location)
            {
                spriteBatch.Draw(SpriteSheets, SpriteSheets.Bounds, Color.White);
            }
        }

        void ISprite.Update(GameTime gameTime)
        {
        }

    }
}