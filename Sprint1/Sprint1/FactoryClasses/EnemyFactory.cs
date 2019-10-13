using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint1.BlockClasses;
using Sprint1.ItemClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint1.FactoryClasses
{
    class EnemyFactory:IFactory
    {
       

        private static EnemyFactory _instance;
        public static EnemyFactory Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new EnemyFactory();
                return _instance;
            }
        }
        Texture2D[] goomba;
        Texture2D[] greenkoopa ;
        Texture2D[] redkoopa ;
        public EnemyFactory()
        {
            //when factory initialzed, load the texture
            LoadTexture();
        }
        private void LoadTexture()
        {
            goomba = new Texture2D[2] {Sprint1Main.Game.Content.Load<Texture2D>("EnemySprite/goomba"),
                Sprint1Main.Game.Content.Load<Texture2D>("EnemySprite/deadGoomba")};

            greenkoopa = new Texture2D[2] { Sprint1Main.Game.Content.Load<Texture2D>("EnemySprite/greenkoopa"),
                Sprint1Main.Game.Content.Load<Texture2D>("EnemySprite/deadGreenkoopa") };
             redkoopa = new Texture2D[2] { Sprint1Main.Game.Content.Load<Texture2D>("EnemySprite/redkoopa"),
                Sprint1Main.Game.Content.Load<Texture2D>("EnemySprite/deadRedkoopa")};
        }
        public EnemyCharacter GetGoomba(MoveParameters moveParameters)
        {
            Point[] rowAndColumn = new Point[2] { new Point(1, 2), new Point(1, 1) };
            return new EnemyCharacter(goomba, rowAndColumn, moveParameters);
        }
        public EnemyCharacter GetRedKoopa(MoveParameters moveParameters)
        {
            Point[] rowAndColumn = new Point[2] { new Point(1, 2), new Point(1, 1) };
            return new EnemyCharacter(redkoopa, rowAndColumn, moveParameters);
        }
        public EnemyCharacter GetGreenKoopa(MoveParameters moveParameters)
        {
            Point[] rowAndColumn = new Point[2] { new Point(1, 2), new Point(1, 1) };
            return new EnemyCharacter(greenkoopa, rowAndColumn, moveParameters);
        }

        public ICharacter FactoryMethod(string name, Vector2 pos)
        {
            MoveParameters parameters = new MoveParameters();
            parameters.SetPosition(pos.X, pos.Y);
            switch (name)
            {
                case "Goomba":
                    return GetGoomba(parameters);
                case "RedKoopa":
                    return GetRedKoopa(parameters);
                case "GreenKoopa":
                    return GetGreenKoopa(parameters);
                default: return new NullCharacter();
            }
        }
    }
}

