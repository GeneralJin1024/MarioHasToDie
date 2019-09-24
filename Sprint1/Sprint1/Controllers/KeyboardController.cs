using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Sprint1.BlockClasses;
using Sprint1.MarioClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint1
{
    class KeyboardController : IController
    {
        private KeyboardState oldkeyboardState;
        private readonly Mario mario;
        private readonly Bricks[] blockList;
        private readonly Dictionary<Keys, ICommand> controllerDic;
        private readonly Sprint1 Game;

        public KeyboardController(Mario mario, Sprint1 game, Bricks[] blockList)
        {
            // KeyboardController set up
            this.mario = mario;
            this.blockList = blockList;
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
            controllerDic.Add(Keys.B, new BlockCommands(blockList[2]));
            controllerDic.Add(Keys.H, new BlockCommands(blockList[1]));
            controllerDic.Add(Keys.OemQuestion, new BlockCommands(blockList[0]));

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

