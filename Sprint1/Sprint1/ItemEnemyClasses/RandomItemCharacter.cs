using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint1.ItemClasses
{
    class RandomItemCharacter : ItemCharacter
    {
        public override Vector2 GetHeightAndWidth { get { return Item.GetHeightAndWidth; } }
        public override Sprint1Main.CharacterType Type
        {
            get { return TypeList[CurrentType]; }
            set { }
        }
        private readonly Sprint1Main.CharacterType[] TypeList;
        private int CurrentType;
        private float Time;
        public RandomItemCharacter(Texture2D texture, Point rowsAndColunms, Vector2 location)
            : base(texture, rowsAndColunms, location)
        {
            CurrentType = 0;
            Time = 0;
            TypeList = new Sprint1Main.CharacterType[] { Sprint1Main.CharacterType.RedMushroom,
                Sprint1Main.CharacterType.Star, Sprint1Main.CharacterType.GreenMushroom,
                Sprint1Main.CharacterType.Flower, Sprint1Main.CharacterType.Bomb};
        }

        public override void Update(float timeOfFrame)
        {
            base.Update(timeOfFrame);

            Time += timeOfFrame;
            if(Time >= 1)
            {
                Time -= 1;
                CurrentType++;
                CurrentType = CurrentType >= TypeList.Length ? 0 : CurrentType;
            }
           
        }

       

        public override void MarioCollide(bool specialCase)
        {

            Parameters.IsHidden = true;
            if(Type != Sprint1Main.CharacterType.Bomb)
                SoundFactory.Instance.MarioGetItem();

        }
    }
}
