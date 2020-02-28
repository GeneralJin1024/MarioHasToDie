
using FinalSprint.MarioClasses;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace FinalSprint.ItemClasses
{
    class FlowerCharacter : ItemCharacter
    {
        public override Vector2 GetHeightAndWidth { get { return Item.GetHeightAndWidth; } }
        public override Sprint5Main.CharacterType Type { get; set; } = Sprint5Main.CharacterType.Flower;
        public FlowerCharacter(Texture2D texture, Point rowsAndColunms, Vector2 location)
            : base(texture, rowsAndColunms, location)
        {

        }

        public override void Update(float timeOfFrame)
        {
            base.Update(timeOfFrame);
        }


        public override void MarioCollide(bool specialCase)
        {

            Sprint5Main.Point += 1000;
            Parameters.IsHidden = true;
            SoundFactory.Instance.MarioGetItem();

        }
    }
}
