using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint1.BlockClasses
{
    
    class Blocks : AnimatedSprite
    {
        public BlockType bType { get; protected set; }
        public Blocks(Texture2D sheet, Point rowAndCol, MoveParameters moveParameters, BlockType type) : base(sheet, rowAndCol, moveParameters)
        {
            bType = type;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);  
        }
        public override void Update(float timeOfFrame)
        {
            base.Update(timeOfFrame);
        }
    }
}
