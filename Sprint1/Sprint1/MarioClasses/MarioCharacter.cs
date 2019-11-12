using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint1.FactoryClasses;
using Sprint1.ItemClasses;
using Sprint1.ItemEnemyClasses;
using Sprint1.LevelLoader;

namespace Sprint1.MarioClasses
{
    public class MarioCharacter : ICharacter
    {
        private readonly Mario Mario;
        public Sprint1Main.CharacterType Type { get; set; } = Sprint1Main.CharacterType.Mario;
        public MarioState.ActionType GetAction { get { return Mario.MarioState.GetActionType; } }
        public MarioState.PowerType GetPower { get { return Mario.MarioState.GetPowerType; } }
        public bool Auto { get { return Mario.AutomaticallyMoving; } }
        public MoveParameters Parameters { get; }
        public Vector2 GetMinPosition
        {
            get
            {
                return new Vector2(Parameters.Position.X, Parameters.Position.Y - Mario.GetHeightAndWidth.X);
            }
        }
        public Vector2 GetMaxPosition
        {
            get { return new Vector2(Parameters.Position.X + Mario.GetHeightAndWidth.Y, Parameters.Position.Y); }
        }
        public Vector2 GetHeightAndWidth
        {
            get { return Mario.GetHeightAndWidth; }
        }
        private readonly MoveParameters InitialParameters;
        public bool Win { get; set; } //马里奥进入控制锁定但仍进行碰撞的状态
        public bool Invincible { get; set; }
        private float Clock;
        public ArrayList DivedPipe { get; private set; }
        public bool IsSuper {
            get
            {
                return Mario.IsSuper();
            }
        }

        public MarioCharacter(Texture2D[][] marioSpriteSheets, Vector2 location)
        {
            Mario = new Mario(marioSpriteSheets, location);
            Parameters = Mario.Parameters;
            InitialParameters = new MoveParameters(false);
            Scene.CopyDataOfParameter(Parameters, InitialParameters);
            Win = false;
            DivedPipe = new ArrayList();
        }
        #region ISprite Methods
        public void Update(float timeOfFrame)
        {
            Mario.Update(timeOfFrame);
            if (Invincible)
            {
                Clock -= timeOfFrame;
                Invincible = Clock >= 0;
                if (!Invincible)
                    Parameters.ChangeColor = false;
            }
            if (Parameters.Position.Y == Stage.Boundary.Y)
            {
                if (!IsDied()) //为死亡：悬崖坠落等
                    Mario.MarioState.ChangeToDied();
                else //死亡：即将脱离屏幕
                    Sprint1Main.Game.LevelControl.MarioDied();
            }
        }
        public void Draw(SpriteBatch spriteBatch) { Mario.Draw(spriteBatch); }
        #endregion

        #region Controller Receiver
        // response to controllers.
        public void MoveUp() { Mario.MarioState.MoveUp(); }
        public void JumpHigher() {
            if (GetAction == MarioState.ActionType.Jump)
                Mario.MarioState.MoveUp();
        }
        public void MoveDown() { Mario.MarioState.MoveDown(); }
        public void MoveLeft() { Mario.MarioState.MoveLeft(); }
        public void MoveRight() { Mario.MarioState.MoveRight(); }
        public void MoveStandard() { Mario.MarioState.ChangeToStandard(); }
        public void MoveSuper() { Mario.MarioState.ChangeToSuper(); }
        public void MoveFire() { Mario.MarioState.ChangeToFire(); }
        public void MoveDestroy() { Mario.MarioState.Destroy(); }
        public void Return() { Mario.MarioState.Return(); }
        public void ThrowFire()
        {
            if (Mario.MarioState.IsFireMario())
            {
                float distance = Parameters.IsLeft ? 0 : GetHeightAndWidth.Y;
                Vector2 location = new Vector2(Parameters.Position.X + distance, Parameters.Position.Y - GetHeightAndWidth.Y / 2);
                ICharacter fireBall = ItemFactory.Instance.AddNewCharacter("FireBall+{1}", location);
                fireBall.Parameters.IsHidden = false;
                fireBall.Parameters.IsLeft = Parameters.IsLeft;
                fireBall.Parameters.SetVelocity(20, -5);
            }
        }
        #endregion
        #region Collide Detection Receivers
        public void CollideWithEnemy(bool isTop, bool isFlowerEnemy)
        {
            if (!isTop || isFlowerEnemy)
            {
                Mario.MarioState.Destroy();
                Invincible = true; Clock = 15; Parameters.ChangeColor = true;
            }
            if (Mario.MarioState.GetPowerType != MarioState.PowerType.Died)
                Mario.ChangeToIdle();
        }
        public void CollideWithFlower()
        {
            if (Mario.MarioState.GetPowerType == MarioState.PowerType.Standard)
                Mario.MarioState.ChangeToSuper();
            else
                Mario.MarioState.ChangeToFire();
        }
        public void CollideWithRedMushRoom() { Mario.MarioState.ChangeToSuper(); }
        public void CollideWithBlock(bool hitBottomOrTop, bool movingUp)
        {
            //Console.WriteLine("Collide1 : hitBottom = " + hitBottom + "    hitLeftOrRight = " + hitLeftOrRight);
            if (hitBottomOrTop && movingUp) // hit block's bottom
            {
                if (Mario.MarioState.GetActionType == MarioState.ActionType.Crouch)
                    Mario.Parameters.SetVelocity(0, 0);
                else if (Mario.MarioState.GetActionType == MarioState.ActionType.Walk)
                {
                    Mario.Parameters.SetVelocity(Mario.XVelocity, 0);
                }
                else if (GetAction == MarioState.ActionType.Other && Win)
                {
                    Mario.ChangeToWalk();
                    Mario.MarioState.LockOrUnlock(true);
                    Parameters.SetVelocity(4, 0);
                }
                else
                    Mario.ChangeToIdle();
            }
            else
            {
                if (!hitBottomOrTop)// hit left or right side
                {
                    Parameters.SetVelocity(0, Parameters.Velocity.Y);
                    float x = Parameters.IsLeft ? 2 : -2;
                    //Console.WriteLine("Mario Position before mdification = " + GetMaxPosition());
                    Parameters.SetPosition(Parameters.Position.X + x, Parameters.Position.Y);
                }
                else if (Parameters.Velocity.Y < 0) //stand on the block
                {
                    Mario.ChangeToFalling();
                    Parameters.SetPosition(Parameters.Position.X, Parameters.Position.Y + 1);
                    Parameters.SetVelocity(Math.Abs(Parameters.Velocity.X), -Parameters.Velocity.Y);
                }
            }
        }

        public void CollideWithPipe(PipeCharacter pipe, bool upOrDown, bool movingDown)
        {
            switch (pipe?.PType)
            {
                case PipeCharacter.PipeType.Pipe: CollideWithBlock(upOrDown, movingDown); break;
                case PipeCharacter.PipeType.VPipe:
                    if (upOrDown && movingDown && 
                        Parameters.Position.X >= pipe.GetMinPosition.X + 2 && GetMaxPosition.X <= pipe.GetMaxPosition.X - 2)
                    {
                        Mario.DiveIn(pipe.GetMinPosition.Y); pipe.MarioGetInside();
                        DivedPipe.Add(pipe.GetMinPosition.X);
                    }
                    else
                        CollideWithBlock(upOrDown, movingDown);
                    break;
                case PipeCharacter.PipeType.HPipe:
                    if (!upOrDown && Parameters.Velocity.X > 0 && GetMinPosition.Y >= pipe.GetMinPosition.Y &&
                        GetMaxPosition.Y <= pipe.GetMaxPosition.X)
                        Mario.DiveInRight(pipe.GetMinPosition.X, pipe.GetMaxPosition.Y);
                    else CollideWithBlock(upOrDown, movingDown);
                    break;
                default: break;
            }
        }

        public void CollideWithFlag(ICharacter flag)
        {
            if (flag is null)
                throw new ArgumentNullException(nameof(flag));
            //Console.WriteLine("Flag will be touched, Mario Position is = " + Parameters.Position);
            Mario.ChangeToWin();
            Win = true;
            Parameters.SetVelocity(0, 0);
            Parameters.SetPosition(flag.GetMinPosition.X, Parameters.Position.Y);
            float bonusHeight = flag.GetMaxPosition.Y - Parameters.Position.Y;
            if (bonusHeight >= 128)
                Sprint1Main.Point += 4000;
            else if (bonusHeight >= 82)
                Sprint1Main.Point += 2000;
            else if (bonusHeight >= 58)
                Sprint1Main.Point += 800;
            else if (bonusHeight >= 18)
                Sprint1Main.Point += 400;
            else
                Sprint1Main.Point += 100;
            if (GetMinPosition.Y <= flag.GetMinPosition.Y)
                Sprint1Main.MarioLife++;
            Sprint1Main.Game.LevelControl.AddTimeBonus();
            //Console.WriteLine("Flag is touched, Mario Position is = " + Parameters.Position);
        }

        public void CollideWith(ICharacter character, bool UpOrDown, bool movingDown)
        {
            if (character is null)
                throw new ArgumentNullException(nameof(character));
            if (character.Type == Sprint1Main.CharacterType.Block ||
                character.Type == Sprint1Main.CharacterType.DiedEnemy)
                CollideWithBlock(UpOrDown, movingDown);
            else if (character.Type == Sprint1Main.CharacterType.Pipe && character is PipeCharacter)
                CollideWithPipe((PipeCharacter)character, UpOrDown, movingDown);
            else if (character.Type == Sprint1Main.CharacterType.RedMushroom)
                CollideWithRedMushRoom();
            else if (character.Type == Sprint1Main.CharacterType.Flower)
                CollideWithFlower();
            else if (character.Type == Sprint1Main.CharacterType.Enemy)
                CollideWithEnemy(UpOrDown && movingDown, character is PlantEnemyCharacter);
            else if (character.Type == Sprint1Main.CharacterType.Flag)
                CollideWithFlag(character);
            else if(character.Type == Sprint1Main.CharacterType.Star)
            {
                Invincible = true; Clock = 100; Parameters.ChangeColor = true;
            }
            else if (character.Type == Sprint1Main.CharacterType.Castle)
            {
                Parameters.IsHidden = true;
                Sprint1Main.Game.LevelControl.ChangeToWinMode();
            }


        }

        /*
         * Since mario didn't do any thing when collide with star and coin in this Sprint, I didn't add corresponding methods
         * In this Sprint, mario hit pipe doing the same thing as hitting a block.
         */
        #endregion
        //get left up coner position.
        //public Vector2 GetMinPosition() { return new Vector2(Parameters.Position.X, Parameters.Position.Y - Mario.GetHeightAndWidth.X); }
        //get right down coner position.
        //public Vector2 GetMaxPosition() { return new Vector2(Parameters.Position.X + Mario.GetHeightAndWidth.Y, Parameters.Position.Y); }
        //public Vector2 GetHeightAndWidth() { return Mario.GetHeightAndWidth; } //get mario's hit and width.
        public bool IsDied() { return Mario.MarioState.GetPowerType == MarioState.PowerType.Died; }
        public bool IsFire() { return Mario.MarioState.IsFireMario(); }
        public void RestoreStates(MarioState.ActionType actionType, MarioState.PowerType powerType, bool isFire)
        {
            switch (actionType)
            {
                case MarioState.ActionType.Crouch: Mario.ChangeToCrouch(); break;
                case MarioState.ActionType.Fall: Mario.ChangeToFalling(); break;
                case MarioState.ActionType.Idle: Mario.ChangeToIdle();break;
                case MarioState.ActionType.Jump: Mario.ChangeToJump(Parameters.Velocity.Y);break;
                case MarioState.ActionType.Walk: Mario.ChangeToWalk();break;
                case MarioState.ActionType.Other: Mario.MarioState.ChangeToDied();break;
                default: break;
            }
            switch (powerType)
            {
                case MarioState.PowerType.Died: Mario.MarioState.ChangeToDied();break;
                case MarioState.PowerType.Super:
                    if (isFire)
                        Mario.MarioState.ChangeToFire();
                    else
                        Mario.MarioState.ChangeToSuper();
                    break;
                default: break; //Mario default powerType is Standard.
            }
            
        }

        public void LockOrUnLock(bool lockOrUnlock)
        {
            Mario.MarioState.LockOrUnlock(lockOrUnlock);
        }
        public void Bump() { Mario.Bump(); }
        public void Suicide() { Mario.MarioState.ChangeToDied(); }
        public void MarioCollide(bool special) { }
        public void BlockCollide(bool isBottom) { } //combine with CollideWithBlock in next Sprint

    }
}
