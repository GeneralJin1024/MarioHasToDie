using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint0.MarioClasses;

namespace Sprint0
{
    public abstract class AbstractFactory
    {
        public abstract Mario GetMario();
        public abstract void GetBlock();
        public abstract void GetItem();
        public abstract void GetEnemy();
    }
}
