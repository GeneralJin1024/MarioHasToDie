using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint1.BlockClasses;
using Sprint1.MarioClasses;
using Sprint1.FactoryClasses;
using Sprint1.Sprites;
using Sprint1.CollideDetection;
using Sprint1.LevelLoader;
using System.Configuration;
using ConfigurationLibrary;
using Sprint1.ItemClasses;

namespace Sprint1
{
    //Teammate: Runmin Zhou
    //Teammate : Zhenhao Lu
    // teammate: Ziye Zhu
    // Teammate: Shengyu Jin
    // Teammate: Jian Zhang
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Sprint1Main : Game
    {
        public enum CharacterType
        {
            Mario, Block, Enemy, DiedEnemy, Flower, GreenMushroom, RedMushroom, Star, Coin, Pipe, Fireball,
            Flag, Castle, Null, JumpMedicine, Bomb
        }

        public static int MarioLife { get; set; } = 3; // The rest of lives of Mario.
        public static int Point { get; set; } = 0; // The point Mario get in all level, will be reset when he died and no life left.
        public static int Coins { get; set; } = 0; // THe coins Mario get in all level, will be reset when he died and no life left.
        /**
         * Main Controller of the whole game.
         **/
        public LevelManager LevelControl { get; private set; }

        private GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        #region Fonts
        public Color FontColor { get; set; } = Color.DarkBlue;
        //private SpriteFont instructionFont;
        #endregion
        public MarioCharacter Mario { get{ return Scene.Mario; } set { } }
        public static Sprint1Main Game { get; private set; }
        public GraphicsDeviceManager Graphics
        {
            get
            {
                return graphics;
            }

            private set
            {
                graphics = value;
            }
        }
        public Stage Stage
        {
            get
            {
                //scenes[CurrSceneIndex-1].Stage
                return LevelControl.Stage;
            }
        }

        public Scene Scene
        {
            get
            {
                //scenes[CurrSceneIndex - 1]
                return LevelControl.Scene;
            }
        }
        //public int CurrSceneIndex { get; private set; }
        //private Scene currScene;
        //readonly List<Scene> scenes;        
        //private readonly int totalScene;

        public Sprint1Main()
        {
            Game = this;
            Graphics = new GraphicsDeviceManager(this);
            
            Content.RootDirectory = "Content";

            //scenes = new List<Scene>{};
            //totalScene = ConfigurationReaderAndWriter.ReadSetting("Scenes");
            //CurrSceneIndex = 1;
            LevelControl = new LevelManager();//仅仅移动代码，并未修改，若需要还原，删除本行并取消上面3行的注释即可
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //for (int i = 1; i <= totalScene; i++)
            //{
            //    Stage stage = new Stage(this);
            //    Scene scene = new Scene(stage);
            //    scene.Initalize(i);
            //    scenes.Add(scene);
            //}
            //currScene = scenes[CurrSceneIndex-1];
            //GameMenu = new Menu(this);
            //MenuMode = true;
            //LoadingMode = true;
            LevelControl.Initialize();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            LevelControl.LoadContent();
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            LevelControl.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            LevelControl.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
