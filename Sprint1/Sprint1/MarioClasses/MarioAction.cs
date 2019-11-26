using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint1.MarioClasses
{
    class IdleState : IActionState
    {
        public MarioState.ActionType Type { get; set; } = MarioState.ActionType.Idle;
        public void Up(Mario mario) { mario.ChangeToJump(Mario.YVelocity); }
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
        public void Return(Mario mario) { }
    }

    class JumpState : IActionState
    {
        public MarioState.ActionType Type { get; set; } = MarioState.ActionType.Jump;
        public void Up(Mario mario) { mario.Jumphigher(); }
        public void Down(Mario mario) { }
        public void Left(Mario mario)
        {
            mario.Parameters.IsLeft = true;
            mario.Parameters.SetVelocity(Mario.XVelocity, mario.Parameters.Velocity.Y);
        }
        public void Right(Mario mario)
        {
            mario.Parameters.IsLeft = false;
            mario.Parameters.SetVelocity(Mario.XVelocity, mario.Parameters.Velocity.Y);
        }
        public void Return(Mario mario) { }
    }

    class WalkState : IActionState
    {
        public MarioState.ActionType Type { get; set; } = MarioState.ActionType.Walk;
        public void Up(Mario mario) { mario.ChangeToJump(Mario.YVelocity); }
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
        public void Return(Mario mario) { mario.ChangeToIdle(); }
    }

    class CrouchState : IActionState
    {
        public MarioState.ActionType Type { get; set; } = MarioState.ActionType.Crouch;
        public void Up(Mario mario) { mario.ChangeToIdle(); } // only Super and Fire can call this method
        public void Down(Mario mario) { }
        public void Left(Mario mario) { mario.Parameters.IsLeft = true; }
        public void Right(Mario mario) { mario.Parameters.IsLeft = false; }
        public void Return(Mario mario) { mario.ChangeToIdle(); }
    }

    class FallingState : IActionState
    {
        public MarioState.ActionType Type { get; set; } = MarioState.ActionType.Fall;
        public void Down(Mario mario) { }
        public void Left(Mario mario)
        {
            mario.Parameters.IsLeft = true;
            mario.Parameters.SetVelocity(Mario.XVelocity, mario.Parameters.Velocity.Y);
        }
        public void Right(Mario mario)
        {
            mario.Parameters.IsLeft = false;
            mario.Parameters.SetVelocity(Mario.XVelocity, mario.Parameters.Velocity.Y);
        }
        public void Up(Mario mario) { }
        public void Return(Mario mario) { }
    }

    class DiedActionState : IActionState
    {
        //Do nothing when died.
        public MarioState.ActionType Type { get; set; } = MarioState.ActionType.Other;
        public void Up(Mario mario) { }
        public void Down(Mario mario) { }
        public void Left(Mario mario) { }
        public void Right(Mario mario) { }
        public void Return(Mario mario) { }
    }
}
