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
        private Song BackgroundMusic;
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
            Content.RootDirectory = "Content";
            BackgroundMusic = Content.Load<Song>("Music/Map_BGM");
            coin = Content.Load<SoundEffect>("Music/smb_coin");
            HitQuestionBlockBottom = Content.Load<SoundEffect>("Music/QuestionBlock");
            JumpEffect = Content.Load<SoundEffect>("Music/smb_jump-small");
            DiedEffect = Content.Load<SoundEffect>("Music/sm64_mario_lost_a_life");
            Bump = Content.Load<SoundEffect>("Music/smb_bump");
            Fireball = Content.Load<SoundEffect>("Music/smb_fireball");
            Pipe = Content.Load<SoundEffect>("Music/sm64_pipe");
            Pause = Content.Load<SoundEffect>("Music/smb_pause");
            Gameover = Content.Load<SoundEffect>("Music/sm64_game_over");
            Powerup = Content.Load<SoundEffect>("Music/smb_powerup");

        }
        public void GameTheme()
        {
            MediaPlayer.Play(BackgroundMusic);
            MediaPlayer.IsRepeating = true;
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
            MediaPlayer.Stop();

        }
        public void HitQuestionBlock()
        {
            HitQuestionBlockBottom.Play();
        }

    }

}
