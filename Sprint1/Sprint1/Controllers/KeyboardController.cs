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
        // variables declarations
        private KeyboardState oldkeyboardState;
        private readonly MarioCharacter mario;
        /*private readonly Bricks[] blockList;*/
        private readonly Dictionary<Keys, ICommand> controllerDic;
        private readonly Dictionary<Keys, ICommand> controllerDicMove;
        private readonly Sprint1Main Game;
        private ICommand ReturnCommand;

        public KeyboardController(MarioCharacter mario, Sprint1Main game /*Bricks[] blockList*/)
        {
            // KeyboardController set up
            this.mario = mario;
            /*this.blockList = blockList;*/
            controllerDic = new Dictionary<Keys, ICommand>();
            controllerDicMove = new Dictionary<Keys, ICommand>();
            Game = game;
            GetCommand();
        }

        public void GetCommand()
        {
            ReturnCommand = new ReturnCommand(mario);
            // Map KeyboardController keys and Game commands
            controllerDicMove.Add(Keys.W, new JumpHigherCommand(mario));
            controllerDicMove.Add(Keys.Up, new JumpHigherCommand(mario));
            controllerDicMove.Add(Keys.Z, new JumpHigherCommand(mario));
            controllerDicMove.Add(Keys.D, new MoveRightCommand(mario));
            controllerDicMove.Add(Keys.Right, new MoveRightCommand(mario));
            controllerDicMove.Add(Keys.S, new MoveDownCommand(mario));
            controllerDicMove.Add(Keys.Down, new MoveDownCommand(mario));
            controllerDicMove.Add(Keys.A, new MoveLeftCommand(mario));
            controllerDicMove.Add(Keys.Left, new MoveLeftCommand(mario));
            controllerDic.Add(Keys.W, new MoveUpCommand(mario));
            controllerDic.Add(Keys.Up, new MoveUpCommand(mario));
            controllerDic.Add(Keys.Z, new MoveUpCommand(mario));
            controllerDic.Add(Keys.Y, new MoveStandardCommand(mario));
            controllerDic.Add(Keys.U, new MoveSuperCommand(mario));
            controllerDic.Add(Keys.I, new MoveFireCommand(mario));
            controllerDic.Add(Keys.O, new MoveDestroyCommand(mario));
            controllerDic.Add(Keys.Q, new QuitCommand(Game));
            controllerDic.Add(Keys.X, new ThrowFireCommand(mario));
            controllerDic.Add(Keys.R, new ResetCommand());
            controllerDic.Add(Keys.P, new PulseCommand(mario));
            controllerDic.Add(Keys.M, new MuteBGMCommand());
            controllerDic.Add(Keys.J, new BuySuperPowerUpCommand(mario));
            /*controllerDic.Add(Keys.B, new BlockCommands(blockList[2]));
            controllerDic.Add(Keys.H, new BlockCommands(blockList[1]));
            controllerDic.Add(Keys.OemQuestion, new BlockCommands(blockList[0]));*/

        }
        public void Update()
        {
            bool hasMoving = false;
            KeyboardState curr = Keyboard.GetState();
            foreach (Keys key in curr.GetPressedKeys())
            {   // check the previous pressed key and current pressed key
                if (controllerDicMove.ContainsKey(key))
                {
                    controllerDicMove[key].Execute(); hasMoving = true;
                }
                if (controllerDic.ContainsKey(key) && !oldkeyboardState.IsKeyDown(key))
                {
                    controllerDic[key].Execute();
                }
            }
            if (!hasMoving) // if no key with moving command is pressed, we should execute return command
                ReturnCommand.Execute();
            //update keyboard state.
            oldkeyboardState = curr;
        }
       
    }
}

