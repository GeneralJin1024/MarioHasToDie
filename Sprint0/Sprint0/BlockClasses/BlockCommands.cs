using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.BlockClasses
{
    class BlockCommands : ICommand
    {
        private Bricks brick;
        
        public BlockCommands(Bricks brick)
        {
            this.brick = brick;
        }

        public virtual void Execute()
        {
            this.brick.currentbState.Handle(this.brick);
        }
    }
}
