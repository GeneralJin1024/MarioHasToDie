using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.MarioClasses
{
    interface PowerState
    {
        Mario.PowerType Type { get; set; }
        void Destroy(Mario mario);
        void Leave(Mario mario);

       
    }
}
