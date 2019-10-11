using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint1.MarioClasses
{
    public class MarioCharacter
    {
        private readonly Mario Mario;

        public MoveParameters Parameters { get; }
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
        }
        #region ISprite Methods
        public void Update(float timeOfFrame) { Mario.Update(timeOfFrame); }
        public void Draw(SpriteBatch spriteBatch) { Mario.Draw(spriteBatch); }
        #endregion

        #region Controller Receiver
        public void MoveUp() { Mario.MarioState.MoveUp(); }
        public void MoveDown() { Mario.MarioState.MoveDown(); }
        public void MoveLeft() { Mario.MarioState.MoveLeft(); }
        public void MoveRight() { Mario.MarioState.MoveRight(); }
        public void MoveStandard() { Mario.MarioState.ChangeToStandard(); }
        public void MoveSuper() { Mario.MarioState.ChangeToSuper(); }
        public void MoveFire() { Mario.MarioState.ChangeToFire(); }
        public void MoveDestroy() { Mario.MarioState.Destroy(); }
        #endregion

        #region Collide Detection Receivers
        public void CollideWithEnemy(bool isTop)
        {
            if (!isTop)
                Mario.MarioState.Destroy();
            if (Mario.MarioState.GetPowerType() != MarioState.PowerType.Died)
                Mario.ChangeToIdle();
        }
        public void CollideWithFlower()
        {
            if (Mario.MarioState.GetPowerType() == MarioState.PowerType.Standard)
                Mario.MarioState.ChangeToSuper();
            else
                Mario.MarioState.ChangeToFire();
        }
        public void CollideWithMushRoom() { Mario.MarioState.ChangeToSuper(); }
        public void CollideWithBlock() { Mario.ChangeToIdle(); }
        //public void CollideWithBlocksTopAndBottom() { Mario.ChangeToIdle(); }
        //public void CollideWithBlocksLeftAndRight() { Mario.Parameters.SetVelocity(0, Mario.Parameters.Velocity.Y); }
        #endregion

        public Vector2 GetMinPosition() { return new Vector2(Parameters.Position.X, Parameters.Position.Y - Mario.GetHeightAndWidth().X); }
        public Vector2 GetMaxPosition() { return new Vector2(Parameters.Position.X + Mario.GetHeightAndWidth().Y, Parameters.Position.Y); }
        public Vector2 GetHeightAndWidth() { return Mario.GetHeightAndWidth(); }

    }
}
