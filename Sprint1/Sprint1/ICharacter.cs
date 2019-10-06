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
        Sprint1Main.CharacterType Type { get; set; }
        MoveParameters Parameters { get; }

        void Update(float timeOfFrame);
        void Draw(SpriteBatch spriteBatch);
        void MarioCollide(bool specialCase);
        Vector2 GetMinPosition();
        Vector2 GetMaxPosition();
        Vector2 GetHeightAndWidth();
    }
}
