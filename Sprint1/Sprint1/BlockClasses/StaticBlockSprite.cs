using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint1.BlockClasses
{
    class FloorBlockSprite : AnimatedSprite
    {
        public FloorBlockSprite(Texture2D texture, Vector2 pos, MoveParameters moveParameters) : base(texture, new Point(4, 1), moveParameters)
        {
            // nothing to do
        }
    }

    class StairBlockSprite : AnimatedSprite
    {
        public StairBlockSprite(Texture2D texture, Vector2 pos, MoveParameters moveParameters) : base(texture, new Point(4, 1), moveParameters)
        {
            // nothing to do
        }
    }
}
