using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Sprint1.MarioClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint1
{
    class GamepadController : IController
    {
        private GamePadState prevGamePadState;
        private readonly Mario mario;
        private readonly Dictionary<Buttons, ICommand> controllerDic;
        private readonly Sprint1 Game;

        public GamepadController(Mario mario, Sprint1 game)
        {
            Game = game;
            this.mario = mario;
            controllerDic = new Dictionary<Buttons, ICommand>();
            prevGamePadState = GamePad.GetState(PlayerIndex.One);
            GetCommand();
        }
        public void GetCommand()
        {
            // Map KeyboardController keys and Game commands
            controllerDic.Add(Buttons.A, new MoveUpCommand(mario));
            controllerDic.Add(Buttons.DPadRight, new MoveRightCommand(mario));
            controllerDic.Add(Buttons.DPadLeft, new MoveLeftCommand(mario));
            controllerDic.Add(Buttons.DPadDown, new MoveDownCommand(mario));
            controllerDic.Add(Buttons.Start, new QuitCommand(Game));

        }
        public void Update()
        {
            GamePadState curr = GamePad.GetState(PlayerIndex.One);
            GamePadState emptyInput = new GamePadState();

            if (curr.IsConnected)
            {
                if (curr != emptyInput) // Button Pressed
                {
                    foreach (KeyValuePair<Buttons, ICommand> button in controllerDic)
                    {
                        if (ButtonPressed(button.Key, curr))
                        {
                            button.Value.Execute();
                        }
                    }
                    prevGamePadState = curr;
                }
            }
        }

        private bool ButtonPressed(Buttons b, GamePadState current)
        {
            return (current.IsButtonDown(b) && !prevGamePadState.IsButtonDown(b));
        }
    }

}

