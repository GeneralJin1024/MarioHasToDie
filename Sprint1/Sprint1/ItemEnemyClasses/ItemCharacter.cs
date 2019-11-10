﻿
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Sprint1.MarioClasses;
using Sprint1.Sprites;
using System;
using Sprint1.LevelLoader;
using Sprint1.ItemEnemyClasses;

namespace Sprint1.ItemClasses
{
    public class ItemCharacter : ICharacter
    {


        public virtual Sprint1Main.CharacterType Type { get; set; }
        public virtual Vector2 GetMinPosition
        {
            get { return new Vector2(Parameters.Position.X, Parameters.Position.Y - Item.GetHeightAndWidth.X); }
        }
        public virtual Vector2 GetMaxPosition
        {
            get { return new Vector2(Parameters.Position.X + Item.GetHeightAndWidth.Y, Parameters.Position.Y); }
        }
        public virtual Vector2 GetHeightAndWidth
        {
            get { return Vector2.Zero; }
        }
        protected bool IsBump { get; set; }
        protected float BumpHigh { get; set; }
        protected float BumpLow { get; set; }
        protected Vector2 SpriteSpeed { get; set; }
        private Point positionOffset;
        protected ItemSprite Item { get; }
        public MoveParameters Parameters { get; }
        private readonly MoveParameters InitialParameter;
        public ItemCharacter(Texture2D texture, Point rowsAndColumns, Vector2 location)
        {
            Item = new ItemSprite(texture, location, rowsAndColumns);
            Parameters = Item.Parameters;
            InitialParameter = new MoveParameters(false);
            Scene.CopyDataOfParameter(Parameters, InitialParameter);
            IsBump = false;
            BumpHigh = 0;
            BumpLow = Sprint1Main.Game.GraphicsDevice.Viewport.Height;
        }

        public virtual void Update(float timeOfFrame)
        {
            Item.Update(timeOfFrame);
            if (!Parameters.IsHidden && IsBump)
            {
                //WARNING:Check next Sprint.
                Parameters.SetPosition(Parameters.Position.X, Parameters.Position.Y - (positionOffset.Y != 0 ? SpriteSpeed.Y * timeOfFrame/10 : 0));
                //if item is at the height, set bump to false.
                if (Parameters.Position.Y < BumpHigh)
                {
                    Parameters.SetPosition(Parameters.Position.X, BumpHigh);
                    SpriteSpeed = new Vector2(SpriteSpeed.X, SpriteSpeed.Y * -1);
                    //SpriteSpeed.Y *= -1;
                }
                if (Parameters.Position.Y > BumpLow)
                {
                    IsBump = false;
                    Parameters.HasGravity = true;
                    Parameters.SetPosition(Parameters.Position.X, BumpLow);
                }
            }
            if (Parameters.Position.Y >= Stage.Boundary.X || Parameters.Position.Y >= Stage.Boundary.Y)
                Parameters.IsHidden = true;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            //Console.WriteLine(item.Parameters.IsHidden + "   Position = " + Parameters.Position);
            Item.Draw(spriteBatch);
        }
        //public virtual Vector2 GetMaxPosition()
        //{
        //    return new Vector2(Parameters.Position.X + Item.GetHeightAndWidth.Y, Parameters.Position.Y);
        //}
        //public virtual Vector2 GetMinPosition()
        //{
        //    return new Vector2(Parameters.Position.X, Parameters.Position.Y - Item.GetHeightAndWidth.X);
        //}

        public void Bumping(Vector2 startP, float minY, float maxY, Vector2 blockSpeed)
        {
            //set the item's position to startP, and set the bump height and speed
            positionOffset = new Point(0, 1);
            SpriteSpeed = new Vector2(0, blockSpeed.Y * 1.2f);
            Parameters.SetPosition(startP.X, startP.Y);
            Parameters.IsHidden = false;
            BumpHigh = minY;
            BumpLow = maxY;
            IsBump = true;
        }

        public virtual void MarioCollide(bool specialCase) { }
       // public virtual Vector2 GetHeightAndWidth() { return Vector2.Zero; }
        public virtual void BlockCollide(bool isBottom)
        {
            if (isBottom)
                Parameters.SetVelocity(Math.Abs(Parameters.Velocity.X), 0);
            else
            {
                Parameters.IsLeft = !Parameters.IsLeft; //转向
                Parameters.SetVelocity(Math.Abs(Parameters.Velocity.X), Parameters.Velocity.Y);
                //Parameters.SetVelocity(0, 0);
                //Parameters.HasGravity = false;
            }
        }
    }
}
