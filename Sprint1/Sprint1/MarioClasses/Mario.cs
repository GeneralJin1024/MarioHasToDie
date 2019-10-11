using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.ComponentModel;

namespace Sprint1.MarioClasses
{
    public class Mario : ISprite
    {
        public Texture2D SpriteSheets { get; set; }//useless variable
        public MoveParameters Parameters { get; set; }
        public MarioState MarioState { get; }

        private ISprite CurrentSprite;
        //{Standard, Super, Fire}
        //{Stand, Jump, Walk, Crouch, (Died only in standard sheets[])}
        private readonly Texture2D[][] MarioSpriteSheets;
        //{Idle, Jump, Walking, Crouch}
        private readonly ISprite[] ActionSprites;

        public Mario(Texture2D[][] marioSpriteSheets, Vector2 location)
        {
            MarioState = new MarioState(this);
            Parameters = new MoveParameters();
            Parameters.SetPosition(location.X, location.Y);
            Parameters.SetVelocity(0, 0);
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
            Console.WriteLine("time = " + timeOfFrame + "    Mario.V = " + Parameters.Velocity);
            CurrentSprite.Update(timeOfFrame);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //CurrentActionAndState[0] will locate the current action sprite.

            CurrentSprite.Draw(spriteBatch);
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
            Parameters.SetVelocity(0, yVelocity);
        }

        public void ChangeToWalk()
        {
            ChangeActionAndSprite(2);
            Parameters.SetVelocity(5, 0);
        }

        public void ChangeToCrouch()
        {
            //Do nothing if Mario power is standard
            ChangeActionAndSprite(3);
            Parameters.SetVelocity(0, 5);
        }

        public void ChangeToRunningJump(float yVelocity)
        {
            ChangeActionAndSprite(4);
            Parameters.SetVelocity(5, yVelocity);
        }
        #endregion Action Change

        #region Power Command Receiver Method
        // No matter what current power state is, the first three command do 
        //the same thing -- change to target power state.
        public void MoveStandard() { MarioState.ChangeToStandard(); }
        public void MoveSuper() { MarioState.ChangeToSuper(); }
        public void MoveFire() { MarioState.ChangeToFire(); }
        public void MoveDestroy() { MarioState.Destroy(); }
        #endregion Power Command Receiver Method

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
            CurrentSprite = ActionSprites[changeNumber];
            MarioState.ChangeAction(changeNumber);
        }

    }
}
