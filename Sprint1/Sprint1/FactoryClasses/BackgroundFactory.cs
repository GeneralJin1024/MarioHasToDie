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

        public void AddBackground(string name, Vector2 posS, List<Layer> layers)
        {
            //generating one background sprite at a time
            switch (name)
            {
                case "BigHill":
                    layers[1].Sprites.Add(GetBigHill(posS)); break;
                case "SmallHill":
                    layers[1].Sprites.Add(GetSmallHill(posS)); break;
                case "BigCloud":
                    layers[0].Sprites.Add(GetBigCloud(posS)); break;
                case "SmallCloud":
                    layers[0].Sprites.Add(GetSmallCloud(posS)); break;
                case "BigBush":
                    layers[2].Sprites.Add(GetBigBush(posS)); break;
                case "SmallBush":
                    layers[2].Sprites.Add(GetSmallBush(posS)); break;
                default:
                    layers[0].Sprites.Add(new NullCharacter()); break;
            }
        }

        public BackgroundCharacter GetBigHill(Vector2 pos)
        {
            return new BackgroundCharacter(bigHill, pos);
        }
        public BackgroundCharacter GetSmallHill(Vector2 pos)
        {
            return new BackgroundCharacter(smallHill, pos);
        }
        public BackgroundCharacter GetBigCloud(Vector2 pos)
        {
            return new BackgroundCharacter(bigCloud, pos);
        }
        public BackgroundCharacter GetSmallCloud(Vector2 pos)
        {
            return new BackgroundCharacter(smallCloud, pos);
        }
        public BackgroundCharacter GetBigBush(Vector2 pos)
        {
            return new BackgroundCharacter(bigBush, pos);
        }
        public BackgroundCharacter GetSmallBush(Vector2 pos)
        {
            return new BackgroundCharacter(smallBush, pos);
        }

        #region UselessMethods
        public List<ICharacter> FactoryMethod(string name, Vector2 posS, Vector2 posE)
        {
            //no character here
            return new List<ICharacter>();
        }

        public List<ICharacter> FactoryMethod(string name, Vector2 posS)
        {
            //no character here
            return new List<ICharacter>();
        }
        #endregion
    }
}



