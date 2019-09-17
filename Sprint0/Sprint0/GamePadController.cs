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
        private Dictionary<Buttons, ICommand> buttonMap;

        public GamePadController(Sprint0 myGame, ISprite marioSpriteSIP, ISprite marioSpriteRIP, ISprite marioSpriteDead, ISprite marioSpriteRunning)
        {
            prevGamePadState = GamePad.GetState(PlayerIndex.One);
            buttonMap = new Dictionary<Buttons, ICommand>();
            buttonMap.Add(Buttons.Start, new QuitGameCommand(myGame));
        }

        public void Update()
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
