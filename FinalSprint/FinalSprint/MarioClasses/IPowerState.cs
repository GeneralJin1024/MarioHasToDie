using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalSprint.MarioClasses
{
    interface IPowerState
    {
        MarioState.PowerType Type { get; set; }
        void Destroy(MarioState marioState);
        void Leave(Mario mario);
    }
}
