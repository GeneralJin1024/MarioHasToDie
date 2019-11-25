using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint1.ItemClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint1.ItemClasses
{
    class FlagCharacter : ItemCharacter
    {
        private Boolean isAnimating = false;
        private AnimatedSprite flag;
        public override Vector2 GetHeightAndWidth { get { return Item.GetHeightAndWidth; } }
        public override Sprint1Main.CharacterType Type { get; set; } = Sprint1Main.CharacterType.Flag;

        public FlagCharacter(Texture2D texture, Point rowsAndColumns, Vector2 location): base(texture, rowsAndColumns,location)
        {
            flag = new AnimatedSprite(Sprint1Main.Game.Content.Load<Texture2D>("ItemSprite/flag"), new Point(1, 1), new MoveParameters(false));
            flag.Parameters.SetPosition(location.X, location.Y);
            flag.Parameters.SetVelocity(0, 0);
            isAnimating = false;
        }

        public override void MarioCollide(bool specialCase)
        {
            isAnimating = true;
        }

        public override void Update(float timeOfFrame)
        {
            base.Update(timeOfFrame);
            if (isAnimating)
            {
                flag.Parameters.SetPosition(flag.Parameters.Position.X, flag.Parameters.Position.Y + 20.0f * timeOfFrame / 5);
                if (flag.Parameters.Position.Y - Item.Parameters.Position.Y >= 120)
                {
                    isAnimating = false;
                    flag.Parameters.SetPosition(flag.Parameters.Position.X, Parameters.Position.Y + Item.GetHeightAndWidth.X - 48);
                }
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            flag.Draw(spriteBatch);
        }
    }
}
