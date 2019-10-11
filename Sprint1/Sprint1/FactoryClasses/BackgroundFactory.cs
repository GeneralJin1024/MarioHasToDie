using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint1.Background;
using Sprint1.BlockClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint1.FactoryClasses
{
    class BackgroundFactory : IFactory
    {
        private static BackgroundFactory _instance;
        public static BackgroundFactory Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new BackgroundFactory();
                return _instance;
            }
        }
        Texture2D bigCloud;
        Texture2D smallCloud;
        Texture2D bigHill;
        Texture2D smallHill;
        Texture2D bigBush;
        Texture2D smallBush;
        public BackgroundFactory()
        {
            //when factory initialzed, load the texture
            LoadTexture();
        }
        private void LoadTexture()
        {
            bigCloud = Sprint1Main.Game.Content.Load<Texture2D>("BackgroundSprite/bigCloud");
            smallCloud = Sprint1Main.Game.Content.Load<Texture2D>("BackgroundSprite/smallCloud");
            bigHill = Sprint1Main.Game.Content.Load<Texture2D>("BackgroundSprite/bigHill");
            smallHill = Sprint1Main.Game.Content.Load<Texture2D>("BackgroundSprite/smallHill");
            bigBush = Sprint1Main.Game.Content.Load<Texture2D>("BackgroundSprite/bigBush");
            smallBush = Sprint1Main.Game.Content.Load<Texture2D>("BackgroundSprite/smallBush");

        }

        public BackgroundSprite GetBigHill(Vector2 pos)
        {
            return new BackgroundSprite(bigHill, pos);
        }
        public BackgroundSprite GetSmallHill(Vector2 pos)
        {
            return new BackgroundSprite(smallHill, pos);
        }
        public BackgroundSprite GetBigCloud(Vector2 pos)
        {
            return new BackgroundSprite(bigCloud, pos);
        }
        public BackgroundSprite GetSmallCloud(Vector2 pos)
        {
            return new BackgroundSprite(smallCloud, pos);
        }
        public BackgroundSprite GetBigBush(Vector2 pos)
        {
            return new BackgroundSprite(bigBush, pos);
        }
        public BackgroundSprite GetSmallBush(Vector2 pos)
        {
            return new BackgroundSprite(smallBush, pos);
        }

        public ICharacter FactoryMethod(string name, Vector2 pos)
        {
            //no character here
            return null;
        }

        public ISprite FactortyMethod2(string name, Vector2 pos)
        {
            switch (name)
            {
                case "BigHill":
                    return GetBigHill(pos);
                case "SmallHill":
                    return GetSmallHill(pos);
                case "BigCloud":
                    return GetBigCloud(pos);
                case "SmallCloud":
                    return GetSmallCloud(pos);
                case "BigBush":
                    return GetBigBush(pos);
                case "SmallBush":
                    return GetSmallBush(pos);
                default: return new Sprites.NullSprite();
            }
        }
    }
}



