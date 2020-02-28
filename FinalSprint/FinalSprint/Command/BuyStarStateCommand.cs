using FinalSprint.MarioClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalSprint
{   // Command Class for the Avatar to fire state
    public class BuyStarStateCommand : ICommand
    {
        private readonly MarioCharacter Mario;
        public BuyStarStateCommand(MarioCharacter mario)
        {
            Mario = mario;
        }

        public void Execute()
        {   // take the receiver method of Star state change
            if (Sprint5Main.Coins >= 50 && !Sprint5Main.Game.Scene.Mario.IsDied())
            {
                Mario.ChangeToStarState(100);
                Sprint5Main.Coins -= 50;
            }
        }
    }
}

