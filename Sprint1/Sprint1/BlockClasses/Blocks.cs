using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint1.FactoryClasses;
using Sprint1.ItemClasses;
using Sprint1.ItemEnemyClasses;
using Sprint1.Sprites;

namespace Sprint1.BlockClasses
{
    public enum BlockType
    {
        Hidden, BNormal, QNormal, Used, Destroyed, Indestructible // stairs and floors are counted as used blocks
    }
    //public for genernate blocks
    class Blocks : AnimatedSprite
    {
        public BlockType BType { get; protected set; }
        private readonly IBlockStates[] bStates;
        public IBlockStates currentbState;
        protected Vector2 bPosition;
        private bool IsBumping;
        private bool containItems;
        private readonly ArrayList items;
        private float MinY, MaxY;
        protected Point positionOffset = new Point(1, 1);
        protected Vector2 spriteSpeed = new Vector2(50.0f, 60.0f);
        public Blocks(Texture2D sheet, MoveParameters moveParameters, Point rowAndColumn, BlockType type, ArrayList itemList) 
            : base(sheet, rowAndColumn, moveParameters)
        {
            BType = type;
            items = itemList;
            containItems = itemList.Count != 0 ? true : false;
            bPosition = moveParameters.Position;
            bStates = new IBlockStates[4] { new HiddenState(), new NormalState(), new BumpingState(), new UsedOrDestroyedState() };
            currentbState = GenerateCurrentState();
            IsBumping = false;
        }

        private IBlockStates GenerateCurrentState()
        {
            switch (BType)
            {
                case BlockType.Hidden:
                    return bStates[0];
                case BlockType.Used:
                case BlockType.Indestructible:
                case BlockType.Destroyed:
                    return bStates[3];
                default:
                    return bStates[1];
            }
        }
        #region CommandReciver
        public void ChangeToBrick()
        {
            BType = BlockType.BNormal;
            //Console.WriteLine("change to Bnormal");
            SpriteSheets = BlockFactory.BlockTextures[0];
            currentbState = GenerateCurrentState();
        }
        private void ChangeToUsed()
        {
            BType = BlockType.Used;
            base.ResizeFrame(BlockFactory.BlockTextures[2], new Point(4, 1), 0);          
            currentbState = GenerateCurrentState();
        }
        public void ChangeToDestroyed()
        {
            BType = BlockType.Destroyed;
            currentbState = GenerateCurrentState();
        }
        public void Bumping()
        {
            IsBumping = true;
            currentbState = bStates[2];
            MinY = Parameters.Position.Y - base.GetHeightAndWidth.X;
            MaxY = Parameters.Position.Y; SoundFactory.Instance.HitQuestionBlock();
        }
        #endregion
        public override void Update(float frameTime)
        {
            //foreach (AnimatedSprite sprite in shownItems)
            //sprite.Update(frameTime);
            base.Update(frameTime);   
            if (IsBumping)
            {                
                bPosition.Y -= positionOffset.Y != 0 ? spriteSpeed.Y * frameTime : 0;
                if (bPosition.Y < MinY)
                {
                    bPosition.Y = MinY;
                    if (containItems)
                    {
                        //Console.WriteLine("item bumps");
                        DiscloseItem();
                        RemoveItem();
                    }
                    else if (BType == BlockType.QNormal) ChangeToUsed();
                    spriteSpeed.Y *= -1;                   
                }
                if (bPosition.Y > MaxY)
                {
                    IsBumping = false;
                    currentbState = GenerateCurrentState();
                    spriteSpeed.Y *= -1;
                    bPosition.Y = MaxY;
                }
                Parameters.SetPosition(Parameters.Position.X, bPosition.Y);
            }
            if (BType == BlockType.Destroyed && containItems)
            {
                DiscloseItem();
                RemoveItem();
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            //foreach (ICharacter sprite in shownItems)
            //    sprite.Draw(spriteBatch);
            if (BType != BlockType.Hidden && BType != BlockType.Destroyed)
            {
                base.Draw(spriteBatch);
            }
        }
        #region Methods Interact Items
        private void RemoveItem()
        {
            items.RemoveAt(0);
            if (items.Count == 0)
            {
                containItems = false;
                if (BType != BlockType.Destroyed)
                    ChangeToUsed();
            }
        }

        internal void LoadItems(ArrayList itemsList)
        {            
            items.AddRange(itemsList);
            containItems = items.Count != 0 ? true : false;
        }

        private void DiscloseItem()
        {
            ItemCharacter item = (ItemCharacter)items[0];
            ItemBumpingCommands bumpItem;
            SoundFactory.Instance.BumpItems();
            if (item is RandomItemCharacter || item is JumpMedicineCharacter)
            {
                item.Bumping(bPosition, bPosition.Y, bPosition.Y, spriteSpeed);
            }
            else
            {
                switch (item.Type)
                {
                    case Sprint1Main.CharacterType.Coin:
                        bumpItem = new CoinBumping(item, bPosition, bPosition.Y - 3.0f * this.GetHeightAndWidth.X, bPosition.Y, spriteSpeed);
                        bumpItem.HandleBumping();
                        break;
                    case Sprint1Main.CharacterType.Flower:
                        bumpItem = new FlowerBumping(item, bPosition, bPosition.Y, bPosition.Y, spriteSpeed);
                        bumpItem.HandleBumping();
                        break;
                    case Sprint1Main.CharacterType.RedMushroom:
                    case Sprint1Main.CharacterType.GreenMushroom:
                        bumpItem = new MushroomBumping(item, bPosition, bPosition.Y - 1.0f * this.GetHeightAndWidth.X, bPosition.Y, spriteSpeed);
                        bumpItem.HandleBumping();
                        break;
                    case Sprint1Main.CharacterType.Star:
                        bumpItem = new StarBumping(item, bPosition, bPosition.Y - 2.0f * this.GetHeightAndWidth.X, bPosition.Y, spriteSpeed);
                        bumpItem.HandleBumping();
                        break;
                }
            }
        }
        #endregion

    }
}
