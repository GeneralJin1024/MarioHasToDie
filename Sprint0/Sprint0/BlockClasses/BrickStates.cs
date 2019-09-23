using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint0.MarioClasses;

namespace Sprint0.BlockClasses
{
    class HiddenState : IBlockStates
    {
        public void Handle(Bricks brick)
        {
            brick.ChangeToBrick();
        }
    }
    class NormalState : IBlockStates
    {
        private Mario mario;
        public void Handle(Bricks brick)
        {
            mario = Sprint1.Game.Mario;
             if (mario.IsSuper() && brick.bType == BrickType.BNormal)
             {
                 brick.ChangeToDestroyed();
             }
             else
             {   
                 brick.Bumping();
             }
        }
    }
    class BumpingState : IBlockStates
    {
        public void Handle(Bricks brick)
        {
        //nothing to do when brick is in the air
        }
    }
    class UsedOrDestroyedState : IBlockStates
    {
        public void Handle(Bricks brick)
        {
         //nothing to do
        }
    }
}
