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
        MoveParameters Parameters { get; } 
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
        void MarioCollideTop(MarioCharacter mario); //with this parameter you can tell mario "You should do ...... if you collide with me" by calling its method
        void MarioCollideBottom(MarioCharacter mario);
        void MarioCollideLeft(MarioCharacter mario);
        void MarioCollideRight(MarioCharacter mario);
        Vector2 GetMinPosition(); //Position is the coordinate of lower left corner, return the coordinate of upper left corner
        Vector2 GetMaxPosition(); // return the coordinate of lower right corner
    }
}
