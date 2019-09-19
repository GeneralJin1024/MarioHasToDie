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
        private Dictionary<Keys, Bricks> keyMap2;   
        public KeyboardController(Sprint0 myGame, Bricks normalBrick, Bricks hiddenBrick, Bricks questionBrick)
        {
            prevKeyboardState = Keyboard.GetState();
            keyMap = new Dictionary<Keys, ICommand>();
            keyMap.Add(Keys.Q, new QuitGameCommand(myGame));
            keyMap2 = new Dictionary<Keys, Bricks>();
            keyMap2.Add(Keys.A, normalBrick);
            keyMap2.Add(Keys.S, hiddenBrick);
            keyMap2.Add(Keys.D, questionBrick);
        }

        public void Update()
        {
            KeyboardState curr = Keyboard.GetState();

            foreach (KeyValuePair<Keys, Bricks> key in keyMap2)
            {
                if (keyPressed(key.Key, curr))
                {
                    key.Value.currentbState.Handle(key.Value);
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
