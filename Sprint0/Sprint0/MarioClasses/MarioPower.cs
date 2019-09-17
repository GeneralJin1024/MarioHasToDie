using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.MarioClasses
{
    class StandardState : PowerState
    {

          void PowerUpToFireMario(Mario mario);

        void PowerUpToSuperMario(Mario mario);

        void PowerDownToStandard(Mario mario);

        public void Destroy(Mario mario) { mario.ChangeToDied(); }

        public void PowerUpToFireMario（Mario mario)   { }

        public  void PowerUpToSuperMario(Mario mario) { mario.ChangeToSuper();}
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
