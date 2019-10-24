﻿using Microsoft.Xna.Framework;
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
        ICharacter FactoryMethod(String name, Vector2 posS, Vector2 posE);
    }
}
