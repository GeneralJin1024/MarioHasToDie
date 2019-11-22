using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint1.MarioClasses;
using System.Collections;
using Sprint1.ItemEnemyClasses;
using Sprint1.ItemClasses;

namespace Sprint1.CollideDetection
{
    public class TileMap
    {
        //NoMoving：coin，block，pipe，flower
        private readonly List<ICharacter>[] Entities;
        //Moving：enemy(live and died)，mushroom(red and green)，star
        private readonly List<ICharacter>[] MovingEntities;
        private Point MapSize; // how many rows(X) and how many clumns (Y)
        private Point ScreenSize; // It will delete in next Sprint

        public TileMap(Point mapSize, ArrayList characterList, Point screenSize)
        {
            if (characterList is null) //check null
                throw new ArgumentNullException(nameof(characterList));
            MapSize = mapSize; //set MapSize: how many rows and columns
            ScreenSize = screenSize; // set the screen size.
            Entities = new List<ICharacter>[mapSize.X * mapSize.Y]; // generate rows * columns grids.
            MovingEntities = new List<ICharacter>[mapSize.X * mapSize.Y];
            //initialize each grid.
            for (int i = 0; i < Entities.Length; i++)
            {
                Entities[i] = new List<ICharacter>();
                MovingEntities[i] = new List<ICharacter>();
            }
            SetEntities(characterList); // put objects into map
        }

        public void GetPossibleCollidedObject(ICharacter mario, ArrayList possibleCollideList)
        {
            if (mario is null || possibleCollideList is null) //check null
                throw new ArgumentNullException(nameof(mario));
            //Get which grids are mario's left up coner and right down coner occupy
            Point currentUpperLeftGrid = GetGridPosition(mario.GetMinPosition);
            Point currentLowerRightGrid = GetGridPosition(mario.GetMaxPosition);
            //Get which grids are mario's left up coner and right down coner will occupy

            //Predict the position after an Update with time = 1 frame
            Vector2 predictMinPosition = PredictNextPosition(mario.GetMinPosition, mario.Parameters.Velocity, mario.GetHeightAndWidth);
            //Get the grids it occupy after updating
            Point newUpperLeftGrid = GetGridPosition(predictMinPosition);
            Point newLowerRightGrid = GetGridPosition(new Vector2(predictMinPosition.X + mario.GetHeightAndWidth.Y,
                predictMinPosition.Y + mario.GetHeightAndWidth.X));

            Point minRegion = new Point();
            Point maxRegion = new Point();
            //get the upper left grid for final searching region
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
            //get the lower right grid for final searching resion
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
            //extend the region(In next sprint, this will obly be called when the object is Mario or fireball because only these two collide with moving object)
            CheckGrids(ref minRegion, ref maxRegion);
            GetObjectsInRegion(minRegion, maxRegion, possibleCollideList, mario); //get possible collided objects

        }

        public void UpdateMovingCharacters()
        {
            //updating the location of objects in movingEntity because they are moving.
            //get all moving objects
            ArrayList MovingCharacterList = new ArrayList();
            for (int i = 0; i < MovingEntities.Length; i++)
            {
                foreach (ICharacter character in MovingEntities[i])
                    if (!MovingCharacterList.Contains(character))
                        MovingCharacterList.Add(character);
                //clear each entity
                MovingEntities[i].Clear();
            }
            //reload
            SetEntities(MovingCharacterList);
        }

        private void GetObjectsInRegion(Point upperLeftGrid, Point lowerRightGrid, ArrayList collideObjects, ICharacter character)
        {
            //go through each grid
            for (int i = upperLeftGrid.Y; i <= lowerRightGrid.Y; i++)
            {
                for (int k = upperLeftGrid.X; k <= lowerRightGrid.X; k++)
                {
                    //Save this console command to check values if the grids number exceed the array's length.
                    //Console.WriteLine("grid = " + (i * MapSize.X + k) + "  i = " + i + "   k = " + k);
                    SearchEntityObjects(collideObjects, character.Type, i, k);
                    SearchMovingEntityObjects(collideObjects, character, i, k);
                }
            }
        }
        private void SearchEntityObjects(ArrayList collideObjects, Sprint1Main.CharacterType type, int row, int column)
        {
            foreach (ICharacter character in Entities[row * MapSize.X + column])
            {
                if (!collideObjects.Contains(character) && !character.Parameters.IsHidden)
                {
                    /*
                     * Except Mario, all moving object only collide with block and pipe
                     */
                    if (type == Sprint1Main.CharacterType.Mario) //Mario collide with anything that is not hidden
                        collideObjects.Add(character);
                    else if(character.Type == Sprint1Main.CharacterType.Block || character.Type == Sprint1Main.CharacterType.Pipe)
                        collideObjects.Add(character);
                    //else if ((type == Sprint1Main.CharacterType.Enemy || type == Sprint1Main.CharacterType.RedMushroom || 
                    //    type == Sprint1Main.CharacterType.Flower || type == Sprint1Main.CharacterType.GreenMushroom || 
                    //    type == Sprint1Main.CharacterType.DiedEnemy || type == Sprint1Main.CharacterType.Star) && 
                    //    (character.Type == Sprint1Main.CharacterType.Block || character.Type == Sprint1Main.CharacterType.Pipe))
                    //    collideObjects.Add(character);
                    //else if (type == Sprint1Main.CharacterType.Fireball && (character.Type == Sprint1Main.CharacterType.Block ||
                    //    character.Type == Sprint1Main.CharacterType.Pipe || character.Type == Sprint1Main.CharacterType.Enemy))
                    //    collideObjects.Add(character);
                }
            }
        }

        private void SearchMovingEntityObjects(ArrayList collideObjects, ICharacter mainCharacter, int row, int column)
        {
            if (mainCharacter.Type == Sprint1Main.CharacterType.Mario)
            {
                MarioCharacter mario = (MarioCharacter)mainCharacter;
                foreach (ICharacter character in MovingEntities[row * MapSize.X + column])
                {
                    if (!collideObjects.Contains(character) && !character.Parameters.IsHidden && !mario.Invincible)
                        collideObjects.Add(character);
                    else if (!collideObjects.Contains(character) && !character.Parameters.IsHidden &&
                        (character.Type != Sprint1Main.CharacterType.Enemy || character is BossEnemyCharacter) && !(character is BombCharacter))
                    {
                        collideObjects.Add(character);
                    }
                }
            }
            else if (mainCharacter.Type == Sprint1Main.CharacterType.Fireball) // fireball only collide with enemy among moving objects.
            {
                foreach (ICharacter character in MovingEntities[row * MapSize.X + column])
                    if (character.Type == Sprint1Main.CharacterType.Enemy && !collideObjects.Contains(character) && !character.Parameters.IsHidden)
                        collideObjects.Add(character);
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

        private void CheckGrids(ref Point minRegion, ref Point maxRegion)
        {
            if (minRegion.X == maxRegion.X)
            {
                minRegion.X = minRegion.X <= 0 ? 0 : minRegion.X - 1;
                maxRegion.X = maxRegion.X >= MapSize.X - 1 ? MapSize.X - 1 : maxRegion.X + 1;
            }
            if (minRegion.Y == maxRegion.Y)
            {
                minRegion.Y = minRegion.Y <= 0 ? 0 : minRegion.Y - 1;
            }
        }
        public void SetEntities(ArrayList characterList)
        {
            if (characterList == null)
                throw new ArgumentNullException(nameof(characterList));
            foreach (ICharacter character in characterList)
            {
                //get the grid it occupy
                Point minGridPosition = GetGridPosition(character.GetMinPosition);
                Point maxGridPosition = GetGridPosition(character.GetMaxPosition);
                for (int row = minGridPosition.Y; row <= maxGridPosition.Y; row++)
                {
                    for (int column = minGridPosition.X; column <= maxGridPosition.X; column++)
                    {
                        if ((character.Type == Sprint1Main.CharacterType.Block || character.Type == Sprint1Main.CharacterType.Coin ||
                            character.Type == Sprint1Main.CharacterType.Pipe || character.Type == Sprint1Main.CharacterType.Flower) &&
                            !Entities[column + MapSize.X * row].Contains(character))
                        {
                            Entities[column + MapSize.X * row].Add(character);
                        }
                        else if (!MovingEntities[column + MapSize.X * row].Contains(character) && character.Type != Sprint1Main.CharacterType.Null)
                            MovingEntities[column + MapSize.X * row].Add(character);
                    }
                }
            }
        }

        private Point GetGridPosition(Vector2 position)
        {
            if (position.X >= ScreenSize.X)
                position.X = ScreenSize.X - 1; 
            if (position.Y >= ScreenSize.Y)
                position.Y = ScreenSize.Y - 1;
            return new Point((int)position.X / (ScreenSize.X / MapSize.X), (int)position.Y / (ScreenSize.Y / MapSize.Y));
        }
    }
}
