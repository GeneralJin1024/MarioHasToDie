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
        public Mario.PowerType Type { get; set; } = Mario.PowerType.Standard;
        public void Destroy(Mario mario) { mario.ChangeToDied(); }
        public void Leave(Mario mario, int[] CurrentActionAndState) { }
    }
    class SuperState : IPowerState
    {
        public Mario.PowerType Type { get; set; } = Mario.PowerType.Super;
        public void Destroy(Mario mario) { mario.ChangeToStandard(); }
        public void Leave(Mario mario, int[] CurrentActionAndState) { }


    }
    class FireState : IPowerState
    {
        public Mario.PowerType Type { get; set; } = Mario.PowerType.Super;
        public void Destroy(Mario mario) { mario.ChangeToSuper(); }
        public void Leave(Mario mario, int[] CurrentActionAndState) { }


    }
    class DiedState : IPowerState
    {
        public Mario.PowerType Type { get; set; } = Mario.PowerType.Died;
        public void Destroy(Mario mario) { }

        public void Leave(Mario mario, int[] CurrentActionAndState)
        {
            //No matter change to which porwer state. The action must become idle.
            CurrentActionAndState[0] = 0;
            CurrentActionAndState[1] = 0;
        }


    }
}
