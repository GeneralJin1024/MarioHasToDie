﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint1.MarioClasses;
using System.Collections;

namespace Sprint1.CollideDetection
{
    public class TileMap
    {
        private readonly List<ICharacter>[] Entities;
        private Point MapSize;
        private Point ScreenSize;

        public TileMap(Point mapSize, ArrayList characterList, Point screenSize)
        {
            if (characterList is null) //check null
                throw new ArgumentNullException(nameof(characterList));
            MapSize = mapSize; //set MapSize: how many rows and columns
            ScreenSize = screenSize; // set the screen size.
            Entities = new List<ICharacter>[mapSize.X * mapSize.Y]; // generate rows * columns grids.
            SetEntities(characterList); // put objects into map
        }

        public void GetPossibleCollidedObject(MarioCharacter mario, ArrayList possibleCollideList)
        {
            if (mario is null || possibleCollideList is null) //check null
                throw new ArgumentNullException(nameof(mario));
            //Get which grids are mario's left up coner and right down coner occupy
            Point currentUpperLeftGrid = GetGridPosition(mario.GetMinPosition());
            Point currentLowerRightGrid = GetGridPosition(mario.GetMaxPosition());
            //Get which grids are mario's left up coner and right down coner will occupy
            Vector2 predictMinPosition = PredictNextPosition(mario.GetMinPosition(), mario.Parameters.Velocity, mario.GetHeightAndWidth());
            Point newUpperLeftGrid = GetGridPosition(predictMinPosition);
            Point newLowerRightGrid = GetGridPosition(new Vector2(predictMinPosition.X + mario.GetHeightAndWidth().Y,
                predictMinPosition.Y + mario.GetHeightAndWidth().Y));
            //calculate the possible collided region which depends on mario's velocity
            Point minRegion = new Point();
            Point maxRegion = new Point();
            // mario will hit no non-moving objects if mario is non-moving.
            if (mario.Parameters.Velocity.X > 0)
            {
                minRegion.X = currentUpperLeftGrid.X;
                maxRegion.X = newLowerRightGrid.X;
            }
            else
            {
                minRegion.X = newUpperLeftGrid.X;
                maxRegion.X = currentLowerRightGrid.X;
            }
            if (mario.Parameters.Velocity.Y > 0)
            {
                minRegion.Y = currentUpperLeftGrid.Y;
                maxRegion.Y = newLowerRightGrid.Y;
            }
            else
            {
                minRegion.Y = newUpperLeftGrid.Y;
                maxRegion.Y = currentLowerRightGrid.Y;
            }
            GetObjectsInRegion(minRegion, maxRegion, possibleCollideList); //get possible collided objects

        }

        //useless for this Sprint
        public void GetPossibleCollidedObject1(MarioCharacter mario, ArrayList possibleCollideList)
        {
            if (mario is null || possibleCollideList is null) //check null
                throw new ArgumentNullException(nameof(mario));
            Point upperLeftGrid = GetGridPosition(mario.GetMinPosition());
            Point lowerRightGrid = GetGridPosition(mario.GetMaxPosition());
            //test for min and max Point before extend
            //Console.WriteLine("zuoshangjiao: " + upperLeftGrid + "      youxiajiao: " + lowerRightGrid);
            if (upperLeftGrid.X == lowerRightGrid.X)
            {
                upperLeftGrid.X = upperLeftGrid.X > 0 ? upperLeftGrid.X - 1 : upperLeftGrid.X;
                lowerRightGrid.X = lowerRightGrid.X < MapSize.X - 1 ? lowerRightGrid.X + 1 : lowerRightGrid.X;
            }
            if (upperLeftGrid.Y == lowerRightGrid.Y)
            {
                upperLeftGrid.Y = upperLeftGrid.Y > 0 ? upperLeftGrid.Y - 1 : upperLeftGrid.Y;
                lowerRightGrid.Y = lowerRightGrid.Y < MapSize.Y - 1 ? lowerRightGrid.Y + 1 : lowerRightGrid.Y;
            }
            GetObjectsInRegion(upperLeftGrid, lowerRightGrid, possibleCollideList);

            //test for min and max after extended and whether objects are edded into possible collide list
            //Console.WriteLine("zuoshangjiao Later: " + upperLeftGrid + "      youxiajiao Later: " + lowerRightGrid);
            //Console.WriteLine("Number of List element: " + possibleCollideObject.Count);            
        }

        private void GetObjectsInRegion(Point upperLeftGrid, Point lowerRightGrid, ArrayList collideObjects)
        {
            for (int i = upperLeftGrid.Y; i <= lowerRightGrid.Y; i++)
            {
                for (int k = upperLeftGrid.X; k <= lowerRightGrid.X; k++)
                {
                    //Save this console command to check values if the grids number exceed the array's length.
                    //Console.WriteLine("grid = " + (i * MapSize.X + k) + "  i = " + i + "   k = " + k);
                    foreach (ICharacter character in Entities[i * MapSize.X + k])
                    {
                        if (!collideObjects.Contains(character) && !character.Parameters.IsHidden)
                            collideObjects.Add(character);
                    }
                }
            }
        }

        public static Vector2 PredictNextPosition(Vector2 minPosition, Vector2 velocity, Vector2 heightAndWidth)
        {
            //these values are both independent to Mario's real position.
            minPosition.X += velocity.X;
            minPosition.Y += velocity.Y;
            LevelLoader.Stage.CheckBoundary(minPosition, heightAndWidth); // check boundary
            return minPosition;
        }

        //private void ExtendRegion(Vector2 velocity, Point[] region)
        //{
        //    region[0].X -= 1;
        //    region[0].Y -= 1;
        //    region[1].X += 1;
        //    region[1].Y += 1;
        //    if (velocity.X < 0 && region[0].X > 0)
        //        region[0].X -= 1;
        //    else if (velocity.X > 0 && region[1].X < MapSize.X)
        //        region[1].X += 1;
        //    if (velocity.Y < 0 && region[0].Y > 0)
        //        region[0].Y -= 1;
        //    else if (velocity.Y > 0 && region[1].Y < MapSize.Y)
        //        region[1].Y += 1;
        //}

        private void SetEntities(ArrayList characterList)
        {
            for (int i = 0; i < Entities.Length; i++) // initialize each element. Did once per map.
                Entities[i] = new List<ICharacter>();
            foreach (ICharacter character in characterList)
            {
                Point minGridPosition = GetGridPosition(character.GetMinPosition());
                Point maxGridPosition = GetGridPosition(character.GetMaxPosition());
                for (int row = minGridPosition.Y; row <= maxGridPosition.Y; row++)
                {
                    for (int column = minGridPosition.X; column <= maxGridPosition.X; column++)
                    {
                        //put in the list only if the list didn't contain this object and this object is not null character.
                        if (!Entities[column + MapSize.X * row].Contains(character) && character.Type != Sprint1Main.CharacterType.Null)
                            Entities[column + MapSize.X * row].Add(character);
                    }
                }
            }
        }

        private Point GetGridPosition(Vector2 position)
        {
            // if position >= (800, 500), change them to (799, 499) and set to grid(7, 4)
            if (position.X >= ScreenSize.X)
                position.X = ScreenSize.X - 1; 
            if (position.Y >= ScreenSize.Y)
                position.Y = ScreenSize.Y - 1;
            return new Point((int)position.X / (ScreenSize.X / MapSize.X), (int)position.Y / (ScreenSize.Y / MapSize.Y));
        }
    }
}
