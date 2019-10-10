using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint1.BlockClasses;
using Sprint1.ItemClasses;
using Sprint1.Sprites;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint1.FactoryClasses
{
    class ItemFactory : IFactory
    {
        private static ItemFactory _instance;
        public static ItemFactory Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ItemFactory();
                return _instance;
            }
        }
        Texture2D coin;
        Texture2D flower;
        Texture2D greenMushroom;
        Texture2D redMushroom;
        Texture2D star;
        Texture2D pipe;
        public ItemFactory()
        {
            //when factory initialzed, load the texture
            LoadTexture();
        }
        private void LoadTexture()
        {
            coin = Sprint1Main.Game.Content.Load<Texture2D>("ItemSprite/coin");
            flower = Sprint1Main.Game.Content.Load<Texture2D>("ItemSprite/flower");
            greenMushroom = Sprint1Main.Game.Content.Load<Texture2D>("ItemSprite/greenMushroom");
            redMushroom = Sprint1Main.Game.Content.Load<Texture2D>("ItemSprite/redMushroom");
            star = Sprint1Main.Game.Content.Load<Texture2D>("ItemSprite/star");
            pipe = Sprint1Main.Game.Content.Load<Texture2D>("ItemSprite/PipeSpriteSheet");
        }
        public void AddToList(ArrayList spriteList)
        {
            //initialize the sprites and add the sprites to the list
            spriteList.Add(GetCoin());
            spriteList.Add(GetFlower());
            spriteList.Add(GetGreenMushroom());
            spriteList.Add(GetRedMushroom());
            spriteList.Add(GetStar());
            spriteList.Add(GetPipe());
        }
        public ItemCharacter GetPipe()
        {
            return new PipeCharacter(pipe, new Point(1, 1), new Vector2(500, 150));
        }
        public ItemCharacter GetCoin()
        {
            return new CoinCharacter(coin, new Point(1, 4), new Vector2(100, 100));
        }
        public ItemCharacter GetFlower()
        {
            return new FlowerCharacter(flower, new Point(1, 8), new Vector2(200, 100));
        }
        public ItemCharacter GetGreenMushroom()
        {
            return new GreenMushroomCharacter(greenMushroom, new Point(1, 1), new Vector2(300, 100));
        }
        public ItemCharacter GetRedMushroom()
        {
            return new RedMushroomCharacter(redMushroom, new Point(1, 1),new Vector2(400, 100));
        }
        public ItemCharacter GetStar()
        {
            return new StarCharacter(star, new Point(1, 4) ,new Vector2(500, 100));
        }

    }
}



