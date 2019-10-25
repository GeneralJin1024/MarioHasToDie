using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint1.FactoryClasses;

namespace Sprint1.MarioClasses
{
    public class MarioCharacter : ICharacter
    {
        private readonly Mario Mario;
        public Sprint1Main.CharacterType Type { get; set; } = Sprint1Main.CharacterType.Mario;
        private bool hasCollision;
        public MoveParameters Parameters { get; }
        private MoveParameters InitialParameters;
        //private static MarioState.PowerType PowerType = MarioState.PowerType.Standard;
        public bool IsSuper {
            get
            {
                return Mario.IsSuper();
            }
        }

        public MarioCharacter(Texture2D[][] marioSpriteSheets, Vector2 location)
        {
            Mario = new Mario(marioSpriteSheets, location);
            //if (Sprint1Main.Game.Mario != null)
            //    Scene.CopyDataOfParameter(Sprint1Main.Game.Mario.Parameters, Mario.Parameters);
            Parameters = Mario.Parameters;
            InitialParameters = new MoveParameters(false);
            Scene.CopyDataOfParameter(Parameters, InitialParameters);
            hasCollision = false;
        }
        #region ISprite Methods
        public void Update(float timeOfFrame)
        {
            Mario.Update(timeOfFrame); hasCollision = false;
        }
        public void Draw(SpriteBatch spriteBatch) { Mario.Draw(spriteBatch); }
        #endregion

        #region Controller Receiver
        // response to controllers.
        public void MoveUp() { Mario.MarioState.MoveUp(); }
        public void MoveDown() { Mario.MarioState.MoveDown(); }
        public void MoveLeft() { Mario.MarioState.MoveLeft(); }
        public void MoveRight() { Mario.MarioState.MoveRight(); }
        public void MoveStandard() { Mario.MarioState.ChangeToStandard(); }
        public void MoveSuper() { Mario.MarioState.ChangeToSuper(); }
        public void MoveFire() { Mario.MarioState.ChangeToFire(); }
        public void MoveDestroy() { Mario.MarioState.Destroy(); }
        public void Return() { Mario.MarioState.Return(); }
        #endregion
        public void ThrowFire()
        {
            if (Mario.MarioState.IsFireMario())
            {
                float distance = Parameters.IsLeft ? 0 : GetHeightAndWidth().Y;
                Vector2 location = new Vector2(Parameters.Position.X + distance, Parameters.Position.Y - GetHeightAndWidth().Y / 2);
                ICharacter fireBall = ItemFactory.Instance.AddNewCharacter("FireBall", location);
                fireBall.Parameters.IsLeft = Parameters.IsLeft;
                fireBall.Parameters.SetVelocity(20, -5);
            }
        }

        #region Collide Detection Receivers
        public void CollideWithEnemy(bool isTop)
        {
            if (!isTop)
                Mario.MarioState.Destroy();
            if (Mario.MarioState.GetPowerType != MarioState.PowerType.Died)
                Mario.ChangeToIdle();
            hasCollision = true;
        }
        public void CollideWithFlower()
        {
            if (Mario.MarioState.GetPowerType == MarioState.PowerType.Standard)
                Mario.MarioState.ChangeToSuper();
            else
                Mario.MarioState.ChangeToFire();
            hasCollision = true;
        }
        public void CollideWithRedMushRoom() { Mario.MarioState.ChangeToSuper(); hasCollision = true; }
        public void CollideWithBlock(bool hitBottom, bool movingUp)
        {
            //Console.WriteLine("Collide1 : hitBottom = " + hitBottom + "    hitLeftOrRight = " + hitLeftOrRight);
            if (hitBottom && movingUp)
            {
                //Console.WriteLine("Hit bottom, Action = " + Mario.marioState.GetActionType());
                //Console.WriteLine("Mario Velocity1 = " + Parameters.Velocity);
                if (Mario.MarioState.GetActionType == MarioState.ActionType.Crouch)
                    Mario.Parameters.SetVelocity(0, 0); //Mario.ChangeToCrouch();
                else if (Mario.MarioState.GetActionType == MarioState.ActionType.Walk)
                {
                    Mario.Parameters.SetVelocity(Mario.XVelocity, 0);
                }
                else
                    Mario.ChangeToIdle();
                //Console.WriteLine("Mario Velocity2 = " + Parameters.Velocity);
            }
            else
            {
                if (!hitBottom)
                    Parameters.SetVelocity(0, Parameters.Velocity.Y);
                else if (Parameters.Velocity.Y < 0)
                {
                    Mario.ChangeToFalling();
                    Parameters.SetVelocity(Math.Abs(Parameters.Velocity.X), -Parameters.Velocity.Y);
                }
            }
            hasCollision = true;
        }

        public void CollideWith(ICharacter character, bool UpOrDown, bool movingDown)
        {
            if (character is null)
                throw new ArgumentNullException(nameof(character));
            if (character.Type == Sprint1Main.CharacterType.Block || character.Type == Sprint1Main.CharacterType.Pipe ||
                character.Type == Sprint1Main.CharacterType.DiedEnemy)
                CollideWithBlock(UpOrDown, Parameters.Velocity.Y > 0);
            else if (character.Type == Sprint1Main.CharacterType.RedMushroom)
                CollideWithRedMushRoom();
            else if (character.Type == Sprint1Main.CharacterType.Flower)
                CollideWithFlower();
            else if (character.Type == Sprint1Main.CharacterType.Enemy)
                CollideWithEnemy(UpOrDown && movingDown);


        }

        /*
         * Since mario didn't do any thing when collide with star and coin in this Sprint, I didn't add corresponding methods
         * In this Sprint, mario hit pipe doing the same thing as hitting a block.
         */
        #endregion
        //get left up coner position.
        public Vector2 GetMinPosition() { return new Vector2(Parameters.Position.X, Parameters.Position.Y - Mario.GetHeightAndWidth().X); }
        //get right down coner position.
        public Vector2 GetMaxPosition() { return new Vector2(Parameters.Position.X + Mario.GetHeightAndWidth().Y, Parameters.Position.Y); }
        public Vector2 GetHeightAndWidth() { return Mario.GetHeightAndWidth(); } //get mario's hit and width.
        public bool IsDied() { return Mario.MarioState.GetPowerType == MarioState.PowerType.Died; }
        public bool IsFire() { return Mario.MarioState.IsFireMario(); }
        public MarioState.ActionType GetAction() { return Mario.MarioState.GetActionType; }
        public MarioState.PowerType GetPower() { return Mario.MarioState.GetPowerType; }
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
        public void MarioCollide(bool special) { }
        public void BlockCollide(bool isBottom) { }

    }
}
