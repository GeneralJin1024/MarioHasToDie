using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint1.Sprites;

namespace Sprint1.BlockClasses
{
    class BrickBlockSprite : Blocks
    {
        private float[] destroyedBrickPosX;
        private float[] destroyedBrickPosY;
        private Vector2 dPos;
        public BrickBlockSprite(Texture2D texture, MoveParameters moveParameters, ArrayList items, BlockType type) : base(texture, moveParameters, new Point(4, 1), type, items)
        {
            destroyedBrickPosX = new float[4];
            destroyedBrickPosY = new float[4];
        }
        public override void Update(float frameTime)
        {
            base.Update(frameTime);
            if (BType == BlockType.Destroyed)
            {
                dPos.X += positionOffset.X != 0 ? spriteSpeed.X * frameTime/3 : 0;
                dPos.Y += positionOffset.Y != 0 ? spriteSpeed.Y * frameTime/5 : 0;

                destroyedBrickPosX[0] = bPosition.X - dPos.X; destroyedBrickPosX[1] = bPosition.X - 2 * dPos.X;
                destroyedBrickPosX[2] = bPosition.X + dPos.X; destroyedBrickPosX[3] = bPosition.X + 2 * dPos.X;

                for (int i = 0; i < 4; i++)
                    destroyedBrickPosY[i] = bPosition.Y + dPos.Y;
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (BType == BlockType.Destroyed)
            {
                for (int index = 0; index < 4; index++)
                    spriteBatch.Draw(SpriteSheets, new Vector2(destroyedBrickPosX[index], destroyedBrickPosY[index]), new Rectangle(0, 0, (int)GetHeightAndWidth.Y / 2, (int)GetHeightAndWidth.X / 2), Color.White);
            }
            else
            {
                base.Draw(spriteBatch);
            }
            
        }
    }

    class QuestionBlockSprite : Blocks
    {
        public QuestionBlockSprite(Texture2D texture, MoveParameters moveParameters, ArrayList items) : base(texture, moveParameters, new Point(3, 4), BlockType.QNormal, items)
        {

        }

    }

    class UsedBlockSprite : Blocks
    {
        public UsedBlockSprite(Texture2D texture, MoveParameters moveParameters) : base(texture, moveParameters, new Point(4, 1), BlockType.Used, new ArrayList { })
        {
        }

    }

    class HiddenBlockSprite : BrickBlockSprite
    {
        public HiddenBlockSprite(Texture2D texture, MoveParameters moveParameters, ArrayList items) : base(texture, moveParameters, items, BlockType.Hidden)
        {
        }
    }
}
