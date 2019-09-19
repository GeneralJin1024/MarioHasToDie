using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint0.MarioClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sprint0.FactoryClasses
{
    public class MarioFactory : AbstractFactory
    {
        public override void GetBlock()
        {
        }

        public override void GetEnemy()
        {
        }

        public override void GetItem()
        {
        }

        public Mario getMario(Texture2D[] standardSheets, Texture2D[] superSheet,
            Texture2D[] fireSheet, Vector2 location)
        {
            return new Mario(standardSheets, superSheet, fireSheet, location);
        }
    }
}
