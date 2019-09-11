using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0
{
    class MarioRunningInPlaceCommand : ICommand
    {
        ISprite sprite;
        public MarioRunningInPlaceCommand(ISprite sprite)
        {
            this.sprite = sprite; 
        }
        public void Execute()
        {
            this.sprite.SwitchVisibility();
        }
    }
}
