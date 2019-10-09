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
            Block, Enemy, DiedEnemy, Flower, Mushroom, Star, Coin, Pipe
        }

        public static Vector2 Boundary { get; private set; }

        private GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private int TimeSinceLastFrame;
        private int MillisecondsPerFrame;
        private CollisionDetector Collision;

        //private Mario Mario;
        private ArrayList factoryList;
        private ArrayList controllerList;
        private ArrayList spriteList;


        #region Fonts
        public Color FontColor { get; set; } = Color.DarkBlue;
        private SpriteFont instructionFont;
        #endregion

        private Menu GameMenu;
        public bool MenuMode { get; set; }
        static private Sprint1Main _game;
        public MarioCharacter Mario { get; set; }
        public static Sprint1Main Game
        {
            get
            {
                return _game;
            }
        }
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

        public Sprint1Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 800;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = 500;   // set this value to the desired height of your window
            graphics.ApplyChanges();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            _game = this;
            Boundary = new Vector2(800, 500);

            spriteList = new ArrayList();

            #region Controllers
            controllerList = new ArrayList();
            #endregion

            factoryList = new ArrayList();
            //initialize menu and start from it
            GameMenu = new Menu(this);
            MenuMode = true;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //load mario texture and construct mario. Then add mario into sprite list.
            //add factories.
            //factoryList.Add(MarioFactory.Instance);
            factoryList.Add(BlockFactory.Instance);
            factoryList.Add(EnemyFactory.Instance);
            factoryList.Add(BackgroundFactory.Instance);
            factoryList.Add(ItemFactory.Instance);
            //get Mario from Mario factory.
            Mario = MarioFactory.Instance.Mario;
            foreach (IFactory factory in factoryList)
                factory.AddToList(spriteList);
            //controllerList.Add(new KeyboardController(Mario, this, 
            //    new Bricks[] { BlockFactory.Instance.qBlockTest, BlockFactory.Instance.hiddenBlockTest, BlockFactory.Instance.brickBlockTest }));
            //controllerList.Add(new GamepadController(Mario, this));

            #region Fonts
            instructionFont = Content.Load<SpriteFont>("arial");
            #endregion
            Collision = new CollisionDetector(Mario, spriteList);
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
                GameMenu.Update(gameTime);
            else
            {
                foreach (IController controller in controllerList)
                    controller.Update();
                TimeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
                if (TimeSinceLastFrame > MillisecondsPerFrame)
                {
                    TimeSinceLastFrame -= MillisecondsPerFrame;
                    Collision.Update();
                }
                //Controller.UpdateInput(...);
                //foreach (IController controller in controllerList)
                //    controller.Update();
                //foreach (ISprite sprite in spriteList)
                //    sprite.Update(gameTime);
                //Mario.Update(gameTime);
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
                foreach (ISprite sprite in spriteList)
                    sprite.Draw(spriteBatch);
                Mario.Draw(spriteBatch);
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

        public static Vector2 CheckBoundary(Vector2 position, Vector2 heightAndWidth)
        {
            position.X = position.X >= 0 ? position.X : 0;
            position.Y = position.Y >= 0 ? position.Y : 0;
            position.X = position.X <= Boundary.X - heightAndWidth.Y ? position.X : Boundary.X - heightAndWidth.Y;
            position.Y = position.Y <= Boundary.Y - heightAndWidth.X ? position.Y : Boundary.Y - heightAndWidth.X;
            return position;
        }
    }
}
