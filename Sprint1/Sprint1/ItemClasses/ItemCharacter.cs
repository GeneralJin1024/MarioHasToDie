
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Sprint1.MarioClasses;
using Sprint1.Sprites;

namespace Sprint1.ItemClasses
{
   abstract class ItemCharacter : ICharacter
    {

       
       public abstract Sprint1Main.CharacterType Type { get; set; }
      
        protected readonly ItemSprite item;
        public MoveParameters Parameters { get; }
        public ItemCharacter(Texture2D texture, Point rowsAndColumns, Vector2 location)
        {
            item = new ItemSprite(texture, location, rowsAndColumns);
            Parameters = item.Parameters;
        }

        public void Update(float timeOfFrame) { item.Update(timeOfFrame); }
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

     
        public abstract void MarioCollide(bool specialCase);
        public abstract Vector2 GetHeightAndWidth();
    }
}
