using Sprint1.MarioClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint1.MarioClasses
{
    /*
     * Died <--> Standard <--> Super <--> Fire
     */
    class StandardState : IPowerState
    {
        public MarioState.PowerType Type { get; set; } = MarioState.PowerType.Standard;
        public void Destroy(MarioState marioState) { marioState.ChangeToDied(); }
        public void Leave(Mario mario) { }
    }
    class SuperState : IPowerState
    {
        public MarioState.PowerType Type { get; set; } = MarioState.PowerType.Super;
        public void Destroy(MarioState marioState) { marioState.ChangeToStandard(); }
        public void Leave(Mario mario) { }


    }
    class FireState : IPowerState
    {
        public MarioState.PowerType Type { get; set; } = MarioState.PowerType.Super;
        public void Destroy(MarioState marioState) { marioState.ChangeToStandard(); }
        public void Leave(Mario mario) { }


    }
    class DiedState : IPowerState
    {
        public MarioState.PowerType Type { get; set; } = MarioState.PowerType.Died;
        public void Destroy(MarioState marioState) { }
        public void Leave(Mario mario)
        {
            mario.ChangeToIdle();
        }


    }
}
