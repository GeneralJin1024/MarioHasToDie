using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint1
{
    class NullCharacter : ICharacter
    {
        public Sprint1Main.CharacterType Type { get; set; }
        public Vector2 GetHeightAndWidth { get { return Vector2.Zero; } }
        public Vector2 GetMaxPosition { get { return Vector2.Zero; } }
        public Vector2 GetMinPosition { get { return Vector2.Zero; } }
        public MoveParameters Parameters { get; }
        public NullCharacter()
        {
            Type = Sprint1Main.CharacterType.Null;
            Parameters = new MoveParameters(false);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //do nothing
        }

        //public Vector2 GetMaxPosition()
        //{
        //    return new Vector2(0, 0);
        //}

        //public Vector2 GetMinPosition()
        //{
        //    return new Vector2(0, 0);
        //}

        public void MarioCollide(bool specialCase)
        {
            //do nothing
        }

        public void Update(float timeOfFrame)
        {
            //do nothing
        }
        public void BlockCollide(bool isBottom) { }

    }
}
