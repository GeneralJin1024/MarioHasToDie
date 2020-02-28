using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FinalSprint.BlockClasses;
using System.Collections;
using System.Collections.Generic;

namespace FinalSprint.FactoryClasses
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
            blockTextures = new Texture2D[5] {Sprint5Main.Game.Content.Load<Texture2D>("BlockSprites/mario-brick-blocks"),
                Sprint5Main.Game.Content.Load<Texture2D>("BlockSprites/mario-gravel-blocks"),
                Sprint5Main.Game.Content.Load<Texture2D>("BlockSprites/mario-hit-block"),
                Sprint5Main.Game.Content.Load<Texture2D>("BlockSprites/mario-question-blocks"),
                Sprint5Main.Game.Content.Load<Texture2D>("BlockSprites/mario-shiny-block")};
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
        public BlockCharacter GetDestructibleStairBlock(MoveParameters moveParameters)
        {
            return new BlockCharacter(new BrickBlockSprite(BlockTextures[4], moveParameters, new ArrayList(), BlockType.Used));
        }
        public ArrayList FactoryMethod(string name, Vector2 posS, Vector2 posE)
        {
            ArrayList list = new ArrayList();
            for (int x = 0; x < (posE.X - posS.X) / 16; x++)
            {
                for (int y = 0; y < (posE.Y - posS.Y) / 16; y++)
                {
                    Vector2 pos = new Vector2(posS.X + x * 16, posS.Y + y * 16);
                    MoveParameters parameters = new MoveParameters(false);
                    parameters.SetPosition(pos.X, pos.Y);
                    switch (name)
                    {
                        case "Brick":
                            list.Add(GetBrickBlock(parameters, new ArrayList()));
                            break;
                        case "QuestionB":
                            list.Add(GetQuestionBlock(parameters, new ArrayList()));
                            break;
                        case "HiddenB":
                            list.Add(GetHiddenBlock(parameters, new ArrayList()));
                            break;
                        case "UsedB":
                            list.Add(GetUsedBlock(parameters));
                            break;
                        case "FloorB":
                            list.Add(GetFloorBlock(parameters));
                            break;
                        case "StairB":
                            list.Add(GetStairBlock(parameters));
                            break;
                        case "DestructibleStairB":
                            list.Add(GetDestructibleStairBlock(parameters));
                            break;
                        default: break;
                    }
                }
            }
            return list;
        }

        public ArrayList FactoryMethod(string name, Vector2 pos)
        {
            //won't get called
            return new ArrayList();
        }
    }
}

