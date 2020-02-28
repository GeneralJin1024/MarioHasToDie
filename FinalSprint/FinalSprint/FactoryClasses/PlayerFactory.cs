using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalSprint.MarioClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Collections;

namespace FinalSprint.FactoryClasses
{
    public class PlayerFactory : IFactory
    {
        private static PlayerFactory _instance;
        public static PlayerFactory Instance // only useless for mario in this Sprint
        {
            get
            {
                if (_instance == null)
                    _instance = new PlayerFactory();
                return _instance;
            }
        }

        public static MarioCharacter FactoryMethod2(String name, Vector2 posS)
        {
            // Generating one mario at a time
            switch (name)
            {
                case "Mario": return GetMario(posS);
                default: return GetMario(new Vector2(0,0)); //trying to return nullCharacter but much works to do
            }
        }

        private static MarioCharacter GetMario(Vector2 location)
        {
            Texture2D[] StandardSheets = new Texture2D[6] {Sprint5Main.Game.Content.Load<Texture2D>("MarioSprites/smallMarioRightStand"),
                Sprint5Main.Game.Content.Load<Texture2D>("MarioSprites/smallMarioRightJump"),
                Sprint5Main.Game.Content.Load<Texture2D>("MarioSprites/smallMarioRightMove"),
                Sprint5Main.Game.Content.Load<Texture2D>("MarioSprites/smallMarioRightStand"),
                Sprint5Main.Game.Content.Load<Texture2D>("MarioSprites/smallMarioRightStand"),
                Sprint5Main.Game.Content.Load<Texture2D>("DiedMario/deadMario"),};
            //16*32
            Texture2D[] SuperSheets = new Texture2D[5] {Sprint5Main.Game.Content.Load<Texture2D>("SuperMario/superMarioRightStand"),
                Sprint5Main.Game.Content.Load<Texture2D>("SuperMario/superMarioJumpRight"),
                Sprint5Main.Game.Content.Load<Texture2D>("SuperMario/superMarioMoveRight"),
                Sprint5Main.Game.Content.Load<Texture2D>("SuperMario/superMarioRightCrouch"),
                Sprint5Main.Game.Content.Load<Texture2D>("SuperMario/superMarioRightStand")};
            //16*32   16*22 for crouch
            Texture2D[] FireSheets = new Texture2D[5] {Sprint5Main.Game.Content.Load<Texture2D>("FireMario/fireMarioRightStand"),
                Sprint5Main.Game.Content.Load<Texture2D>("FireMario/fireMarioJumpRight"),
                Sprint5Main.Game.Content.Load<Texture2D>("FireMario/fireMarioRightMove"),
                Sprint5Main.Game.Content.Load<Texture2D>("FireMario/fireMarioRightCrouch"),
                Sprint5Main.Game.Content.Load<Texture2D>("FireMario/fireMarioRightStand")};
            return new MarioCharacter(new Texture2D[][] { StandardSheets, SuperSheets, FireSheets}, location);
        }
        #region UselessMethod
        public ArrayList FactoryMethod(string name, Vector2 posS, Vector2 posE)
        {
            return new ArrayList();
        }

        public ArrayList FactoryMethod(string name, Vector2 pos)
        {
            return new ArrayList();
        }
        #endregion
    }
}
