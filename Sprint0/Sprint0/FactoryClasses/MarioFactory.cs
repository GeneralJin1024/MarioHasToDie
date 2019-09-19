using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint0.MarioClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Sprint0.FactoryClasses
{
    public class MarioFactory : AbstractFactory
    {
        private ContentManager Content;

        public MarioFactory(ContentManager content)
        {
            Content = content;
        }
        public override void GetBlock()
        {
        }

        public override void GetEnemy()
        {
        }

        public override void GetItem()
        {
        }

        public override Mario GetMario(Vector2 location)
        {
            Texture2D[] StandardSheets = new Texture2D[5] {Content.Load<Texture2D>("MarioSprites/smallMarioRightStand"),
                Content.Load<Texture2D>("MarioSprites/smallMarioRightJump"),
                Content.Load<Texture2D>("MarioSprites/smallMarioRightMove"),
                Content.Load<Texture2D>("MarioSprites/smallMarioRightStand"), Content.Load<Texture2D>("DiedMario/deadMario")};
            //16*32
            Texture2D[] SuperSheets = new Texture2D[4] {Content.Load<Texture2D>("SuperMario/superMarioRightStand"),
                Content.Load<Texture2D>("SuperMario/superMarioJumpRight"),
                Content.Load<Texture2D>("SuperMario/superMarioMoveRight"),
                Content.Load<Texture2D>("SuperMario/superMarioRightCrouch")};
            //16*32   16*22
            Texture2D[] FireSheets = new Texture2D[4] {Content.Load<Texture2D>("FireMario/fireMarioRightStand"),
                Content.Load<Texture2D>("FireMario/fireMarioJumpRight"),
                Content.Load<Texture2D>("FireMario/fireMarioRightMove"),
                Content.Load<Texture2D>("FireMario/fireMarioRightCrouch")};
            return new Mario(StandardSheets, SuperSheets, FireSheets, location);
        }
    }
}
