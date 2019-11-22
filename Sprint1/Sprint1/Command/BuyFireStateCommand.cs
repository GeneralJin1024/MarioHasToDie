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
        MarioCharacter mario;
        public BuyFireStateCommand(MarioCharacter mario)
        {
            this.mario = mario;
        }

        public void Execute()
        {   // take the receiver method of Fire state change
            mario.MoveFire();
        }
    }
}
