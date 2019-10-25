using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint1
{
    public interface IFactory
    {
        List<ICharacter> FactoryMethod(String name, Vector2 posS, Vector2 posE);    //generating a series of objects at a time

        List<ICharacter> FactoryMethod(String name, Vector2 posS);  //generating one object at a time
    }
}
