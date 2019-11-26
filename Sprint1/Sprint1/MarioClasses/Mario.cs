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
        public static float YVelocity{ get;} = -16; // -16 is initial value
        public Vector2 GetHeightAndWidth { get { return CurrentSprite.GetHeightAndWidth; } }
        public Texture2D SpriteSheets { get; set; }//useless variable
        public MoveParameters Parameters { get; set; }
        public MarioState MarioState { get; }
        public bool ThrowBullet { get; set; }
        public bool JumpTwice { get; set; }
        public bool AutomaticallyMoving { get; private set; }

        private ISprite CurrentSprite;
        //{Standard, Super, Fire}
        //{Stand, Jump, Walk, Crouch, (Died only in standard sheets[])}
        private readonly Texture2D[][] MarioSpriteSheets;
        //{Idle, Jump, Walking, Crouch}
        private readonly ISprite[] ActionSprites;
        private readonly ISprite FlagSprite;
        private bool JumpHigher;
        private bool Dive; //Mario dive into VPipe
        private bool Shoot; //Bump Mario
        private float Top; //If Mario's max Y-Position > Top when Bumping, then the whole Mario has left the pipe.
        private bool DiveRight; //When its true, Mario should has already collide with L Pipe and start entering.
        private float Clock; // The Clock used for Died Animation

        public Mario(Texture2D[][] marioSpriteSheets, Vector2 location)
        {
            MarioState = new MarioState(this);
            //initialize position and velocity, where the MoveParameter itself will not do.
            Parameters = new MoveParameters(true);
            Parameters.SetPosition(location.X, location.Y);
            Parameters.SetVelocity(0, 0);
            JumpHigher = false; Dive = false; AutomaticallyMoving = false; Shoot = false; DiveRight = false;
            //store 13 Mario textures
            MarioSpriteSheets = marioSpriteSheets ?? throw new ArgumentNullException(nameof(marioSpriteSheets));
            ActionSprites = new ISprite[6] { new AnimatedSprite(MarioSpriteSheets[0][0], new Point(1, 1), Parameters),
                new AnimatedSprite(MarioSpriteSheets[0][1], new Point(1, 1), Parameters),
                new AnimatedSprite(MarioSpriteSheets[0][2], new Point(1, 3), Parameters),
                new AnimatedSprite(MarioSpriteSheets[0][3], new Point(1, 1), Parameters),
                new AnimatedSprite(MarioSpriteSheets[0][1], new Point(1, 1), Parameters),
                new AnimatedSprite(MarioSpriteSheets[0][5], new Point(1, 1), Parameters)};
            FlagSprite = new AnimatedSprite(MarioSpriteSheets[0][4], new Point(1, 1), Parameters);
            CurrentSprite = ActionSprites[0];
            Clock = 0;
        }
        #region ISprite Methods
        public void Update(float timeOfFrame)
        {
            if (MarioState.GetPowerType == MarioState.PowerType.Died && !Parameters.HasGravity)
            {
                Clock += timeOfFrame;
                if (Clock >= 20)
                {
                    Parameters.SetVelocity(0, -10); Parameters.HasGravity = true; Clock = 0;
                }
            }
            /*
             * In this Sprint, I cannot make Mario jumpHigher in JumpingState because the system read input per Update, not
             * read each 100ms (the frequency of collision update)
             */
            if (JumpHigher)
            {
                Parameters.SetVelocity(Math.Abs(Parameters.Velocity.X), Parameters.Velocity.Y - 0.5f);
                JumpHigher = false;
            }
            if (Parameters.Velocity.Y > 0 && timeOfFrame > 0 && !Dive && MarioState.GetActionType != MarioState.ActionType.Other)
            {
                ChangeToFalling();//change to falling
            }
            CurrentSprite.Update(timeOfFrame);
            DivingAndShooting();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //CurrentActionAndState[0] will locate the current action sprite.
            CurrentSprite.Draw(spriteBatch);
        }
        #endregion ISprite Methods

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
            Parameters.SetVelocity(Math.Abs(Parameters.Velocity.X), JumpTwice ? yVelocity * 1.5f : yVelocity);
            SoundFactory.Instance.MarioJump();
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

        private void DivingAndShooting()
        {
            if (Dive && (Parameters.Position.Y - GetHeightAndWidth.X >= Top))
            {
                Dive = false; Sprint1Main.Game.Scene.ByPassMario(); Sprint1Main.Game.LevelControl.SceneFlash(false,false,0);
            }
            else if (DiveRight && (Parameters.Position.X >= Top))
            {
                DiveRight = false; Sprint1Main.Game.LevelControl.GoToNormalArea();
            }
            if (Shoot && Parameters.Position.Y < Top)
            {
                Shoot = false;
                MarioState.LockOrUnlock(false);
                Parameters.HasGravity = true;
            }
        }

        public void DiveIn(float top)
        {
            Dive = true;
            Top = top;
            ChangeToIdle();
            MarioState.LockOrUnlock(true);
            Parameters.HasGravity = false;
            Parameters.SetVelocity(0, 1);
            AutomaticallyMoving = true;
        }

        public void DiveInRight(float leftSide, float bottom)
        {
            DiveRight = true;
            Top = leftSide;
            ChangeToWalk();
            MarioState.LockOrUnlock(true);
            Parameters.SetVelocity(1, 0);
            Parameters.HasGravity = false;
            if (Parameters.Position.Y >= bottom)
                Parameters.SetPosition(Parameters.Position.X, bottom - 1);
        }
        public void Bump()
        {
            Shoot = true;
            Parameters.SetVelocity(0, -1);
        }
        public void ChangeToWin()
        {
            CurrentSprite = FlagSprite;
            MarioState.LockOrUnlock(true);
        }
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
            FlagSprite.SpriteSheets = MarioSpriteSheets[sheetNum][4];
        }

        //only change action sprites and action states because they always change together
        public void ChangeActionAndSprite(int changeNumber)
        {
            CurrentSprite = ActionSprites[changeNumber]; // change sprite in mario.
            MarioState.ChangeAction(changeNumber); // change action state in mario state.
        }

    }
}
