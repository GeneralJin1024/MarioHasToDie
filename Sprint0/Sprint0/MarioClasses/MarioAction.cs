using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.MarioClasses
{
    class IdleState : ActionState
    {
        public void Up(Mario mario) { mario.ChangeToJump(); }

        public void Down(Mario mario) { mario.ChangeToCrouch(); }
        public void Left(Mario mario) {
            if (mario.IsLeft)
                mario.ChangeToWalk();
            else
                mario.IsLeft = true;
        }
        public void Right(Mario mario) {
            if (mario.IsLeft)
                mario.IsLeft = false;
            else
                mario.ChangeToWalk();
        }
    }

    class JumpState : ActionState
    {
        public void Up(Mario mario) { }
        public void Down(Mario mario) { mario.ChangeToIdle(); }
        public void Left(Mario mario) {
            if (!mario.IsLeft)
                mario.IsLeft = true;
        }
        public void Right(Mario mario) {
            if (mario.IsLeft)
                mario.IsLeft = false;
        }
    }

    class RunningJumpState : ActionState
    {
        public void Up(Mario mario) { }
        public void Down(Mario mario) { mario.ChangeToWalk(); }
        public void Left(Mario mario) {
            if (!mario.IsLeft)
                mario.IsLeft = true;
        }
        public void Right(Mario mario) {
            if (mario.IsLeft)
                mario.IsLeft = false;
        }
    }

    class WalkState : ActionState
    {
        public void Up(Mario mario) { mario.ChangeToRunningJump(); }
        public void Down(Mario mario) { }
        public void Left(Mario mario) {
            if (!mario.IsLeft)
                mario.ChangeToIdle();
        }
        public void Right(Mario mario) {
            if (mario.IsLeft)
                mario.ChangeToIdle();
        }
    }

    class CrouchState : ActionState
    {
        public void Up(Mario mario) { mario.ChangeToIdle();}
        public void Down(Mario mario) { }
        public void Left(Mario mario) {
            if (!mario.IsLeft)
                mario.IsLeft = true;
        }
        public void Right(Mario mario)
        {
            if (mario.IsLeft)
                mario.IsLeft = false;
        }
    }
}
