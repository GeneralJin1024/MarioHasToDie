using Sprint1.MarioClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint1
{
    public class QuitCommand : ICommand
    {
        Sprint1Main game;

        public QuitCommand(Sprint1Main myGame)
        {
            game = myGame;
        }

        public void Execute()
        {   // Quit the running game
            game.Exit();
        }
    }
}