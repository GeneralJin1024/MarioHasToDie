
using Sprint1.MarioClasses;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint1.ItemClasses
{
    class RedMushroomCharacter : ItemCharacter
    {
        public override Sprint1Main.CharacterType Type { get; set; } = Sprint1Main.CharacterType.RedMushroom;
        public RedMushroomCharacter(Texture2D texture, Point rowsAndColunms, Vector2 location)
            : base(texture, rowsAndColunms, location) { }

        public override void Update(float timeOfFrame)
        {
            base.Update(timeOfFrame);
            if (!Parameters.IsHidden && !isBump && Parameters.Velocity.X == 0)
            {
                Parameters.IsLeft = Sprint1Main.Game.Scene.Mario.GetMinPosition().X <= Parameters.Position.X;
                Parameters.SetVelocity(3, 0);
            }
        }

        public override Vector2 GetHeightAndWidth()
        {
            return item.GetHeightAndWidth();
        }

        public override void MarioCollide(bool specialCase)
        {
            Sprint1Main.Point += 1000;
            Parameters.IsHidden = true;
        }

        
    }
}
