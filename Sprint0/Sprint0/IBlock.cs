using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0
{
    interface IBlock
    {
        void Draw();
        void Update();
        void Hit(IList<IItem> items, bool isMario, bool isBig);
    }
}
