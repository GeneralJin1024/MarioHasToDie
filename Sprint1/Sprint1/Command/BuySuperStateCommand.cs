using Sprint1.MarioClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint1
{   // Command Class for the Avatar to Super state
    public class BuySuperStateCommand : ICommand
    {
        MarioCharacter mario;
        public BuySuperStateCommand(MarioCharacter mario)
        {
            this.mario = mario;
        }

        public void Execute()
        {   // take the receiver method of Super state change
            if (Sprint1Main.Coins >= 50 && mario.GetPower == MarioState.PowerType.Standard)
            {
                mario.MoveSuper();
                Sprint1Main.Coins -= 50;
            }
        }
    }
}