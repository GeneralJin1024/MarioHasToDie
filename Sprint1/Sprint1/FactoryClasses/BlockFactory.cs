using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint1.BlockClasses;
using System.Collections;

namespace Sprint1.FactoryClasses
{
    class BlockFactory : IFactory
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
        public BlockFactory()
        {
            //when factory initialzed, load the texture
            LoadTexture();
        }
        private static void LoadTexture()
        {
            blockTextures = new Texture2D[5] {Sprint1Main.Game.Content.Load<Texture2D>("BlockSprites/mario-brick-blocks"),
                Sprint1Main.Game.Content.Load<Texture2D>("BlockSprites/mario-gravel-blocks"),
                Sprint1Main.Game.Content.Load<Texture2D>("BlockSprites/mario-hit-block"),
                Sprint1Main.Game.Content.Load<Texture2D>("BlockSprites/mario-question-blocks"),
                Sprint1Main.Game.Content.Load<Texture2D>("BlockSprites/mario-shiny-block")};
        }
        
        public BlockCharacter GetBrickBlock(MoveParameters moveParameters, ArrayList items)
        {
            return new BlockCharacter(new BrickBlockSprite(BlockTextures[0], moveParameters, items, BlockType.BNormal));
        }
        public BlockCharacter GetHiddenBlock(MoveParameters moveParameters, ArrayList items)
        {
            return new BlockCharacter(new HiddenBlockSprite(BlockTextures[0], moveParameters, items));
        }
        public BlockCharacter GetQuestionBlock(MoveParameters moveParameters, ArrayList items)
        {
            return new BlockCharacter(new QuestionBlockSprite(BlockTextures[3], moveParameters, items));
        }
        public BlockCharacter GetUsedBlock(MoveParameters moveParameters)
        {
            return new BlockCharacter(new UsedBlockSprite(BlockTextures[2], moveParameters));
        }
        public BlockCharacter GetFloorBlock(MoveParameters moveParameters)
        {
            return new BlockCharacter(new FloorBlockSprite(BlockTextures[1], moveParameters));
        }
        public BlockCharacter GetStairBlock(MoveParameters moveParameters)
        {
            return new BlockCharacter(new StairBlockSprite(BlockTextures[4], moveParameters));
        }

        public ICharacter FactoryMethod(string name, Vector2 pos)
        {
            MoveParameters parameters = new MoveParameters(false);
            parameters.SetPosition(pos.X, pos.Y);
            switch(name)
            {
                case "Brick":
                    return GetBrickBlock(parameters, new ArrayList());
                case "QuestionB":
                    return GetQuestionBlock(parameters, new ArrayList());
                case "HiddenB":
                    return GetHiddenBlock(parameters, new ArrayList());
                case "UsedB":
                    return GetUsedBlock(parameters);
                case "FloorB":
                    return GetFloorBlock(parameters);
                case "StairB":
                    return GetStairBlock(parameters);
                default : return new NullCharacter();
            }
        }
    }
}

