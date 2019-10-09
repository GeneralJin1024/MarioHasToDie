using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint1.BlockClasses
{
    class FloorBlockSprite : Blocks
    {
        public FloorBlockSprite(Texture2D texture, MoveParameters moveParameters) : base(texture, moveParameters, new Point(4, 1), BlockType.Used, new ArrayList())
        {
            // nothing to do
        }
    }

    class StairBlockSprite : Blocks
    {
        public StairBlockSprite(Texture2D texture, MoveParameters moveParameters) : base(texture, moveParameters, new Point(4, 1), BlockType.Used, new ArrayList())
        {
            // nothing to do
        }
    }
}
