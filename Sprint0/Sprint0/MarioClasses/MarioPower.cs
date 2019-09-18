using Sprint0.MarioClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.MarioClasses
{
    class StandardState : PowerState
    {
        public Mario.PowerType Type { get; set; } = Mario.PowerType.Standard;
        public void Destroy(Mario mario) { mario.ChangeToDied(); }
        public void Leave(Mario mario) { }
    }
    class SuperState : PowerState
    {
        public Mario.PowerType Type { get; set; } = Mario.PowerType.Super;
        public void Destroy(Mario mario) { mario.ChangeToStandard(); }
        public void Leave(Mario mario) { }


    }
    class FireState : PowerState
    {
        public Mario.PowerType Type { get; set; } = Mario.PowerType.Super;
        public void Destroy(Mario mario) { mario.ChangeToSuper(); }
        public void Leave(Mario mario) { }


    }
    class DiedState : PowerState
    {
        public Mario.PowerType Type { get; set; } = Mario.PowerType.Died;
        public void Destroy(Mario mario) { }

        public void Leave(Mario mario)
        {
            mario.CurrentActionAndState[0] = 0;
            mario.CurrentActionAndState[1] = 0;
        }


    }
}
