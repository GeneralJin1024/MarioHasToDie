using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint1.Sprites;

namespace Sprint1.BlockClasses
{

    class HiddenBlockSprite : Bricks
    {
        public HiddenBlockSprite(Texture2D texture, MoveParameters moveParameters, ArrayList items) : base(texture, moveParameters, new Point(4, 1), BlockType.Hidden, items)
        {
        }
    }
}
