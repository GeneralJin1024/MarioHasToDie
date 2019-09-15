using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0.BlockClasses
{
    public enum BlockState
    {
        bempty, bcoin, bstar, qcoin, qempty, qitem, qlife, destroyed
    } //public for genernate blocks
    class Bricks : ISprite
    {
        public Texture2D SpriteSheets { get; set; }
        private BlockState bState;
        private Vector2 bPosition;
        private ISprite blockSprite;

        public Bricks(Vector2 pos, BlockState s)
        {
            this.bPosition.X = pos.X;
            this.bPosition.Y = pos.Y;
            this.bState = s;
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 location, bool isLeft)
        {
            blockSprite.Draw(spriteBatch, location, isLeft);
        }

        public void Update(GameTime gameTime)
        {
            blockSprite.Update(gameTime);
        }

        public void Hit()
        {

        }

        public Vector2 GetHeightAndWidth()
        {
            throw new NotImplementedException();
        }
    }
}
