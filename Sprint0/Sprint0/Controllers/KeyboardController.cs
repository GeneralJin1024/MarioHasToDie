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
        private KeyboardState oldkeyboardState;
        private Mario mario;
        private Dictionary<Keys, ICommand> controllerDic;
        private Sprint0 Game;

        public KeyboardController(Mario mario, Sprint0 game)
        {
            // KeyboardController set up
            this.mario = mario;
            controllerDic = new Dictionary<Keys, ICommand>();
            Game = game;
            GetCommand();
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
            controllerDic.Add(Keys.Y, new MoveStandardCommand(mario));
            controllerDic.Add(Keys.U, new MoveSuperCommand(mario));
            controllerDic.Add(Keys.I, new MoveFireCommand(mario));
            controllerDic.Add(Keys.O, new MoveDestroyCommand(mario));
            controllerDic.Add(Keys.Q, new QuitCommand(Game));

        }
        public void Update()
        {
            KeyboardState curr = Keyboard.GetState();
                foreach (Keys key in curr.GetPressedKeys())
                {
                    if (controllerDic.ContainsKey(key) && !oldkeyboardState.IsKeyDown(key))
                    {
                        controllerDic[key].Execute();
                    }
                }
            oldkeyboardState = curr;
        }
       
    }
}

