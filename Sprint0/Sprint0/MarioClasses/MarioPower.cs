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
        public void Destroy(Mario mario) { mario.ChangeToDied(); }

        void PowerState.PowerUpToFireMario(Mario mario)
        {
        }

        public void PowerUpToSuperMario(Mario mario)
        {
        }

        public void PowerDownToStandard(Mario mario)
        {
        }
    }
    class SuperState : PowerState
    {
        public void Destroy(Mario mario) { mario.ChangeToStandard(); }

        public void PowerDownToStandard(Mario mario)
        {
        }

        public void PowerUpToFireMario(Mario mario)
        {
        }

        public void PowerUpToSuperMario(Mario mario)
        {
        }
    }
    class FireState : PowerState
    {
        public void Destroy(Mario mario) { mario.ChangeToSuper(); }

        public void PowerDownToStandard(Mario mario)
        {
        }

        public void PowerUpToFireMario(Mario mario)
        {
        }

        public void PowerUpToSuperMario(Mario mario)
        {
        }
    }
    class DiedState : PowerState
    {
        public void Destroy(Mario mario) { }

        public void PowerDownToStandard(Mario mario)
        {
        }

        public void PowerUpToFireMario(Mario mario)
        {
        }

        public void PowerUpToSuperMario(Mario mario)
        {
        }
    }
}
