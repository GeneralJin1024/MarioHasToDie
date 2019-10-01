using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint1.MarioClasses;

namespace Sprint1.BlockClasses
{
    class BlockCharacter : ICharacter
    {
        private Blocks block;
        public MoveParameters Parameters { get; }

        public void Draw(SpriteBatch spriteBatch)
        {
            block.Draw(spriteBatch);
        }

        public Vector2 GetMaxPosition()
        {
            return new Vector2(block.Position.X + block.GetHeightAndWidth().Y, block.Position.Y + block.GetHeightAndWidth().X);
        }

        public Vector2 GetMinPosition()
        {
            return new Vector2(block.Position.X, block.Position.Y + block.GetHeightAndWidth().X);
        }

        public void MarioCollideBottom(MarioCharacter mario)
        {
            throw new NotImplementedException();
        }

        public void MarioCollideLeft(MarioCharacter mario)
        {
            mario.CollideWithBlocks();
        }

        public void MarioCollideRight(MarioCharacter mario)
        {
            mario.CollideWithBlocks();
        }

        public void MarioCollideTop(MarioCharacter mario)
        {
            mario.CollideWithBlocks();
        }

        public void Update(GameTime gameTime)
        {
            block.Update(gameTime);
        }
    }

}
