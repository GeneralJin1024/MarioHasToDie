using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.MarioClasses
{
    interface PowerState
    {
        void Update(Mario mario);
        void Destroy(Mario mario);
    }
}
