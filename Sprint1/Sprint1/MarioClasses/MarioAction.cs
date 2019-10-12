using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint1.MarioClasses
{
    class IdleState : IActionState
    {
        public MarioState.ActionType Type { get; set; } = MarioState.ActionType.Other;
        public void Up(Mario mario) { mario.ChangeToJump(-5); }
        public void Down(Mario mario) { mario.ChangeToCrouch(); }
        public void Left(Mario mario) // walk left if facing left, stand left if facing right.
        {
            if (mario.Parameters.IsLeft)
                mario.ChangeToWalk();
            else
                mario.Parameters.IsLeft = true;
        }
        public void Right(Mario mario) // walk right if facing right, stand right if facing left.
        {
            if (mario.Parameters.IsLeft)
                mario.Parameters.IsLeft = false;
            else
            {
                mario.ChangeToWalk();
            }
        }
    }

    class JumpState : IActionState
    {
        public MarioState.ActionType Type { get; set; } = MarioState.ActionType.Other;
        public void Up(Mario mario) { }
        public void Down(Mario mario) { mario.ChangeToIdle(); }
        public void Left(Mario mario)
        {
            if (!mario.Parameters.IsLeft)
                mario.Parameters.IsLeft = true;
            else // In the future, y velocity will depend on gravity, so only left and right speed is constant.
                mario.ChangeToRunningJump(mario.Parameters.Velocity.Y);
        }
        public void Right(Mario mario)
        {
            if (mario.Parameters.IsLeft)
                mario.Parameters.IsLeft = false;
            else
                mario.ChangeToRunningJump(mario.Parameters.Velocity.Y);
        }
    }

    class RunningJumpState : IActionState
    {
        public MarioState.ActionType Type { get; set; } = MarioState.ActionType.Other;
        public void Up(Mario mario) { }
        public void Down(Mario mario) { mario.ChangeToWalk(); } // go back to walk if it has horizontal velocity.
        public void Left(Mario mario)
        {
            mario.Parameters.IsLeft = true;
            mario.ChangeToRunningJump(mario.Parameters.Velocity.Y);
        }
        public void Right(Mario mario)
        {
            mario.Parameters.IsLeft = false;
            mario.ChangeToRunningJump(mario.Parameters.Velocity.Y);
        }
    }

    class WalkState : IActionState
    {
        public MarioState.ActionType Type { get; set; } = MarioState.ActionType.Other;
        public void Up(Mario mario)
        {
            mario.ChangeToRunningJump(-5); // -5 is the initial velocity of jumping
        }
        public void Down(Mario mario) { }
        public void Left(Mario mario)
        {
            if (!mario.Parameters.IsLeft)
                mario.ChangeToIdle();
        }
        public void Right(Mario mario)
        {
            if (mario.Parameters.IsLeft)
                mario.ChangeToIdle();
        }
    }

    class CrouchState : IActionState
    {
        public MarioState.ActionType Type { get; set; } = MarioState.ActionType.Crouch;
        public void Up(Mario mario) { mario.ChangeToIdle(); } // only Super and Fire can call this method
        public void Down(Mario mario) { }
        public void Left(Mario mario) { mario.Parameters.IsLeft = true; }
        public void Right(Mario mario) { mario.Parameters.IsLeft = false; }
    }

    class DiedActionState : IActionState
    {
        //Do nothing when died.
        public MarioState.ActionType Type { get; set; } = MarioState.ActionType.Other;
        public void Up(Mario mario) { }
        public void Down(Mario mario) { }
        public void Left(Mario mario) { }
        public void Right(Mario mario) { }
    }
}
