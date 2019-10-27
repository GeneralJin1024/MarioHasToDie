using Microsoft.Xna.Framework;
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
        //非移动型：coin，砖，管子，花
        private readonly List<ICharacter>[] Entities;
        //移动型：敌人（生死），蘑菇，星星
        private List<ICharacter>[] MovingEntities;
        private Point MapSize;
        private Point ScreenSize;

        public TileMap(Point mapSize, ArrayList characterList, Point screenSize)
        {
            if (characterList is null) //check null
                throw new ArgumentNullException(nameof(characterList));
            MapSize = mapSize; //set MapSize: how many rows and columns
            ScreenSize = screenSize; // set the screen size.
            Entities = new List<ICharacter>[mapSize.X * mapSize.Y]; // generate rows * columns grids.
            MovingEntities = new List<ICharacter>[mapSize.X * mapSize.Y];
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
            //当前位置占据格
            Point currentUpperLeftGrid = GetGridPosition(mario.GetMinPosition());
            Point currentLowerRightGrid = GetGridPosition(mario.GetMaxPosition());
            //Get which grids are mario's left up coner and right down coner will occupy

            //满1移动后坐标
            Vector2 predictMinPosition = PredictNextPosition(mario.GetMinPosition(), mario.Parameters.Velocity, mario.GetHeightAndWidth());
            //左上角，右下角占据格（预计）
            Point newUpperLeftGrid = GetGridPosition(predictMinPosition);
            Point newLowerRightGrid = GetGridPosition(new Vector2(predictMinPosition.X + mario.GetHeightAndWidth().Y,
                predictMinPosition.Y + mario.GetHeightAndWidth().X));
            //捕获区域左上角，右下角
            Point minRegion = new Point();
            Point maxRegion = new Point();
            // mario will hit no non-moving objects if mario is non-moving.
            //确定捕获区域左上角
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
            //确定捕获区域右下角
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
            //加大区域（左，右，上）
            CheckGrids(ref minRegion, ref maxRegion);
            //抓取
            GetObjectsInRegion(minRegion, maxRegion, possibleCollideList, mario.Type); //get possible collided objects

        }

        public void UpdateMovingCharacters()
        {
            //updating in next sprint
            //提取
            ArrayList MovingCharacterList = new ArrayList();
            for (int i = 0; i < MovingEntities.Length; i++)
            {
                foreach (ICharacter character in MovingEntities[i])
                    if (!MovingCharacterList.Contains(character))
                        MovingCharacterList.Add(character);
                //清空
                MovingEntities[i].Clear();
            }
            //再加载
            SetEntities(MovingCharacterList);
        }

        private void GetObjectsInRegion(Point upperLeftGrid, Point lowerRightGrid, ArrayList collideObjects, Sprint1Main.CharacterType type)
        {
            for (int i = upperLeftGrid.Y; i <= lowerRightGrid.Y; i++)
            {
                for (int k = upperLeftGrid.X; k <= lowerRightGrid.X; k++)
                {
                    //Save this console command to check values if the grids number exceed the array's length.
                    //Console.WriteLine("grid = " + (i * MapSize.X + k) + "  i = " + i + "   k = " + k);
                    SearchEntityObjects(collideObjects, type, i, k);
                    SearchMovingEntityObjects(collideObjects, type, i, k);
                }
            }
        }
        private void SearchEntityObjects(ArrayList collideObjects, Sprint1Main.CharacterType type, int row, int column)
        {
            foreach (ICharacter character in Entities[row * MapSize.X + column])
            {
                if (!collideObjects.Contains(character) && !character.Parameters.IsHidden)
                {
                    //Mario碰一切，敌人物品碰砖块管子， 火球碰敌人，砖块，管子
                    //死去的敌人（设置速度0，0）可以被马里奥察觉.但主动不察觉其他物体，速度锁死，不再与砖块，管子产生碰撞
                    if (type == Sprint1Main.CharacterType.Mario)
                        collideObjects.Add(character);
                    else if ((type == Sprint1Main.CharacterType.Enemy || type == Sprint1Main.CharacterType.RedMushroom || 
                        type == Sprint1Main.CharacterType.Flower || type == Sprint1Main.CharacterType.GreenMushroom || 
                        type == Sprint1Main.CharacterType.DiedEnemy || type == Sprint1Main.CharacterType.Star) && 
                        (character.Type == Sprint1Main.CharacterType.Block || character.Type == Sprint1Main.CharacterType.Pipe))
                        collideObjects.Add(character);
                    else if (type == Sprint1Main.CharacterType.Fireball && (character.Type == Sprint1Main.CharacterType.Block ||
                        character.Type == Sprint1Main.CharacterType.Pipe || character.Type == Sprint1Main.CharacterType.Enemy))
                        collideObjects.Add(character);
                }
            }
        }

        private void SearchMovingEntityObjects(ArrayList collideObjects, Sprint1Main.CharacterType type, int row, int column)
        {
            if (type == Sprint1Main.CharacterType.Mario)
            {
                foreach (ICharacter character in MovingEntities[row * MapSize.X + column])
                    if (!collideObjects.Contains(character) && !character.Parameters.IsHidden)
                        collideObjects.Add(character);
            }
            else if (type == Sprint1Main.CharacterType.Fireball)
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
                Point minGridPosition = GetGridPosition(character.GetMinPosition());
                Point maxGridPosition = GetGridPosition(character.GetMaxPosition());
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
            // if position >= (800, 500), change them to (799, 499) and set to grid(7, 4)
            if (position.X >= ScreenSize.X)
                position.X = ScreenSize.X - 1; 
            if (position.Y >= ScreenSize.Y)
                position.Y = ScreenSize.Y - 1;
            return new Point((int)position.X / (ScreenSize.X / MapSize.X), (int)position.Y / (ScreenSize.Y / MapSize.Y));
        }
    }
}
