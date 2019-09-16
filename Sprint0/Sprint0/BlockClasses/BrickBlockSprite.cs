using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0.BlockClasses
{
    class BrickBlockSprite : Bricks
    {
        private bool isHit;
        private int ratio;
        public BrickBlockSprite(Vector2 pos) : base(Sprint0.BlockTextures[0], pos, new Point(4, 1), 1, BrickState.bcoin, false)
        {
            this.isHit = false;
            this.ratio = 1;
        }

        public void Bumping()
        {

        }

    }
}
