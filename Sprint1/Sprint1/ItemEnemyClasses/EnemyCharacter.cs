
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Sprint1.MarioClasses;
using Sprint1.Sprites;
using System;

namespace Sprint1.ItemClasses
{
   class EnemyCharacter : ICharacter
    {

        public Sprint1Main.CharacterType Type { get; set; } = Sprint1Main.CharacterType.Enemy;
        
        private ISprite liveEnemy;
        private ISprite diedEnemy;
        private ISprite currentSprite;
        private Boolean isDead = false;
        public MoveParameters Parameters { get; }
        public EnemyCharacter(Texture2D[] texture, Point[] rowsAndColumns, MoveParameters moveParameters)
        {
            Parameters = moveParameters;
            Parameters.SetVelocity(0, 0);
            liveEnemy = new AnimatedSprite(texture[0], rowsAndColumns[0], Parameters);
            diedEnemy = new AnimatedSprite(texture[1], rowsAndColumns[1], Parameters);
            currentSprite = liveEnemy;
        }

        public void Update(float timeOfFrame) { currentSprite.Update(timeOfFrame); }
        public void Draw(SpriteBatch spriteBatch)
        {
            currentSprite.Draw(spriteBatch);
        }
        public Vector2 GetMaxPosition()
        {
            return new Vector2(Parameters.Position.X + currentSprite.GetHeightAndWidth().Y, Parameters.Position.Y);
        }
        public Vector2 GetMinPosition()
        {
            return new Vector2(Parameters.Position.X, Parameters.Position.Y - currentSprite.GetHeightAndWidth().X);
        }


        public void MarioCollide(bool specialCase)
        {
            if (specialCase)
            {
                 isDead = true;
                 currentSprite = diedEnemy;
            }
        }
        public Vector2 GetHeightAndWidth()
        {
            return currentSprite.GetHeightAndWidth();
        }
    }
}
