using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Sprint1.LevelLoader;
using Sprint1.MarioClasses;

namespace Sprint1.CollideDetection
{
    public class CollisionDetector
    {
        private readonly ArrayList CharacterList;
        private List<ICharacter> MovingCharacters;
        private readonly ArrayList FireBallCharacters;
        private readonly List<CollidePair> CollidePairs;
        private readonly MarioCharacter Mario;
        private readonly TileMap Map;
        public CollisionDetector(MarioCharacter mario, ArrayList characterList, ArrayList fireBallCharacterList)
        {
            if (characterList is null || fireBallCharacterList is null)
                throw new ArgumentNullException(nameof(characterList));
            CharacterList = characterList;
            //Console.WriteLine("Contain the flower" + CharacterList.Contains(Sprint1Main.Flower));
            FireBallCharacters = fireBallCharacterList;
            //Console.WriteLine("At first, has items = " + FireBallCharacters.Count);
            foreach (ICharacter items in FireBallCharacters)
            {
                CharacterList.Add(items);
                if (!items.Parameters.IsHidden)
                {
                    Console.WriteLine("This item is added by block and it is not hidden, please check again.  Its type is " + items.Type);
                    Sprint1Main.Game.Exit();
                }
            }
            FireBallCharacters.Clear();
            DivideIntoList();
            Mario = mario;
            CollidePairs = new List<CollidePair>();
            Map = new TileMap(new Point(10, 5), CharacterList, new Point(1000, 500));
        }
        public void Update()
        {
            foreach (ICharacter character in CharacterList)
            {
                if (Mario.Parameters.Position.X <= (Stage.Boundary.X / 2))
                    character.Parameters.InScreen = character.Parameters.Position.X <= Stage.Boundary.X;
                else if (Mario.Parameters.Position.X >= (Stage.MapBoundary.X - Stage.Boundary.X / 2))
                    character.Parameters.InScreen = character.GetMaxPosition().X >= Stage.MapBoundary.X - Stage.Boundary.X;
                else
                {
                    character.Parameters.InScreen = character.Parameters.Position.X >= (Mario.Parameters.Position.X - 800 / 2) &&
                    character.Parameters.Position.X <= Mario.Parameters.Position.X + 800 / 2;
                }
                if (Mario.IsDied())
                    character.Parameters.InScreen = false;
            }
            int insurance = 0;
            float timeOfFrame = 1; // total time for collision
            while (timeOfFrame > 0)
            {
                insurance++;
                if(insurance > 15)
                {
                    Console.WriteLine("It looks the loop will not stop. Check!  The rest of Time = " + timeOfFrame); Sprint1Main.Game.Exit();
                }
                Map.UpdateMovingCharacters();
                //Console.WriteLine("Mario Velocity Before Collide1 = " + Mario.Parameters.Velocity);
                List<CollidePair> firstContactPairs = new List<CollidePair>();
                //UpdateItemPoint();
                //List<ICharacter> possibleCollideList = new List<ICharacter>();
                ////Map.SetEntities(CharacterList);
                //Map.GetPossibleCollidedObject(Mario, possibleCollideList);
                ////foreach (ICharacter character in MovingCharacters)
                ////    Map.GetPossibleCollidedObject(character, possibleCollideList);
                ////Console.WriteLine("Finding = " + possibleCollideList.Count);
                //foreach (ICharacter character in possibleCollideList)
                //{
                //    CollidePair collidePair = new CollidePair(Mario, character);
                //    collidePair.GetFirstContactTime();
                //    CollidePairs.Add(collidePair);
                //}
                if(!Mario.IsDied())
                    CreateCollidePairs(Mario);
                foreach (ICharacter character in MovingCharacters)
                    if (!character.Parameters.IsHidden)
                        CreateCollidePairs(character);
                foreach (ICharacter character in FireBallCharacters)
                    if (!character.Parameters.IsHidden)
                        CreateCollidePairs(character);
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
                //foreach (ICharacter character in CharacterList)
                //    character.Update(longestTime);
                foreach (ICharacter character in FireBallCharacters)
                    character.Update(longestTime);
                foreach (ICharacter character in CharacterList)
                {
                    //if (character.Type == Sprint1.CharacterType.Coin && !character.Parameters.IsHidden)
                    //    Console.WriteLine("Exist " + character.Parameters.Velocity + "     position = " + character.Parameters.Position);
                    character.Update(longestTime);
                }
                //Do collision response. Use List since we don't know whether more than one objects collide with mario at the same time.
                foreach (CollidePair pair in firstContactPairs)
                {
                    if (pair.Time == longestTime)
                        pair.Collide();
                }
                //foreach (ICharacter character in MovingCharacters)
                //{
                //    if (character.Parameters.IsHidden)
                //        Console.WriteLine("MushRoom is not visible");
                //}
                CollidePairs.Clear(); // clear collide pairs
                firstContactPairs.Clear(); //clear sorted collide pairs
                timeOfFrame -= longestTime; // change the rest of time.
                //Console.WriteLine("Mario Velocity Before Collide3 = " + Mario.Parameters.Velocity);
            }
        }

        private void DivideIntoList()
        {
            MovingCharacters = new List<ICharacter>();
            foreach (ICharacter character in CharacterList)
            {
                switch (character.Type)
                {
                    case Sprint1Main.CharacterType.Enemy: MovingCharacters.Add(character); break;
                    case Sprint1Main.CharacterType.DiedEnemy: MovingCharacters.Add(character); break;
                    case Sprint1Main.CharacterType.Star: MovingCharacters.Add(character); break;
                    case Sprint1Main.CharacterType.RedMushroom: MovingCharacters.Add(character); break;
                    case Sprint1Main.CharacterType.GreenMushroom: MovingCharacters.Add(character); break;
                    case Sprint1Main.CharacterType.Flower: MovingCharacters.Add(character);break;
                    case Sprint1Main.CharacterType.Fireball: FireBallCharacters.Add(character); break;
                    default: break;
                }
            }
        }

        private void CreateCollidePairs(ICharacter character1)
        {
            ArrayList possibleCollideList = new ArrayList();
            if (!character1.Parameters.IsHidden && character1.Parameters.InScreen)
            {
                Map.GetPossibleCollidedObject(character1, possibleCollideList);
                foreach (ICharacter character in possibleCollideList)
                {
                    CollidePair collidePair = new CollidePair(character1, character);
                    collidePair.GetFirstContactTime();
                    CollidePairs.Add(collidePair);
                }
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
