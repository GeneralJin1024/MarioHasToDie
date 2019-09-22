using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.BlockClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.FactoryClasses
{
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

