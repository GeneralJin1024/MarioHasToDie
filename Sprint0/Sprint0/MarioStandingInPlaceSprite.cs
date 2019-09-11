using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0
{
    class MarioStandingInPlaceSprite : ISprite
    {
        private static SpriteBatch spriteBatch;
        private static Texture2D frame;
        private Vector2 spritePosition;

        static public void Init(SpriteBatch batch, Texture2D f)
        {
            spriteBatch = batch;
            frame = f;
        }
        public MarioStandingInPlaceSprite(Vector2 position)
        {
            Visibility = false;
            this.spritePosition = new Vector2(0, 0);
            this.spritePosition.X = position.X;
            this.spritePosition.Y = position.Y;
        }
        public bool Visibility { get; set; }

        public void Draw()
        {
            spriteBatch.Draw(frame,this.spritePosition, Color.White);
        }

        public void Update(GraphicsDevice graphicsDevice, GameTime gameTime)
        {
            //nothing to do here
        }

        public void SwitchVisibility()
        {
            this.Visibility = !this.Visibility;
        }
    }
}
