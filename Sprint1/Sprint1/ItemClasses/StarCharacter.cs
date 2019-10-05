
using Sprint1.MarioClasses;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint1.ItemClasses
{
    class StarCharacter : ItemCharacter
    {
        public StarCharacter(Texture2D texture, Point rowsAndColunms, Vector2 location)
            : base(texture, rowsAndColunms, location)
        {

        }

        public override void MarioCollideBottom(MarioCharacter mario)
        {
            Parameters.IsHidden = true;
           

        }

        public override void MarioCollideLeft(MarioCharacter mario)
        {
            Parameters.IsHidden = true;
           

        }

        public override void MarioCollideRight(MarioCharacter mario)
        {
            Parameters.IsHidden = true;
           

        }

        public override void MarioCollideTop(MarioCharacter mario)
        {
            Parameters.IsHidden = true;
           

        }
    }
}
