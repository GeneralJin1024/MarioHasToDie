using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0
{
    public class QuitCommand : ICommand
    {
        Sprint0 game;

        public QuitCommand()
        {
        }

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