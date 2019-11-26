using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint1.ItemClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint1.ItemClasses
{
    class JumpMedicineCharacter : ItemCharacter
    {
        public override Vector2 GetHeightAndWidth { get { return Item.GetHeightAndWidth; } }
        public override Sprint1Main.CharacterType Type { get; set; } = Sprint1Main.CharacterType.JumpMedicine;
        public JumpMedicineCharacter(Texture2D texture, Point rowsAndColunms, Vector2 location)
            : base(texture, rowsAndColunms, location)
        {

        }

        public override void Update(float timeOfFrame)
        {
            base.Update(timeOfFrame);
        }

        public override void MarioCollide(bool specialCase)
        {

           
            Parameters.IsHidden = true;
            SoundFactory.Instance.MarioGetItem();

        }
    }
}
