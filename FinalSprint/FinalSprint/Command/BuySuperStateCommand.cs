using FinalSprint.MarioClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalSprint
{   // Command Class for the Avatar to Super state
    public class BuySuperStateCommand : ICommand
    {
        private readonly MarioCharacter Mario;
        public BuySuperStateCommand(MarioCharacter mario)
        {
            Mario = mario;
        }

        public void Execute()
        {   // take the receiver method of Super state change
            if (Sprint5Main.Coins >= 50 && Mario.GetPower == MarioState.PowerType.Standard)
            {
                Mario.MoveSuper();
                Sprint5Main.Coins -= 50;
                SoundFactory.Instance.PowerUp();
            }
        }
    }
}