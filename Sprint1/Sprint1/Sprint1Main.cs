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
            Mario, Block, Enemy, DiedEnemy, Flower, GreenMushroom, RedMushroom, Star, Coin, Pipe, Fireball, Flag, Null
        }

        public static ICharacter Flower { get; set; }

        private GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        #region Fonts
        public Color FontColor { get; set; } = Color.DarkBlue;
        private SpriteFont instructionFont;
        #endregion

        private Menu GameMenu;
        public bool MenuMode { get; set; }
        public bool LoadingMode { get; set; }
        public MarioCharacter Mario { get; set; }
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
                return scenes[CurrSceneIndex-1].Stage;
            }
        }

        public Scene Scene
        {
            get
            {
                return scenes[CurrSceneIndex - 1];
            }
        }
        public int CurrSceneIndex { get; private set; }
        private Scene currScene;
        readonly List<Scene> scenes;        
        private readonly int totalScene;

        public Sprint1Main()
        {
            Game = this;
            Graphics = new GraphicsDeviceManager(this);
            
            Content.RootDirectory = "Content";

            scenes = new List<Scene>{};
            totalScene = ConfigurationReaderAndWriter.ReadSetting("Scenes");
            CurrSceneIndex = 1;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            for (int i = 1; i <= totalScene; i++)
            {
                Stage stage = new Stage(this);
                Scene scene = new Scene(stage);
                scene.Initalize(i);
                scenes.Add(scene);
            }
            currScene = scenes[CurrSceneIndex-1];
            GameMenu = new Menu(this);
            MenuMode = true;
            LoadingMode = true;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            for (int i = 1; i <= totalScene; i++)
            {
                scenes[i - 1].LoadContent();
            }
            #region Fonts
            instructionFont = Content.Load<SpriteFont>("arial");
            #endregion
            
            GameMenu.LoadContent(instructionFont);
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
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();
            if (gameTime == null)
                throw new ArgumentNullException(nameof(gameTime));
            if (MenuMode)
                GameMenu.Update(1);
            else
            {
                if (!LoadingMode)
                    currScene.Update(gameTime);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //change background when change from menu to game.
            if (MenuMode)
                GraphicsDevice.Clear(Color.Black);
            else
                GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(blendState: BlendState.AlphaBlend);
            
            if (MenuMode)
                GameMenu.Draw(spriteBatch);
            else
            {
                if (!LoadingMode)
                    currScene.Draw();
            }
                

            #region Fonts
            //DrawFonts();
            #endregion

            spriteBatch.End();

            base.Draw(gameTime);
        }

        //private void DrawFonts()
        //{
        //    #region Legend
        //    Color color = FontColor;
        //    spriteBatch.DrawString(instructionFont, "Legend for Keyboard", new Vector2(0.00f, 0.00f), color);
        //    spriteBatch.DrawString(instructionFont, "Q - Quit Game", new Vector2(0.00f, 20.00f), color);
        //    spriteBatch.DrawString(instructionFont, "W - Display a Non-moving Non-animated Sprite", new Vector2(0.00f, 40.00f), color);
        //    spriteBatch.DrawString(instructionFont, "E - Display a Non-moving Animated Sprite", new Vector2(0.00f, 60.00f), color);
        //    spriteBatch.DrawString(instructionFont, "R - Display a Moving Non-animated sprite", new Vector2(0.00f, 80.00f), color);
        //    spriteBatch.DrawString(instructionFont, "T - Display a Moving and Animated sprite", new Vector2(0.00f, 100.00f), color);

        //    spriteBatch.DrawString(instructionFont, "Legend for Gamepad", new Vector2(GraphicsDevice.Viewport.Width - 400.0f, 0.00f), color);
        //    spriteBatch.DrawString(instructionFont, "Start - Quit Game", new Vector2(GraphicsDevice.Viewport.Width - 400.0f, 20.00f), color);
        //    spriteBatch.DrawString(instructionFont, "A - Display a Non-moving Non-animated Sprite", new Vector2(GraphicsDevice.Viewport.Width - 400.0f, 40.00f), color);
        //    spriteBatch.DrawString(instructionFont, "B - Display a Non-moving Animated Sprite", new Vector2(GraphicsDevice.Viewport.Width - 400.0f, 60.00f), color);
        //    spriteBatch.DrawString(instructionFont, "X - Display a Moving Non-animated sprite", new Vector2(GraphicsDevice.Viewport.Width - 400.0f, 80.00f), color);
        //    spriteBatch.DrawString(instructionFont, "Y - Display a Moving and Animated sprite", new Vector2(GraphicsDevice.Viewport.Width - 400.0f, 100.00f), color);
        //    #endregion
        //}
        public void ResetScene()
        {
            #region Reset
            //save backup
            MoveParameters tempParameter = new MoveParameters(true);
            Scene.CopyDataOfParameter(currScene.Mario.Parameters, tempParameter);
            MarioState.ActionType actionType = currScene.Mario.GetAction;
            MarioState.PowerType powerType = currScene.Mario.GetPower;
            bool isFire = currScene.Mario.IsFire();

            LoadingMode = true;       
            scenes.Remove(currScene);
            currScene.Dispose();
            Stage stage = new Stage(this);
            currScene = new Scene(stage);
            scenes.Insert(CurrSceneIndex-1, currScene);
            currScene.Initalize(CurrSceneIndex);
            currScene.LoadContent();          
            LoadingMode = false;

            //use backup to rewrite
            Scene.CopyDataOfParameter(tempParameter, currScene.Mario.Parameters);
            currScene.Mario.RestoreStates(actionType, powerType, isFire);
            currScene.Camera.LookAt(currScene.Mario.Parameters.Position);
            #endregion

        }

    }
}
