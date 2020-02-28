using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FinalSprint.ItemClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalSprint.ItemClasses
{
    class CastleCharacter : ItemCharacter
    {
        public override Sprint5Main.CharacterType Type { get; set; } = Sprint5Main.CharacterType.Castle;
        public CastleCharacter(Texture2D texture, Point rowsAndColunms, Vector2 location)
            : base(texture, rowsAndColunms, location) { }
        public override Vector2 GetHeightAndWidth
        {
            get { return new Vector2(); }
        }
        public override Vector2 GetMinPosition
        {
            get { return new Vector2(Parameters.Position.X + 33, Parameters.Position.Y - 48); }
        }
        public override Vector2 GetMaxPosition
        {
            get { return new Vector2(Parameters.Position.X + 48, Parameters.Position.Y); }
        }
    }
}
