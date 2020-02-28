using FinalSprint.MarioClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalSprint
{   // Command Class for the Avatar to a Jump idle state
    public class MoveUpCommand : ICommand
    {
        MarioCharacter mario;

        public MoveUpCommand(MarioCharacter mario)
        {
            this.mario = mario;
        }

        public void Execute()
        {   // take the receiver method of mario action change
            mario.MoveUp();
        }
    }

    public class JumpHigherCommand : ICommand
    {
        MarioCharacter mario;

        public JumpHigherCommand(MarioCharacter mario)
        {
            this.mario = mario;
        }

        public void Execute()
        {   // take the receiver method of mario action change
            mario.JumpHigher();
        }
    }
}
