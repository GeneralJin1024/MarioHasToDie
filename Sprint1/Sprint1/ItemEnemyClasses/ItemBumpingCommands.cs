using Microsoft.Xna.Framework;
using Sprint1.ItemClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint1.ItemEnemyClasses
{
    interface ItemBumpingCommands
    {
        void HandleBumping();
    }
    class CoinBumping : ItemBumpingCommands
    {
        private CoinCharacter item;
        private float minY;
        private float startHeight;
        private Vector2 speed;
        private Vector2 startPos;
        public CoinBumping(ItemCharacter item, Vector2 startPos, float minY, float startHeight, Vector2 speed)
        {
            this.item = (CoinCharacter)item;
            this.minY = minY;
            this.startHeight = startHeight;
            this.speed = speed;
            this.startPos = startPos;
        }

        public void HandleBumping()
        {
            item.Bumping(startPos, minY, startHeight, speed);
        }
    }

    class MushroomBumping : ItemBumpingCommands
    {
        private ItemCharacter item;
        private float minY;
        private float startHeight;
        private Vector2 speed;
        private Vector2 startPos;
        public MushroomBumping(ItemCharacter item, Vector2 startPos, float minY, float startHeight, Vector2 speed)
        {
            this.item = item;
            this.minY = minY;
            this.startHeight = startHeight;
            this.speed = speed;
            this.startPos = startPos;
        }

        public void HandleBumping()
        {
            item.Bumping(startPos, minY, startHeight, speed);
        }
    }

    class StarBumping : ItemBumpingCommands
    {
        private StarCharacter item;
        private float minY;
        private float startHeight;
        private Vector2 speed;
        private Vector2 startPos;
        public StarBumping(ItemCharacter item, Vector2 startPos, float minY, float startHeight, Vector2 speed)
        {
            this.item = (StarCharacter)item;
            this.minY = minY;
            this.startHeight = startHeight;
            this.speed = speed;
            this.startPos = startPos;
        }

        public void HandleBumping()
        {
            item.Bumping(startPos, minY, startHeight, speed);
        }
    }

    class FlowerBumping : ItemBumpingCommands
    {
        private FlowerCharacter item;
        private float minY;
        private float startHeight;
        private Vector2 speed;
        private Vector2 startPos;
        public FlowerBumping(ItemCharacter item, Vector2 startPos, float minY, float startHeight, Vector2 speed)
        {
            this.item = (FlowerCharacter)item;
            this.minY = minY;
            this.startHeight = startHeight;
            this.speed = speed;
            this.startPos = startPos;
        }

        public void HandleBumping()
        {
            item.Bumping(startPos, minY, startHeight, speed);
        }
    }
}
