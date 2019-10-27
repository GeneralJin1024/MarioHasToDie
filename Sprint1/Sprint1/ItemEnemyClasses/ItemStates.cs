using Microsoft.Xna.Framework;
using Sprint1.ItemClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint1.ItemEnemyClasses
{
    interface ItemStates
    {
        void HandleBumping();
    }
    class CoinBumping : ItemStates
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

    class MushroomBumping : ItemStates
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

    class StarBumping : ItemStates
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

    class FlowerBumping : ItemStates
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
