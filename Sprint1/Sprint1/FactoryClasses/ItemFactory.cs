﻿using Microsoft.Xna.Framework;
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
        private ArrayList CharacterList;
        Texture2D coin;
        Texture2D flower;
        Texture2D greenMushroom;
        Texture2D redMushroom;
        Texture2D star;
        Texture2D pipe;
        Texture2D fireBall;
        public ItemFactory()
        {
            //when factory initialzed, load the texture
            LoadTexture();
        }
        public void Initialize(ArrayList characterList) { CharacterList = characterList; }
        public void AddNewCharacter(Sprint1Main.CharacterType characterType, Vector2 location, bool isLeft)
        {
            if (characterType == Sprint1Main.CharacterType.Fireball)
                CharacterList.Add(GetFireBall(location, isLeft));
        }

        private void LoadTexture()
        {
            coin = Sprint1Main.Game.Content.Load<Texture2D>("ItemSprite/coin");
            flower = Sprint1Main.Game.Content.Load<Texture2D>("ItemSprite/flower");
            greenMushroom = Sprint1Main.Game.Content.Load<Texture2D>("ItemSprite/greenMushroom");
            redMushroom = Sprint1Main.Game.Content.Load<Texture2D>("ItemSprite/redMushroom");
            star = Sprint1Main.Game.Content.Load<Texture2D>("ItemSprite/star");
            pipe = Sprint1Main.Game.Content.Load<Texture2D>("ItemSprite/PipeSpriteSheet");
            fireBall = Sprint1Main.Game.Content.Load<Texture2D>("ItemSprite/redMushroom");
        }
        
        public ItemCharacter GetPipe(Vector2 pos)
        {
            return new PipeCharacter(pipe, new Point(1, 1), pos);
        }
        public ItemCharacter GetCoin(Vector2 pos)
        {
            return new CoinCharacter(coin, new Point(1, 4), pos);
        }
        public ItemCharacter GetFlower(Vector2 pos)
        {
            return new FlowerCharacter(flower, new Point(1, 8), pos);
        }
        public ItemCharacter GetGreenMushroom(Vector2 pos)
        {
            return new GreenMushroomCharacter(greenMushroom, new Point(1, 1), pos);
        }
        public ItemCharacter GetRedMushroom(Vector2 pos)
        {
            return new RedMushroomCharacter(redMushroom, new Point(1, 1), pos);
        }
        public ItemCharacter GetStar(Vector2 pos)
        {
            return new StarCharacter(star, new Point(1, 4) , pos);
        }
        public ItemCharacter GetFireBall(Vector2 location, bool isLeft)
        {
            return new FireBallCharacter(fireBall, new Point(1, 1), location, isLeft);
        }

        public ICharacter FactoryMethod(string name, Vector2 pos)
        {
            switch (name)
            {
                case "Pipe":
                    return GetPipe(pos);
                case "Coin":
                    return GetCoin(pos);
                case "Flower":
                    return GetFlower(pos);
                case "GreenMushroom":
                    return GetGreenMushroom(pos);
                case "RedMashroom":
                    return GetRedMushroom(pos);
                case "Star":
                    return GetStar(pos);
                default: return new NullCharacter();
            }
        }
    }
}



