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
        public const int WIDTH = 1200;
        public const int HEIGHT = 800;

        public int HeroHP = 50;
        public int[,] panelStage, spriteMove, spriteHP, panelBoundary, chipAttack, virusAttack;
        //public int[,] spriteMove2, spriteHP2;
        public SpriteFont _font;
        public Texture2D[] effectsTexture;

        public Song song;
        public List<SoundEffect> soundEffects;
        public float MasterBGMVolume;
        public float MasterSFXVolume;

        public bool atkFinished, selectChipSuccess;
        public bool useChip, useChipDuring, useChipSuccess;
        public String useChipName = "Test";
        public Queue<String> chipSlotIn;
        public int[] chipSelect;

        public enum GameState
        {
            GameCustomScreen,
            GamePlaying,
            GameWaitingChip,
            GameUseChip,
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
