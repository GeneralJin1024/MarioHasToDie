using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalSprint.FactoryClasses
{
    class RandomNumberGenerator
    {
        Random random;
        public RandomNumberGenerator()
        {
            random = new Random();
        }

        public int RandomEntityNumber(int minSeed, int maxSeed)
        {
            return random.Next(maxSeed - minSeed) + minSeed;
        }

        public Vector2 RandomEntityLocation(Vector2 minSeed, Vector2 MaxSeed)
        {
            float x = (float)(random.NextDouble() * (MaxSeed.X - minSeed.X) + minSeed.X);
            float y = (float)(random.NextDouble() * (MaxSeed.Y - minSeed.Y) + minSeed.Y);
            return new Vector2(x, y);
        }
    }
}
