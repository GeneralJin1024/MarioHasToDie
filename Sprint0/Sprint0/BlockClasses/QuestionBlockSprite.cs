using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0.BlockClasses
{
    class QuestionBlockSprite : Bricks
    {
        public QuestionBlockSprite(Sprint0 game, Vector2 pos, List<ISprite> items) : base(game.Content.Load<Texture2D>("BlockSprites/mario-question-blocks"), pos, new Point(4, 3), 4, BrickState.bitem, items)
        {

        }

    }
}
