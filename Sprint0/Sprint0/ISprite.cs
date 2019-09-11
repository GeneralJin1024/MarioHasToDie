using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0
{
    interface ISprite
    {
        bool Visibility { get; set; }
        
        void Draw();

        void Update(GraphicsDevice graphicsDevice, GameTime gameTime);

        void SwitchVisibility();

    }
}
