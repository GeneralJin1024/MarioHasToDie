using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint0.MarioClasses
{
    class Mario : ISprite
    {
        public Texture2D SpriteSheets { get; set; }//useless variable

        #region Textures
        //{Stand, Jump, Walk, Crouch}
        private Texture2D[] StandardMario;
        private Texture2D[] SuperMario;
        private Texture2D[] FireMario;
        private Texture2D DiedSheet;
        #endregion Textures

        #region ActionSprites
        private ISprite IdleSprite;
        private ISprite JumpSprite;
        private ISprite CrouchSprite;
        private ISprite WalkingSprite;
        private ISprite DiedSprite;
        private ISprite currentMarioAction;
        #endregion ActionSprites

        #region Action States
        private ActionState IdleState;
        private ActionState JumpState;
        private ActionState WalkState;
        private ActionState RunningJumpState;
        private ActionState CrouchState;
        private ActionState Action;
        #endregion Action States

        #region PowerState
        PowerState Standard;
        PowerState Super;
        PowerState Fire;
        PowerState Died;
        PowerState Power;
        #endregion PowerState

        public bool IsLeft { get; set; }
        public bool IsSuper { get; set; }
        private bool IsDied;
        private bool IsCrouch;

        private Vector2 Location;

        public Mario(Texture2D[] standardSheets, Texture2D[] superSheet, 
            Texture2D[] fireSheet, Texture2D diedSheet, Vector2 location)
        {
            StandardMario = standardSheets;
            SuperMario = superSheet;
            FireMario = fireSheet;
            SetActionSprites();
            SetActionStates();
            SetPowerStates();
            DiedSprite = new AnimatedSprite(diedSheet, new Point(1, 1));
            IsLeft = false;
            IsSuper = false;
            IsCrouch = false;
            IsDied = false;
            Location = location;
        }
        #region ISprite Methods
        public void Update(GameTime gameTime)
        {
            currentMarioAction.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location, bool isLeft)
        {
            currentMarioAction.Draw(spriteBatch, Location, IsLeft);
        }
        #endregion ISprite Methods

        #region Action Command Receiver Method
        public void MoveRight() {
            if(!IsDied)
                Action.Right(this);
        }

        public void MoveLeft() {
            if(!IsDied)
                Action.Left(this);
        }

        public void MoveUp() {
            if(!IsDied)
                Action.Up(this);
        }

        public void MoveDown() {
            if(!IsDied)
                Action.Down(this);
        }
        #endregion Action Command Receiver Method

        #region Action Change
        public void ChangeToIdle()
        {
            Action = IdleState;
            currentMarioAction = IdleSprite;
        }

        public void ChangeToJump()
        {
            Action = JumpState;
            currentMarioAction = JumpSprite;
        }

        public void ChangeToWalk()
        {
            Action = WalkState;
            currentMarioAction = WalkingSprite;
            if (IsSuper && IsCrouch)
                // The difference of height between standing and crouch.
                Location.Y -= 10;
            IsCrouch = false;
        }

        public void ChangeToCrouch()
        {
            Action = CrouchState;
            currentMarioAction = CrouchSprite;
            if (IsSuper)
                //The difference of height between standing and crouch.
                Location.Y += 10;
            IsCrouch = true;
        }

        public void ChangeToRunningJump()
        {
            Action = RunningJumpState;
            currentMarioAction = JumpSprite;
        }
        #endregion Action Change

        #region Power Command Receiver Method
        public void MoveUpdate() { }

        public void MoveDestroy() { }
        #endregion Power Command Receiver Method

        #region Power Change
        public void ChangeToSuper() { }
        public void ChangeToFire() { }
        public void ChangeToStandard() { }
        public void ChangeToDied() { }
        #endregion Power Change


        private void SetActionSprites()
        {
            IdleSprite = new AnimatedSprite(StandardMario[0], new Point(1, 1));
            JumpSprite = new AnimatedSprite(StandardMario[1], new Point(1, 1));
            WalkingSprite = new AnimatedSprite(StandardMario[2], new Point(1, 3));
            CrouchSprite = new AnimatedSprite(StandardMario[3], new Point(1, 1));
            currentMarioAction = IdleSprite;
        }

        private void SetActionStates()
        {
            IdleState = new IdleState();
            JumpState = new JumpState();
            WalkState = new WalkState();
            RunningJumpState = new RunningJumpState();
            CrouchState = new CrouchState();
            Action = IdleState;
        }

        private void SetPowerStates()
        {
            Standard = new StandardState();
            Super = new SuperState();
            Fire = new FireState();
            Died = new DiedState();
            Power = Standard;
        }
    }
}
