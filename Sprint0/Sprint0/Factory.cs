using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint0.MarioClasses;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using sprint0.Item;
using sprint0.Enemy;
using sprint0.Background;

namespace Sprint0
{
    class Factory
    {
        public Mario getMario(Texture2D[] standardSheets, Texture2D[] superSheet,
            Texture2D[] fireSheet, Texture2D diedSheet, Vector2 location)
        {
            return new Mario(standardSheets, superSheet, fireSheet, diedSheet, location);
        }
                public CoinSprite getCoin(Texture2D texture)
        {
            return new CoinSprite(texture);
        }
        public FlowerSprite getFlower(Texture2D texture)
        {
            return new FlowerSprite(texture);
        }
        public GreenMushroomSprite getGreenMushroom(Texture2D texture)
        {
            return new GreenMushroomSprite(texture);
        }
        public RedMushroomSprite getRedMushroom(Texture2D texture)
        {
            return new RedMushroomSprite(texture);
        }
        public StarSprite getStar(Texture2D texture)
        {
            return new StarSprite(texture);
        }
        public GoombaSprite getGoomba(Texture2D texture)
        {
            return new GoombaSprite(texture);
        }
        public GreenkoopaSprite getGreenkoopa(Texture2D texture)
        {
            return new GreenkoopaSprite(texture);
        }
        public RedkoopaSprite getRedkoopa(Texture2D texture)
        {
            return new RedkoopaSprite(texture);
        }
        public BigHillSprite getBigHill(Texture2D texture)
        {
            return new BigHillSprite(texture);
        }
        public SmallHillSprite getSmallHill(Texture2D texture)
        {
            return new SmallHillSprite(texture);
        }
        public BigCloudSprite getBigCloud(Texture2D texture)
        {
            return new BigCloudSprite(texture);
        }
        public SmallCloudSprite getSmallCloud(Texture2D texture)
        {
            return new SmallCloudSprite(texture);
        }
    }
}
