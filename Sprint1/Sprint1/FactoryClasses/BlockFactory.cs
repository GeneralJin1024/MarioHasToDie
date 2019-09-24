using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint1.BlockClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint1.FactoryClasses
{
    class BlockFactory:IFactory
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

        public static Texture2D[] BlockTextures
        {
            get
            {
                if (blockTextures == null)
                {
                    LoadTexture();
                }
                return blockTextures;
            }
        }
        private static Texture2D[] blockTextures;
        public Bricks qBlockTest;
        public Bricks hitBlockTest;
        public Bricks hiddenBlockTest;
        public Bricks brickBlockTest;
        public Blocks floorBlockTest;
        public Blocks stairBlockTest;
        public BlockFactory()
        {
            //when factory initialzed, load the texture
            LoadTexture();
        }
        private static void LoadTexture()
        {
            blockTextures = new Texture2D[5] {Sprint1.Game.Content.Load<Texture2D>("BlockSprites/mario-brick-blocks"),
                Sprint1.Game.Content.Load<Texture2D>("BlockSprites/mario-gravel-blocks"),
                Sprint1.Game.Content.Load<Texture2D>("BlockSprites/mario-hit-block"),
                Sprint1.Game.Content.Load<Texture2D>("BlockSprites/mario-question-blocks"),
                Sprint1.Game.Content.Load<Texture2D>("BlockSprites/mario-shiny-block")};
        }
        public void AddToList(ArrayList spriteList)
        {
            //initialize the sprites and add the sprites to the list
            float x = Sprint1.Game.GraphicsDevice.Viewport.Width / 8;
            float y = Sprint1.Game.GraphicsDevice.Viewport.Height / 2;
            qBlockTest = BlockFactory.Instance.GetQuestionBlock(new Vector2(x, y), new ArrayList { "redMushroom" });
            hitBlockTest = BlockFactory.Instance.GetUsedBlock(new Vector2(2 * x, y));
            hiddenBlockTest = BlockFactory.Instance.GetHiddenBlock(new Vector2(3 * x, y), new ArrayList { });
            floorBlockTest = BlockFactory.Instance.GetFloorBlock(new Vector2(4 * x, y));
            stairBlockTest = BlockFactory.Instance.GetStairBlock(new Vector2(5 * x, y));
            brickBlockTest = BlockFactory.Instance.GetBrickBlock(new Vector2(6 * x, y), new ArrayList { "coin", "coin" });
            spriteList.Add(qBlockTest);
            spriteList.Add(hiddenBlockTest);
            spriteList.Add(brickBlockTest);
            spriteList.Add(hitBlockTest);
            spriteList.Add(floorBlockTest);
            spriteList.Add(stairBlockTest);
        }
        public BrickBlockSprite GetBrickBlock(Vector2 pos, ArrayList items)
        {
            return new BrickBlockSprite(BlockTextures[0], pos, items);
        }
        public HiddenBlockSprite GetHiddenBlock(Vector2 pos, ArrayList items)
        {
            return new HiddenBlockSprite(BlockTextures[0], pos, items);
        }
        public QuestionBlockSprite GetQuestionBlock(Vector2 pos, ArrayList items)
        {
            return new QuestionBlockSprite(BlockTextures[3], pos, items);
        }
        public UsedBlockSprite GetUsedBlock(Vector2 pos)
        {
            return new UsedBlockSprite(BlockTextures[2], pos);
        }
        public FloorBlockSprite GetFloorBlock(Vector2 pos)
        {
            return new FloorBlockSprite(BlockTextures[1], pos);
        }
        public StairBlockSprite GetStairBlock(Vector2 pos)
        {
            return new StairBlockSprite(BlockTextures[4], pos);
        }

    }
}

