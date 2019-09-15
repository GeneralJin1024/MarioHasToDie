using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0.BlockClasses
{

    class HiddenBlockSprite : ISprite
    {
        private Vector2 myPos;
        private static Texture2D mySheet;
        private static Point frameSize { get; set; }
        private static Point sheetSize { get; set; }
        public Texture2D SpriteSheets { get; set; }

        public void Draw(SpriteBatch spriteBatch, Vector2 location, bool isLeft)
        {
            this.myPos.X = location.X;
            this.myPos.Y = location.Y;
            spriteBatch.Draw(mySheet, this.myPos, new Rectangle(0, 0, frameSize.X, frameSize.Y), Color.White);
        }

        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public Vector2 GetHeightAndWidth()
        {
            throw new NotImplementedException();
        }
    }
}
