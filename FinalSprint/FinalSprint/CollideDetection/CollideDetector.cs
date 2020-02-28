using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using FinalSprint.ItemEnemyClasses;
using FinalSprint.LevelLoader;
using FinalSprint.MarioClasses;

namespace FinalSprint.CollideDetection
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
            FireBallCharacters = fireBallCharacterList;
            DivideIntoList(); //divide all objects into two lists
            Mario = mario;
            CollidePairs = new List<CollidePair>();
            Map = new TileMap(new Point(10, 5), CharacterList, new Point((int)Stage.MapBoundary.X, (int)Stage.MapBoundary.Y));
        }
        public void Update()
        {
            //check whether they are out of screen
            foreach (ICharacter character in CharacterList)
            {
                if (Mario.Parameters.Position.X <= (Stage.Boundary.X / 2))
                    character.Parameters.InScreen = character.Parameters.Position.X <= Stage.Boundary.X;
                else if (Mario.Parameters.Position.X >= (Stage.MapBoundary.X - Stage.Boundary.X / 2))
                    character.Parameters.InScreen = character.GetMaxPosition.X >= Stage.MapBoundary.X - Stage.Boundary.X;
                else
                {
                    character.Parameters.InScreen = character.GetMaxPosition.X >= (Mario.Parameters.Position.X - 800 / 2) &&
                    character.Parameters.Position.X <= Mario.Parameters.Position.X + 800 / 2;
                }
                if (character is BossEnemyCharacter)
                    character.Parameters.InScreen = true;
                if (Mario.IsDied() || Mario.Win)
                    character.Parameters.InScreen = false;
            }
            int insurance = 0; // an insurance for unstop loop
            float timeOfFrame = 1; // total time for collision
            while (timeOfFrame > 0)
            {
                insurance++;
                if(insurance > 50)
                {
                    Console.WriteLine("It looks the loop will not stop. Check!  The rest of Time = " + timeOfFrame); Sprint5Main.Game.Exit(); break;
                }

                Map.UpdateMovingCharacters();
                List<CollidePair> firstContactPairs = new List<CollidePair>();
                /*
                 * Generate collide pair for mario, moving objects and fireball.
                 */
                if (!Mario.IsDied() && (Mario.GetAction != MarioState.ActionType.Other || Mario.Win))
                {
                    CreateCollidePairs(Mario);
                }
                foreach (ICharacter character in MovingCharacters)
                    if (!character.Parameters.IsHidden)
                        CreateCollidePairs(character);
                foreach (ICharacter character in FireBallCharacters)
                    if (!character.Parameters.IsHidden)
                        CreateCollidePairs(character);
                CollidePair[] pairs = CollidePairs.ToArray();


                float shortestTime = timeOfFrame;
                // find the smallest first contact time.
                for (int i = 0; i < pairs.Length; i++)
                {
                    if (CollidePairs[i].Time <= shortestTime && CollidePairs[i].Time >= 0)
                    {
                        shortestTime = CollidePairs[i].Time;
                        firstContactPairs.Insert(0, CollidePairs[i]);
                    }
                }
                Mario.Update(shortestTime); // Update with smallest first contact time.
                //Update all objects

                foreach (ICharacter character in FireBallCharacters)
                    character.Update(shortestTime);
                foreach (ICharacter character in CharacterList)
                    character.Update(shortestTime);
                //Do collision response. Use List since we don't know whether more than one objects collide with mario at the same time.
                CollidePair[] cp = firstContactPairs.ToArray();
                for (int i = 0; i < cp.Length && cp[i].Time == shortestTime; i++)
                    cp[i].Collide();
                CollidePairs.Clear(); // clear collide pairs
                firstContactPairs.Clear(); //clear sorted collide pairs
                timeOfFrame -= shortestTime; // change the rest of time.
            }
        }

        private void DivideIntoList()
        {
            MovingCharacters = new List<ICharacter>();
            foreach (ICharacter character in CharacterList)
            {
                switch (character.Type)
                {
                    case Sprint5Main.CharacterType.Enemy: MovingCharacters.Add(character); break;
                    case Sprint5Main.CharacterType.DiedEnemy: MovingCharacters.Add(character); break;
                    case Sprint5Main.CharacterType.Star: MovingCharacters.Add(character); break;
                    case Sprint5Main.CharacterType.RedMushroom: MovingCharacters.Add(character); break;
                    case Sprint5Main.CharacterType.GreenMushroom: MovingCharacters.Add(character); break;
                    case Sprint5Main.CharacterType.Flower: MovingCharacters.Add(character);break;
                    case Sprint5Main.CharacterType.Bomb: MovingCharacters.Add(character); break;
                    case Sprint5Main.CharacterType.Fireball: FireBallCharacters.Add(character); break;
                    case Sprint5Main.CharacterType.JumpMedicine: MovingCharacters.Add(character);break;
                    default: break;
                }
            }
        }
        
        private void CreateCollidePairs(ICharacter character1)
        {
            ArrayList possibleCollideList = new ArrayList();
            if (!character1.Parameters.IsHidden && character1.Parameters.InScreen && !(character1 is PlantEnemyCharacter))
            {
                Map.GetPossibleCollidedObject(character1, possibleCollideList);
                //Generate collide pairs and get first contact time
                foreach (ICharacter character in possibleCollideList)
                {
                    if (!(character1 is BossEnemyCharacter) || (character1.GetMaxPosition.Y < character.GetMaxPosition.Y) || character is BlockClasses.BlockCharacter)
                    {
                        CollidePair collidePair = new CollidePair(character1, character);
                        collidePair.GetFirstContactTime();
                        CollidePairs.Add(collidePair);
                    }
                }
            }
        }
    }
}
