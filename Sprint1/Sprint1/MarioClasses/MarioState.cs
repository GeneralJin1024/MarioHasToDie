using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint1.MarioClasses
{
    public class MarioState
    {
        //Used to change to Idle when crouch super Mario want to change to standard Mario
        public enum ActionType
        {
            Crouch, Other
        }
        //In Super, the brick block can bump.
        //In Died, reject all action command.
        public enum PowerType
        {
            Standard, Super, Died
        }

        public ActionType GetActionType { get { return actionStates[CurrentState[0]].Type; } }
        public PowerType GetPowerType { get { return powerStates[CurrentState[1]].Type; } }

        private readonly IActionState[] actionStates;
        private readonly IPowerState[] powerStates;
        public Mario Mario { get; }
        private readonly int[] CurrentState;

        public MarioState(Mario mario)
        {
            actionStates = new IActionState[] {new IdleState(), new JumpState(), new WalkState(),
                new CrouchState(), new RunningJumpState(), new DiedActionState() };
            powerStates = new IPowerState[] { new StandardState(), new SuperState(), new FireState(), new DiedState() };
            Mario = mario;
            CurrentState = new int[] { 0, 0 };
        }

        #region Move Command Receiver
        public void MoveUp()
        {
            actionStates[CurrentState[0]].Up(Mario);
        }

        public void MoveDown()
        {
            //(CurrentState[0] != 0 || CurrentState[1] != 0)
            actionStates[CurrentState[0]].Down(Mario);
        }

        public void MoveLeft()
        {
            actionStates[CurrentState[0]].Left(Mario);
        }

        public void MoveRight()
        {
            actionStates[CurrentState[0]].Right(Mario);
        }
        #endregion

        #region Change Power
        public void ChangeToStandard()
        {
            if (powerStates[CurrentState[1]].Type == PowerType.Super && actionStates[CurrentState[0]].Type == ActionType.Crouch)
                Mario.ChangeToIdle();
            powerStates[CurrentState[1]].Leave(Mario);
            Mario.ChangeTexture(0);
            CurrentState[1] = 0;
        }

        public void ChangeToSuper()
        {
            powerStates[CurrentState[1]].Leave(Mario);
            Mario.ChangeTexture(1);
            CurrentState[1] = 1;
        }

        public void ChangeToFire()
        {
            powerStates[CurrentState[1]].Leave(Mario);
            Mario.ChangeTexture(2);
            CurrentState[1] = 2;
        }

        public void ChangeToDied()
        {
            //Die code
            Mario.ChangeActionAndSprite(5);
            Mario.Parameters.SetVelocity(0, 0);
            CurrentState[1] = 3;
        }
        #endregion

        public void Destroy()
        {
            powerStates[CurrentState[1]].Destroy(this);
        }

        public void ChangeAction(int changeNumber)
        {
            CurrentState[0] = changeNumber;
        }

        //public ActionType GetActionType()
        //{
        //    return actionStates[CurrentState[0]].Type;
        //}

        //public PowerType GetPowerType()
        //{
        //    return powerStates[CurrentState[1]].Type;
        //}
    }
}
