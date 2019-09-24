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
    class QuestionBlockSprite : Bricks
    {
        public QuestionBlockSprite(Texture2D texture, Vector2 pos, ArrayList items) : base(texture, pos, new Point(4, 3), 4, BrickType.QNormal, items)
        {

        }

    }
}
