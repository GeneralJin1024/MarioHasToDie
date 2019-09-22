using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint0.BlockClasses;
using Sprint0.MarioClasses;
using Sprint0.FactoryClasses;
using Sprint0.Sprites;

namespace Sprint0
{
   //Teammate: Runmin Zhou
    //Teammate : Zhenhao Lu
    // teammate: Ziye Zhu
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Sprint1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Factory factory;
        public Mario Mario { get; set; }

        private ArrayList controllerList;
        private ArrayList spriteList;
        #region Sprite
        #endregion

        private Bricks qBlockTest;
        private Bricks hitBlockTest;
        private Bricks hiddenBlockTest;
        private Blocks floorBlockTest;
        private Blocks stairBlockTest;
        private Bricks brickBlockTest;

        #region Fonts
        public Color fontColor { get; set; } = Color.DarkBlue;
        private SpriteFont instructionFont;
        #endregion

        private Menu GameMenu;
        public bool MenuMode { get; set; }

        static private Sprint1 _game;
        public static Sprint1 Game
        {
            get
            {
                return _game;
            }
        }
        public Sprint1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            factory = new Factory();

            #region TestBlockSprites
            InitialBlocks();
            spriteList = new ArrayList();
            spriteList.Add(qBlockTest);
            spriteList.Add(hiddenBlockTest);
            spriteList.Add(brickBlockTest);
            spriteList.Add(hitBlockTest);
            spriteList.Add(floorBlockTest);
            spriteList.Add(stairBlockTest);

            #endregion

            #region Controllers
            controllerList = new ArrayList();
            #endregion

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
            Mario = new MarioFactory(Content).GetMario(new Vector2(400, 300));
            spriteList.Add(Mario);
            controllerList.Add(new KeyboardController(Mario, this, new Bricks[] { qBlockTest, hiddenBlockTest, brickBlockTest }));
            controllerList.Add(new GamepadController(Mario, this));
            LoadEnemyItemTexture();
            LoadBackgroundTexture();

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
            if (MenuMode)
                GameMenu.Update(gameTime);
            else
            {
                //Controller.UpdateInput(...);
                foreach (IController controller in controllerList)
                    controller.Update();
                foreach (ISprite sprite in spriteList)
                    sprite.Update(gameTime);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
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
                    sprite.Draw(spriteBatch, sprite.Position, true);
            }
                

            #region Fonts
            //DrawFonts(spriteBatch);
            #endregion

            spriteBatch.End();

            base.Draw(gameTime);
        }

        //private void DrawFonts(SpriteBatch spriteBatch)
        //{
        //    #region Legend
        //    Color color = fontColor;
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

        private void InitialBlocks()
        {
            float x = GraphicsDevice.Viewport.Width / 8;
            float y = GraphicsDevice.Viewport.Height / 2;
            qBlockTest = BlockFactory.Instance.GetQuestionBlock(new Vector2(x, y), new ArrayList { "redMushroom" });
            hitBlockTest = BlockFactory.Instance.GetUsedBlock(new Vector2(2 * x, y));
            hiddenBlockTest = BlockFactory.Instance.GetHiddenBlock(new Vector2(3 * x, y), new ArrayList { });
            floorBlockTest = BlockFactory.Instance.GetFloorBlock(new Vector2(4 * x, y));
            stairBlockTest = BlockFactory.Instance.GetStairBlock(new Vector2(5 * x, y));
            brickBlockTest = BlockFactory.Instance.GetBrickBlock(new Vector2(6 * x, y), new ArrayList { "coin", "coin" });
        }

        private void LoadEnemyItemTexture()
        {

            Texture2D goomba =Content.Load<Texture2D>("EnemySprite/goomba");
            Texture2D greenkoopa = Content.Load<Texture2D>("EnemySprite/greenkoopa");
            Texture2D redkoopa = Content.Load<Texture2D>("EnemySprite/redkoopa");
            Texture2D coin = Content.Load<Texture2D>("ItemSprite/coin");
            Texture2D flower = Content.Load<Texture2D>("ItemSprite/flower");
            Texture2D greenMushroom = Content.Load<Texture2D>("ItemSprite/greenMushroom");
            Texture2D redMushroom = Content.Load<Texture2D>("ItemSprite/redMushroom");
            Texture2D star = Content.Load<Texture2D>("ItemSprite/star");
            spriteList.Add(factory.getCoin(coin));
            spriteList.Add(factory.getFlower(flower));
            spriteList.Add(factory.getGreenMushroom(greenMushroom));
            spriteList.Add(factory.getRedMushroom(redMushroom));
            spriteList.Add(factory.getStar(star));
            spriteList.Add(factory.getGreenKoopa(greenkoopa));
            spriteList.Add(factory.getRedKoopa(redkoopa));
            spriteList.Add(factory.getGoomba(goomba));
        }
        private void LoadBackgroundTexture()
        {

            Texture2D bigCloud = Content.Load<Texture2D>("BackgroundSprite/bigCloud");
            Texture2D smallCloud = Content.Load<Texture2D>("BackgroundSprite/smallCloud");
            Texture2D bigHill = Content.Load<Texture2D>("BackgroundSprite/bigHill");
            Texture2D smallHill = Content.Load<Texture2D>("BackgroundSprite/smallHill");
            Texture2D bigBush = Content.Load<Texture2D>("BackgroundSprite/bigBush");
            spriteList.Add(factory.getBigCloud(bigCloud));
            spriteList.Add(factory.getBigHill(bigHill));
            spriteList.Add(factory.getSmallCloud(smallCloud));
            spriteList.Add(factory.getSmallHill(smallHill));
            spriteList.Add(factory.getBigBush(bigBush));
        }
    }
}
