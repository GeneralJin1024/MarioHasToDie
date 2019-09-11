using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0
{
    class GamePadController : IController
    {
        private GamePadState prevGamePadState;
        private Sprint0 game;
        private Dictionary<Buttons, ICommand> buttonMap;
        private ICommand buttonStart;
        private ICommand buttonA;
        private ICommand buttonB;
        private ICommand buttonX;
        private ICommand buttonY;
        private ISprite marioStandingInPlaceSprite;
        private ISprite marioRunningInPlaceSprite;
        private ISprite marioDeadSprite;
        private ISprite marioRunningSprite;

        public GamePadController(Sprint0 game, ISprite marioSpriteSIP, ISprite marioSpriteRIP, ISprite marioSpriteDead, ISprite marioSpriteRunning)
        {
            this.game = game;
            this.marioStandingInPlaceSprite = marioSpriteSIP;
            this.marioRunningInPlaceSprite = marioSpriteRIP;
            this.marioDeadSprite = marioSpriteDead;
            this.marioRunningSprite = marioSpriteRunning;
            prevGamePadState = GamePad.GetState(PlayerIndex.One);
            buttonMap = new Dictionary<Buttons, ICommand>();
            buttonMap.Add(Buttons.Start, buttonStart);
            buttonMap.Add(Buttons.A, buttonA);
            buttonMap.Add(Buttons.B, buttonB);
            buttonMap.Add(Buttons.X, buttonX);
            buttonMap.Add(Buttons.Y, buttonY);
        }

        public void UpdateInput()
        {
            GamePadState curr = GamePad.GetState(PlayerIndex.One);
            GamePadState emptyInput = new GamePadState();

            if (curr.IsConnected)
            {
                if (curr != emptyInput) // Button Pressed
                {
                    foreach (KeyValuePair<Buttons, ICommand> button in buttonMap)
                    {
                        if (ButtonPressed(button.Key, curr))
                        {
                            switch (button.Key)
                            {
                                case Buttons.Start:
                                    this.buttonStart = new QuitGameCommand(this.game);
                                    this.buttonStart.Execute();
                                    break;
                                case Buttons.A:
                                    this.buttonA = new MarioStandingInPlaceCommand(this.marioStandingInPlaceSprite);
                                    this.buttonA.Execute();
                                    break;
                                case Buttons.B:
                                    this.buttonB = new MarioRunningInPlaceCommand(this.marioRunningInPlaceSprite);
                                    this.buttonB.Execute();
                                    break;
                                case Buttons.X:
                                    this.buttonX = new MarioDeadCommand(this.marioDeadSprite);
                                    this.buttonX.Execute();
                                    break;
                                case Buttons.Y:
                                    this.buttonY = new MarioRunningCommand(this.marioRunningSprite);
                                    this.buttonY.Execute();
                                    break;
                            }
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
