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
        private ArrayList characterList;
        private ArrayList FireBallList;
        private ArrayList BackgroundList;
        public Camera Camera { get; private set; }
        private List<Layer> Layers;
        public MarioCharacter Mario { get; internal set; }
        public Stage Stage
        {
            get { return stage; }
        }
        public Scene(Stage stage)
        {
            this.stage = stage;         
        }

        public void Add(ICharacter character)
        {
            characterList.Add(character);
        }
        public void Initalize(int levelIndex)
        {



            characterList = new ArrayList();
            BackgroundList = new ArrayList();
            FireBallList = new ArrayList();
            this.level = levelIndex;
            //factoryList = new ArrayList();
            stage.Initialize();

        }

        public void LoadContent()
        {
            Camera = new Camera(Sprint1Main.Game.GraphicsDevice.Viewport) { Limits = new Rectangle(0, 0, 1000, 500) };
            Layers = new List<Layer>
            {
                new Layer(Camera) { Parallax = new Vector2(0.2f, 1.0f) }, //云
                new Layer(Camera) { Parallax = new Vector2(0.8f, 1.0f) }, //山
                new Layer(Camera) { Parallax = new Vector2(1.0f, 1.0f) }, //mario，草
                new Layer(Camera) { Parallax = new Vector2(1.0f, 1.0f) }, //item，enemy，block
                new Layer(Camera) { Parallax = new Vector2(1.0f, 1.0f) } // 火球
            };

            stage.SpriteLocationReader(level, characterList, FireBallList, Layers);
            spriteBatch = new SpriteBatch(stage.Game.GraphicsDevice);
            Layers[2].Sprites.Add(Mario);
            Layers[3].Sprites = characterList;
            Layers[4].Sprites = FireBallList;

            //factoryList.Add(BlockFactory.Instance);
            //factoryList.Add(EnemyFactory.Instance);
            //BackgroundFactory.Instance.AddToList(BackgroundList);
            //factoryList.Add(ItemFactory.Instance);

            //foreach (IFactory factory in factoryList)
            //factory.AddToList(spriteList);
            stage.LoadContent(characterList, FireBallList);
            
        }

        public void Update(GameTime gameTime)
        {
            stage.Update(gameTime);
            Camera.LookAt(Mario.Parameters.Position);

            //foreach (ICharacter character in characterList)
            //    character.Update(1);
        }

        public void Draw()
        {
            //spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            ////Console.WriteLine("Length = " + characterList.Count);
            //foreach (ISprite sprite in BackgroundList)
            //    sprite.Draw(spriteBatch);
            //foreach (ICharacter character in characterList)
            //    character.Draw(spriteBatch);
            //foreach (ICharacter character in FireBallList)
            //    character.Draw(spriteBatch);
            //Mario.Draw(spriteBatch);
            //spriteBatch.End();
            foreach (Layer layer in Layers)
                layer.Draw(spriteBatch);
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

        public static void CopyDataOfParameter(MoveParameters parameter, MoveParameters newParameter)
        {
            if (parameter is null || newParameter is null)
                throw new ArgumentNullException(nameof(parameter));
            newParameter.IsHidden = parameter.IsHidden;
            newParameter.IsLeft = parameter.IsLeft;
            newParameter.SetPosition(parameter.Position.X, parameter.Position.Y);
            newParameter.SetVelocity(Math.Abs(parameter.Velocity.X), parameter.Velocity.Y);
            newParameter.HasGravity = parameter.HasGravity;
        }

    }
}
