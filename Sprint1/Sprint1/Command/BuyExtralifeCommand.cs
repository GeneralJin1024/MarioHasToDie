using Sprint1.MarioClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint1
{   // Command Class for the Avatar to fire state
    public class BuyExtralifeCommand : ICommand
    {
        MarioCharacter mario;
        public BuyExtralifeCommand(MarioCharacter mario)
        {
            this.mario = mario;
        }

        public void Execute()
        {   // The Mario get one more extra life
            Sprint1Main.MarioLife++;
        }
    }
}

