using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using FinalSprint.MarioClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalSprint
{
    class GamepadController : IController
    {   
        // variables declarations
        private GamePadState prevGamePadState;
        private readonly MarioCharacter mario;
        private readonly Dictionary<Buttons, ICommand> controllerDicMove;
        private readonly Dictionary<Buttons, ICommand> controllerDic;
        private readonly Sprint5Main Game;
        private ICommand ReturnCommand;

        public GamepadController(MarioCharacter mario, Sprint5Main game)
        {
            // GamepadController set up
            Game = game;
            this.mario = mario;
            controllerDicMove = new Dictionary<Buttons, ICommand>();
            controllerDic = new Dictionary<Buttons, ICommand>();
            prevGamePadState = GamePad.GetState(PlayerIndex.One);
            GetCommand();
        }
        public void GetCommand()
        {
            ReturnCommand = new ReturnCommand(mario);
            // Map KeyboardController keys and Game commands
            controllerDicMove.Add(Buttons.A, new JumpHigherCommand(mario));
            controllerDicMove.Add(Buttons.DPadRight, new MoveRightCommand(mario));
            controllerDicMove.Add(Buttons.DPadLeft, new MoveLeftCommand(mario));
            controllerDicMove.Add(Buttons.DPadDown, new MoveDownCommand(mario));
            controllerDic.Add(Buttons.A, new MoveUpCommand(mario));
            controllerDic.Add(Buttons.Start, new QuitCommand(Game));
            controllerDic.Add(Buttons.Back, new ResetCommand());
            controllerDic.Add(Buttons.B, new ThrowFireCommand(mario));
           
        }
        public void Update()
        {
            bool hasMoving = false;
            GamePadState curr = GamePad.GetState(PlayerIndex.One);
            GamePadState emptyInput = new GamePadState();
            // check if the gamepad is connected 
            if (curr.IsConnected)
            {
                if (curr != emptyInput) // Button Pressed
                {
                    foreach (KeyValuePair<Buttons, ICommand> button in controllerDicMove)
                    {   
                        if (curr.IsButtonDown(button.Key))
                        {
                            // execute commands
                            button.Value.Execute();
                            hasMoving = true;
                        }
                    }
                    foreach (KeyValuePair<Buttons, ICommand> button in controllerDic)
                    {
                        if (ButtonPressed(button.Key,curr))
                        {
                            // execute commands
                            button.Value.Execute();
                        }
                    }
                }
                if (!hasMoving)
                    ReturnCommand.Execute();
                prevGamePadState = curr;
            }
        }
        // method to check the previous pressed buttons 
        private bool ButtonPressed(Buttons b, GamePadState current)
        {
            return (current.IsButtonDown(b) && !prevGamePadState.IsButtonDown(b));
        }
    }

}

