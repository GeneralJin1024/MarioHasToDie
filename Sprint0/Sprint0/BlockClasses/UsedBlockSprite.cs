using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0.BlockSprites
{
    class UsedBlockSprite : ISprite
    {
        private Vector2 myPosition;
        private static Texture2D mySheet;
        private static Point frameSize { get; set; }
        private static Point sheetSize { get; set; }
        public Texture2D SpriteSheets { get; set; }

        public UsedBlockSprite()
        {
            mySheet = SpriteSheets;
            sheetSize = new Point(4, 1);
            frameSize = new Point(mySheet.Width / sheetSize.X, mySheet.Height / sheetSize.Y);
        }

        public void Update(GameTime gameTime)
        {
            //nothing to do
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location, bool isLeft)
        {
            this.myPosition.X = location.X;
            this.myPosition.Y = location.Y;
            spriteBatch.Draw(mySheet, this.myPosition, new Rectangle(0, 0, frameSize.X, frameSize.Y), Color.White);
        }

        public Vector2 GetHeightAndWidth()
        {
            throw new NotImplementedException();
        }
    }
}
