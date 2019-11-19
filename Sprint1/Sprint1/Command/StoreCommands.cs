using Sprint1.MarioClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint1
{
    class BuySuperPowerUpCommand : ICommand
    {
        private MarioCharacter Mario;
        public BuySuperPowerUpCommand(MarioCharacter mario) { Mario = mario; }
        void ICommand.Execute()
        {
            if (!Mario.IsSuper && Sprint1Main.Coins >= 50)
            {
                Mario.CollideWithRedMushRoom();
                Sprint1Main.Coins -= 50;
            }
        }
    }
}
