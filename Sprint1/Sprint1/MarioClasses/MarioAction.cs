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
        public void Up(Mario mario)
        {
            mario.ChangeToJump(-5);
        }
        public void Down(Mario mario)
        {
            mario.ChangeToCrouch();
        }
        public void Left(Mario mario)
        {
            if (mario.Parameters.IsLeft)
                mario.ChangeToWalk();
            else
                mario.Parameters.IsLeft = true;
        }
        public void Right(Mario mario)
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
        public void Down(Mario mario)
        {
            mario.ChangeToIdle();
        }
        public void Left(Mario mario)
        {
            if (!mario.Parameters.IsLeft)
                mario.Parameters.IsLeft = true;
            else
                mario.ChangeToRunningJump(mario.Parameters.Velocity.Y);
            //else
            //    marioState.ChangeActionAndSprite(4);
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
        //(+-5, -5)
        public MarioState.ActionType Type { get; set; } = MarioState.ActionType.Other;
        public void Up(Mario mario) { }
        public void Down(Mario mario)
        {
            mario.ChangeToWalk();
        }
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
            mario.ChangeToRunningJump(-5);
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
        public void Up(Mario mario)
        {
            mario.ChangeToIdle();
        }
        public void Down(Mario mario) { }
        public void Left(Mario mario)
        {
            mario.Parameters.IsLeft = true;
        }
        public void Right(Mario mario)
        {
            mario.Parameters.IsLeft = false;
        }
    }

    class DiedActionState : IActionState
    {
        public MarioState.ActionType Type { get; set; } = MarioState.ActionType.Other;
        public void Up(Mario mario) { }
        public void Down(Mario mario) { }
        public void Left(Mario mario) { }
        public void Right(Mario mario) { }
    }
}
