using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint1.BlockClasses;
using Sprint1.ItemClasses;
using Sprint1.Sprites;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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
        Texture2D fireBall;
        Texture2D bullet;
        Texture2D HP1;
        Texture2D HP2;
        Texture2D flag;
        Texture2D castle;
        Texture2D bomb;
        Texture2D randomItem;
        Texture2D medicine;
        public ItemFactory()
        {
            //when factory initialzed, load the texture
            LoadTexture();
        }
        public ICharacter AddNewCharacter(string characterType, Vector2 location)
        {

            ICharacter newItem = (ICharacter)FactoryMethod(characterType, location)[0];
            Sprint1Main.Game.Scene.FireBallList.Add(newItem);
            return newItem;
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
            bullet = Sprint1Main.Game.Content.Load<Texture2D>("ItemSprite/redMushroom");
            HP1 = Sprint1Main.Game.Content.Load<Texture2D>("ItemSprite/mario-underground-pipe1");
            HP2 = Sprint1Main.Game.Content.Load<Texture2D>("ItemSprite/mario-underground-pipe2");
            flag = Sprint1Main.Game.Content.Load<Texture2D>("ItemSprite/mario-flagpole");
            castle = Sprint1Main.Game.Content.Load<Texture2D>("ItemSprite/mario-castle");
            bomb = Sprint1Main.Game.Content.Load<Texture2D>("ItemSprite/greenMushroom");
            randomItem = Sprint1Main.Game.Content.Load<Texture2D>("ItemSprite/redMushroom");
            medicine = Sprint1Main.Game.Content.Load<Texture2D>("ItemSprite/greenMushroom");
        }
        public ItemCharacter GetPipe(Vector2 pos)
        {
            return new PipeCharacter(pipe, new Point(1, 1), pos, PipeCharacter.PipeType.Pipe);
        }
        public ItemCharacter GetVPipe(Vector2 pos)
        {
            return new PipeCharacter(pipe, new Point(1, 1), pos, PipeCharacter.PipeType.VPipe);
        }
        public ArrayList GetHPipe(Vector2 pos)
        {
            Vector2 position = new Vector2(pos.X+HP2.Width,pos.Y);
            return new ArrayList(){new PipeCharacter(HP1, new Point(1, 1), position, PipeCharacter.PipeType.Pipe),
                new PipeCharacter(HP2, new Point(1, 1), pos, PipeCharacter.PipeType.HPipe)};
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
        public ItemCharacter GetFireBall(Vector2 pos)
        {
            return new FireBallCharacter(fireBall, new Point(1, 1), pos);
        }
        public ItemCharacter GetBullet(Vector2 pos) { return new FireBallCharacter(bullet, new Point(1, 1), pos); }
        public ItemCharacter GetFlag(Vector2 pos) { return new FlagCharacter(flag, new Point(1, 1), pos); }
        public ItemCharacter GetCastle(Vector2 pos) { return new CastleCharacter(castle, new Point(1, 1), pos); }
        public ItemCharacter GetBomb(Vector2 pos) { return new BombCharacter(bomb, new Point(1, 1), pos); }
        public ItemCharacter GetRandomItem(Vector2 pos) { return new RandomItemCharacter(randomItem, new Point(1, 1), pos); }
        public ItemCharacter GetJumpMedicine(Vector2 pos) { return new JumpMedicineCharacter(medicine, new Point(1, 1), pos); }
        public ArrayList FactoryMethod(string name, Vector2 posS, Vector2 posE)
        {
            ArrayList list = new ArrayList();
            for (int x = 0; x < (posE.X - posS.X) / 16; x++)
            {
                for (int y = 0; y < (posE.Y - posS.Y) / 16; y++)
                {
                    Vector2 pos = new Vector2(posS.X + x * 16, posS.Y + y * 16);
                    switch (name)
                    {
                        case "Pipe":
                            list.Add(GetPipe(posS));
                            return list;    //Due to the special Format of Pipe sheet
                        case "VPipe":
                            list.Add(GetVPipe(pos));break;
                        case "HPipe":
                            list.AddRange(GetHPipe(pos));break;
                        case "Coin":
                            list.Add(GetCoin(pos));
                            break;
                        case "Flower":
                            list.Add(GetFlower(pos));
                            break;
                        case "GreenMushroom":
                            list.Add(GetGreenMushroom(pos));
                            break;
                        case "RedMashroom":
                            list.Add(GetRedMushroom(pos));
                            break;
                        case "Star":
                            list.Add(GetStar(pos));
                            break;
                        case "FireBall":
                            list.Add(GetFireBall(pos));
                            break;
                        case "Bullet": list.Add(GetBullet(pos)); break;
                        case "Flag": list.Add(GetFlag(pos)); break;
                        case "Castle": list.Add(GetCastle(pos)); break;
                        case "Bomb": list.Add(GetBomb(pos)); break;
                        case "Random": list.Add(GetRandomItem(pos)); break;
                        case "JumpMedicine": list.Add(GetJumpMedicine(pos)); break;
                        default: break;
                    }
                }
            }
            return list;
        }
        public ArrayList FactoryMethod(string namePlusNum, Vector2 pos)
        {
            //generating embedded items
            int startInd = 0;
            string name = namePlusNum.Substring(startInd, namePlusNum.IndexOf("+{", StringComparison.Ordinal) - startInd);
            startInd = namePlusNum.IndexOf("+{", StringComparison.Ordinal) + 2;
            int num = (int)decimal.Parse(namePlusNum.Substring(startInd, namePlusNum.IndexOf("}", StringComparison.Ordinal) - startInd), CultureInfo.CurrentCulture);
            ArrayList list = new ArrayList();
            for (int i = 0; i < num; i++)
            {
                switch (name)
                {
                    case "Coin":
                        list.Add(GetCoin(pos));
                        //Console.WriteLine("add Coins");
                        break;
                    case "Flower":
                        //Console.WriteLine("add Flower");
                        list.Add(GetFlower(pos));
                        break;
                    case "GreenMushroom":
                        list.Add(GetGreenMushroom(pos));
                        break;
                    case "RedMushroom":
                        //Console.WriteLine("add RedMushroom");
                        list.Add(GetRedMushroom(pos));
                        break;
                    case "Star":
                        list.Add(GetStar(pos));
                        break;
                    case "FireBall":
                        list.Add(GetFireBall(pos));
                        break;
                    case "Bullet": list.Add(GetBullet(pos)); break;
                    case "Random": list.Add(GetRandomItem(pos)); break;
                    case "JumpMedicine": list.Add(GetJumpMedicine(pos)); break;
                    default: break;
                }              
            }
            foreach (ICharacter character in list)
            {
                character.Parameters.IsHidden = true;
            }
            return list;
        }
    }
}



