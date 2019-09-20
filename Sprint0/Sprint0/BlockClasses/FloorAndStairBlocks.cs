using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.BlockClasses
{
    class FloorBlockSprite : Blocks
    {
        public FloorBlockSprite(Texture2D texture, Vector2 pos) : base(texture, pos, new Point(4, 1), 1)
        {
            // nothing to do
        }
    }

    class StairBlockSprite : Blocks
    {
        public StairBlockSprite(Texture2D texture, Vector2 pos) : base(texture, pos, new Point(4, 1), 1)
        {
            // nothing to do
        }
    }
}
