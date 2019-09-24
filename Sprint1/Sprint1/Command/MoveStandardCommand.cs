using Sprint1.MarioClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint1
{ 
    public class MoveStandardCommand : ICommand
    {
        Mario mario;
        public MoveStandardCommand(Mario mario)
        {
            this.mario = mario;
        }

        public void Execute()
        {
            mario.MoveStandard();
        }
    }
}
