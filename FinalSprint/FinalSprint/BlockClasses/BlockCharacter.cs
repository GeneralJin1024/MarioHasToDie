﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FinalSprint.MarioClasses;

namespace FinalSprint.BlockClasses
{
    class BlockCharacter : ICharacter
    {
        private Blocks block;
        public MoveParameters Parameters { get; }
        public BlockType BlockType { get; private set; }
        public Sprint5Main.CharacterType Type { get; set; }
        public Vector2 GetHeightAndWidth { get { return block.GetHeightAndWidth; } }
        public Vector2 GetMaxPosition
        {
            get
            {
                if (block.BType != BlockType.Destroyed)
                    return new Vector2(block.Parameters.Position.X + block.GetHeightAndWidth.Y, block.Parameters.Position.Y);
                else
                    return new Vector2(0, 0);
            }
        }
        public Vector2 GetMinPosition
        {
            get
            {
                if (block.BType != BlockType.Destroyed)
                    return new Vector2(block.Parameters.Position.X, block.Parameters.Position.Y - block.GetHeightAndWidth.X);
                else
                    return new Vector2(0, 0);
            }
        }
        public BlockCharacter(Blocks block)
        {
            this.block = block;
            Type = Sprint5Main.CharacterType.Block;
            Parameters = block.Parameters;
            BlockType = block.BType;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            block.Draw(spriteBatch);
        }

        public void Update(float timeOfFrame)
        {
                block.Update(timeOfFrame);
        }

        public void MarioCollide(bool specialCase)
        {
            if (specialCase)
            {
                block.currentbState.Handle(block);
                BlockType = block.BType;
            }
        }

        public void BossEnemyCollide()
        {
            if (block.BType != BlockType.Indestructible)
                block.ChangeToDestroyed();
        }

        public void BlockCollide(bool isBottom)
        {
        }

        public void LoadItems(ArrayList items)
        {
            block.LoadItems(items);
        }
    }

}
