using ConfigurationLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Sprint1.MarioClasses;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Sprint1.LevelLoader
{
    public class LevelManager : IDisposable
    {
        private Menu GameMenu;
        private Menu GameOver;
        private Menu GameWin;
        public int CurrSceneIndex { get; private set; }
        private Scene currScene;
        readonly List<Scene> scenes;
        private readonly int totalScene;
        private SpriteFont instructionFont;
        private ISprite Coin;
        private ISprite Mario;
        private int Mode;//0: MenuMode, 1: CurrentScene, 2: Game Over, 3: Loading 
        private float CheckPoint;
        private float RestOfTime;
        private int previousScene;
        private readonly ResourceManager StringManager;
        public Stage Stage
        {
            get
            {
                return scenes[CurrSceneIndex].Stage;
            }
        }

        public Scene Scene
        {
            get
            {
                return scenes[CurrSceneIndex];
            }
        }
        public LevelManager()
        {
            scenes = new List<Scene> { };
            totalScene = ConfigurationReaderAndWriter.ReadSetting("Scenes");
            Console.WriteLine("Scene = " + totalScene);
            CurrSceneIndex = 1;
            Mode = 0; RestOfTime = 0;
            StringManager = new ResourceManager("Sprint1.Resource1", Assembly.GetExecutingAssembly());
        }

        public void Initialize()
        {
            for (int i = 0; i < totalScene; i++)
            {
                Stage stage = new Stage(Sprint1Main.Game);
                Scene scene = new Scene(stage);
                scene.Initalize(i);
                scenes.Add(scene);
            }
            currScene = scenes[CurrSceneIndex];
            GameMenu = new Menu(Sprint1Main.Game, new string[] { "Welcome To Mario", "Start", "Quit" });
            GameOver = new Menu(Sprint1Main.Game, new string[] { "Game Over", "Replay", "Exit" });
            GameWin = new Menu(Sprint1Main.Game, new string[] { "Congratulations", "Replay", "Exit" });
            //currScene.Dispose();
        }

        public void LoadContent()
        {
            for (int i = 1; i <= totalScene; i++)
            {
                CurrSceneIndex = i - 1;
                scenes[i - 1].LoadContent();
            }
            CurrSceneIndex = 1;
            #region Fonts
            instructionFont = Sprint1Main.Game.Content.Load<SpriteFont>("arial");
            #endregion
            Coin = new AnimatedSprite(
                Sprint1Main.Game.Content.Load<Texture2D>("ItemSprite/coin"), new Point(1, 4), new MoveParameters(false));
            Coin.Parameters.SetPosition(156, 41);
            Mario = new AnimatedSprite(Sprint1Main.Game.Content.Load<Texture2D>("MarioSprites/smallMarioRightStand"), 
                new Point(1, 1), new MoveParameters(false));
            Mario.Parameters.SetPosition(320, 41);
            GameMenu.LoadContent(instructionFont);
            GameOver.LoadContent(instructionFont);
            GameWin.LoadContent(instructionFont);
            CheckPoint = Scene.Mario.GetMinPosition.X;
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
            else if (Mode == 1)
            {
                RestOfTime -= (RestOfTime > 0 && !Scene.Mario.Win && !Stage.Pulse) ? (float)gameTime.ElapsedGameTime.TotalSeconds : 0;
                if (RestOfTime < 0)
                {
                    Scene.Mario.Suicide(); RestOfTime = 0;
                }
                currScene.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (spriteBatch is null)
                throw new ArgumentNullException(nameof(spriteBatch));
            if (Mode != 1)
                Sprint1Main.Game.GraphicsDevice.Clear(Color.Black);
            //else
            //    Sprint1Main.Game.GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(blendState: BlendState.AlphaBlend);
            Color fontColor = Mode == 1 && Stage.BackgroundColor != Color.Black ? Color.Black : Color.White;
            spriteBatch.DrawString(instructionFont, ":   " + Sprint1Main.Coins, new Vector2(172, 20), fontColor,
                0, Vector2.Zero, 1.2f, SpriteEffects.None, 0); //剩余生命
            spriteBatch.DrawString(instructionFont, StringManager.GetString("MARIO", CultureInfo.CurrentCulture), new Vector2(20, 0), fontColor,
                0, Vector2.Zero, 1.2f, SpriteEffects.None, 0); //得分
            spriteBatch.DrawString(instructionFont, "" + Sprint1Main.Point, new Vector2(20, 20), fontColor,
                0, Vector2.Zero, 1.2f, SpriteEffects.None, 0);//得分
            spriteBatch.DrawString(instructionFont, StringManager.GetString("TIME", CultureInfo.CurrentCulture), new Vector2(640, 0), fontColor,
                0, Vector2.Zero, 1.2f, SpriteEffects.None, 0);
            spriteBatch.DrawString(instructionFont, "" + (int)RestOfTime, new Vector2(640, 20), fontColor, 0, Vector2.Zero, 1.2f, SpriteEffects.None, 0);
            spriteBatch.DrawString(instructionFont, StringManager.GetString("WORLD", CultureInfo.CurrentCulture), new Vector2(480, 0), fontColor, 
                0, Vector2.Zero, 1.2f, SpriteEffects.None, 0);
            spriteBatch.DrawString(instructionFont, " : " + Sprint1Main.MarioLife,
                new Vector2(340, 20), fontColor, 0, Vector2.Zero, 1.2f, SpriteEffects.None, 0);
            Coin.Draw(spriteBatch); //加一张贴图
            Mario.Draw(spriteBatch);
            if (Mode == 0)
                GameMenu.Draw(spriteBatch);
            else if (Mode == 2)
                GameOver.Draw(spriteBatch);
            else if (Mode == 3)
                GameWin.Draw(spriteBatch);
            else if (Mode == 1)
            {
                spriteBatch.DrawString(instructionFont, "1 - " + CurrSceneIndex, new Vector2(480, 20), 
                    fontColor, 0, Vector2.Zero, 1.2f, SpriteEffects.None, 0);
                //Coin.Draw(spriteBatch); //加一张贴图
                //Mario.Draw(spriteBatch);
                currScene.Draw();
            }
        }

        //马里奥死亡，场景切换
        public void MarioDied()
        {
            Sprint1Main.MarioLife--;
            if (Sprint1Main.MarioLife > 0)
            {
                SceneFlash(true, true, CurrSceneIndex); SoundFactory.Instance.BackgroundMusic.Play();
            }
            else
            {
                ChangeToGamoverMode(); Sprint1Main.MarioLife = 3;
            }
        }

        public void ChangeToNormalMode()
        {
            Mode = 1; RestOfTime = 400; Sprint1Main.Point = 0; Sprint1Main.Coins = 0;
            SoundFactory.Instance.BackgroundMusic.Play();
            //MediaPlayer.Play(SoundFactory.Instance.BackgroundMusic);MediaPlayer.IsRepeating = true;
        }
        public void ChangeToMenuMode() { Mode = 0; }
        public void ChangeToGamoverMode() { Mode = 2; SceneFlash(true, false, CurrSceneIndex); }
        public void ChangeToWinMode() { Mode = 3; Sprint1Main.MarioLife = 3; SceneFlash(true, false, CurrSceneIndex); }
        public void ChangeToLoadingMode() { Mode = 4; }
        public void AddTimeBonus() { Sprint1Main.Point += ((int)RestOfTime + 1) * 10; }

        public void SceneFlash(bool resetAll, bool goToCheckPoint, int changeToSceneIndex)
        {
            MarioState.ActionType actionType;
            MarioState.PowerType powerType = currScene.Mario.GetPower;
            bool isFire = currScene.Mario.IsFire();
            bool invincible = Scene.Mario.Invincible;
            if (changeToSceneIndex == CurrSceneIndex)
            {
                actionType = currScene.Mario.GetAction;
                ResetScene(resetAll, goToCheckPoint);
            }
            else
            {
                GoToSecretScene(changeToSceneIndex);
                actionType = currScene.Mario.GetAction;
            }
            if (!resetAll)
            {
                Scene.Mario.Invincible = invincible;
                currScene.Mario.RestoreStates(actionType, powerType, isFire);
            }
        }

        private void ResetScene(bool resetAll, bool goToCheckPoint)
        {
            #region Reset
            //save backup
            MoveParameters tempParameter = new MoveParameters(true);
            Scene.CopyDataOfParameter(currScene.Mario.Parameters, tempParameter);
            bool Win = currScene.Mario.Win;
            int preMode = Mode;
            List<float> pipeList = Scene.Mario.DivedPipe;

            ChangeToLoadingMode();
            scenes.Remove(currScene);
            Stage stage = new Stage(Sprint1Main.Game);
            currScene.Stage = stage;
            scenes.Insert(CurrSceneIndex, currScene);
            currScene.Initalize(CurrSceneIndex);
            currScene.LoadContent();
            Mode = preMode;

            //use backup to rewrite
            if (!resetAll)
            {
                ChangeToNormalMode();
                currScene.Mario.Win = Win;
                Scene.CopyDataOfParameter(tempParameter, currScene.Mario.Parameters);
                currScene.Camera.LookAt(currScene.Mario.Parameters.Position);
                Scene.DisableVPipes(pipeList);
            }
            else if (goToCheckPoint)
            {
                Scene.DisableVPipes(pipeList);
                Scene.Mario.Parameters.SetPosition(CheckPoint, Scene.Mario.GetMinPosition.Y - 32);
            }
            else
                CheckPoint = Scene.Mario.GetMinPosition.X;
            #endregion

        }

        public void GoToNormalArea()
        {
            //这里应该有代码将currScene替换回来
            ResetScene(true, false);
            CurrSceneIndex = previousScene;
            currScene = Scene;
            //
            Console.WriteLine(Scene.Mario.GetMaxPosition);
            Scene.Mario.Bump();
        }

        public void GoToSecretScene(int x)
        {
            CheckPoint = Scene.Mario.GetMinPosition.X;
            //这里的代码应该是替换currScene。鉴于这门课我们只做一关和一个隐藏关，切换可以直接用数字
            //GoToNormalArea();
            previousScene = CurrSceneIndex;
            CurrSceneIndex = x;
            currScene = Scene;
            //
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
