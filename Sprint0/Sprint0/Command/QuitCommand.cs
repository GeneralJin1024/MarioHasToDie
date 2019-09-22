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
        Sprint1 game;

        public QuitCommand(Sprint1 myGame)
        {
            game = myGame;
        }

        public void Execute()
        {
            game.Exit();
        }
    }
}