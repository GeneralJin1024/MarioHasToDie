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
        public FloorBlockSprite(Sprint0 game, Vector2 pos) : base(game.Content.Load<Texture2D>("BlockSprites/mario-gravel-blocks"), pos, new Point(4, 1), 1)
        {
            // nothing to do
        }
    }

    class StairBlockSprite : Blocks
    {
        public StairBlockSprite(Sprint0 game, Vector2 pos) : base(game.Content.Load<Texture2D>("BlockSprites/mario-shiny-block"), pos, new Point(4, 1), 1)
        {
            // nothing to do
        }
    }
}
