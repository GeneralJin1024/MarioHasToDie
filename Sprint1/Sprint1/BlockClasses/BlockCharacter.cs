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
        public Sprint1Main.CharacterType Type { get; set; }

        public BlockCharacter(Blocks block)
        {
            this.block = block;
            Type = Sprint1Main.CharacterType.Block;
            Parameters = block.Parameters;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            block.Draw(spriteBatch);
        }

        public Vector2 GetMaxPosition()
        {
            return new Vector2(block.Parameters.Position.X + block.GetHeightAndWidth().Y, block.Parameters.Position.Y + block.GetHeightAndWidth().X);
        }

        public Vector2 GetMinPosition()
        {
            return new Vector2(block.Parameters.Position.X, block.Parameters.Position.Y + block.GetHeightAndWidth().X);
        }

        public void Update(float timeOfFrame)
        {
            block.Update(timeOfFrame);
        }

        public void MarioCollide(bool specialCase)
        {
            if (specialCase)
            {
                block.currentbState.Handle(block);
            }
        }

        public Vector2 GetHeightAndWidth()
        {
            return block.GetHeightAndWidth();
        }
    }

}
