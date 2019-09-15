using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.MarioClasses
{
    class StandardState : PowerState
    {
        public void Update(Mario mario) { mario.ChangeToSuper(); }
        public void Destroy(Mario mario) { mario.ChangeToDied(); }
    }
    class SuperState: PowerState
    {
        public void Update(Mario mario) { mario.ChangeToFire(); }
        public void Destroy(Mario mario) { mario.ChangeToStandard(); }
    }
    class FireState: PowerState
    {
        public void Update(Mario mario) { }
        public void Destroy(Mario mario) { mario.ChangeToSuper(); }
    }
    class DiedState : PowerState
    {
        public void Update(Mario mario) { mario.ChangeToStandard(); }
        public void Destroy(Mario mario) { }
    }
}
