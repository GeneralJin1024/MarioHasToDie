using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint1.ItemClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint1.ItemClasses
{
    class CastleCharacter : ItemCharacter
    {
        public override Sprint1Main.CharacterType Type { get; set; } = Sprint1Main.CharacterType.Castle;
        public CastleCharacter(Texture2D texture, Point rowsAndColunms, Vector2 location)
            : base(texture, rowsAndColunms, location) { }
        public override Vector2 GetHeightAndWidth()
        {
            return new Vector2();
        }
        public override Vector2 GetMinPosition()
        {
            return new Vector2(Parameters.Position.X + 33, Parameters.Position.Y - 48);
        }
        public override Vector2 GetMaxPosition()
        {
            return new Vector2(Parameters.Position.X + 48, Parameters.Position.Y);
        }
    }
}
