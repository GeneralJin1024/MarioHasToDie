using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalSprint.ItemClasses
{
    class BombCharacter : ItemCharacter
    {

        
        public override Vector2 GetHeightAndWidth { get { return Item.GetHeightAndWidth; } }
        public override Sprint5Main.CharacterType Type { get; set; } = Sprint5Main.CharacterType.Bomb;

        private bool Bombed = false;
        public BombCharacter(Texture2D texture, Point rowsAndColunms, Vector2 location)
            : base(texture, rowsAndColunms, location)
        {
            Parameters.IsHidden = true;
            Parameters.SetPosition(Parameters.Position.X, 150);
        }

        public override void Update(float timeOfFrame)
        {
            if(!Bombed && Parameters.IsHidden && (Parameters.Position.X - Sprint5Main.Game.Scene.Mario.GetMaxPosition.X <= 10) && 
                (Parameters.Position.Y < Sprint5Main.Game.Scene.Mario.GetMinPosition.Y))
            {
                Parameters.IsHidden = false;
                Parameters.SetVelocity(0, -2);
                Parameters.HasGravity = true;
              
            }
            base.Update(timeOfFrame);
            
        }

       

        public override void MarioCollide(bool specialCase)
        {

            Bombed = true;
            Parameters.IsHidden = true;

        }

        public override void BlockCollide(bool isBottom)
        {
            Bombed = true;
            Parameters.IsHidden = true;
        }


    }
}
