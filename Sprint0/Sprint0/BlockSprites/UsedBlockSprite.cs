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
        private SpriteBatch spriteBatch;
        private static Texture2D spriteFrame;
        private Vector2 spritePosition;
        private static Point frameSize { get; set; }
        private static Point sheetSize { get; set; }
        public UsedBlockSprite(Vector2 myPosition, SpriteBatch myBatch, Texture2D f)
        {
            spriteBatch = myBatch;
            spriteFrame = f;
            this.spritePosition = new Vector2(0, 0);
            this.spritePosition.X = myPosition.X;
            this.spritePosition.Y = myPosition.Y;
            sheetSize = new Point(4, 1);
            frameSize = new Point(spriteFrame.Width / sheetSize.X, spriteFrame.Height / sheetSize.Y);
        }
        public bool Visibility { get; set; }

        public void Draw()
        {
            this.spriteBatch.Draw(spriteFrame, this.spritePosition, new Rectangle(0, 0, frameSize.X, frameSize.Y), Color.White);
        }

        public void SwitchVisibility()
        {
            //do nothing here
        }

        public void Update(GraphicsDevice graphicsDevice, GameTime gameTime)
        {
            //do nothing here
        }
    }
}
