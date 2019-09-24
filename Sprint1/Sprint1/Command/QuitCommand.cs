using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

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
        {
            game.Exit();
        }
    }
}