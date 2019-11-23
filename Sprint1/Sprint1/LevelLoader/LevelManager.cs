using ConfigurationLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Sprint1.FactoryClasses;
using Sprint1.MarioClasses;
using System;
using System.Collections;
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
        readonly List<Scene> scenes; //level list.
        private readonly int totalScene; //number of Scenes (included Secrete area)
        private SpriteFont instructionFont; //HUD font.
        private ISprite Coin; //HUD picture
        private ISprite Mario; //HUD picture.
        private int Mode;//0: MenuMode, 1: CurrentScene, 2: Game Over, 3: Loading 
        private float CheckPoint; //Check Point in the game.
        private float RestOfTime; //HUD: Time, when time goes to 0, Mario will suicide.
        private int previousScene;
        private readonly ResourceManager StringManager; //used to read strings from resource file.
        public bool IsLastLevel //help the Win Page present different sentences.
        {
            get { return CurrSceneIndex == 1; }
        }
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
            CurrSceneIndex = 2;
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
            //set the three special Pages.
            GameMenu = new Menu(Sprint1Main.Game, new string[] { "Welcome To Mario", "Start", "Quit" });
            GameOver = new Menu(Sprint1Main.Game, new string[] { "Game Over", "Replay", "Exit" });
            GameWin = new Menu(Sprint1Main.Game, new string[] { "Congratulations", "Replay", "Exit", "Next Level"});
            //currScene.Dispose();
        }

        public void LoadContent()
        {
            for (int i = 1; i <= totalScene; i++)
            {
                // some code Called Current Scene's Mario to do something. Hence change CurrSceneIndex synchronously
                CurrSceneIndex = i - 1;
                scenes[i - 1].LoadContent();
            }
            CurrSceneIndex = 1;
            #region Fonts
            instructionFont = Sprint1Main.Game.Content.Load<SpriteFont>("arial");
            #endregion
            //create HUD pictures.
            Coin = new AnimatedSprite(
                Sprint1Main.Game.Content.Load<Texture2D>("ItemSprite/coin"), new Point(1, 4), new MoveParameters(false));
            Coin.Parameters.SetPosition(156, 41);
            Mario = new AnimatedSprite(Sprint1Main.Game.Content.Load<Texture2D>("MarioSprites/smallMarioRightStand"), 
                new Point(1, 1), new MoveParameters(false));
            Mario.Parameters.SetPosition(320, 41);
            
            //Load fonts.
            GameMenu.LoadContent(instructionFont);
            GameOver.LoadContent(instructionFont);
            GameWin.LoadContent(instructionFont);
            //initialize CheckPoint (reborn place)
            CheckPoint = Scene.Mario.GetMinPosition.X;
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
                //will not change when arrives 0, or pulse the game, or Mario wins (touch the flag)
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
            if (Mode != 1) // only in normal level (not secrete area), the background is Blue.
                Sprint1Main.Game.GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(blendState: BlendState.AlphaBlend);
            Color fontColor = Mode == 1 && Stage.BackgroundColor != Color.Black ? Color.Black : Color.White;
            spriteBatch.DrawString(instructionFont, ":   " + Sprint1Main.Coins, new Vector2(172, 20), fontColor,
                0, Vector2.Zero, 1.2f, SpriteEffects.None, 0);
            spriteBatch.DrawString(instructionFont, StringManager.GetString("MARIO", CultureInfo.CurrentCulture), new Vector2(20, 0), fontColor,
                0, Vector2.Zero, 1.2f, SpriteEffects.None, 0);
            spriteBatch.DrawString(instructionFont, "" + Sprint1Main.Point, new Vector2(20, 20), fontColor,
                0, Vector2.Zero, 1.2f, SpriteEffects.None, 0);
            spriteBatch.DrawString(instructionFont, StringManager.GetString("TIME", CultureInfo.CurrentCulture), new Vector2(640, 0), fontColor,
                0, Vector2.Zero, 1.2f, SpriteEffects.None, 0);
            spriteBatch.DrawString(instructionFont, "" + (int)RestOfTime, new Vector2(640, 20), fontColor, 0, Vector2.Zero, 1.2f, SpriteEffects.None, 0);
            spriteBatch.DrawString(instructionFont, StringManager.GetString("WORLD", CultureInfo.CurrentCulture), new Vector2(480, 0), fontColor, 
                0, Vector2.Zero, 1.2f, SpriteEffects.None, 0);
            spriteBatch.DrawString(instructionFont, " : " + Sprint1Main.MarioLife,
                new Vector2(340, 20), fontColor, 0, Vector2.Zero, 1.2f, SpriteEffects.None, 0);
            //draw HUD pictures.
            Coin.Draw(spriteBatch); 
            Mario.Draw(spriteBatch);

            if (Mode == 0)
                GameMenu.Draw(spriteBatch);
            else if (Mode == 2)
                GameOver.Draw(spriteBatch);
            else if (Mode == 3)
                GameWin.Draw(spriteBatch);
            else if (Mode == 1)
            {
                //print Level number in HUD.
                spriteBatch.DrawString(instructionFont, "1 - " + CurrSceneIndex, new Vector2(480, 20), 
                    fontColor, 0, Vector2.Zero, 1.2f, SpriteEffects.None, 0);
                //draw entites.
                currScene.Draw();
            }
        }

        public void MarioDied()
        {
            Sprint1Main.MarioLife--;
            if (Sprint1Main.MarioLife > 0) //reborn at check point.
            {
                SceneFlash(true, true, CurrSceneIndex); SoundFactory.Instance.BackgroundMusic.Play();
            }
            else //game over.
            {
                ChangeToGamoverMode(); Sprint1Main.MarioLife = 3;
            }
        }

        public void ChangeToNormalMode()
        {
            //go from Menu/GameOver/Win, reset the time and play the BGM.
            Mode = 1; RestOfTime = 400;
            SoundFactory.Instance.BackgroundMusic.Play();
        }
        public void ChangeToMenuMode() { Mode = 0; }
        public void ChangeToGamoverMode()
        {
            //reset Point and Coins, reset current level, play game over sound effect.
            Mode = 2; Sprint1Main.Point = 0; Sprint1Main.Coins = 0;
            SceneFlash(true, false, CurrSceneIndex);
            SoundFactory.Instance.GameOver();
        }
        //will not reset Point and Coin, Mario can extend them in next level.
        public void ChangeToWinMode() { Mode = 3; Sprint1Main.MarioLife = 3; SceneFlash(true, false, CurrSceneIndex); GotoNextScene(); }
        public void ChangeToLoadingMode() { Mode = 4; }

        // Add Points based on the rest of time when Mario wins the current level.
        public void AddTimeBonus() { Sprint1Main.Point += ((int)RestOfTime + 1) * 10; }

        public void SceneFlash(bool resetAll, bool goToCheckPoint, int changeToSceneIndex)
        {
            MarioState.ActionType actionType;
            MarioState.PowerType powerType = currScene.Mario.GetPower;
            bool isFire = currScene.Mario.IsFire(); bool throwBullet = Scene.Mario.ThrowBullet;
            bool invincible = Scene.Mario.Invincible; bool jumpTwice = Scene.Mario.JumpTwice;
            if (changeToSceneIndex == CurrSceneIndex)
            {
                //need reset
                actionType = currScene.Mario.GetAction;
                ResetScene(resetAll, goToCheckPoint);
            }
            else
            {
                // Go to Secre Area, keep the current level.
                GoToSecretScene(changeToSceneIndex);
                actionType = currScene.Mario.GetAction;
            }
            if (!resetAll)
            {
                Scene.Mario.Invincible = invincible;
                currScene.Mario.RestoreStates(actionType, powerType, isFire, throwBullet, jumpTwice);
            }
        }

        private void ResetScene(bool resetAll, bool goToCheckPoint)
        {
            #region Reset
            //save backup
            MoveParameters tempParameter = new MoveParameters(true);
            Scene.CopyDataOfParameter(currScene.Mario.Parameters, tempParameter); //copy all state from Parameters
            //back up some special state which not included in Parameters
            bool Win = currScene.Mario.Win;
            //record the current Mode
            int preMode = Mode;

            ChangeToLoadingMode();
            scenes.Remove(currScene); //delete current level and remove it from level's list.
            //recreate a stage with initial state and connect it with current Scene.
            Stage stage = new Stage(Sprint1Main.Game);
            currScene.Stage = stage;
            scenes.Insert(CurrSceneIndex, currScene);
            //Initialize the scene and reload content.
            currScene.Initalize(CurrSceneIndex);
            currScene.LoadContent();
            Mode = preMode;
            //use backup to rewrite
            if (!resetAll) //Press [Rr] to reset all entities.
            {
                ChangeToNormalMode();
                currScene.Mario.Win = Win;
                Scene.CopyDataOfParameter(tempParameter, currScene.Mario.Parameters);
                currScene.Camera.LookAt(currScene.Mario.Parameters.Position);
            }
            else if (goToCheckPoint) //Mario died (with rest life > 1)
            {
                Scene.Mario.Parameters.SetPosition(CheckPoint, Scene.Mario.GetMinPosition.Y - 32);
            }
            else //Game Over or Win the game.
                CheckPoint = Scene.Mario.GetMinPosition.X;
            #endregion

        }

        public void GoToNormalArea()
        {
            ResetScene(true, false); // Reset Secrete Area
            CurrSceneIndex = previousScene; // return to the normal level
            currScene = Scene;
            CheckPoint = Scene.Mario.GetMinPosition.X; //Update Check Point
            Scene.DisableVPipes(Scene.Mario.DivedPipe); //Disable all Vpipe that Mario get inside or get out.
            Scene.Mario.Bump(); // Make Mario Get Out.
        }

        public void GoToSecretScene(int x)
        {
            previousScene = CurrSceneIndex; //store the current scene position to get back to.
            CurrSceneIndex = x;
            currScene = Scene; //Change current scene to secrete level scene.
        }
        public void GotoNextScene()
        {
            if (CurrSceneIndex < scenes.Count - 1)
                CurrSceneIndex++; // not the final level
            else
                CurrSceneIndex = 1; //final level, if players want to continue, he will replay the first level.
            currScene = Scene;
            CheckPoint = Scene.Mario.GetMinPosition.X; //reset CheckPoint.
        }

        /*Eliminate Warning*/
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
