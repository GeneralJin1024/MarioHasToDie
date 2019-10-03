using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Sprint1.MarioClasses;

namespace Sprint1.CollideDetection
{
    public class CollisionDetector
    {
        private List<ICharacter> CharacterList;
        private List<CollidePair> CollidePairs;
        private MarioCharacter Mario;
        private TileMap Map;
        public CollisionDetector(MarioCharacter mario, List<ICharacter> characterList)
        {
            CharacterList = characterList;
            Mario = mario;
            CollidePairs = new List<CollidePair>();
            Map = new TileMap(new Point(8, 5), CharacterList, new Point(800, 500));
        }
        public void Update()
        {
            float timeOfFrame = 1;
            while (timeOfFrame > 0)
            {
                List<CollidePair> firstContactPairs = new List<CollidePair>();
                List<ICharacter> possibleCollideList = new List<ICharacter>();
                Map.GetPossibleCollidedObject(Mario, possibleCollideList);
                //Console.WriteLine("Finding = " + possibleCollideList.Count);
                foreach (ICharacter character in possibleCollideList)
                {
                    CollidePair collidePair = new CollidePair(Mario, character);
                    collidePair.GetFirstContactTime();
                    CollidePairs.Add(collidePair);
                }
                CollidePair[] pairs = CollidePairs.ToArray();
                float longestTime = timeOfFrame;
                for (int i = 0; i < pairs.Length; i++)
                {
                    if (CollidePairs[i].Time <= longestTime && CollidePairs[i].Time >= 0)
                    {
                        longestTime = CollidePairs[i].Time;
                        firstContactPairs.Insert(0, CollidePairs[i]);
                    }
                }
                //Console.WriteLine("Executing");
                //Mario.Update(longestTime);
                //foreach (ICharacter character in CharacterList)
                //    character.Update(longestTime);
                foreach (CollidePair pair in firstContactPairs)
                {
                    if (pair.Time == longestTime)
                        pair.Collide();
                }
                CollidePairs.Clear();
                firstContactPairs.Clear();
                timeOfFrame -= longestTime;
            }
        }

        private void SortCollidePairs()
        {
            List<CollidePair> tempList = new List<CollidePair>();
            CollidePair[] pairs = CollidePairs.ToArray();
            while (CollidePairs.Count != 0)
            {
                float largestTime = int.MaxValue;
                for (int i = 0; i < pairs.Length; i++)
                {
                    CollidePairs.RemoveAt(i);
                    if (CollidePairs[i].Time <= largestTime && CollidePairs[i].Time > 0)
                    {
                        tempList.Add(CollidePairs[i]);
                        largestTime = CollidePairs[i].Time;
                    }
                }
            }
        }
    }
}
