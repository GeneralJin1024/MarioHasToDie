using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint0.BlockClasses;
using Sprint0.MarioClasses;

namespace Sprint0
{
    class KeyboardController : IController
    {
        private KeyboardState prevKeyboardState;
        //private enum PossibleCommands { StandingNoAnim, StandingAnim, MovingNoAnim, MovingAnim, Quit};
        private Dictionary<Keys, ICommand> keyMap;

        public KeyboardController(Sprint0 myGame, Bricks brick1)
        {
            prevKeyboardState = Keyboard.GetState();
            keyMap = new Dictionary<Keys, ICommand>();
            keyMap.Add(Keys.Q, new QuitGameCommand(myGame));
            keyMap.Add(Keys.A, new BBrickChangeState(brick1, null));
        }

        public void Update()
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
