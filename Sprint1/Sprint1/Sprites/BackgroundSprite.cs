using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.ComponentModel;
using Sprint1;
using System.Collections;

namespace Sprint1.Background
{
    class BackgroundSprite : ISprite
    {
        private Vector2 location;
        public Texture2D SpriteSheets { get; set; }
        public MoveParameters Parameters { get; set; }

        //void Update(GameTime gameTime) { }
        public BackgroundSprite(Texture2D texture, Vector2 pos)
        {
            SpriteSheets = texture;
            location = pos;
        }
        public Vector2 GetHeightAndWidth()
        {
            return new Vector2((float)SpriteSheets.Height, (float)SpriteSheets.Width);
        }

        void ISprite.Update(float timeOfFrame)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(SpriteSheets, location, Color.White);  
        }
    }
}