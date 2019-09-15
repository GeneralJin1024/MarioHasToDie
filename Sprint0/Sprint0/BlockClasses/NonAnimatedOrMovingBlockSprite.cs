using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0
{
    class NonAnimatedOrMovingBlockSprite : ISprite  // For floor and stair blocks
    {
        private Vector2 myPos;
        private Point currentFrame;
        private static Point frameSize { get; set; }
        private static Point sheetSize { get; set; }
        public Texture2D SpriteSheets { get; set; }

        public NonAnimatedOrMovingBlockSprite()
        {
            sheetSize = new Point(4, 1);
            frameSize = new Point(SpriteSheets.Width / sheetSize.X, SpriteSheets.Height / sheetSize.Y);
            currentFrame = new Point(0, 0); 
        }
     
        public void Update(GameTime gameTime)
        {
            //nothing to do
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location, bool isLeft)
        {
            this.myPos.X = location.X;
            this.myPos.Y = location.Y;
            spriteBatch.Draw(SpriteSheets, this.myPos, new Rectangle(currentFrame.X, currentFrame.Y, frameSize.X, frameSize.Y), Color.White);
        }

        public Vector2 GetHeightAndWidth()
        {
            throw new System.NotImplementedException();
        }
    }
}
