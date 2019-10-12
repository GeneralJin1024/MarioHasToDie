using System;
using System.Collections;
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
        private readonly ArrayList CharacterList;
        private readonly List<CollidePair> CollidePairs;
        private readonly MarioCharacter Mario;
        private readonly TileMap Map;
        public CollisionDetector(MarioCharacter mario, ArrayList characterList)
        {
            CharacterList = characterList;
            Mario = mario;
            CollidePairs = new List<CollidePair>();
            Map = new TileMap(new Point(8, 5), CharacterList, new Point(800, 500));
        }
        public void Update()
        {
            float timeOfFrame = 1; // total time for collision
            while (timeOfFrame > 0)
            {
                List<CollidePair> firstContactPairs = new List<CollidePair>();
                ArrayList possibleCollideList = new ArrayList();
                Map.GetPossibleCollidedObject(Mario, possibleCollideList); // add possible collided object in possibleCollideList
                //generate collide pairs
                foreach (ICharacter character in possibleCollideList)
                {
                    CollidePair collidePair = new CollidePair(Mario, character);
                    collidePair.GetFirstContactTime(); // get first contact time and store it in CollidePair.Time
                    CollidePairs.Add(collidePair);
                }
                CollidePair[] pairs = CollidePairs.ToArray();
                float longestTime = timeOfFrame;
                // find the smallest first contact time.
                for (int i = 0; i < pairs.Length; i++)
                {
                    if (CollidePairs[i].Time <= longestTime && CollidePairs[i].Time >= 0)
                    {
                        longestTime = CollidePairs[i].Time;
                        firstContactPairs.Insert(0, CollidePairs[i]);
                    }
                }
                Mario.Update(longestTime); // Update with smallest first contact time.
                //Update all objects
                foreach (ICharacter character in CharacterList)
                    character.Update(longestTime);
                //Do collision response. Use List since we don't know whether more than one objects collide with mario at the same time.
                foreach (CollidePair pair in firstContactPairs)
                {
                    if (pair.Time == longestTime)
                        pair.Collide();
                }
                CollidePairs.Clear(); // clear collide pairs
                firstContactPairs.Clear(); //clear sorted collide pairs
                timeOfFrame -= longestTime; // change the rest of time.
            }
        }

        //private void SortCollidePairs()
        //{
        //    List<CollidePair> tempList = new List<CollidePair>();
        //    CollidePair[] pairs = CollidePairs.ToArray();
        //    while (CollidePairs.Count != 0)
        //    {
        //        float largestTime = int.MaxValue;
        //        for (int i = 0; i < pairs.Length; i++)
        //        {
        //            CollidePairs.RemoveAt(i);
        //            if (CollidePairs[i].Time <= largestTime && CollidePairs[i].Time > 0)
        //            {
        //                tempList.Add(CollidePairs[i]);
        //                largestTime = CollidePairs[i].Time;
        //            }
        //        }
        //    }
        //}
    }
}
