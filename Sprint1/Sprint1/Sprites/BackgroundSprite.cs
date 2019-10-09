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
        public Vector2 Position { get => BPosition; }
        protected ArrayList Location;
        public Texture2D SpriteSheets { get; set; }
        public MoveParameters Parameters { get; set; }

        private Vector2 BPosition; // use to eleminate warning

        //void Update(GameTime gameTime) { }
        public BackgroundSprite(Texture2D texture)
        {
            SpriteSheets = texture;
            BPosition = new Vector2(0, 0); // use to eleminate warning
            Location = new ArrayList();
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
            foreach (Vector2 vector in Location)
            {
                spriteBatch.Draw(SpriteSheets, vector, Color.White);
            }
        }
    }
}