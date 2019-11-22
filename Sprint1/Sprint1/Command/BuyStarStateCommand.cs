using Sprint1.MarioClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint1
{   // Command Class for the Avatar to fire state
    public class BuyStarStateCommand : ICommand
    {
        MarioCharacter mario;
        public BuyStarStateCommand(MarioCharacter mario)
        {
            this.mario = mario;
        }

        public void Execute()
        {   // take the receiver method of Star state change
            if (Sprint1Main.Coins >= 50)
            {
                mario.ChangeToStarState(50);
                Sprint1Main.Coins -= 50;
            }
        }
    }
}

