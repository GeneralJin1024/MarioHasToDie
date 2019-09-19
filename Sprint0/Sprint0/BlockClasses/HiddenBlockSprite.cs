using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Sprites;

namespace Sprint0.BlockClasses
{

    class HiddenBlockSprite : Bricks
    {
        public HiddenBlockSprite(Sprint0 game, Vector2 pos, List<ItemSprite> items) : base(game.Content.Load<Texture2D>("BlockSprites/mario-brick-blocks"), pos, new Point(4, 1), 1, BrickType.Hidden, items)
        {
        }
    }
}
