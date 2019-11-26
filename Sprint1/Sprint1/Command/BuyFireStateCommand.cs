using Sprint1.MarioClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint1
{   // Command Class for the Avatar to fire state
    public class BuyFireStateCommand : ICommand
    {
        private readonly MarioCharacter Mario;
        public BuyFireStateCommand(MarioCharacter mario)
        {
            Mario = mario;
        }

        public void Execute()
        {   // take the receiver method of Fire state change
            if (!Mario.IsFire() && !Mario.IsDied() && Sprint1Main.Coins >= 70)
            {
                Mario.MoveFire();
                Sprint1Main.Coins -= 70;
                SoundFactory.Instance.PowerUp();
            }
        }
    }

    public class GetCoins: ICommand
    {
        public void Execute() { Sprint1Main.Coins += 100; }
    }
}
