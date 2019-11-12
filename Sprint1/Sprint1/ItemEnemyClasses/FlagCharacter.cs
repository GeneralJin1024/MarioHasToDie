﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint1.ItemClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint1.ItemClasses
{
    class FlagCharacter : ItemCharacter
    {
        public override Vector2 GetHeightAndWidth { get { return Item.GetHeightAndWidth; } }
        public override Sprint1Main.CharacterType Type { get; set; } = Sprint1Main.CharacterType.Flag;

        public FlagCharacter(Texture2D texture, Point rowsAndColumns, Vector2 location): base(texture, rowsAndColumns,location)
        {

        }

    }
}
