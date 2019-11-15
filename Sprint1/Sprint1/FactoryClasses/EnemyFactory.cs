using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint1.BlockClasses;
using Sprint1.ItemClasses;
using Sprint1.ItemEnemyClasses;
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
        Texture2D[] plant;
        Texture2D[] boss;
        Texture2D[] cloud;
        Texture2D[] jump;
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
            plant = new Texture2D[2] { Sprint1Main.Game.Content.Load<Texture2D>("EnemySprite/enemyPlant"),
               Sprint1Main.Game.Content.Load<Texture2D>("EnemySprite/enemyPlant")};
            boss = new Texture2D[2] { Sprint1Main.Game.Content.Load<Texture2D>("EnemySprite/boss"),
                Sprint1Main.Game.Content.Load<Texture2D>("EnemySprite/boss")};
            cloud = new Texture2D[2] { Sprint1Main.Game.Content.Load<Texture2D>("EnemySprite/cloudEnemy1"),
                Sprint1Main.Game.Content.Load<Texture2D>("EnemySprite/cloudEnemy2")};
            jump = new Texture2D[2] { Sprint1Main.Game.Content.Load<Texture2D>("EnemySprite/jump1"),
                Sprint1Main.Game.Content.Load<Texture2D>("EnemySprite/jump2")};
            

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
        public EnemyCharacter GetPlant(MoveParameters moveParameters)
        {
            Point[] rowAndColumn = new Point[2] { new Point(1, 2), new Point(1, 2) };
            return new PlantEnemyCharacter(plant, rowAndColumn, moveParameters);
        }
        public EnemyCharacter GetBoss(MoveParameters moveParameters)
        {
            Point[] rowAndColumn = new Point[2] { new Point(1, 3), new Point(1, 3) };
            return new BossEnemyCharacter(boss, rowAndColumn, moveParameters);
        }
        public EnemyCharacter GetCloudEnemy(MoveParameters moveParameters)
        {
            Point[] rowAndColumn = new Point[2] { new Point(1, 1), new Point(1, 1) };
            return new CloudEnemyCharacter(cloud, rowAndColumn, moveParameters);
        }
        public EnemyCharacter GetJumpEnemy(MoveParameters moveParameters)
        {
            Point[] rowAndColumn = new Point[2] { new Point(1, 2), new Point(1, 2) };
            return new JumpEnemyCharacter(jump, rowAndColumn, moveParameters);
        }


        public ArrayList FactoryMethod(string name, Vector2 pos)
        {
            //Generating one enemy at a time
            ArrayList list = new ArrayList();
            MoveParameters parameters = new MoveParameters(true);
            parameters.SetPosition(pos.X, pos.Y);
            switch (name)
            {
                case "Goomba":
                    list.Add(GetGoomba(parameters));
                    break;
                case "RedKoopa":
                    list.Add(GetRedKoopa(parameters));
                    break;
                case "GreenKoopa":
                    list.Add(GetGreenKoopa(parameters));
                    break;
                case "Plant":
                    list.Add(GetPlant(parameters));
                    break;
                case "JumpEnemy":
                    list.Add(GetJumpEnemy(parameters));
                    break;
                case "CloudEnemy":
                    list.Add(GetCloudEnemy(parameters));
                    break;
                case "Boss":
                    list.Add(GetBoss(parameters));
                    break;
                default: break;


            }
            return list;
        }

        public ArrayList FactoryMethod(string name, Vector2 posS, Vector2 posE)
        {
            //won't get called
            return new ArrayList();
        }
    }
}

