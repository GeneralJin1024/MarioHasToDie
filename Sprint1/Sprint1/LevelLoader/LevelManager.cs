using ConfigurationLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Sprint1.MarioClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint1.LevelLoader
{
    public class LevelManager : IDisposable
    {
        private Menu GameMenu;
        private Menu GameOver;
        private Menu GameWin;
        public bool MenuMode { get; set; }
        public bool LoadingMode { get; set; }
        public int CurrSceneIndex { get; private set; }
        private Scene currScene;
        readonly List<Scene> scenes;
        private readonly int totalScene;
        private SpriteFont instructionFont;
        private ISprite Coin;
        private int Mode;//0: MenuMode, 1: CurrentScene, 2: Game Over
        private Song BackgroundMusic;
        private float CheckPoint;
        public Stage Stage
        {
            get
            {
                return scenes[CurrSceneIndex - 1].Stage;
            }
        }

        public Scene Scene
        {
            get
            {
                return scenes[CurrSceneIndex - 1];
            }
        }
        public LevelManager()
        {
            scenes = new List<Scene> { };
            totalScene = ConfigurationReaderAndWriter.ReadSetting("Scenes");
            CurrSceneIndex = 1;
            Mode = 0;
        }

        public void Initialize()
        {
            for (int i = 1; i <= totalScene; i++)
            {
                Stage stage = new Stage(Sprint1Main.Game);
                Scene scene = new Scene(stage);
                scene.Initalize(i);
                scenes.Add(scene);
            }
            currScene = scenes[CurrSceneIndex - 1];
            GameMenu = new Menu(Sprint1Main.Game, new string[] { "Welcome To Mario", "Start", "Quit" });
            GameOver = new Menu(Sprint1Main.Game, new string[] { "Game Over", "Replay", "Exit" });
            GameWin = new Menu(Sprint1Main.Game, new string[] { "Congratulations", "Replay", "Exit" });
            MenuMode = true;
            LoadingMode = true;
            //currScene.Dispose();
        }

        public void LoadContent()
        {
            for (int i = 1; i <= totalScene; i++)
            {
                scenes[i - 1].LoadContent();
            }
            #region Fonts
            instructionFont = Sprint1Main.Game.Content.Load<SpriteFont>("arial");
            #endregion
            Coin = new AnimatedSprite(
                Sprint1Main.Game.Content.Load<Texture2D>("ItemSprite/coin"), new Point(1, 4), new MoveParameters(false));
            Coin.Parameters.SetPosition(216, 36);
            GameMenu.LoadContent(instructionFont);
            GameOver.LoadContent(instructionFont);
            GameWin.LoadContent(instructionFont);
            CheckPoint = Scene.Mario.GetMinPosition().X - 100;
            //BackgroundMusic = MusicFactory.Instance.AddBackgroundMusic();
        }

        public void Update(GameTime gameTime)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();
            Coin.Update(0.6f);
            if (gameTime == null)
                throw new ArgumentNullException(nameof(gameTime));
            if (Mode == 0)
                GameMenu.Update(1);
            else if (Mode == 2)
                GameOver.Update(1);
            else if (Mode == 3)
                GameWin.Update(1);
            else
            {
                if (!LoadingMode)
                    currScene.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (spriteBatch is null)
                throw new ArgumentNullException(nameof(spriteBatch));
            if (Mode != 1)
                Sprint1Main.Game.GraphicsDevice.Clear(Color.Black);
            else
                Sprint1Main.Game.GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(blendState: BlendState.AlphaBlend);
            Color fontColor = Mode == 1 ? Color.Black : Color.White;
            Coin.Draw(spriteBatch); //加一张贴图
            spriteBatch.DrawString(instructionFont, ":   " + Sprint1Main.Coins, new Vector2(232, 15), fontColor,
                0, Vector2.Zero, 1.2f, SpriteEffects.None, 0); //剩余生命
            spriteBatch.DrawString(instructionFont, "Score", new Vector2(20, 0), fontColor,
                0, Vector2.Zero, 1.2f, SpriteEffects.None, 0); //得分
            spriteBatch.DrawString(instructionFont, "" + Sprint1Main.Point, new Vector2(20, 20), fontColor,
                0, Vector2.Zero, 1.2f, SpriteEffects.None, 0);//得分

            if (Mode == 0)
                GameMenu.Draw(spriteBatch);
            else if (Mode == 2)
                GameOver.Draw(spriteBatch);
            else if (Mode == 3)
                GameWin.Draw(spriteBatch);
            else
            {
                if (!LoadingMode)
                    currScene.Draw();
            }
        }

        //马里奥死亡，场景切换
        public void MarioDied()
        {
            Sprint1Main.MarioLife--;
            if (Sprint1Main.MarioLife > 0)
                ResetScene(true, true);
            else
                ChangeToGamoverMode();
        }
        public void GoToSecreteArea()
        {
            CheckPoint = Scene.Mario.GetMinPosition().X;
            GoToNormalArea();
        }

        public void GoToNormalArea()
        {
            Scene.Mario.Bump();
        }

        public void ChangeToNormalMode() { Mode = 1; /*MediaPlayer.Play(BackgroundMusic);*/ }
        public void ChangeToMenuMode() { Mode = 0; }
        public void ChangeToGamoverMode() { Mode = 2; ResetScene(true, false);/*MediaPlayer.Stop();*/ }
        public void ChangeToWinMode() { Mode = 3; ResetScene(true, false); }
        public void ResetScene(bool resetAll, bool goToCheckPoint)
        {
            #region Reset
            //save backup
            MoveParameters tempParameter = new MoveParameters(true);
            Scene.CopyDataOfParameter(currScene.Mario.Parameters, tempParameter);
            MarioState.ActionType actionType = currScene.Mario.GetAction;
            MarioState.PowerType powerType = currScene.Mario.GetPower;
            bool isFire = currScene.Mario.IsFire(); bool Win = currScene.Mario.Win;
            bool invincible = Scene.Mario.Invincible;

            LoadingMode = true;
            scenes.Remove(currScene);
            //currScene.Dispose();
            Stage stage = new Stage(Sprint1Main.Game);
            currScene = new Scene(stage);
            scenes.Insert(CurrSceneIndex - 1, currScene);
            currScene.Initalize(CurrSceneIndex);
            currScene.LoadContent();
            LoadingMode = false;

            //use backup to rewrite
            if (!resetAll)
            {
                Scene.CopyDataOfParameter(tempParameter, currScene.Mario.Parameters);
                currScene.Mario.Win = Win; Scene.Mario.Invincible = invincible;
                currScene.Mario.RestoreStates(actionType, powerType, isFire);
                currScene.Camera.LookAt(currScene.Mario.Parameters.Position);
            }
            else if (goToCheckPoint)
            {
                Scene.Mario.Parameters.SetPosition(CheckPoint, Scene.Mario.GetMinPosition().Y);
            }
            #endregion

        }

        /*消除报警*/
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            currScene.Dispose();
        }
    }
}
