using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalSprint.MarioClasses;

namespace FinalSprint.BlockClasses
{
    class HiddenState : IBlockStates
    {
        public void Handle(Blocks brick)
        {
            brick.ChangeToBrick();
        }
    }
    class NormalState : IBlockStates
    {
        private MarioCharacter mario;
        public void Handle(Blocks brick)
        {
            mario = Sprint5Main.Game.Scene.Mario;
            if (mario.IsSuper && brick.BType == BlockType.BNormal)
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
        public void Handle(Blocks brick)
        {
        //nothing to do when brick is in the air
        }
    }
    class UsedOrDestroyedState : IBlockStates
    {
        public void Handle(Blocks brick)
        {
         //nothing to do
        }
    }
}
