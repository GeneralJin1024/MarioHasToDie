using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.BlockClasses
{
    interface IBlockStates
    {
        void Handle(Bricks brick);
    }
}
