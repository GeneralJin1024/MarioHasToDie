using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint0.MarioClasses
{
    class Mario : ISprite
    {
        public Texture2D SpriteSheets { get; set; }//useless variable

        //{Stand, Jump, Walk, Crouch}
        private Texture2D[] StandardMario;
        private Texture2D[] SuperMario;
        private Texture2D[] FireMario;
        private Texture2D DiedSheet;

        private ISprite IdleSprite;
        private ISprite JumpSprite;
        private ISprite CrouchSprite;
        private ISprite WalkingSprite;
        private ISprite DiedSprite;
        private ISprite currentMarioAction;

        private bool IsLeft;

        private Vector2 Location;

        public Mario(Texture2D[] standardSheets, Texture2D[] superSheet, Texture2D[] fireSheet, Texture2D diedSheet)
        {
            StandardMario = standardSheets;
            SuperMario = superSheet;
            FireMario = fireSheet;
            SetActionSprites();
            DiedSprite = new ActionSprite(diedSheet, new Point(1, 1));
            currentMarioAction = IdleSprite;
            IsLeft = false;
            Location = new Vector2(400, 300);
        }
        #region ISprite Methods
        public void Update(GameTime gameTime)
        {
            currentMarioAction.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location, bool isLeft)
        {
            currentMarioAction.Draw(spriteBatch, Location, IsLeft);
        }
        #endregion ISprite Methods

        #region Action Command Receiver Method
        public void moveRight()
        {

        }

        public void moveLeft()
        {

        }

        public void moveUp()
        {

        }

        public void moveDown()
        {

        }
        #endregion Action Command Receiver Method

        private void SetActionSprites()
        {
            IdleSprite = new ActionSprite(StandardMario[0], new Point(1, 1));
            JumpSprite = new ActionSprite(StandardMario[1], new Point(1, 1));
            WalkingSprite = new ActionSprite(StandardMario[2], new Point(1, 3));
            CrouchSprite = new ActionSprite(StandardMario[3], new Point(1, 1));
        }
    }
}
