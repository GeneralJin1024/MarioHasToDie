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
        Sprint1Main.CharacterType Type { get; set; } // used to help Mario to know what he should do.
        MoveParameters Parameters { get; } //make CollideDetector know its velocity and whether it is hidden
        void Update(float timeOfFrame);
        void Draw(SpriteBatch spriteBatch);
        //what the object should do when Mario collide with it. For Block, the special case is Mario hit its bottom.
        //For enemy, the special case is Mario hit its top.
        void MarioCollide(bool specialCase); 
        Vector2 GetMinPosition(); // get the position of left up corner
        Vector2 GetMaxPosition(); //get the position of right down corner
        Vector2 GetHeightAndWidth(); //get the height and width of object
    }
}
