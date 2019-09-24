using Sprint1.MarioClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint1
{   // Command Class for the Avatar to fire state
    public class MoveFireCommand : ICommand
    {
        Mario mario;
        public MoveFireCommand(Mario mario)
        {
            this.mario = mario;
        }

        public void Execute()
        {   // take the receiver method of power state change
            mario.MoveFire();
        }
    }
}

