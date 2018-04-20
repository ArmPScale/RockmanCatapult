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
            UseChipNormal,
            Dead
        }
        public PlayerState CurrentPlayerState;
        public int HeroHP = 10, maxHeroHP = 200, HeroAttack = 1, HeroBarrier = 0, HeroAura = 0;
        public Point currentPlayerPoint;
        public bool isDamaged, isRecovered, statusBugHP = false;
        public string choosePlayerAnimate = "", chooseEmotionPlayer = "NormalEmotion";
        public float currentChipCoolDown = 0;

        //enemyVar
        public int enemyAtk = 0;

        //panelVar
        public int[,] areaCracked = new int[3, 10]
        {
            { 0,0,0,0,0,0,0,0,0,0},
            { 0,0,0,0,0,0,0,0,0,0},
            { 0,0,0,0,0,0,0,0,0,0},
        };

        //folderPlayer
        public List<string> folderList;
        public Queue<string> nextChipFolder;

        //initialArr
        public int[,] panelStage, spriteMove, spriteHP, panelBoundary, chipAttack, virusAttack;
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
