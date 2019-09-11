using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0
{
    class MarioDeadCommand : ICommand
    {
        ISprite sprite;
        public MarioDeadCommand(ISprite sprite)
        {
            this.sprite = sprite;
        }
        public void Execute()
        {
            this.sprite.SwitchVisibility();
        }
    }
}
