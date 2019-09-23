using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.BlockClasses;
using Sprint0.Sprites.Sprint0.Sprites;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.FactoryClasses
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
        Texture2D goomba;
        Texture2D greenkoopa ;
        Texture2D redkoopa ;
        public EnemyFactory()
        {
            //when factory initialzed, load the texture
            LoadTexture();
        }
        private void LoadTexture()
        {
            goomba = Sprint1.Game.Content.Load<Texture2D>("EnemySprite/goomba");
            greenkoopa = Sprint1.Game.Content.Load<Texture2D>("EnemySprite/greenkoopa");
            redkoopa = Sprint1.Game.Content.Load<Texture2D>("EnemySprite/redkoopa");
        }
        public void AddToList(ArrayList spriteList)
        {
            //initialize the sprites and add the sprites to the list
            spriteList.Add(getGreenKoopa());
            spriteList.Add(getRedKoopa());
            spriteList.Add(getGoomba());
        }
        public EnemySprite getGoomba()
        {
            return new EnemySprite(goomba, new Vector2(600, 100), new Point(1, 2));
        }
        public EnemySprite getRedKoopa()
        {
            return new EnemySprite(redkoopa, new Vector2(100, 150), new Point(1, 2));
        }
        public EnemySprite getGreenKoopa()
        {
            return new EnemySprite(greenkoopa, new Vector2(600, 150), new Point(1, 2));
        }

    }
}

