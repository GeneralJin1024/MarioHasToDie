using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.ComponentModel;
using Sprint1.LevelLoader;

namespace Sprint1.MarioClasses
{
    public class Mario : ISprite
    {
        public static float XVelocity { get; } = 4;
        public static float YVelocity{ get;} = -16; // -16为初始值
        public Texture2D SpriteSheets { get; set; }//useless variable
        public MoveParameters Parameters { get; set; }
        public MarioState MarioState { get; }

        private ISprite CurrentSprite;
        //{Standard, Super, Fire}
        //{Stand, Jump, Walk, Crouch, (Died only in standard sheets[])}
        private readonly Texture2D[][] MarioSpriteSheets;
        //{Idle, Jump, Walking, Crouch}
        private readonly ISprite[] ActionSprites;
        private bool JumpHigher;

        public Mario(Texture2D[][] marioSpriteSheets, Vector2 location)
        {
            MarioState = new MarioState(this);
            //initialize position and velocity, where the MoveParameter itself will not do.
            Parameters = new MoveParameters(true);
            Parameters.SetPosition(location.X, location.Y);
            Parameters.SetVelocity(0, 0);
            JumpHigher = false;
            //store 13 Mario textures
            MarioSpriteSheets = marioSpriteSheets ?? throw new ArgumentNullException(nameof(marioSpriteSheets));
            ActionSprites = new ISprite[6] { new AnimatedSprite(MarioSpriteSheets[0][0], new Point(1, 1), Parameters),
                new AnimatedSprite(MarioSpriteSheets[0][1], new Point(1, 1), Parameters),
                new AnimatedSprite(MarioSpriteSheets[0][2], new Point(1, 3), Parameters),
                new AnimatedSprite(MarioSpriteSheets[0][3], new Point(1, 1), Parameters),
                new AnimatedSprite(MarioSpriteSheets[0][1], new Point(1, 1), Parameters),
                new AnimatedSprite(MarioSpriteSheets[0][4], new Point(1, 1), Parameters)};
            CurrentSprite = ActionSprites[0];
        }
        #region ISprite Methods
        public void Update(float timeOfFrame)
        {
            //CurrentActionAndState[0] will locate the current action sprite.
            // && marioState.GetActionType() != MarioState.ActionType.Jump
            Console.WriteLine("Before Mario Velocity = " + Parameters.Velocity + "Current State = " + MarioState.GetActionType);
            if (JumpHigher)
            {
                Parameters.SetVelocity(Math.Abs(Parameters.Velocity.X), Parameters.Velocity.Y - 0.5f);
                JumpHigher = false;
            }
            if (Parameters.Velocity.Y > 0 && timeOfFrame > 0)
            {
                ChangeToFalling();//change to falling
            }
            CurrentSprite.Update(timeOfFrame);
            if (Parameters.Position.Y == Stage.Boundary.Y)
                MarioState.ChangeToDied();
            //marioState.Return();
            Console.WriteLine("After Mario Velocity = " + Parameters.Velocity + "Current State = " + MarioState.GetActionType);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //CurrentActionAndState[0] will locate the current action sprite.
            CurrentSprite.Draw(spriteBatch);
            //The code below will use when the key should be "hold" instead of "pressed".
            //if (marioState.GetPowerType() != MarioState.PowerType.Died)
            //    ChangeToIdle();
        }

        public Vector2 GetHeightAndWidth()
        {
            //CurrentActionAndState[0] will locate the current action sprite.
            return CurrentSprite.GetHeightAndWidth();
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
            MarioState.MoveRight();
        }

        public void MoveLeft()
        {
            MarioState.MoveLeft();
        }

        public void MoveUp()
        {
            MarioState.MoveUp();
        }

        public void MoveDown()
        {
            MarioState.MoveDown();
        }
        #endregion Action Command Receiver Method

        #region Action Change
        public void ChangeToIdle()
        {
            //change location caused by the difference of size between crouch and idle.
            ChangeActionAndSprite(0);
            Parameters.SetVelocity(0, 0);
        }

        public void ChangeToJump(float yVelocity)
        {
            ChangeActionAndSprite(1);
            Parameters.SetVelocity(Math.Abs(Parameters.Velocity.X), yVelocity);
        }

        public void ChangeToWalk()
        {
            ChangeActionAndSprite(2);
            Parameters.SetVelocity(XVelocity, Parameters.Velocity.Y);
        }

        public void ChangeToCrouch()
        {
            //Do nothing if Mario power is standard
            ChangeActionAndSprite(3);
            Parameters.SetVelocity(0, 0);
        }

        public void ChangeToRunningJump(float yVelocity)
        {
            ChangeActionAndSprite(4);
            Parameters.SetVelocity(5, yVelocity);
        }

        public void ChangeToFalling() { ChangeActionAndSprite(4); }
        #endregion Action Change

        public void Jumphigher() { JumpHigher = true; }
        // give Block to justify
        public bool IsSuper()
        {
            return MarioState.GetPowerType == MarioState.PowerType.Super;
        }

        //change four action sprites' textures with the textures of current power state.
        public void ChangeTexture(int sheetNum)
        {
            for (int i = 0; i < 4; i++)
                ActionSprites[i].SpriteSheets = MarioSpriteSheets[sheetNum][i];
            ActionSprites[4].SpriteSheets = MarioSpriteSheets[sheetNum][1];
        }

        //only change action sprites and action states because they always change together
        public void ChangeActionAndSprite(int changeNumber)
        {
            CurrentSprite = ActionSprites[changeNumber]; // change sprite in mario.
            MarioState.ChangeAction(changeNumber); // change action state in mario state.
        }

    }
}
