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
        private readonly MarioCharacter Mario;
        public BuySuperPowerUpCommand(MarioCharacter mario) { Mario = mario; }
        void ICommand.Execute()
        {
            if (Mario.GetPower == MarioState.PowerType.Standard && Sprint1Main.Coins >= 50)
            {
                //When Mario is Super,Fire or Died, player cannot buy
                Mario.CollideWithRedMushRoom();
                Sprint1Main.Coins -= 50;
            }
        }
    }

    class BuyFirePowerUpCommand : ICommand
    {
        private readonly MarioCharacter Mario;
        public BuyFirePowerUpCommand(MarioCharacter mario) { Mario = mario; }
        void ICommand.Execute()
        {
            /*
             * All code below must fit a condition: Coins >= 70
             */
            if (Mario.GetPower == MarioState.PowerType.Standard && Sprint1Main.Coins >= 70)
            {
                Mario.CollideWithRedMushRoom(); // if current Mario is not Super/Fire/Died, Go to Super First
            }
            if (Mario.IsSuper && !Mario.IsFire() && Sprint1Main.Coins >= 70)
            {
                //In this line, Mario must be SuperMario
                Mario.CollideWithFlower();
                Sprint1Main.Coins -= 70;
            }
        }
    }
}
