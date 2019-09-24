using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint1.MarioClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Collections;

namespace Sprint1.FactoryClasses
{
    public class MarioFactory : IFactory
    {
        public Mario Mario { get; set; }
        private static MarioFactory _instance;
        public static MarioFactory Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MarioFactory();
                return _instance;
            }
        }

        public MarioFactory()
        {
            //Create Mario as soon as the factory is initialized.
            Mario = GetMario(new Vector2(400, 300));
        }

        public void AddToList(ArrayList spriteList)
        {
            //check null first
            if (spriteList == null)
            {
                throw new ArgumentNullException(nameof(spriteList));
            }
            spriteList.Add(Mario);
        }


        private static Mario GetMario(Vector2 location)
        {
            Texture2D[] StandardSheets = new Texture2D[5] {Sprint1Main.Game.Content.Load<Texture2D>("MarioSprites/smallMarioRightStand"),
                Sprint1Main.Game.Content.Load<Texture2D>("MarioSprites/smallMarioRightJump"),
                Sprint1Main.Game.Content.Load<Texture2D>("MarioSprites/smallMarioRightMove"),
                Sprint1Main.Game.Content.Load<Texture2D>("MarioSprites/smallMarioRightStand"),
                Sprint1Main.Game.Content.Load<Texture2D>("DiedMario/deadMario")};
            //16*32
            Texture2D[] SuperSheets = new Texture2D[4] {Sprint1Main.Game.Content.Load<Texture2D>("SuperMario/superMarioRightStand"),
                Sprint1Main.Game.Content.Load<Texture2D>("SuperMario/superMarioJumpRight"),
                Sprint1Main.Game.Content.Load<Texture2D>("SuperMario/superMarioMoveRight"),
                Sprint1Main.Game.Content.Load<Texture2D>("SuperMario/superMarioRightCrouch")};
            //16*32   16*22
            Texture2D[] FireSheets = new Texture2D[4] {Sprint1Main.Game.Content.Load<Texture2D>("FireMario/fireMarioRightStand"),
                Sprint1Main.Game.Content.Load<Texture2D>("FireMario/fireMarioJumpRight"),
                Sprint1Main.Game.Content.Load<Texture2D>("FireMario/fireMarioRightMove"),
                Sprint1Main.Game.Content.Load<Texture2D>("FireMario/fireMarioRightCrouch")};
            return new Mario(StandardSheets, SuperSheets, FireSheets, location);
        }
    }
}
