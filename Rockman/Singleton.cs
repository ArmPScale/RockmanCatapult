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

        //heroVar
        public enum PlayerState
        {
            Playing,
            BusterShot,
            Dead
        }
        public PlayerState CurrentPlayerState;
        public int HeroHP = 10, maxHeroHP = 500, HeroAttack = 1, HeroBarrier = 0, HeroAura = 0;
        public Point currentPlayerPoint;
        public bool isDamaged;

        //enemyVar
        public int enemyAtk;

        //folderPlayer
        public List<string> folderList;
        public Queue<string> nextChipFolder;

        //initialArr
        public int[,] panelStage, spriteMove, spriteHP, panelBoundary, chipAttack, virusAttack;
        //public int[,] spriteMove2, spriteHP2;
        public SpriteFont _font;
        public Texture2D[] effectsTexture;

        public Song song;
        public List<SoundEffect> soundEffects;
        public float MasterBGMVolume;
        public float MasterSFXVolume;

        //utility
        public bool newTurnCustom, isCustomBarFull;
        public bool atkFinished, selectChipSuccess;
        public bool useChip, useChipDuring, useChipNearlySuccess, useChipSuccess;
        public bool useNormalChip, useSceneChip;
        public String useChipName = "Test";
        public String chipType,chipClass;
        public Stack<String> chipSlotIn, useChipSlotIn;
        public Stack<int> indexChipSlotIn;
        public int[] chipSelect;
        public String[] chipCustomSelect, chipStackImg;
        public Point currentChipSelect;

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

        public Random rng = new Random();

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
