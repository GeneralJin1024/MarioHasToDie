using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Sprint0.MarioClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0
{
    class KeyboardController : IController
    {
        private KeyboardState keyboardState;
        private Mario mario;
        private Dictionary<Keys, ICommand> controllerDic;
        ICommand currentCommand;
        private Sprint0 Game;

        public KeyboardController(Mario mario, Sprint0 game)
        {
            // KeyboardController set up
            this.mario = mario;
            controllerDic = new Dictionary<Keys, ICommand>();
            GetCommand();
            Game = game;
            Console.WriteLine("game: " + (Game == null));
        }

        public void GetCommand()
        {
            // Map KeyboardController keys and Game commands
            controllerDic.Add(Keys.W, new MoveUpCommand(mario));
            controllerDic.Add(Keys.Up, new MoveUpCommand(mario));
            controllerDic.Add(Keys.D, new MoveRightCommand(mario));
            controllerDic.Add(Keys.Right, new MoveRightCommand(mario));
            controllerDic.Add(Keys.S, new MoveDownCommand(mario));
            controllerDic.Add(Keys.Down, new MoveDownCommand(mario));
            controllerDic.Add(Keys.A, new MoveLeftCommand(mario));
            controllerDic.Add(Keys.Left, new MoveLeftCommand(mario));
            controllerDic.Add(Keys.Q, new QuitCommand(Game));

        }
        public void Update()
        {
            keyboardState = Keyboard.GetState();
            foreach (Keys key in keyboardState.GetPressedKeys())
            {
                if (controllerDic.ContainsKey(key))
                {
                    currentCommand = controllerDic[key];
                    currentCommand.Execute();
                }
            }
        }
    }
}

