﻿
using Sprint1.MarioClasses;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint1.ItemClasses
{
    class FlowerCharacter : ItemCharacter
    {

        public override Sprint1Main.CharacterType Type { get; set; } = Sprint1Main.CharacterType.Flower;
        public FlowerCharacter(Texture2D texture, Point rowsAndColunms, Vector2 location)
            :base (texture, rowsAndColunms, location)
        {

        }

        public override Vector2 GetHeightAndWidth()
        {
            return item.GetHeightAndWidth();
        }

        public override void MarioCollide(bool specialCase)
        {


            Parameters.IsHidden = true;

           
        }

      

    }
}