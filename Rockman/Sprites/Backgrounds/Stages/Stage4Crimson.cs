using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rockman.Models;

namespace Rockman.Sprites.Screens
{
    class Stage4Crimson : Stage
    {
        private float _timer = 0f;

        public Stage4Crimson(Texture2D[] texture)
            : base(texture)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            if (Singleton.Instance.stagesName == "Stage4Crimson")
            {
                switch (Singleton.Instance.CurrentGameState)
                {
                    case Singleton.GameState.GameEnemyAppear:

                        //mediaPlay --> BossBattle1
                        Singleton.Instance.mediaPlaySong = "VsCrimsonDragon";
                        //useBlackAce
                        Singleton.Instance.chipCustomSelect[6] = "BlackAce";
                        //resetChip
                        Singleton.Instance.chipSelect = new int[7]
                        {
                            1,0,0,0,0,0,0
                        };
                        //shuffleBattleChipInFolder
                        if (!isBackUp)
                        {
                            backUpFolderList = new List<string>(Singleton.Instance.folderList);
                            isBackUp = true;
                        }
                        Singleton.Instance.folderList.Shuffle();
                        Singleton.Instance.nextChipFolder = new Queue<string>(Singleton.Instance.folderList);
                        Singleton.Instance.panelBoundary = new int[3, 10]
                        {
                            { 0,0,0,0,0,1,1,1,1,1},
                            { 0,0,0,0,0,1,1,1,1,1},
                            { 0,0,0,0,0,1,1,1,1,1},
                        };
                        Singleton.Instance.panelStage = new int[3, 10]
                        {
                            { 0,0,0,0,0,0,0,0,0,4},
                            { 0,0,0,0,0,0,0,0,0,4},
                            { 0,0,0,0,0,0,0,0,0,4},
                        };
                        Singleton.Instance.panelElement = new int[3, 10]
                        {
                            { 0,0,0,0,0,0,0,0,0,0},
                            { 0,0,0,0,0,0,0,0,0,0},
                            { 0,0,0,0,0,0,0,0,0,0},
                        };
                        Singleton.Instance.playerMove = new int[3, 10]
                        {
                            { 0,0,0,0,0,0,0,0,0,0},
                            { 0,1,0,0,0,0,0,0,0,0},
                            { 0,0,0,0,0,0,0,0,0,0},
                        };
                        Singleton.Instance.spriteMove = new int[3, 10]
                        {
                            { 0,0,0,0,0,0,0,0,0,0},
                            { 0,0,0,0,0,11,0,10,0,0},
                            { 0,0,0,0,0,0,0,0,0,0},
                        };
                        Singleton.Instance.spriteHP = new int[3, 10]
                        {
                            { 0,0,0,0,0,0,0,0,0,0},
                            { 0,0,0,0,0,100,0,1200,0,0},
                            { 0,0,0,0,0,0,0,0,0,0 },
                        };
                        Singleton.Instance.chipEffect = new int[3, 10]
                        {
                            { 0,0,0,0,0,0,0,0,0,0},
                            { 0,0,0,0,0,0,0,0,0,0},
                            { 0,0,0,0,0,0,0,0,0,0},
                        };
                        Singleton.Instance.panelYellow = new int[3, 10]
                        {
                            { 0,0,0,0,0,0,0,0,0,0},
                            { 0,0,0,0,0,0,0,0,0,0},
                            { 0,0,0,0,0,0,0,0,0,0},
                        };
                        Singleton.Instance.virusAttack = new int[3, 10]
                        {
                            { 0,0,0,0,0,0,0,0,0,0},
                            { 0,0,0,0,0,0,0,0,0,0},
                            { 0,0,0,0,0,0,0,0,0,0},
                        };
                        Singleton.Instance.bossAttack = new int[3, 10]
                        {
                            { 0,0,0,0,0,0,0,0,0,0},
                            { 0,0,0,0,0,0,0,0,0,0},
                            { 0,0,0,0,0,0,0,0,0,0},
                        };

                        //resetMisc
                        Singleton.Instance.CurrentPlayerState = Singleton.PlayerState.Playing;
                        Singleton.Instance.HeroHP = Singleton.Instance.maxHeroHP;
                        Singleton.Instance.HeroBarrier = 0;
                        Singleton.Instance.HeroAura = 0;
                        Singleton.Instance.playerChipAtk = 0;
                        Singleton.Instance.choosePlayerAnimate = "Alive";
                        Singleton.Instance.statusBugHP = false;
                        Singleton.Instance.currentChipCoolDown = 0;
                        Singleton.Instance.currentChipAtkTime = 0;
                        Singleton.Instance.useChipName = "";
                        Singleton.Instance.drawChipEffectName = "";
                        Singleton.Instance.chipSlotIn.Clear();
                        Singleton.Instance.useChipSlotIn.Clear();
                        Singleton.Instance.indexChipSlotIn.Clear();
                        fade = new Color(0, 0, 0, 0);
                        break;
                    case Singleton.GameState.GamePlaying:
                        if (Singleton.Instance.CurrentPlayerState == Singleton.PlayerState.Dead)
                        {
                            Reset();
                        }
                        break;
                    case Singleton.GameState.GameClear:
                        _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        Reset();
                        //fadeScreen
                        if (_timer > 2f && _timer < 3f)
                        {
                            if (fade.A < 255)
                            {
                                fade.A += 5;
                            }
                        }
                        //returnMenuScreen
                        else if (_timer > 3f)
                        {
                            _timer = 0f;
                            Singleton.Instance.stagesName = "";
                            Singleton.Instance.CurrentMenuState = Singleton.MenuState.StoryMode;
                            Singleton.Instance.CurrentScreenState = Singleton.ScreenState.MenuScreen;
                        }
                        break;
                    case Singleton.GameState.GameOver:
                        Reset();
                        break;
                }
            }
            base.Update(gameTime, sprites);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.GameClear:
                    if (Singleton.Instance.stagesName == "Stage4Crimson")
                    {
                        spriteBatch.Draw(_texture[0], new Vector2(0, 0), null, fade, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                    }
                    break;
            }
            base.Draw(spriteBatch);
        }

        public override void Reset()
        {
            Singleton.Instance.nextChipFolder.Clear();
            Singleton.Instance.folderList = backUpFolderList;
            Singleton.Instance.chipStackImg = new string[7]
            {
                        "","","","","","",""
            };
            Singleton.Instance.chipCustomSelect[6] = "BlackAce";
            Singleton.Instance.chipEffect = new int[3, 10]
            {
                        { 0,0,0,0,0,0,0,0,0,0},
                        { 0,0,0,0,0,0,0,0,0,0},
                        { 0,0,0,0,0,0,0,0,0,0},
            };
            Singleton.Instance.virusAttack = new int[3, 10]
            {
                        { 0,0,0,0,0,0,0,0,0,0},
                        { 0,0,0,0,0,0,0,0,0,0},
                        { 0,0,0,0,0,0,0,0,0,0},
            };
            Singleton.Instance.bossAttack = new int[3, 10]
            {
                        { 0,0,0,0,0,0,0,0,0,0},
                        { 0,0,0,0,0,0,0,0,0,0},
                        { 0,0,0,0,0,0,0,0,0,0},
            };
            base.Reset();
        }
    }
}
