using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint0.BlockSprites;
using Sprint0.MarioClasses;

namespace Sprint0
{
   //Teammate: Runmin Zhou
    //Teammate : Zhenhao Lu
    // teammate: Ziye Zhu
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Sprint0 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Factory factory;
        private Mario Mario;

        private String Zhenhao = "HelloWorld";
        private String Jian  = "WWE";
        private ArrayList controllerList;
        private ArrayList spriteList;
        #region Sprite
        private ISprite marioStandingInPlaceSprite;
        private ISprite marioRunningInPlaceSprite;
        private ISprite marioDeadSprite;
        private ISprite marioRunningSprite;

        private Vector2 marioStandingInPlaceSpritePosition;
        private Vector2 marioRunningInPlaceSpritePosition;
        private Vector2 marioRunningSpritePosition;
        private Vector2 marioDeadSpritePosition;
        #endregion

        private ISprite qBlockTest;
        private ISprite hitBlockTest;

        #region Fonts
        public Color fontColor { get; set; } = Color.DarkBlue;
        private SpriteFont instructionFont;
        #endregion
        public Sprint0()
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
            factory = new Factory();

            #region Sprites
            spriteList = new ArrayList();
            marioStandingInPlaceSpritePosition = new Vector2(GraphicsDevice.Viewport.Width / 4, GraphicsDevice.Viewport.Height / 4);
            marioRunningInPlaceSpritePosition = new Vector2(GraphicsDevice.Viewport.Width / 4, 3 * GraphicsDevice.Viewport.Height / 4);
            marioDeadSpritePosition = new Vector2(3 * GraphicsDevice.Viewport.Width / 4, GraphicsDevice.Viewport.Height / 4);
            marioRunningSpritePosition = new Vector2(3 * GraphicsDevice.Viewport.Width / 4, 3 * GraphicsDevice.Viewport.Height / 4);   
            #endregion

            #region Controllers
            controllerList = new ArrayList();
            #endregion

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

            #region Sprites
            LoadMarioTexture();
            spriteList.Add(Mario);
            #endregion

            qBlockTest = new QuestionBlockSprite(new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2), spriteBatch, this.Content.Load<Texture2D>("BlockSprites/mario-question-blocks"));
            hitBlockTest = new UsedBlockSprite(new Vector2(GraphicsDevice.Viewport.Width / 4, GraphicsDevice.Viewport.Height / 4), spriteBatch, this.Content.Load<Texture2D>("BlockSprites/mario-shiny-block"));
            #region Controller
            controllerList.Add(new KeyboardController(this, marioStandingInPlaceSprite, marioRunningInPlaceSprite, marioDeadSprite, marioRunningSprite));
            controllerList.Add(new GamePadController(this, marioStandingInPlaceSprite, marioRunningInPlaceSprite, marioDeadSprite, marioRunningSprite));
            #endregion

            #region Fonts
            instructionFont = Content.Load<SpriteFont>("arial");
            #endregion
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
            foreach(IController controller in controllerList)
            {
                controller.UpdateInput();
            }
            foreach(ISprite sprite in spriteList)
            {
                sprite.Update(gameTime);
            }

            qBlockTest.Update(this.GraphicsDevice, gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(blendState: BlendState.AlphaBlend);

            foreach (ISprite sprite in spriteList)
            {
                sprite.Draw(spriteBatch, new Vector2(0, 0), true);
            }

            #region Fonts
            DrawFonts(spriteBatch);
            #endregion

            qBlockTest.Draw();
            hitBlockTest.Draw();
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawFonts(SpriteBatch spriteBatch)
        {
            #region Legend
            Color color = fontColor;
            spriteBatch.DrawString(instructionFont, "Legend for Keyboard", new Vector2(0.00f, 0.00f), color);
            spriteBatch.DrawString(instructionFont, "Q - Quit Game", new Vector2(0.00f, 20.00f), color);
            spriteBatch.DrawString(instructionFont, "W - Display a Non-moving Non-animated Sprite", new Vector2(0.00f, 40.00f), color);
            spriteBatch.DrawString(instructionFont, "E - Display a Non-moving Animated Sprite", new Vector2(0.00f, 60.00f), color);
            spriteBatch.DrawString(instructionFont, "R - Display a Moving Non-animated sprite", new Vector2(0.00f, 80.00f), color);
            spriteBatch.DrawString(instructionFont, "T - Display a Moving and Animated sprite", new Vector2(0.00f, 100.00f), color);

            spriteBatch.DrawString(instructionFont, "Legend for Gamepad", new Vector2(GraphicsDevice.Viewport.Width - 400.0f, 0.00f), color);
            spriteBatch.DrawString(instructionFont, "Start - Quit Game", new Vector2(GraphicsDevice.Viewport.Width - 400.0f, 20.00f), color);
            spriteBatch.DrawString(instructionFont, "A - Display a Non-moving Non-animated Sprite", new Vector2(GraphicsDevice.Viewport.Width - 400.0f, 40.00f), color);
            spriteBatch.DrawString(instructionFont, "B - Display a Non-moving Animated Sprite", new Vector2(GraphicsDevice.Viewport.Width - 400.0f, 60.00f), color);
            spriteBatch.DrawString(instructionFont, "X - Display a Moving Non-animated sprite", new Vector2(GraphicsDevice.Viewport.Width - 400.0f, 80.00f), color);
            spriteBatch.DrawString(instructionFont, "Y - Display a Moving and Animated sprite", new Vector2(GraphicsDevice.Viewport.Width - 400.0f, 100.00f), color);
            #endregion
        }

        private void LoadMarioTexture()
        {
            //13*16
            Texture2D[] StandardSheets = new Texture2D[4] {Content.Load<Texture2D>("MarioSprite/smallMarioRightStand"),
                Content.Load<Texture2D>("MarioSprite/smallMarioRightJump"),
                Content.Load<Texture2D>("MarioSprite/smallMarioRightMove"),
                Content.Load<Texture2D>("MarioSprite/smallMarioRightStand")};
            //16*32
            Texture2D[] SuperSheets = new Texture2D[4] {Content.Load<Texture2D>("SuperMario/superMarioRightStand"),
                Content.Load<Texture2D>("SuperMario/superMarioJumpRight"),
                Content.Load<Texture2D>("SuperMario/superMarioMoveRight"),
                Content.Load<Texture2D>("SuperMario/superMarioRightCrouch")};
            //16*32   16*22
            Texture2D[] FireSheets = new Texture2D[4] {Content.Load<Texture2D>("FireMario/fireMarioRightStand"),
                Content.Load<Texture2D>("FireMario/fireMarioJumpRight"),
                Content.Load<Texture2D>("FireMario/fireMarioRightMove"),
                Content.Load<Texture2D>("FireMario/fireMarioRightCrouch")};
            Mario = factory.getMario(StandardSheets, SuperSheets, FireSheets,
                Content.Load<Texture2D>("DiedMario/deadMario"), new Vector2(400, 300));
        }
    }
}
