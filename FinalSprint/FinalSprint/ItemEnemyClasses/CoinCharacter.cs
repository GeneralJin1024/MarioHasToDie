
using FinalSprint.MarioClasses;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace FinalSprint.ItemClasses
{
    class CoinCharacter : ItemCharacter
    {
        public override Sprint5Main.CharacterType Type { get; set; } = Sprint5Main.CharacterType.Coin;
        public override Vector2 GetHeightAndWidth { get { return Item.GetHeightAndWidth; } }
        public CoinCharacter(Texture2D texture, Point rowsAndColunms, Vector2 location)
            : base(texture, rowsAndColunms, location)
        {

        }

        public override void Update(float timeOfFrame)
        {
            base.Update(timeOfFrame);
            if (!Parameters.IsHidden && IsBump)
            {              
                if (Parameters.Position.Y <= BumpHigh)
                {
                    IsBump = false;
                    GetACoin();
                    Parameters.IsHidden = true; //Handle Coins
                    SoundFactory.Instance.MarioGetItem();
                }
            }
        }
        private void GetACoin()
        {
            Sprint5Main.Coins++;
            if (Sprint5Main.Coins >= 100)
            {
                Sprint5Main.MarioLife++;
                Sprint5Main.Coins -= 100;
            }
            Sprint5Main.Point += 200;
        }

        public override void MarioCollide(bool specialCase)
        {
            GetACoin();
            Parameters.IsHidden = true;
            SoundFactory.Instance.MarioGetItem();

        }
    }
}
