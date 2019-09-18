using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.ComponentModel;

namespace Sprint0.MarioClasses
{
    class Mario : ISprite
    {
        public Texture2D SpriteSheets { get; set; }//useless variable

        public enum ActionType {
            [Description ("Crouch")]
            Crouch,
            [Description("Other")]
            Other
        }

        public enum PowerType {
            [Description("Standard")]
            Standard,
            [Description("Super")]
            Super,
            [Description("Died")]
            Died
        }

        #region Textures
        //{Stand, Jump, Walk, Crouch}
        private Texture2D[] StandardMario;
        private Texture2D[] SuperMario;
        private Texture2D[] FireMario;
        #endregion Textures

        private ActionType actionType;
        private PowerType powerType;

        #region ActionSprites
        private ISprite DiedSprite;
        private ISprite currentMarioAction;
        //{Idle, Jump, Walking, Crouch}
        private ISprite[] ActionSprites;
        #endregion ActionSprites

        #region Action States
        private ActionState CurrentAction;
        //{Idle, Jump, Walk, RunningJump, Crouch}
        private ActionState[] ActionStates;
        #endregion Action States

        #region PowerState
        private PowerState CurrentPower;
        //{Standard, Super, Fire, Died}
        private PowerState[] PowerStates;
        #endregion PowerState

        public bool IsLeft { get; set; }

        public Vector2 Position
        {
            get
            {
                return Location;
            }
        }

        private Vector2 Location;

        public Mario(Texture2D[] standardSheets, Texture2D[] superSheet, 
            Texture2D[] fireSheet, Texture2D diedSheet, Vector2 location)
        {
            StandardMario = standardSheets;
            SuperMario = superSheet;
            FireMario = fireSheet;
            DiedSprite = new AnimatedSprite(diedSheet, new Point(1, 1));
            ActionSprites = new ISprite[4] { new AnimatedSprite(StandardMario[0], new Point(1, 1)),
                new AnimatedSprite(StandardMario[1], new Point(1, 1)),
                new AnimatedSprite(StandardMario[2], new Point(1, 3)),
                new AnimatedSprite(StandardMario[3], new Point(1, 1))};
            ActionStates = new ActionState[5] { new IdleState(), new JumpState(), new WalkState(),
                new RunningJumpState(), new CrouchState() };
            PowerStates = new PowerState[4] { new StandardState(), new SuperState(), new FireState(),
                new DiedState() };
            currentMarioAction = ActionSprites[0];
            CurrentAction = ActionStates[0];
            CurrentPower = PowerStates[0];
            IsLeft = false;
            actionType = ActionType.Other;
            powerType = PowerType.Standard;
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

        public Vector2 GetHeightAndWidth()
        {
            return currentMarioAction.GetHeightAndWidth();
        }
        #endregion ISprite Methods

        #region Action Command Receiver Method
        public void MoveRight() {
            if (powerType != PowerType.Died)
                CurrentAction.Right(this);
        }

        public void MoveLeft() {
            if (powerType != PowerType.Died)
                CurrentAction.Left(this);
        }

        public void MoveUp() {
            if (powerType != PowerType.Died)
                CurrentAction.Up(this);
        }

        public void MoveDown() {
            if (powerType != PowerType.Died)
                CurrentAction.Down(this);
        }
        #endregion Action Command Receiver Method

        #region Action Change
        public void ChangeToIdle()
        {
            CurrentAction = ActionStates[0];
            currentMarioAction = ActionSprites[0];
            if (powerType == PowerType.Super && actionType == ActionType.Crouch)
                // The difference of height between standing and crouch.
                Location.Y -= 10;
            actionType = ActionType.Other;
        }

        public void ChangeToJump()
        {
            CurrentAction = ActionStates[1];
            currentMarioAction = ActionSprites[1];
        }

        public void ChangeToWalk()
        {
            CurrentAction = ActionStates[2];
            currentMarioAction = ActionSprites[2];
        }

        public void ChangeToCrouch()
        {
            CurrentAction = ActionStates[4];
            currentMarioAction = ActionSprites[3];
            if (powerType == PowerType.Super)
                //The difference of height between standing and crouch.
                Location.Y += 10;
            actionType = ActionType.Crouch;
        }

        public void ChangeToRunningJump()
        {
            CurrentAction = ActionStates[3];
            currentMarioAction = ActionSprites[1];
        }
        #endregion Action Change

        #region Power Command Receiver Method
        // try update
        public void MoveStandard() { ChangeToStandard();}
        public void MoveSuper() { ChangeToSuper();}
        public void MoveFire() { ChangeToFire();}
        public void MoveDestroy() { CurrentPower.Destroy(this);}
        #endregion Power Command Receiver Method

        #region Power Change
        public void ChangeToSuper()
        {
            CurrentPower = PowerStates[1];
            ActionSprites[0].SpriteSheets = SuperMario[0];
            ActionSprites[1].SpriteSheets = SuperMario[1];
            ActionSprites[2].SpriteSheets = SuperMario[2];
            ActionSprites[3].SpriteSheets = SuperMario[3];

            if (powerType == PowerType.Died)
                currentMarioAction = ActionSprites[0];
            if (powerType  != PowerType.Super)
            {
                Location.Y -=16;
                powerType = PowerType.Super;
            }

        }
        public void ChangeToStandard()
        {
            if(actionType == ActionType.Crouch)  
                ChangeToIdle();
            CurrentPower = PowerStates[0];
            ActionSprites[0].SpriteSheets = StandardMario[0];
            ActionSprites[1].SpriteSheets = StandardMario[1];
            ActionSprites[2].SpriteSheets = StandardMario[2];
            ActionSprites[3].SpriteSheets = StandardMario[3];
            
            if(powerType == PowerType.Died)

                currentMarioAction = ActionSprites[0];

            else if (powerType ==PowerType.Super)
            {
                Location.Y +=16;
            }
            powerType = PowerType.Standard;

        }
        public void ChangeToFire()
        {
            CurrentPower = PowerStates[2];
            ActionSprites[0].SpriteSheets = FireMario[0];
            ActionSprites[1].SpriteSheets = FireMario[1];
            ActionSprites[2].SpriteSheets = FireMario[2];
            ActionSprites[3].SpriteSheets = FireMario[3];

            if(powerType ==PowerType.Died)
                currentMarioAction = ActionSprites[0];
            if(powerType != PowerType.Super)
            {
                Location.Y -=16;
                powerType = PowerType.Super;
            }

        }
        public void ChangeToDied()
        {
            CurrentPower = PowerStates[3];
            currentMarioAction = DiedSprite;
            if(powerType == PowerType.Super)
                Location.Y +=16;
            powerType = PowerType.Died;
        }
        #endregion Power Change

        // give Block to justify
       public bool IsSuper()
        {
            return (powerType == PowerType.Super);
        }
    }
}
