using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0
{
    class QuitGameCommand : ICommand
    {
        Sprint1 game;
        public QuitGameCommand(Sprint1 myGame)
        {
            game = myGame;
        }
        public void Execute()
        {
            game.Exit();
        }
    }
}
