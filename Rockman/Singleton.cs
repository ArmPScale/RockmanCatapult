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
        public int HeroHP = 200, maxHeroHP = 200, HeroAttack = 4, HeroBarrier = 0, HeroAura = 0, playerChipAtk = 0, NoisePercent = 0;
        public int currentVirusGotDmgIndex , Zenny = 9999999;
        public Point currentPlayerPoint;
        public bool isDamaged, isRecovered, statusBugHP = false, isGetChipResult = false;
        public string choosePlayerAnimate = "Alive", chooseEmotionPlayer = "NormalEmotion";
        public float currentChipCoolDown = 0f, currentChipAtkTime = 0f;

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
        public Queue<string> nextChipFolder, nextChipInPack;
        public Dictionary<string, int> allChipDict;

        //initialArr
        public int[,] panelBoundary, panelStage, panelElement, panelYellow, playerMove, spriteMove, spriteHP, chipEffect, virusAttack, bossAttack;
        public SpriteFont _font, normalFont;
        public Texture2D[] effectsTexture;
        public Dictionary<string, Song> song;
        public string mediaPlaySong = "", stagesName = "";
        public float MasterBGMVolume;
        public float MasterSFXVolume;

        //utility
        public bool newTurnCustom, isCustomBarFull, isCustomBomb = false;
        public bool selectChipSuccess;
        public bool useChip, useChipDuring, useChipNearlySuccess, useChipSuccess;
        public bool useNormalChip, useSceneChip, useThrowableChip;
        public bool whiteScreen = false, blackScreen = false;
        public String chipType ,chipClass, useChipName = "", drawChipEffectName = "";
        public Stack<String> chipSlotIn, useChipSlotIn;
        public Stack<int> indexChipSlotIn;
        public int[] chipSelect;
        public string[] chipCustomSelect, chipStackImg;
        public Point currentChipSelect;

        public enum ScreenState
        {
            TitleScreen,
            MenuScreen,
            StoryMode,
            Quit
        }
        public ScreenState CurrentScreenState;

        public enum MenuState
        {
            MainMenu,
            StoryMode,
            EditFolderChip,
            Shop,
            Practice,
            Option,
            Credits,
            Quit
        }
        public MenuState CurrentMenuState;

        public enum GameState
        {
            GameEnemyAppear,
            GameCustomScreen,
            GameWaiting,
            GamePlaying,
            GameWaitingChip,
            GameUseChip,
            GameClear,
            GameOver,
        }
        public GameState CurrentGameState;

        public KeyboardState PreviousKey, CurrentKey;
        public MouseState PreviousMouse, CurrentMouse;

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
