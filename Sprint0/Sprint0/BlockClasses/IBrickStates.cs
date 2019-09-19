using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sprint0.MarioClasses;
using System.Threading.Tasks;

namespace Sprint0.BlockClasses
{
    interface IBrickStates
    {
        void Handle(Bricks brick);
    }
}
