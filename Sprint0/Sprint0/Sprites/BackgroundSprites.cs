using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.ComponentModel;
using Sprint0;
using System.Collections;

namespace sprint0.Background
{
    class BigHillSprite : BackgroundSprite
    {
        public BigHillSprite(Texture2D texture) : base(texture)
        {
            Location.Add(new Vector2(300, 70));
        }
    }
    class SmallCloudSprite : BackgroundSprite
    {
        private ArrayList location;
        public SmallCloudSprite(Texture2D texture) : base(texture)
        {
            location = new ArrayList();
            location.Add(new Vector2(300, 50));
            location.Add(new Vector2(550, 50));
            location.Add(new Vector2(630, 50));
        }
    }
    class SmallHillSprite : BackgroundSprite
    {
        private ArrayList location;
        public SmallHillSprite(Texture2D texture) : base(texture)
        {
            location = new ArrayList();
            location.Add(new Vector2(500, 480));
        }
    }
    class BigCloudSprite : BackgroundSprite
    {
        private ArrayList location;
        public BigCloudSprite(Texture2D texture) : base(texture)
        {
            location = new ArrayList();
            location.Add(new Vector2(500, 480));
        }
    }
    class BigBushSprite : BackgroundSprite
    {
        private ArrayList location;
        public BigBushSprite(Texture2D texture) : base(texture)
        {
            location = new ArrayList();
            location.Add(new Vector2(500, 480));
        }
    }
}