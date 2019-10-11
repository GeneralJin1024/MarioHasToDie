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
        public void AddToList(ArrayList spriteList)
        {
            //initialize the sprites and add the sprites to the list
            //spriteList.Add(GetGreenKoopa(new Vector2(400, 200)));
            //spriteList.Add(GetRedKoopa(new Vector2(300, 200)));
            //spriteList.Add(GetGoomba(new Vector2(100,200)));
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
                    return GetBrickBlock(parameters, new ArrayList());
                case "RedKoopa":
                    return GetQuestionBlock(parameters, new ArrayList());
                case "GreenKoopa":
                    return GetHiddenBlock(parameters, new ArrayList());
                default: return new NullCharacter();
            }
        }
    }
}

