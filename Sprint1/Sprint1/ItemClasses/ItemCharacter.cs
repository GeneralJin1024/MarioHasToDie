using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Sprint1.MarioClasses;
using Sprint1.Sprites;

namespace Sprint1.ItemClasses
{
   abstract class ItemCharacter : ICharacter
    {
        protected readonly ItemSprite item;
        public MoveParameters Parameters { get; }
        public ItemCharacter(Texture2D texture, Point rowsAndColumns, Vector2 location)
        {
            item = new ItemSprite(texture, location, rowsAndColumns);
            Parameters = item.Parameters;
        }

        public void Update(GameTime gameTime) { item.Update(gameTime); }
        public void Draw(SpriteBatch spriteBatch)
        {
            item.Draw(spriteBatch);
        }
        public Vector2 GetMaxPosition()
        {
            return new Vector2(Parameters.Position.X + item.GetHeightAndWidth().Y, Parameters.Position.Y);
        }
        public Vector2 GetMinPosition()
        {
            return new Vector2(Parameters.Position.X, Parameters.Position.Y - item.GetHeightAndWidth().X);
        }

        public abstract void MarioCollideBottom(MarioCharacter mario);

        public abstract void MarioCollideLeft(MarioCharacter mario);

        public abstract void MarioCollideRight(MarioCharacter mario);

        public abstract void MarioCollideTop(MarioCharacter mario);



    }
}
