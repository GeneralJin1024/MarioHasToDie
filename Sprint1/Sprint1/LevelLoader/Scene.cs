using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint1.LevelLoader;

namespace Sprint1
{
    class Scene : IDisposable
    {
        Stage stage;
        SpriteBatch spriteBatch;
        List<ICharacter> listCharacter;

        public Stage Stage
        {
            get { return stage; }
        }
        public Scene(Stage stage)
        {
            this.stage = stage;
            listCharacter = new List<ICharacter>();
        }

        public void Add(ICharacter character)
        {
            listCharacter.Add(character);
        }
        public void Initalize()
        {
            stage.Initialize();
        }

        public void LoadContent()
        {
            spriteBatch = new SpriteBatch(stage.Game.GraphicsDevice);
            stage.LoadContent();
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }

    }
}
