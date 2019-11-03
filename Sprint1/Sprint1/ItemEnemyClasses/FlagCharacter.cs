using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint1.ItemClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint1.ItemEnemyClasses
{
    class FlagCharacter : ItemCharacter
    {
        public override Sprint1Main.CharacterType Type { get; set; } = Sprint1Main.CharacterType.Flag;

        public FlagCharacter(Texture2D texture, Point rowsAndColumns, Vector2 location): base(texture, rowsAndColumns,location)
        {

        }

        public override Vector2 GetHeightAndWidth()
        {

            return item.GetHeightAndWidth();
        }

    }
}
