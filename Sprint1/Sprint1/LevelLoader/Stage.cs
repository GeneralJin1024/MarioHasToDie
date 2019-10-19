using ConfigurationLibrary;
using Microsoft.Xna.Framework;
using Sprint1.CollideDetection;
using Sprint1.FactoryClasses;
using Sprint1.MarioClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint1.LevelLoader
{
    public class Stage
    {
        public Sprint1Main Game { get; set; }
        public static Vector2 Boundary { get; private set; }

        readonly List<IController> controllerList;
        //private ArrayList factoryList;
        private int TimeSinceLastFrame;
        private int MillisecondsPerFrame;
        private CollisionDetector Collision;
        private int DiedTime = 0;
        public GraphicsDeviceManager GraphicsDevice
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
            //ConfigurationManager.RefreshSection("Configuration");
            //ConfigurationReaderAndWriter.ReadAllSettings();
            GraphicsDevice.PreferredBackBufferWidth = ConfigurationReaderAndWriter.ReadSetting("WindowWidth");
            //!= -1 ? ConfigurationReaderAndWriter.ReadSetting("WindowWidth") : graphicsDevice.GraphicsDevice.Viewport.Width;  // set this value to the desired width of your window
            GraphicsDevice.PreferredBackBufferHeight = ConfigurationReaderAndWriter.ReadSetting("WindowHeight");
            //!= -1 ? ConfigurationReaderAndWriter.ReadSetting("WindowHeight") : graphicsDevice.GraphicsDevice.Viewport.Height;   // set this value to the desired height of your window
            GraphicsDevice.ApplyChanges();
            MillisecondsPerFrame = 100;
            Boundary = new Vector2(GraphicsDevice.PreferredBackBufferWidth, GraphicsDevice.PreferredBackBufferHeight);       
            
        }

        public void LoadContent(ArrayList spriteList, ArrayList fireBallList)
        {
            //nothing to do here           
            Collision = new CollisionDetector(Game.Scene.Mario, spriteList, fireBallList);
            controllerList.Add(new KeyboardController(Game.Scene.Mario, Game));
            controllerList.Add(new GamepadController(Game.Scene.Mario, Game));
        }
        public void Update(GameTime gameTime)
        {
            if (gameTime == null)
                throw new ArgumentNullException(nameof(gameTime));
            foreach (IController controller in controllerList)
                controller.Update();
            TimeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (TimeSinceLastFrame > MillisecondsPerFrame)
            {
                TimeSinceLastFrame -= MillisecondsPerFrame;
                Collision.Update();
            }
            if (Game.Scene.Mario.IsDied())
                DiedTime++;
            if (DiedTime >= 50)
                Game.Exit();

        }

        internal void SpriteLocationReader(int levelIndex, ArrayList spriteList, ArrayList backgroundList, ArrayList fireBallList)
        {

            if (!((LevelSection)ConfigurationManager.GetSection("Level" + levelIndex) is LevelSection myLevelSection))
            {
                Console.WriteLine("Failed to load Level" + levelIndex);
            }
            else
            {
                ItemFactory.Instance.Initialize(fireBallList);
                Game.Scene.Mario = PlayerFactory.FactoryMethod2(myLevelSection.Player[1].SpriteName, StringToVecter2(myLevelSection.Player[1].SpriteLocation));
                for (int i = 1; i < myLevelSection.Backgrounds.Count; i++)
                {
                    backgroundList.Add(BackgroundFactory.Instance.FactoryMethod2(myLevelSection.Backgrounds[i].SpriteName, StringToVecter2(myLevelSection.Backgrounds[i].SpriteLocation)));
                }
                for (int i = 1; i < myLevelSection.Items.Count; i++)
                {
                    spriteList.Add(ItemFactory.Instance.FactoryMethod(myLevelSection.Items[i].SpriteName, StringToVecter2(myLevelSection.Items[i].SpriteLocation)));
                }
                for (int i = 1; i < myLevelSection.Blocks.Count; i++)
                {
                    spriteList.Add(BlockFactory.Instance.FactoryMethod(myLevelSection.Blocks[i].SpriteName, StringToVecter2(myLevelSection.Blocks[i].SpriteLocation)));
                }
                for (int i = 1; i < myLevelSection.Enemys.Count; i++)
                {
                    spriteList.Add(EnemyFactory.Instance.FactoryMethod(myLevelSection.Enemys[i].SpriteName, StringToVecter2(myLevelSection.Enemys[i].SpriteLocation)));
                }
            }
        }

        public static Vector2 CheckBoundary(Vector2 position, Vector2 heightAndWidth)
        {
            position.X = position.X >= 0 ? position.X : 0;
            position.Y = position.Y >= 0 ? position.Y : 0;
            position.X = position.X <= Boundary.X - heightAndWidth.Y ? position.X : Boundary.X - heightAndWidth.Y;
            position.Y = position.Y <= Boundary.Y - heightAndWidth.X ? position.Y : Boundary.Y - heightAndWidth.X;
            return position;
        }

        private Vector2 StringToVecter2(string pos)
        {
            int startInd = pos.IndexOf("X:", StringComparison.Ordinal) + 2;
            float aXPosition = float.Parse(pos.Substring(startInd, pos.IndexOf(" Y", StringComparison.Ordinal) - startInd), CultureInfo.CurrentCulture);
            startInd = pos.IndexOf("Y:", StringComparison.Ordinal) + 2;
            float aYPosition = float.Parse(pos.Substring(startInd, pos.IndexOf("}", StringComparison.Ordinal) - startInd), CultureInfo.CurrentCulture);
            return new Vector2(aXPosition, aYPosition);
        }
    }
}
