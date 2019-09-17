using Sprint0.MarioClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.BlockClasses
{
    class BBrickChangeState : ICommand
    {
        private Bricks brick;
        private Mario mario;
        public BBrickChangeState(Bricks brick, Mario mario)
        {
            this.brick = brick;
            this.mario = mario;
        }
        public void Execute()
        {
            if (this.mario.IsSuper())
            {
                this.brick.ChangeToDestroyed();
            }
            else
            {
                this.brick.Bumping();
            }
        }
    }

    class BQuestionChangeState : ICommand
    {
        private Bricks brick;
        public BQuestionChangeState(Bricks brick)
        {
            this.brick = brick;
        }
        public void Execute()
        {
            this.brick.Bumping();
        }
    }

    class BHiddenChangeState : ICommand
    {
        private Bricks brick;
        public BHiddenChangeState(Bricks brick)
        {
            this.brick = brick;
        }
        public void Execute()
        {
            this.brick.ChangeToBrick();
        }
    }
}
