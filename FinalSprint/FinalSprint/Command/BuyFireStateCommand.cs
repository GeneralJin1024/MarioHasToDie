using FinalSprint.MarioClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalSprint
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
            if (!Mario.IsFire() && !Mario.IsDied() && Sprint5Main.Coins >= 70)
            {
                Mario.MoveFire();
                Sprint5Main.Coins -= 70;
                SoundFactory.Instance.PowerUp();
            }
        }
    }

    public class GetCoins: ICommand
    {
        public void Execute() { Sprint5Main.Coins += 100; }
    }
}
