using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint1.LevelLoader
{
    class Stage
    {
        public Sprint1Main Game { get; set; }
        List<IController> controllerList;

        public List<IController> Controllers
        {
            get { return this.controllerList; }
        }

        public GraphicsDeviceManager graphicsDevice
        {
            get { return Game.Graphics; }
        }

        public Stage (Sprint1Main game)
        {
            Game = game;
            controllerList = new List<IController>();
        }

        public void Initialize()
        {
            controllerList.Add(new KeyboardController(Game.Mario, Game));
            controllerList.Add(new GamepadController(Game.Mario, Game));
        }

        public void LoadContent()
        {
            //nothing to do here
        }
        public void Update(float TimeOfFrame)
        {
            foreach (IController controller in controllerList)
                controller.Update();
        }

        public void Draw(float TimeOfFrame)
        {
            //do nothing here
        }
    }
}
