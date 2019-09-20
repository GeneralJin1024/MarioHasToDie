﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Sprites;

namespace Sprint0.BlockClasses
{
    class QuestionBlockSprite : Bricks
    {
        public QuestionBlockSprite(Sprint0 game, Vector2 pos, ArrayList items) : base(game.Content.Load<Texture2D>("BlockSprites/mario-question-blocks"), pos, new Point(4, 3), 4, BrickType.Normal, items)
        {

        }

    }
}
