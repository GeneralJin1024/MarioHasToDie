using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sprint0
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Sprint0 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

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
            #region Sprites
            spriteList = new ArrayList();
            marioStandingInPlaceSpritePosition = new Vector2(GraphicsDevice.Viewport.Width / 4, GraphicsDevice.Viewport.Height / 4);
            marioStandingInPlaceSprite = new MarioStandingInPlaceSprite(marioStandingInPlaceSpritePosition);
            marioRunningInPlaceSpritePosition = new Vector2(GraphicsDevice.Viewport.Width / 4, 3 * GraphicsDevice.Viewport.Height / 4);
            marioRunningInPlaceSprite = new MarioRunningInPlaceSprite(marioRunningInPlaceSpritePosition);
            marioDeadSpritePosition = new Vector2(3 * GraphicsDevice.Viewport.Width / 4, GraphicsDevice.Viewport.Height / 4);
            marioDeadSprite = new MarioDeadSprite(marioDeadSpritePosition);
            marioRunningSpritePosition = new Vector2(3 * GraphicsDevice.Viewport.Width / 4, 3 * GraphicsDevice.Viewport.Height / 4);
            marioRunningSprite = new MarioRunningSprite(marioRunningSpritePosition);

            spriteList.Add(marioStandingInPlaceSprite);
            spriteList.Add(marioRunningInPlaceSprite);
            spriteList.Add(marioDeadSprite);
            spriteList.Add(marioRunningSprite);
            #endregion

            #region Controllers
            controllerList = new ArrayList();
            controllerList.Add(new KeyboardController(this, marioStandingInPlaceSprite, marioRunningInPlaceSprite, marioDeadSprite, marioRunningSprite));
            controllerList.Add(new GamePadController(this, marioStandingInPlaceSprite, marioRunningInPlaceSprite, marioDeadSprite, marioRunningSprite));
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
            MarioStandingInPlaceSprite.Init(spriteBatch, this.Content.Load<Texture2D>("MarioSprites/large_standing_mario"));          
            MarioRunningInPlaceSprite.Init(spriteBatch, this.Content.Load<Texture2D>("MarioSprites/mario_sprite_sheet_animated"));           
            MarioDeadSprite.Init(spriteBatch, this.Content.Load<Texture2D>("MarioSprites/dead_mario"));
            MarioRunningSprite.Init(spriteBatch, this.Content.Load<Texture2D>("MarioSprites/mario_sprite_sheet_animated"));
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
                if (sprite.Visibility)
                    sprite.Update(this.GraphicsDevice, gameTime);
            }
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
                if (sprite.Visibility)
                    sprite.Draw();
            }

            #region Fonts
            DrawFonts(spriteBatch);
            #endregion

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

    }
}
