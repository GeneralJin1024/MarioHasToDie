using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint1.Sprites
{
    class NullSprite : AnimatedSprite
    {
        public Texture2D SpriteSheets { get; set; }
        public MoveParameters Parameters { get; set; }
        public NullSprite(Texture2D texture) : base(texture, new Point(0, 0), new MoveParameters(false)) { }
        public void Draw(SpriteBatch spriteBatch)
        {
            //do nothing;
        }

        public Vector2 GetHeightAndWidth()
        {
            return new Vector2(0, 0);
        }

        public void Update(float timeOfFrame)
        {
            //do nothing;
        }
    }
}
