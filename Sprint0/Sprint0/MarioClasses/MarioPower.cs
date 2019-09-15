using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.MarioClasses
{
    class StandardState : PowerState
    {
        public void Destroy(Mario mario) { mario.ChangeToDied(); }
    }
    class SuperState : PowerState
    {
        public void Destroy(Mario mario) { mario.ChangeToStandard(); }
    }
    class FireState : PowerState
    {
        public void Destroy(Mario mario) { mario.ChangeToSuper(); }
    }
    class DiedState : PowerState
    {
        public void Destroy(Mario mario) { }
    }
}
