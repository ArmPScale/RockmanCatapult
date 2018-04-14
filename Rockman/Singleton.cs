using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;

namespace Rockman
{
    class Singleton
    {
        public const int WIDTH = 800;
        public const int HEIGHT = 480;

        public int HeroHP = 100;
        public int[,] panelStage, spriteMove, spriteHP, panelBoundary, chipAttack, virusAttack;
        //public int[,] spriteMove2, spriteHP2;
        public SpriteFont _font;
        public Texture2D[] effectsTexture;

        public Song song;
        public List<SoundEffect> soundEffects;
        public float MasterBGMVolume;
        public float MasterSFXVolume;

        public enum GameState
        {
            GamePlaying,
            GameClear,
            GameOver,
        }
        public GameState CurrentGameState;

        public KeyboardState PreviousKey, CurrentKey;

        private static Singleton instance;

        private Singleton() { }

        public static Singleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Singleton();
                }
                return instance;
            }
        }
    }
}
