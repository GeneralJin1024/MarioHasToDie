using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint1.MarioClasses;
using Sprint1.ItemClasses;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sprint1.ItemClasses
{
    public class PipeCharacter: ItemCharacter
    {
        public enum PipeType { Pipe, VPipe, HPipe}

        public PipeType PType { get; private set; }

        public override Vector2 GetHeightAndWidth { get { return Item.GetHeightAndWidth; } }
        public override Sprint1Main.CharacterType Type { get; set; } = Sprint1Main.CharacterType.Pipe;

        public PipeCharacter(Texture2D texture, Point rowsAndColunms, Vector2 location, PipeType p)
            : base(texture, rowsAndColunms, location)
        {
            PType = p;
        }

        public override void Update(float timeOfFrame)
        {
        }

        public override void MarioCollide(bool specialCase)
        {
          
        }
        public void MarioGetInside() { PType = PipeType.Pipe; }
    }
}
