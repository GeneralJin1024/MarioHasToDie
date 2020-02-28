
using FinalSprint.MarioClasses;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace FinalSprint.ItemClasses
{
    class StarCharacter : ItemCharacter
    {
        public override Vector2 GetHeightAndWidth { get { return Item.GetHeightAndWidth; } }
        public override Sprint5Main.CharacterType Type { get; set; } = Sprint5Main.CharacterType.Star;
        public StarCharacter(Texture2D texture, Point rowsAndColunms, Vector2 location)
            : base(texture, rowsAndColunms, location) { }


        public override void Update(float timeOfFrame)
        {
            base.Update(timeOfFrame);
            if (!Parameters.IsHidden && !IsBump && Parameters.Velocity.X == 0)
            {
                Parameters.IsLeft = Sprint5Main.Game.Scene.Mario.GetMinPosition.X >= Parameters.Position.X;
                Parameters.SetVelocity(3, -15);
            }
        }

        public override void MarioCollide(bool specialCase)
        {

            Sprint5Main.Point += 1000;
            Parameters.IsHidden = true; SoundFactory.Instance.MarioGetItem();
        }

    }

    class FireBallCharacter : ItemCharacter
    {
        public override Vector2 GetHeightAndWidth { get { return Item.GetHeightAndWidth; } }
        public override Sprint5Main.CharacterType Type { get; set; } = Sprint5Main.CharacterType.Fireball;
        public FireBallCharacter(Texture2D texture, Point rowsAndColumns, Vector2 location) : base(texture, rowsAndColumns, location)
        {
            Parameters.HasGravity = true;
        }

        public override void MarioCollide(bool specialCase) {}
        public override void BlockCollide(bool isBottom) { Parameters.IsHidden = true; }
    }
}
