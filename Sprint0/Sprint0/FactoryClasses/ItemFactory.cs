using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.BlockClasses;
using Sprint0.Sprites;
using Sprint0.Sprites.Sprint0.Sprites;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.FactoryClasses
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
        public ItemFactory()
        {
            LoadTexture();
        }
        public void LoadTexture()
        {
            coin = Sprint1.Game.Content.Load<Texture2D>("ItemSprite/coin");
            flower = Sprint1.Game.Content.Load<Texture2D>("ItemSprite/flower");
            greenMushroom = Sprint1.Game.Content.Load<Texture2D>("ItemSprite/greenMushroom");
            redMushroom = Sprint1.Game.Content.Load<Texture2D>("ItemSprite/redMushroom");
            star = Sprint1.Game.Content.Load<Texture2D>("ItemSprite/star");
        }
        public void AddToList(ArrayList spriteList)
        {
            spriteList.Add(getCoin());
            spriteList.Add(getFlower());
            spriteList.Add(getGreenMushroom());
            spriteList.Add(getRedMushroom());
            spriteList.Add(getStar());
        }
        public ItemSprite getCoin()
        {
            return new ItemSprite(coin, new Vector2(100, 100), new Point(1, 4));
        }
        public ItemSprite getFlower()
        {
            return new ItemSprite(flower, new Vector2(200, 100), new Point(1, 8));
        }
        public ItemSprite getGreenMushroom()
        {
            return new ItemSprite(greenMushroom, new Vector2(300, 100), new Point(1, 1));
        }
        public ItemSprite getRedMushroom()
        {
            return new ItemSprite(redMushroom, new Vector2(400, 100), new Point(1, 1));
        }
        public ItemSprite getStar()
        {
            return new ItemSprite(star, new Vector2(500, 100), new Point(1, 4));
        }

    }
}



