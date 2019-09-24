using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint1.MarioClasses
{
    interface IPowerState
    {
        Mario.PowerType Type { get; set; }
        void Destroy(Mario mario);
        void Leave(Mario mario, int[] CurrentActionAndState);

       
    }
}
