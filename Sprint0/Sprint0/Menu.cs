using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Sprint0
{
    class Menu
    {
        private readonly Sprint0 Game;

        public Color FontColor { get; set; } = Color.White;
        private SpriteFont Font;
        private ISprite chooseSprite;

        private bool FirstChoose;
        private Vector2 CurrentChoose;

        private KeyboardState oldState;

        public Menu(Sprint0 game)
        {
            Game = game;
            FirstChoose = true;
            oldState = Keyboard.GetState();
        }


        public void LoadContent(SpriteFont font, Texture2D chooseSheet)
        {
            Font = font;
            chooseSprite = new AnimatedPlayerSprite(chooseSheet, new Point(1, 1));
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState newState = Keyboard.GetState();
            Keys[] pressedKeys = newState.GetPressedKeys();
            foreach (Keys key in pressedKeys)
            {
                if (oldState.IsKeyUp(key))
                {
                    if (key == Keys.W || key == Keys.Up || key == Keys.Down || key == Keys.S)
                        FirstChoose = !FirstChoose;
                }
                else if (key == Keys.Z)
                {
                    if (FirstChoose)
                        Game.MenuMode = false;
                    else
                        Game.Exit();
                }
            }
            if (FirstChoose)
                CurrentChoose = new Vector2(350.0f, 300.0f);
            else
                CurrentChoose = new Vector2(350.0f, 350.0f);

            oldState = newState;
            chooseSprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //GraphicsDevice.Clear(Color.CornflowerBlue);
            //GraphicsDevice.Clear(Color.Black);
            spriteBatch.DrawString(Font, "Welcome To Mario", new Vector2(190.0f, 150.0f), FontColor, 0,
                Vector2.Zero, 2.5f, SpriteEffects.None, 0);
            spriteBatch.DrawString(Font, "Start", new Vector2(370.0f, 300.0f), FontColor);
            spriteBatch.DrawString(Font, "Quit", new Vector2(370.0f, 350.0f), FontColor);
            spriteBatch.DrawString(Font, "W/Up Arrow: Up       S/Down Arrow: Down        Z: choose",
                new Vector2(0, 450), FontColor);
            chooseSprite.Draw(spriteBatch, CurrentChoose, false);
        }
    }
}
