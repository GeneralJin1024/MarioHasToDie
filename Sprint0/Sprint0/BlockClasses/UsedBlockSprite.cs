using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0.BlockClasses
{
    class UsedBlockSprite : Bricks
    {
        public UsedBlockSprite(Sprint0 game, Vector2 pos) : base(game.Content.Load<Texture2D>("BlockSprites/mario-hit-block"), pos, new Point(4, 1), 1, BrickState.used, new List<ISprite>())
        {
        }

    }
}
