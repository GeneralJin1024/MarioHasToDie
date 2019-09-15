using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint0.MarioClasses;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint0
{
    class Factory
    {
        public Mario getMario(Texture2D[] standardSheets, Texture2D[] superSheet,
            Texture2D[] fireSheet, Texture2D diedSheet, Vector2 location)
        {
            return new Mario(standardSheets, superSheet, fireSheet, diedSheet, location);
        }
    }
}
