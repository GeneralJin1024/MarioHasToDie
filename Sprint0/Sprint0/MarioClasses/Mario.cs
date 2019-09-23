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

        //Used to change to Idle when crouch super Mario want to change to standard Mario
        public enum ActionType
        {
            [Description("Crouch")]
            Crouch,
            [Description("Other")]
            Other
        }

        //In Super, the brick block can bump.
        //In Died, reject all action command.
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
        //{Standard, Super, Fire}
        //{Stand, Jump, Walk, Crouch, (Died only in standard sheets[])}
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

        //{current ActionSprite, current ActionState, current PowerState}
        private readonly int[] CurrentActionAndState;

        //if true, then mario will face to left. Default is right.
        public bool IsLeft { get; set; }

        //the property and field below work together to both let other classes access Mario's position
        //and let Mario class modify Positions without assigning it a new Vector2.
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
            //check null of three texture arrays
            if (standardSheets == null || superSheet == null || fireSheet == null)
            {
                throw new ArgumentNullException(nameof(standardSheets));
            }
            //store 13 Mario textures
            MarioSpriteSheets = new Texture2D[3][] { standardSheets, superSheet, fireSheet };
            ActionSprites = new ISprite[5] { new AnimatedSprite(MarioSpriteSheets[0][0], new Point(1, 1)),
                new AnimatedSprite(MarioSpriteSheets[0][1], new Point(1, 1)),
                new AnimatedSprite(MarioSpriteSheets[0][2], new Point(1, 3)),
                new AnimatedSprite(MarioSpriteSheets[0][3], new Point(1, 1)),
                new AnimatedSprite(MarioSpriteSheets[0][4], new Point(1, 1))};
            //Initialize 5 action states and 4 power states.
            ActionStates = new IActionState[5] { new IdleState(), new JumpState(), new WalkState(),
                new CrouchState(), new RunningJumpState() };
            PowerStates = new IPowerState[4] { new StandardState(), new SuperState(), new FireState(),
                new DiedState() };
            //Set default state to IdleSpite, Idle action state and standard power state.
            CurrentActionAndState = new int[3] { 0, 0, 0 };
            //Point to right
            IsLeft = false;
            //initial location determined by factory
            Location = location;
        }
        #region ISprite Methods
        public void Update(GameTime gameTime)
        {
            //CurrentActionAndState[0] will locate the current action sprite.
            ActionSprites[CurrentActionAndState[0]].Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location, bool isLeft)
        {
            //CurrentActionAndState[0] will locate the current action sprite.
            ActionSprites[CurrentActionAndState[0]].Draw(spriteBatch, Location, IsLeft);
        }

        public Vector2 GetHeightAndWidth()
        {
            //CurrentActionAndState[0] will locate the current action sprite.
            return ActionSprites[CurrentActionAndState[0]].GetHeightAndWidth();
        }
        #endregion ISprite Methods

        #region Action Command Receiver Method
        /*
         * All four receivers should check if Mario is Died. If yes, then reject all command.
         * 
         * CurrentActionAndState[1] locate the current ActionState
         * CurrentActionAndState[2] locate the current power state.
         */
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
                ActionStates[CurrentActionAndState[1]].Down(this);
        }
        #endregion Action Command Receiver Method

        #region Action Change
        public void ChangeToIdle()
        {
            //change location caused by the difference of size between crouch and idle.
            if (PowerStates[CurrentActionAndState[2]].Type == PowerType.Super &&
                ActionStates[CurrentActionAndState[1]].Type == ActionType.Crouch)
                Location.Y -= 10; // The difference of height between standing and crouch.
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
            //Do nothing if Mario power is standard
            if (PowerStates[CurrentActionAndState[2]].Type == PowerType.Super)
            {
                Location.Y += 10; //The difference of height between standing and crouch.
                ChangeActionStateAndSprite(3);
            }
        }

        public void ChangeToRunningJump()
        {
            ChangeActionStateAndSprite(4); //Move down will change to running
        }
        #endregion Action Change

        #region Power Command Receiver Method
        // No matter what current power state is, the first three command do 
        //the same thing -- change to target power state.
        public void MoveStandard() { ChangeToStandard(); }
        public void MoveSuper() { ChangeToSuper(); }
        public void MoveFire() { ChangeToFire(); }
        //This command action depend on current power state.
        public void MoveDestroy() { PowerStates[CurrentActionAndState[2]].Destroy(this); }
        #endregion Power Command Receiver Method

        #region Power Change
        //For all four methods below, the first thing to do is change the texture to 
        //current power state.
        public void ChangeToSuper()
        {
            ChangeTexture(MarioSpriteSheets[1]);
            // Do the actions that must do when leave this power state.
            PowerStates[CurrentActionAndState[2]].Leave(this, CurrentActionAndState);
            //change the location caused by the difference of sheet size between Standard and Super
            if (PowerStates[CurrentActionAndState[2]].Type != PowerType.Super)
            {
                Location.Y -= 16;
            }
            CurrentActionAndState[2] = 1;
        }
        public void ChangeToStandard()
        {
            //Super/Fire Crouch -> Standard Idle
            if (ActionStates[CurrentActionAndState[1]].Type == ActionType.Crouch)
                ChangeToIdle();

            ChangeTexture(MarioSpriteSheets[0]);
            // Do the actions that must do when leave this power state.
            PowerStates[CurrentActionAndState[2]].Leave(this, CurrentActionAndState);
            //change the location caused by the difference of sheet size between Standard and Super
            if (PowerStates[CurrentActionAndState[2]].Type == PowerType.Super)
            {
                Location.Y += 16;
            }
            CurrentActionAndState[2] = 0;
        }
        public void ChangeToFire()
        {
            ChangeTexture(MarioSpriteSheets[2]);
            // Do the actions that must do when leave this power state.
            PowerStates[CurrentActionAndState[2]].Leave(this, CurrentActionAndState);
            //change the location caused by the difference of sheet size between Standard and Super
            if (PowerStates[CurrentActionAndState[2]].Type != PowerType.Super)
            {
                Location.Y -= 16;
            }
            CurrentActionAndState[2] = 2;
        }
        public void ChangeToDied()
        {
            CurrentActionAndState[0] = 4;
            CurrentActionAndState[2] = 3;
        }
        #endregion Power Change

        // give Block to justify
        public bool IsSuper()
        {
            return PowerStates[CurrentActionAndState[2]].Type == PowerType.Super;
        }

        //change four action sprites' textures with the textures of current power state.
        private void ChangeTexture(Texture2D[] newTexture)
        {
            for (int i = 0; i < 4; i++)
                ActionSprites[i].SpriteSheets = newTexture[i];
        }

        //only change action sprites and action states because they always change together
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
