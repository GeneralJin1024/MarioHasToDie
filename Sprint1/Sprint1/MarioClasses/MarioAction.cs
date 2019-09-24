using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint1.MarioClasses
{
    class IdleState : IActionState
    {
        public Mario.ActionType Type { get; set; } = Mario.ActionType.Other;
        public void Up(Mario mario) { mario.ChangeToJump(); }
        public void Down(Mario mario) { mario.ChangeToCrouch(); }
        public void Left(Mario mario) {
            if (mario.IsLeft)
                mario.ChangeToWalk();
            else
                mario.IsLeft = true; // if Mario points to right, turn left
        }
        public void Right(Mario mario) {
            if (mario.IsLeft)
                mario.IsLeft = false; //if Mario points to right, turn right.
            else
                mario.ChangeToWalk();
        }
    }

    class JumpState : IActionState
    {
        //Jump can only from Idle.MoveUp
        public Mario.ActionType Type { get; set; } = Mario.ActionType.Other;
        public void Up(Mario mario) { }
        public void Down(Mario mario) { mario.ChangeToIdle(); }
        public void Left(Mario mario) {
            if (!mario.IsLeft)
                mario.IsLeft = true; //if Mario points to left, turn right
        }
        public void Right(Mario mario) {
            if (mario.IsLeft)
                mario.IsLeft = false; //if Mario points to right, turn left
        }
    }

    class RunningJumpState : IActionState
    {
        //RunningJump can only from Running.MoveUp, and only to Running
        public Mario.ActionType Type { get; set; } = Mario.ActionType.Other;
        public void Up(Mario mario) { }
        public void Down(Mario mario) { mario.ChangeToWalk(); }
        public void Left(Mario mario) {
            if (!mario.IsLeft)
                mario.IsLeft = true; //if Mario points to right, turn left
        }
        public void Right(Mario mario) {
            if (mario.IsLeft)
                mario.IsLeft = false; //if Mario points to right, turn left
        }
    }

    class WalkState : IActionState
    {
        public Mario.ActionType Type { get; set; } = Mario.ActionType.Other;
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

    class CrouchState : IActionState
    {
        public Mario.ActionType Type { get; set; } = Mario.ActionType.Crouch;
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
