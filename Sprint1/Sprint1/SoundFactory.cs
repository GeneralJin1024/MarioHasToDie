using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections;

namespace Sprint1
{
    public class SoundFactory : Game
    {
        public static SoundFactory Instance
        {
            get
            {
                if (_instance is null)
                    _instance = new SoundFactory();
                return _instance;

            }

        }
        private static SoundFactory _instance;
        //public SoundEffect BackgroundMusic { get; set; }
        public SoundEffectInstance BackgroundMusic
        {
            get
            {
                return _backgroundMusic;
            }
        }
        private SoundEffectInstance _backgroundMusic;
        //public Song BackgroundMusic { get; private set; }
        /* SoundEffect coin here is GetItemEffect */
        private SoundEffect coin;
        private SoundEffect HitQuestionBlockBottom;
        private SoundEffect JumpEffect;
        private SoundEffect DiedEffect;
        /* for the further sound effect implement use */
        private SoundEffect Bump;
        private SoundEffect Fireball;
        private SoundEffect Pipe;
        private SoundEffect Pause;
        private SoundEffect Gameover;
        private SoundEffect Powerup;

        public SoundFactory()
        {
            _backgroundMusic = Sprint1Main.Game.Content.Load<SoundEffect>("Music/Map_BGM").CreateInstance();
            _backgroundMusic.IsLooped = true;

            coin = Sprint1Main.Game.Content.Load<SoundEffect>("Music/smb_coin");
            HitQuestionBlockBottom = Sprint1Main.Game.Content.Load<SoundEffect>("Music/QuestionBlock");
            JumpEffect = Sprint1Main.Game.Content.Load<SoundEffect>("Music/smb_jump-small");
            DiedEffect = Sprint1Main.Game.Content.Load<SoundEffect>("Music/sm64_mario_lost_a_life");
            Bump = Sprint1Main.Game.Content.Load<SoundEffect>("Music/smb_bump");
            Fireball = Sprint1Main.Game.Content.Load<SoundEffect>("Music/smb_fireball");
            Pipe = Sprint1Main.Game.Content.Load<SoundEffect>("Music/sm64_pipe");
            Pause = Sprint1Main.Game.Content.Load<SoundEffect>("Music/smb_pause");
            Gameover = Sprint1Main.Game.Content.Load<SoundEffect>("Music/sm64_game_over");
            Powerup = Sprint1Main.Game.Content.Load<SoundEffect>("Music/smb_powerup");

        }


        public void MarioJump()
        {
            JumpEffect.Play();

        }

        public void MarioGetItem()
        {
            coin.Play();

        }
        public void MarioDied()
        {
            DiedEffect.Play();
        }
        public void HitQuestionBlock()
        {
            HitQuestionBlockBottom.Play();
        }

    }

}
