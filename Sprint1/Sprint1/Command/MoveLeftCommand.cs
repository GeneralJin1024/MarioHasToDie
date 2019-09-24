using Sprint1.MarioClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint1
{   // Command Class for the Avatar to a left-facing walking state
    public class MoveLeftCommand : ICommand
    {
        Mario mario;
        public MoveLeftCommand(Mario mario)
        {
            this.mario = mario;
        }

        public void Execute()
        {   // take the receiver method of mario action change
            mario.MoveLeft();
        }
    }
}
