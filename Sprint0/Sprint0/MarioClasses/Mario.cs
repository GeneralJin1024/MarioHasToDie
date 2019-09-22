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
    public class Mario : ISprite
    {
        public Texture2D SpriteSheets { get; set; }//useless variable

        public enum ActionType
        {
            [Description("Crouch")]
            Crouch,
            [Description("Other")]
            Other
        }

        public enum PowerType
        {
            [Description("Standard")]
            Standard,
            [Description("Super")]
            Super,
            [Description("Died")]
            Died
        }

        #region Textures
        //{Stand, Jump, Walk, Crouch}
        private readonly Texture2D[][] MarioSpriteSheets;
        #endregion Textures

        #region ActionSprites
        //{Idle, Jump, Walking, Crouch}
        private readonly ISprite[] ActionSprites;
        #endregion ActionSprites

        #region Action States
        //{Idle, Jump, Walk, RunningJump, Crouch}
        private readonly IActionState[] ActionStates;
        #endregion Action States

        #region PowerState
        //{Standard, Super, Fire, Died}
        private readonly IPowerState[] PowerStates;
        #endregion PowerState

        //{ActionSprite, ActionState, PowerState}
        private readonly int[] CurrentActionAndState;

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
            Texture2D[] fireSheet, Vector2 location)
        {
            MarioSpriteSheets = new Texture2D[3][] { standardSheets, superSheet, fireSheet };
            ActionSprites = new ISprite[5] { new AnimatedSprite(MarioSpriteSheets[0][0], new Point(1, 1)),
                new AnimatedSprite(MarioSpriteSheets[0][1], new Point(1, 1)),
                new AnimatedSprite(MarioSpriteSheets[0][2], new Point(1, 3)),
                new AnimatedSprite(MarioSpriteSheets[0][3], new Point(1, 1)),
                new AnimatedSprite(MarioSpriteSheets[0][4], new Point(1, 1))};
            ActionStates = new IActionState[5] { new IdleState(), new JumpState(), new WalkState(),
                new CrouchState(), new RunningJumpState() };
            PowerStates = new IPowerState[4] { new StandardState(), new SuperState(), new FireState(),
                new DiedState() };
            CurrentActionAndState = new int[3] { 0, 0, 0 };
            IsLeft = false;
            Location = location;
        }
        #region ISprite Methods
        public void Update(GameTime gameTime)
        {
            ActionSprites[CurrentActionAndState[0]].Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location, bool isLeft)
        {
            ActionSprites[CurrentActionAndState[0]].Draw(spriteBatch, Location, IsLeft);
        }

        public Vector2 GetHeightAndWidth()
        {
            return ActionSprites[CurrentActionAndState[0]].GetHeightAndWidth();
        }
        #endregion ISprite Methods

        #region Action Command Receiver Method
        public void MoveRight()
        {
            if (PowerStates[CurrentActionAndState[2]].Type != PowerType.Died)
                ActionStates[CurrentActionAndState[1]].Right(this);
        }

        public void MoveLeft()
        {
            if (PowerStates[CurrentActionAndState[2]].Type != PowerType.Died)
                ActionStates[CurrentActionAndState[1]].Left(this);
        }

        public void MoveUp()
        {
            if (PowerStates[CurrentActionAndState[2]].Type != PowerType.Died)
                ActionStates[CurrentActionAndState[1]].Up(this);
        }

        public void MoveDown()
        {
            if (PowerStates[CurrentActionAndState[2]].Type != PowerType.Died)
                //CurrentAction.Down(this);
                ActionStates[CurrentActionAndState[1]].Down(this);
        }
        #endregion Action Command Receiver Method

        #region Action Change
        public void ChangeToIdle()
        {
            if (PowerStates[CurrentActionAndState[2]].Type == PowerType.Super &&
                ActionStates[CurrentActionAndState[1]].Type == ActionType.Crouch)
                // The difference of height between standing and crouch.
                Location.Y -= 10;
            ChangeActionStateAndSprite(0);
        }

        public void ChangeToJump()
        {
            ChangeActionStateAndSprite(1);
        }

        public void ChangeToWalk()
        {
            ChangeActionStateAndSprite(2);
        }

        public void ChangeToCrouch()
        {
            if (PowerStates[CurrentActionAndState[2]].Type == PowerType.Super)
            {
                if (PowerStates[CurrentActionAndState[2]].Type == PowerType.Super)
                    //The difference of height between standing and crouch.
                    Location.Y += 10;
                ChangeActionStateAndSprite(3);
            }
        }

        public void ChangeToRunningJump()
        {
            ChangeActionStateAndSprite(4);
        }
        #endregion Action Change

        #region Power Command Receiver Method
        // try update
        public void MoveStandard() { ChangeToStandard(); }
        public void MoveSuper() { ChangeToSuper(); }
        public void MoveFire() { ChangeToFire(); }
        public void MoveDestroy() { PowerStates[CurrentActionAndState[2]].Destroy(this); }
        #endregion Power Command Receiver Method

        #region Power Change
        public void ChangeToSuper()
        {
            ChangeTexture(MarioSpriteSheets[1]);
            PowerStates[CurrentActionAndState[2]].Leave(this, CurrentActionAndState);
            if (PowerStates[CurrentActionAndState[2]].Type != PowerType.Super)
            {
                Location.Y -= 16;
            }
            CurrentActionAndState[2] = 1;
        }
        public void ChangeToStandard()
        {
            if (ActionStates[CurrentActionAndState[1]].Type == ActionType.Crouch)
                ChangeToIdle();

            ChangeTexture(MarioSpriteSheets[0]);
            PowerStates[CurrentActionAndState[2]].Leave(this, CurrentActionAndState);
            if (PowerStates[CurrentActionAndState[2]].Type == PowerType.Super)
            {
                Location.Y += 16;
            }
            CurrentActionAndState[2] = 0;
        }
        public void ChangeToFire()
        {
            ChangeTexture(MarioSpriteSheets[2]);
            PowerStates[CurrentActionAndState[2]].Leave(this, CurrentActionAndState);
            if (PowerStates[CurrentActionAndState[2]].Type != PowerType.Super)
            {
                Location.Y -= 16;
            }
            CurrentActionAndState[2] = 2;
        }
        public void ChangeToDied()
        {
            CurrentActionAndState[0] = 4;
            //if (PowerStates[CurrentActionAndState[2]].Type == PowerType.Super)
            //    Location.Y += 16;
            CurrentActionAndState[2] = 3;
        }
        #endregion Power Change

        // give Block to justify
        public bool IsSuper()
        {
            return PowerStates[CurrentActionAndState[2]].Type == PowerType.Super;
        }

        private void ChangeTexture(Texture2D[] newTexture)
        {
            for (int i = 0; i < 4; i++)
                ActionSprites[i].SpriteSheets = newTexture[i];
        }

        private void ChangeActionStateAndSprite(int changeNumber)
        {
            if (changeNumber == 4)
                CurrentActionAndState[0] = 1;
            else
                CurrentActionAndState[0] = changeNumber;
            CurrentActionAndState[1] = changeNumber;
        }

    }
}
