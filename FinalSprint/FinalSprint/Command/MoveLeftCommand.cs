using FinalSprint.MarioClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalSprint
{   // Command Class for the Avatar to a left-facing walking state
    public class MoveLeftCommand : ICommand
    {
        MarioCharacter mario;
        public MoveLeftCommand(MarioCharacter mario)
        {
            this.mario = mario;
        }

        public void Execute()
        {   // take the receiver method of mario action change
            mario.MoveLeft();
        }
    }
}
