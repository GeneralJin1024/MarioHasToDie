using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0
{
    class KeyboardController : IController
    {
        private KeyboardState prevKeyboardState;
        //private enum PossibleCommands { StandingNoAnim, StandingAnim, MovingNoAnim, MovingAnim, Quit};
        private Dictionary<Keys, ICommand> keyMap;

        public KeyboardController(Sprint0 myGame, ISprite marioSpriteSIP, ISprite marioSpriteRIP, ISprite marioSpriteDead, ISprite marioSpriteRunning)
        {
            prevKeyboardState = Keyboard.GetState();
            keyMap = new Dictionary<Keys, ICommand>();
            keyMap.Add(Keys.Q, new QuitGameCommand(myGame));
        }

        public void UpdateInput()
        {
            KeyboardState curr = Keyboard.GetState();

            foreach (KeyValuePair<Keys, ICommand> key in keyMap)
            {
                if (keyPressed(key.Key, curr))
                {
                    key.Value.Execute();
                }
            }
            prevKeyboardState = curr;
        }

        private bool keyPressed(Keys k, KeyboardState current)
        {
            return (current.IsKeyDown(k) && !prevKeyboardState.IsKeyDown(k));
        }
    }
}
