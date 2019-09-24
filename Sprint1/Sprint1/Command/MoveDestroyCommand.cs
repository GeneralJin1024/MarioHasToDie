using Sprint1.MarioClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint1
{   // Command Class for the Avatar to take damage
    public class MoveDestroyCommand : ICommand
    {
        Mario mario;
        public MoveDestroyCommand(Mario mario)
        {
            this.mario = mario;
        }

        public void Execute()
        {   // take the receiver method of power state change
            mario.MoveDestroy();
        }
    }
}

