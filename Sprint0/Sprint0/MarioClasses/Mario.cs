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

        public bool IsLeft { get; set; }

        private Vector2 Location;

        public Mario(Texture2D[] standardSheets, Texture2D[] superSheet, Texture2D[] fireSheet, Texture2D diedSheet)
        {
            StandardMario = standardSheets;
            SuperMario = superSheet;
            FireMario = fireSheet;
            SetActionSprites();
            setActionStates();
            DiedSprite = new ActionSprite(diedSheet, new Point(1, 1));
            IsLeft = false;
            Location = new Vector2(400, 300);
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
        public void moveRight() { Action.Right(this); }

        public void moveLeft() { Action.Left(this); }

        public void moveUp() { Action.Up(this); }

        public void moveDown() { Action.Down(this); }
        #endregion Action Command Receiver Method

        #region Action Change
        public void ChangeToIdle()
        {

        }

        public void ChangeToJump()
        {

        }

        public void ChangeToWalk()
        {

        }

        public void ChangeToCrouch()
        {

        }

        public void ChangeToRunningJump() { }
        #endregion Action Change

        private void SetActionSprites()
        {
            IdleSprite = new ActionSprite(StandardMario[0], new Point(1, 1));
            JumpSprite = new ActionSprite(StandardMario[1], new Point(1, 1));
            WalkingSprite = new ActionSprite(StandardMario[2], new Point(1, 3));
            CrouchSprite = new ActionSprite(StandardMario[3], new Point(1, 1));
            currentMarioAction = IdleSprite;
        }

        private void setActionStates()
        {
            IdleState = new IdleState();
            JumpState = new JumpState();
            WalkState = new WalkState();
            RunningJumpState = new RunningJumpState();
            CrouchState = new CrouchState();
            Action = IdleState;
        }
    }
}
