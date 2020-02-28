using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;

[assembly: NeutralResourcesLanguage("en-US")]
namespace FinalSprint
{
    class Menu
    {
        private readonly Sprint5Main Game;

        private Color FontColor = Color.White;//use white font when background is black
        private SpriteFont Font;
        private ISprite chooseSprite; //the replacement of mouse is an idle standard mario

        private bool FirstChoose; //since there are only start and quit, use a bool to determine where the choose sprite is.

        private KeyboardState OldKeyState; //keyboard state of
        private GamePadState OldPadState;
        private readonly string[] Content;
        public Menu(Sprint5Main game, string[] content)
        {
            Game = game;
            FirstChoose = true; // mouse stop at first choice first.
            // read keyboard and gamepad state
            OldKeyState = Keyboard.GetState();
            OldPadState = GamePad.GetState(PlayerIndex.One);
            //set up resource manager for eliminating warning
            Content = content;
        }


        public void LoadContent(SpriteFont font)
        {
            Font = font; //set font
            //set "mouse"
            chooseSprite = new AnimatedSprite(
                Game.Content.Load<Texture2D>("MarioSprites/smallMarioRightStand"), new Point(1, 1), new MoveParameters(false));
        }

        public void Update(float timeOfFrame)
        {
            /*
             * Read keyboard and execute commands.
             */
            #region Keyboard
            KeyboardState newKeyState = Keyboard.GetState();
            Keys[] pressedKeys = newKeyState.GetPressedKeys();
            foreach (Keys key in pressedKeys)
            {
                if (OldKeyState.IsKeyUp(key))
                {
                    if (key == Keys.W || key == Keys.Up || key == Keys.Down || key == Keys.S)
                        FirstChoose = !FirstChoose;
                }
                else if ( key == Keys.Enter )
                {
                    if (FirstChoose)
                    {
                        Game.LevelControl.ChangeToNormalMode();
                    }
                    else
                        Game.Exit();
                }
            }
            OldKeyState = newKeyState;
            #endregion
            /*
             * Read gamepad and execute commands.
             */
            #region GamePad
            GamePadState newPadState = GamePad.GetState(PlayerIndex.One);
            GamePadState emptyInput = new GamePadState();
            if(newPadState.IsConnected && newPadState != emptyInput)
            {
                if(CheckPressedButtons(Buttons.DPadDown, newPadState) || CheckPressedButtons(Buttons.DPadUp, newPadState))
                    FirstChoose = !FirstChoose;
                if(CheckPressedButtons(Buttons.X, newPadState))
                {
                    if (FirstChoose)
                        Game.LevelControl.ChangeToNormalMode();
                    else
                        Game.Exit();
                }
            }
            OldPadState = newPadState;
            #endregion
            //changing mouse position if the user want to choose another choice.
            if (FirstChoose)
                chooseSprite.Parameters.SetPosition(350, 300 + chooseSprite.GetHeightAndWidth.X);
            else
                chooseSprite.Parameters.SetPosition(350, 350 + chooseSprite.GetHeightAndWidth.X);

            chooseSprite.Update(timeOfFrame);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Font, Content[0], new Vector2(400 - Content[0].Length * 13, 150.0f),
                FontColor, 0, Vector2.Zero, 2.5f, SpriteEffects.None, 0);
            spriteBatch.DrawString(Font, Content[(!Sprint5Main.Game.LevelControl.IsLastLevel && Content.Length == 4) ? 3 : 1], 
                new Vector2(370.0f, 300.0f), FontColor);
            spriteBatch.DrawString(Font, Content[2], new Vector2(370.0f, 350.0f), FontColor);
            //Draw "mouse"
            chooseSprite.Draw(spriteBatch);
        }

        private bool CheckPressedButtons(Buttons button, GamePadState newPadState)
        {
            return newPadState.IsButtonDown(button) && OldPadState.IsButtonUp(button);
        }
    }
}
