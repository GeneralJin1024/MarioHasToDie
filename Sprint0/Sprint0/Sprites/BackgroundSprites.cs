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
            Location.Add(new Vector2(150, Sprint1.Game.GraphicsDevice.Viewport.Height - texture.Height));
            Location.Add(new Vector2(250, Sprint1.Game.GraphicsDevice.Viewport.Height - texture.Height));
        }
    }
    class SmallCloudSprite : BackgroundSprite
    {
        public SmallCloudSprite(Texture2D texture) : base(texture)
        {
            Location.Add(new Vector2(200, 50));
            Location.Add(new Vector2(400, 50));
            Location.Add(new Vector2(600, 50));
        }
    }
    class SmallHillSprite : BackgroundSprite
    {
        public SmallHillSprite(Texture2D texture) : base(texture)
        {
            Location = new ArrayList();
            Location.Add(new Vector2(50, Sprint1.Game.GraphicsDevice.Viewport.Height-texture.Height));
            Location.Add(new Vector2(100, Sprint1.Game.GraphicsDevice.Viewport.Height - texture.Height));
        }
    }
    class BigCloudSprite : BackgroundSprite
    {
        public BigCloudSprite(Texture2D texture) : base(texture)
        {
            Location.Add(new Vector2(100, 50));
            Location.Add(new Vector2(300, 50));
            Location.Add(new Vector2(500, 50));
        }
    }
    class BigBushSprite : BackgroundSprite
    {
        public BigBushSprite(Texture2D texture) : base(texture)
        {
            Location.Add(new Vector2(350, Sprint1.Game.GraphicsDevice.Viewport.Height - texture.Height));
        }
    }
    class SmallBushSprite : BackgroundSprite
    {
        public SmallBushSprite(Texture2D texture) : base(texture)
        {
            Location.Add(new Vector2(550, Sprint1.Game.GraphicsDevice.Viewport.Height - texture.Height));
            Location.Add(new Vector2(450, Sprint1.Game.GraphicsDevice.Viewport.Height - texture.Height));
            Location.Add(new Vector2(500, Sprint1.Game.GraphicsDevice.Viewport.Height - texture.Height));
        }
    }
}