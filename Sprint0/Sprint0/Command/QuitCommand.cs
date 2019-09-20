using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Sprint0
{
    public class QuitCommand : ICommand
    {
        Sprint0 game;

        public QuitCommand(Sprint0 myGame)
        {
            game = myGame;
        }

        public void Execute()
        {
            game.Exit();
        }
    }
}