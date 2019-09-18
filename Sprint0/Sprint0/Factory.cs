using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint0.MarioClasses;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using sprint0.Background;
using Sprint0.Sprites;
using Sprint0.Sprites.Sprint0.Sprites;

namespace Sprint0
{
    class Factory
    {
        public Mario getMario(Texture2D[] standardSheets, Texture2D[] superSheet,
            Texture2D[] fireSheet, Texture2D diedSheet, Vector2 location)
        {
            return new Mario(standardSheets, superSheet, fireSheet, diedSheet, location);
        }
        public ItemSprite getCoin(Texture2D texture)
        {
            return new ItemSprite(texture, new Vector2(100, 100), new Point(1, 4));
        }
        public ItemSprite getFlower(Texture2D texture)
        {
            return new ItemSprite(texture, new Vector2(200, 100), new Point(1, 8));
        }
        public ItemSprite getGreenMushroom(Texture2D texture)
        {
            return new ItemSprite(texture, new Vector2(300, 100), new Point(1, 1));
        }
        public ItemSprite getRedMushroom(Texture2D texture)
        {
            return new ItemSprite(texture, new Vector2(400, 100), new Point(1, 1));
        }
        public ItemSprite getStar(Texture2D texture)
        {
            return new ItemSprite(texture, new Vector2(500, 100), new Point(1, 4));
        }
        public EnemySprite getGoomba(Texture2D texture)
        {
            return new EnemySprite(texture, new Vector2(600, 100), new Point(1, 2));
        }
        public EnemySprite getRedKoopa(Texture2D texture)
        {
            return new EnemySprite(texture, new Vector2(100, 150), new Point(1, 2));
        }
        public EnemySprite getGreenKoopa(Texture2D texture)
        {
            return new EnemySprite(texture, new Vector2(600, 150), new Point(1, 2));
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
        public BigBushSprite getBigBush(Texture2D texture)
        {
            return new BigBushSprite(texture);
        }

    }
}
