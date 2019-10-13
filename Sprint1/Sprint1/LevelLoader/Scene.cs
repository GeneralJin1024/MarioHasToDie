using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint1.FactoryClasses;
using Sprint1.LevelLoader;
using Sprint1.MarioClasses;

namespace Sprint1
{
    public class Scene : IDisposable
    {
        readonly Stage stage;
        SpriteBatch spriteBatch;
        private int level;
        private readonly ArrayList characterList;
        private readonly ArrayList BackgroundList;
        public MarioCharacter Mario { get; internal set; }
        public Stage Stage
        {
            get { return stage; }
        }
        public Scene(Stage stage)
        {
            this.stage = stage;
            characterList = new ArrayList();
            BackgroundList = new ArrayList();
        }

        public void Add(ICharacter character)
        {
            characterList.Add(character);
        }
        public void Initalize(int levelIndex)
        {
            this.level = levelIndex;
            //factoryList = new ArrayList();
            stage.Initialize();
        }

        public void LoadContent()
        {
            stage.SpriteLocationReader(level, characterList, BackgroundList);
            spriteBatch = new SpriteBatch(stage.Game.GraphicsDevice);

            //factoryList.Add(BlockFactory.Instance);
            //factoryList.Add(EnemyFactory.Instance);
            //BackgroundFactory.Instance.AddToList(BackgroundList);
            //factoryList.Add(ItemFactory.Instance);
            
            //foreach (IFactory factory in factoryList)
            //factory.AddToList(spriteList);
            stage.LoadContent(characterList);
            
        }

        public void Update(GameTime gameTime)
        {
            stage.Update(gameTime);
            
            //foreach (ICharacter character in characterList)
            //    character.Update(1);
        }

        public void Draw()
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            Console.WriteLine("Length = " + characterList.Count);
            foreach (ISprite sprite in BackgroundList)
                sprite.Draw(spriteBatch);
            foreach (ICharacter character in characterList)
                character.Draw(spriteBatch);      
            Mario.Draw(spriteBatch);
            spriteBatch.End();
        }

        // IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // dispose managed resources
                spriteBatch.Dispose();
            }
            // free native resources
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
