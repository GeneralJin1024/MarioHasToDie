using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint1.LevelLoader
{
    class LiteralOnlyScene
    {
        private SpriteFont Font;
        private string[] Content;
        private float Clock;
        public LiteralOnlyScene(string[] content)
        {
            Content = content;
        }
        public void LoadContent(SpriteFont font)
        {
            Font = font; Clock = 0;
        }
        public void Update(GameTime gameTime)
        {
            Clock += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (Clock >= 10)
                Sprint1Main.Game.Exit();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Sprint1Main.Game.GraphicsDevice.Clear(Color.Black);
            for (int i = 0; i < Content.Length; i++)
            {
                spriteBatch.DrawString(Font, Content[i], new Vector2(400 - Content[i].Length * 10, 250 - Content.Length * 5 * i), Color.White, 0,
                    Vector2.Zero, 2, SpriteEffects.None, 0);
            }
            //spriteBatch.End();
        }
    }
}
