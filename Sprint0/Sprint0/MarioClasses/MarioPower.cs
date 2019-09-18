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
    }
    class SuperState : PowerState
    {
        public Mario.PowerType Type { get; set; } = Mario.PowerType.Super;
        public void Destroy(Mario mario) { mario.ChangeToStandard(); }

      
    }
    class FireState : PowerState
    {
        public Mario.PowerType Type { get; set; } = Mario.PowerType.Super;
        public void Destroy(Mario mario) { mario.ChangeToSuper(); }

       
    }
    class DiedState : PowerState
    {
        public Mario.PowerType Type { get; set; } = Mario.PowerType.Died;
        public void Destroy(Mario mario) { }

      
    }
}
