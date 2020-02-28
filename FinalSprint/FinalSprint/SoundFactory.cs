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

namespace FinalSprint
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
        public SoundEffectInstance BackgroundMusic { get; }
        public Dictionary<string, SoundEffect> SoundEffectList { get; }

        public SoundFactory()
        {
            BackgroundMusic = Sprint5Main.Game.Content.Load<SoundEffect>("Music/Map_BGM").CreateInstance();
            BackgroundMusic.IsLooped = true;
            SoundEffectList = new Dictionary<string, SoundEffect>
            {
                { "getCoin", Sprint5Main.Game.Content.Load<SoundEffect>("Music/smb_coin") },
                { "hit?BlockBottom", Sprint5Main.Game.Content.Load<SoundEffect>("Music/QuestionBlock") },
                { "jump", Sprint5Main.Game.Content.Load<SoundEffect>("Music/smb_jump-small") },
                { "die", Sprint5Main.Game.Content.Load<SoundEffect>("Music/sm64_mario_lost_a_life") },
                { "bump", Sprint5Main.Game.Content.Load<SoundEffect>("Music/smb_bump") },
                { "throwFireball", Sprint5Main.Game.Content.Load<SoundEffect>("Music/smb_fireball") },
                { "getIntoPipe", Sprint5Main.Game.Content.Load<SoundEffect>("Music/sm64_pipe") },
                { "pause", Sprint5Main.Game.Content.Load<SoundEffect>("Music/smb_pause") },
                { "gameover", Sprint5Main.Game.Content.Load<SoundEffect>("Music/sm64_game_over") },
                { "powerUp", Sprint5Main.Game.Content.Load<SoundEffect>("Music/smb_powerup") }
            };

        }


        public void MarioJump() { SoundEffectList["jump"].Play(); }
        public void MarioGetItem() { SoundEffectList["getCoin"].Play(); }
        public void MarioDied() { SoundEffectList["die"].Play(); }
        public void HitQuestionBlock() { SoundEffectList["hit?BlockBottom"].Play(); }
        public void BumpItems() { SoundEffectList["bump"].Play(); }
        public void ThrowFireBall() { SoundEffectList["throwFireball"].Play(); }
        public void GetIntoPipe() { SoundEffectList["getIntoPipe"].Play(); }
        public void Pause() { SoundEffectList["pause"].Play(); }
        public void GameOver() { SoundEffectList["gameover"].Play(); }
        public void PowerUp() { SoundEffectList["powerUp"].Play(); }

    }

}
