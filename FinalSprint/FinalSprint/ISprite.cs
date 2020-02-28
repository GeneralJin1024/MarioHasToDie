using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalSprint
{
    public interface ISprite
    {
        Texture2D SpriteSheets { get; set; }
        Vector2 GetHeightAndWidth { get; }
        MoveParameters Parameters { get; set; }
        void Update(float timeOfFrame);
        void Draw(SpriteBatch spriteBatch);
        //Vector2 GetHeightAndWidth();   //x = Height, y = Width
    }
}
