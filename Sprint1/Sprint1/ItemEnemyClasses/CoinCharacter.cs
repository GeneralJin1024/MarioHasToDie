
using Sprint1.MarioClasses;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint1.ItemClasses
{
    class CoinCharacter : ItemCharacter
    {
        public override Sprint1Main.CharacterType Type { get; set; } = Sprint1Main.CharacterType.Coin;

        public CoinCharacter(Texture2D texture, Point rowsAndColunms, Vector2 location)
            : base(texture, rowsAndColunms, location)
        {

        }

        public override Vector2 GetHeightAndWidth()
        {
            return item.GetHeightAndWidth();
        }

        public override void Update(float timeOfFrame)
        {
            base.Update(timeOfFrame);
            if (!Parameters.IsHidden && isBump)
            {              
                if (Parameters.Position.Y <= bumpHigh)
                {
                    isBump = false;
                    GetACoin();
                    Parameters.IsHidden = true; //Handle Coins
                }
            }
        }
        private void GetACoin()
        {
            Sprint1Main.Coins++;
            if (Sprint1Main.Coins >= 100)
            {
                Sprint1Main.MarioLife++;
                Sprint1Main.Coins -= 100;
            }
            Sprint1Main.Point += 200;
        }

        public override void MarioCollide(bool specialCase)
        {
            GetACoin();
            Parameters.IsHidden = true;

        }
    }
}
