using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint0.MarioClasses;

namespace Sprint0.BlockClasses
{
    class HiddenState : IBrickStates
    {
        public void Handle(Bricks brick)
        {
            brick.ChangeToBrick();
        }
    }
    class NormalState : IBrickStates
    {
        private Mario mario;
        public void Handle(Bricks brick)
        {
            if (mario.IsSuper())
            {
                brick.ChangeToDestroyed();
            }
            else
            {   
                brick.Bumping();
            }
        }
    }
    class BumpingState : IBrickStates
    {
        public void Handle(Bricks brick)
        {
        //nothing to do
        }
    }
    class UsedOrDestroyedState : IBrickStates
    {
        public void Handle(Bricks brick)
        {
         //nothing to do
        }
    }
}
