﻿using ConfigurationLibrary;
using Microsoft.Xna.Framework;
using Sprint1.CollideDetection;
using Sprint1.FactoryClasses;
using Sprint1.MarioClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint1.LevelLoader
{
    public class Stage
    {
        public Sprint1Main Game { get; set; }
        public static Vector2 Boundary { get; private set; }

        List<IController> controllerList;
        private ArrayList spriteList;
        //private ArrayList factoryList;
        private int TimeSinceLastFrame;
        private int MillisecondsPerFrame;
        private CollisionDetector Collision;
        public List<IController> Controllers
        {
            get { return this.controllerList; }
        }

        public GraphicsDeviceManager graphicsDevice
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
            graphicsDevice.PreferredBackBufferWidth = ConfigurationReaderAndWriter.ReadSetting("WindowWidth");
            //!= -1 ? ConfigurationReaderAndWriter.ReadSetting("WindowWidth") : graphicsDevice.GraphicsDevice.Viewport.Width;  // set this value to the desired width of your window
            graphicsDevice.PreferredBackBufferHeight = ConfigurationReaderAndWriter.ReadSetting("WindowHeight");
            //!= -1 ? ConfigurationReaderAndWriter.ReadSetting("WindowHeight") : graphicsDevice.GraphicsDevice.Viewport.Height;   // set this value to the desired height of your window
            graphicsDevice.ApplyChanges();
            MillisecondsPerFrame = 100;
            Boundary = new Vector2(graphicsDevice.PreferredBackBufferWidth, graphicsDevice.PreferredBackBufferHeight);       
            
        }

        public void LoadContent(ArrayList spriteList)
        {
            //nothing to do here           
            Collision = new CollisionDetector(Game.Scene.Mario, spriteList);
            controllerList.Add(new KeyboardController(Game.Scene.Mario, Game));
            controllerList.Add(new GamepadController(Game.Scene.Mario, Game));
        }
        public void Update(GameTime gameTime)
        {
            foreach (IController controller in controllerList)
                controller.Update();
            TimeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (TimeSinceLastFrame > MillisecondsPerFrame)
            {
                TimeSinceLastFrame -= MillisecondsPerFrame;
                Collision.Update();
            }

        }

        public void Draw()
        {
            //do nothing here
        }

        internal void spriteLocationReader(int levelIndex, ArrayList spriteList, ArrayList backgroundList)
        {
            ConfigurationLibrary.LevelSection myLevelSection = (ConfigurationLibrary.LevelSection)System.Configuration.ConfigurationManager.GetSection("Level" + levelIndex) as LevelSection;
            if (myLevelSection == null)
            {
                Console.WriteLine("Failed to load Level" + levelIndex);
            }
            else
            {
                Game.Scene.Mario = PlayerFactory.Instance.FactoryMethod2(myLevelSection.Player[1].SpriteName, StringToVecter2(myLevelSection.Player[1].SpriteLocation));       
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
            int startInd = pos.IndexOf("X:") + 2;
            float aXPosition = float.Parse(pos.Substring(startInd, pos.IndexOf(" Y") - startInd));
            startInd = pos.IndexOf("Y:") + 2;
            float aYPosition = float.Parse(pos.Substring(startInd, pos.IndexOf("}") - startInd));
            return new Vector2(aXPosition, aYPosition);
        }
    }
}
