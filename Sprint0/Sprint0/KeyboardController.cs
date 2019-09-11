using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0
{
    class KeyboardController : IController
    {
        private KeyboardState prevKeyboardState;
        private Sprint0 game;
        //private enum PossibleCommands { StandingNoAnim, StandingAnim, MovingNoAnim, MovingAnim, Quit};
        private Dictionary<Keys, ICommand> keyMap;
        private ICommand keyQ;
        private ICommand keyW;
        private ICommand keyE;
        private ICommand keyR;
        private ICommand keyT;
        private ISprite marioStandingInPlaceSprite;
        private ISprite marioRunningInPlaceSprite;
        private ISprite marioDeadSprite;
        private ISprite marioRunningSprite;

        public KeyboardController(Sprint0 myGame, ISprite marioSpriteSIP, ISprite marioSpriteRIP, ISprite marioSpriteDead, ISprite marioSpriteRunning)
        {
            this.game = myGame;
            this.marioStandingInPlaceSprite = marioSpriteSIP;
            this.marioRunningInPlaceSprite = marioSpriteRIP;
            this.marioDeadSprite = marioSpriteDead;
            this.marioRunningSprite = marioSpriteRunning;
            prevKeyboardState = Keyboard.GetState();
            keyMap = new Dictionary<Keys, ICommand>();
            keyMap.Add(Keys.Q, this.keyQ);
            keyMap.Add(Keys.W, this.keyW);
            keyMap.Add(Keys.E, this.keyE);
            keyMap.Add(Keys.R, this.keyR);
            keyMap.Add(Keys.T, this.keyT);
        }

        public void UpdateInput()
        {
            KeyboardState curr = Keyboard.GetState();

            foreach (KeyValuePair<Keys, ICommand> key in keyMap)
            {
                if (keyPressed(key.Key, curr))
                {
                    switch (key.Key)
                    {
                        case Keys.Q:
                            this.keyQ = new QuitGameCommand(this.game);
                            this.keyQ.Execute();
                            break;
                        case Keys.W:
                            this.keyW = new MarioStandingInPlaceCommand(this.marioStandingInPlaceSprite);
                            this.keyW.Execute();
                            break;
                        case Keys.E:
                            this.keyE = new MarioRunningInPlaceCommand(this.marioRunningInPlaceSprite);
                            this.keyE.Execute();
                            break;
                        case Keys.R:
                            this.keyR = new MarioDeadCommand(this.marioDeadSprite);
                            this.keyR.Execute();
                            break;
                        case Keys.T:
                            this.keyT = new MarioRunningCommand(this.marioRunningSprite);
                            this.keyT.Execute();
                            break;
                    }    
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
