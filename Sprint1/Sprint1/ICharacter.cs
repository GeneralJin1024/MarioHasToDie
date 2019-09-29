using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint1.MarioClasses;

namespace Sprint1
{
    public interface ICharacter
    {
        MoveParameters Parameter { get; }
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
        void MarioCollideTop(Mario mario);
        void MarioCollideBottom(Mario mario);
        void MarioCollideLeft(Mario mario);
        void MarioCollideRight(Mario mario);
        Vector2 GetRealPosition();
    }
}
