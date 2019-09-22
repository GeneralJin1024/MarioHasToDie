using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Background;
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
        public BackgroundFactory()
        {
            LoadTexture();
        }
        public void LoadTexture()
        {
            bigCloud = Sprint1.Game.Content.Load<Texture2D>("BackgroundSprite/bigCloud");
           smallCloud = Sprint1.Game.Content.Load<Texture2D>("BackgroundSprite/smallCloud");
            bigHill = Sprint1.Game.Content.Load<Texture2D>("BackgroundSprite/bigHill");
            smallHill = Sprint1.Game.Content.Load<Texture2D>("BackgroundSprite/smallHill");
            bigBush = Sprint1.Game.Content.Load<Texture2D>("BackgroundSprite/bigBush");
        }
        public void AddToList(ArrayList spriteList)
        {
            spriteList.Add(getBigCloud());
            spriteList.Add(getBigHill());
            spriteList.Add(getSmallCloud());
            spriteList.Add(getSmallHill());
            spriteList.Add(getBigBush());
        }
        public BigHillSprite getBigHill()
        {
            return new BigHillSprite(bigHill);
        }
        public SmallHillSprite getSmallHill()
        {
            return new SmallHillSprite(smallHill);
        }
        public BigCloudSprite getBigCloud()
        {
            return new BigCloudSprite(bigCloud);
        }
        public SmallCloudSprite getSmallCloud()
        {
            return new SmallCloudSprite(smallCloud);
        }
        public BigBushSprite getBigBush()
        {
            return new BigBushSprite(bigBush);
        }

    }
}



