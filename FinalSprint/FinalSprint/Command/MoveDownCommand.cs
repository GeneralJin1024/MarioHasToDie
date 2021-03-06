﻿using FinalSprint.MarioClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalSprint
{   // Command Class for the Avatar to a crouching state
    public class MoveDownCommand : ICommand
    {
        MarioCharacter mario;
        public MoveDownCommand(MarioCharacter mario)
        {
            this.mario = mario;
        }

        public void Execute()
        {   // take the receiver method of mario action change
            if (mario.OnPipe)
                mario.OnPipePressDown = true;
            mario.MoveDown();
        }
    }
}

