﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint0.MarioClasses;
using Sprint0.BlockClasses;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using sprint0.Background;
using Sprint0.Sprites;
using Sprint0.Sprites.Sprint0.Sprites;
using Microsoft.Xna.Framework.Content;
using System.Collections;

namespace Sprint0
{
    class Factory
    {
        /*
        private ContentManager Content;

        public Factory(ContentManager content)
        {
            Content = content;
        }
        public void LoadSpriteToList(ArrayList spriteList)
        {
            LoadEnemyItemTexture(spriteList);
        }
        private void LoadEnemyItemTexture(ArrayList spriteList)
        {

            Texture2D goomba = Content.Load<Texture2D>("EnemySprite/goomba");
            Texture2D greenkoopa = Content.Load<Texture2D>("EnemySprite/greenkoopa");
            Texture2D redkoopa = Content.Load<Texture2D>("EnemySprite/redkoopa");
            Texture2D coin = Content.Load<Texture2D>("ItemSprite/coin");
            Texture2D flower = Content.Load<Texture2D>("ItemSprite/flower");
            Texture2D greenMushroom = Content.Load<Texture2D>("ItemSprite/greenMushroom");
            Texture2D redMushroom = Content.Load<Texture2D>("ItemSprite/redMushroom");
            Texture2D star = Content.Load<Texture2D>("ItemSprite/star");
            spriteList.Add(getCoin(coin));
            spriteList.Add(getFlower(flower));
            spriteList.Add(getGreenMushroom(greenMushroom));
            spriteList.Add(getRedMushroom(redMushroom));
            spriteList.Add(getStar(star));
            spriteList.Add(getGreenKoopa(greenkoopa));
            spriteList.Add(getRedKoopa(redkoopa));
            spriteList.Add(getGoomba(goomba));
        }
        */
        public Mario getMario(Texture2D[] standardSheets, Texture2D[] superSheet,
            Texture2D[] fireSheet, Vector2 location)
        {
            return new Mario(standardSheets, superSheet, fireSheet, location);
        }
        public ItemSprite getCoin(Texture2D texture)
        {
            return new ItemSprite(texture, new Vector2(100, 100), new Point(1, 4));
        }
        public ItemSprite getFlower(Texture2D texture)
        {
            return new ItemSprite(texture, new Vector2(200, 100), new Point(1, 8));
        }
        public ItemSprite getGreenMushroom(Texture2D texture)
        {
            return new ItemSprite(texture, new Vector2(300, 100), new Point(1, 1));
        }
        public ItemSprite getRedMushroom(Texture2D texture)
        {
            return new ItemSprite(texture, new Vector2(400, 100), new Point(1, 1));
        }
        public ItemSprite getStar(Texture2D texture)
        {
            return new ItemSprite(texture, new Vector2(500, 100), new Point(1, 4));
        }
        public EnemySprite getGoomba(Texture2D texture)
        {
            return new EnemySprite(texture, new Vector2(600, 100), new Point(1, 2));
        }
        public EnemySprite getRedKoopa(Texture2D texture)
        {
            return new EnemySprite(texture, new Vector2(100, 150), new Point(1, 2));
        }
        public EnemySprite getGreenKoopa(Texture2D texture)
        {
            return new EnemySprite(texture, new Vector2(600, 150), new Point(1, 2));
        }
        public BigHillSprite getBigHill(Texture2D texture)
        {
            return new BigHillSprite(texture);
        }
        public SmallHillSprite getSmallHill(Texture2D texture)
        {
            return new SmallHillSprite(texture);
        }
        public BigCloudSprite getBigCloud(Texture2D texture)
        {
            return new BigCloudSprite(texture);
        }
        public SmallCloudSprite getSmallCloud(Texture2D texture)
        {
            return new SmallCloudSprite(texture);
        }
        public BigBushSprite getBigBush(Texture2D texture)
        {
            return new BigBushSprite(texture);
        }
        //public FloorBlockSprite GetFloorBlockSprite(Texture2D texture)
        //{
           
        //}
    }

    class BlockFactory
    {
        private static BlockFactory _instance;
        public static BlockFactory Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new BlockFactory();
                return _instance;
            }
        }
        private static Texture2D[] blockTextures;
        public BlockFactory()
        {
            LoadBlockTexture();
        }
        private void LoadBlockTexture()
        {
            blockTextures = new Texture2D[5] {Sprint0.Game.Content.Load<Texture2D>("BlockSprites/mario-brick-blocks"),
                Sprint0.Game.Content.Load<Texture2D>("BlockSprites/mario-gravel-blocks"),
                Sprint0.Game.Content.Load<Texture2D>("BlockSprites/mario-hit-block"),
                Sprint0.Game.Content.Load<Texture2D>("BlockSprites/mario-question-blocks"),
                Sprint0.Game.Content.Load<Texture2D>("BlockSprites/mario-shiny-block")};
        }
        public Texture2D GetBlockTextures(int index)
        {
            return blockTextures[index];
        }
        public BrickBlockSprite GetBrickBlock(Vector2 pos, ArrayList items)
        {
            return new BrickBlockSprite(blockTextures[0], pos, items);
        }
        public HiddenBlockSprite GetHiddenBlock(Vector2 pos, ArrayList items)
        {
            return new HiddenBlockSprite(blockTextures[0], pos, items);
        }
        public QuestionBlockSprite GetQuestionBlock(Vector2 pos, ArrayList items)
        {
            return new QuestionBlockSprite(blockTextures[3], pos, items);
        }
        public UsedBlockSprite GetUsedBlock(Vector2 pos)
        {
            return new UsedBlockSprite(blockTextures[2], pos);
        }
        public FloorBlockSprite GetFloorBlock(Vector2 pos)
        {
            return new FloorBlockSprite(blockTextures[1], pos);
        }
        public StairBlockSprite GetStairBlock(Vector2 pos)
        {
            return new StairBlockSprite(blockTextures[4], pos);
        }

    }
}
