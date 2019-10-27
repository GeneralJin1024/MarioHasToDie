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
        public MarioState.ActionType GetAction { get { return Mario.MarioState.GetActionType; } }
        public MarioState.PowerType GetPower { get { return Mario.MarioState.GetPowerType; } }
        public MoveParameters Parameters { get; }
        private readonly MoveParameters InitialParameters;
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
        }
        #region ISprite Methods
        public void Update(float timeOfFrame)
        {
            Mario.Update(timeOfFrame);
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
        public void ThrowFire()
        {
            if (Mario.MarioState.IsFireMario())
            {
                float distance = Parameters.IsLeft ? 0 : GetHeightAndWidth().Y;
                Vector2 location = new Vector2(Parameters.Position.X + distance, Parameters.Position.Y - GetHeightAndWidth().Y / 2);
                ICharacter fireBall = ItemFactory.Instance.AddNewCharacter("FireBall+{1}", location);
                fireBall.Parameters.IsHidden = false;
                fireBall.Parameters.IsLeft = Parameters.IsLeft;
                fireBall.Parameters.SetVelocity(20, -5);
            }
        }
        #endregion
        #region Collide Detection Receivers
        public void CollideWithEnemy(bool isTop)
        {
            if (!isTop)
                Mario.MarioState.Destroy();
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
                else
                    Mario.ChangeToIdle();
            }
            else
            {
                if (!hitBottomOrTop) // hit left or right side
                    Parameters.SetVelocity(0, Parameters.Velocity.Y);
                else if (Parameters.Velocity.Y < 0) //stand on the block
                {
                    Mario.ChangeToFalling();
                    Parameters.SetVelocity(Math.Abs(Parameters.Velocity.X), -Parameters.Velocity.Y);
                }
            }
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
        public void BlockCollide(bool isBottom) { } //combine with CollideWithBlock in next Sprint

    }
}
