using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.ComponentModel;

public class CoinSprite: AnimatedPlayerSprite
{
    public CoinSprite(Texture2D texture) : base(texture, new Point(1, 4))
    {
    }

}
