using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0
{
    class MarioRunningCommand : ICommand
    {
        ISprite sprite;
        public MarioRunningCommand(ISprite sprite)
        {
            this.sprite = sprite;
        }
        public void Execute()
        {
           this.sprite.SwitchVisibility();
        }
    }
}
