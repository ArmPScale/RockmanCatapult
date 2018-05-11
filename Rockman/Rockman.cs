using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using Rockman.Sprites;
using Rockman.Sprites.Screens;
using Rockman.Sprites.Chips;
using Rockman.Models;

namespace Rockman
{
    public class Rockman : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private List<Sprite> _screenSprites, _sprites;
        Texture2D[] titleScreenTexture, menuScreenTexture,
            playersTexture, panelTexture, enemiesTexture, backgroundTexture, fadeScreenTexture, chipTexture, customScreenTexture;
        private int _numScreenSprites, _numObject;

        public Rockman()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = Singleton.WIDTH;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = Singleton.HEIGHT;   // set this value to the desired height of your window
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            IsMouseVisible = true;

            titleScreenTexture = new Texture2D[5];
            menuScreenTexture = new Texture2D[5];
            backgroundTexture = new Texture2D[10];
            panelTexture = new Texture2D[6];
            playersTexture = new Texture2D[10];
            enemiesTexture = new Texture2D[10];
            fadeScreenTexture = new Texture2D[2];
            customScreenTexture = new Texture2D[10];
            chipTexture = new Texture2D[20];
            Singleton.Instance.effectsTexture = new Texture2D[10];
            //Singleton.Instance.soundEffects = new List<SoundEffect>();
            Singleton.Instance.chipSlotIn = new Stack<string>();
            Singleton.Instance.indexChipSlotIn = new Stack<int>();
            Singleton.Instance.useChipSlotIn = new Stack<string>();
            Singleton.Instance.chipStackImg = new string[7]
            {
                "","","","","","",""
            };
            Singleton.Instance.chipSelect = new int[7]
            {
                1,0,0,0,0,0,0
            };
            Singleton.Instance.chipCustomSelect = new string[7]
            {
                "","","","","","NoChip","BlackAce"
            };
            //shuffleBattleChipInFolder
            Singleton.Instance.folderList = new List<string>()
            {
                //"DarkRecovery","DoubleCrack","SpreadGun3","Recovery120","Recovery300","DarkSpread",
                //"DreamAura","HiCannon","Barrier100","TripleCrack","AirShot","Cannon","MegaCannon","DarkCannon",
                "PanelReturn","HolyPanel","Sanctuary","BlackBomb","CannonBall","DarkStage","CherprangRiver","JaneRiver"
                //"MiniBomb","BigBomb","EnergyBomb","MegaEnergyBomb","SearchBomb3","DarkBomb","BugBomb",
            };
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
                { 3,1,0,0,0,0,0,0,0,0},
                { 1,1,0,0,0,0,0,0,0,0},
                { 1,1,0,0,0,0,0,0,0,0},
            };
            Singleton.Instance.panelElement = new int[3, 10]
            {
                { 0,0,0,1,0,0,0,1,0,0},
                { 0,0,1,1,0,0,1,1,1,0},
                { 0,0,1,0,0,0,0,1,0,0},
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
                { 0,0,0,0,0,0,0,0,5,0},
                { 0,0,0,0,0,0,0,3,0,4},
            };
            Singleton.Instance.spriteHP = new int[3, 10]
            {
                { 0,0,0,0,0,0,0,0,0,0},
                { 0,0,0,0,0,0,0,0,900,0},
                { 0,0,0,0,0,0,0,100,0,100},
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
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Color[] data = new Color[Singleton.WIDTH * Singleton.HEIGHT];
            //fadeBlack
            fadeScreenTexture[0] = new Texture2D(graphics.GraphicsDevice, Singleton.WIDTH, Singleton.HEIGHT);
            for (int i = 0; i < data.Length; ++i) data[i] = Color.Black;
            fadeScreenTexture[0].SetData(data);
            //fadeWhite
            fadeScreenTexture[1] = new Texture2D(graphics.GraphicsDevice, Singleton.WIDTH, Singleton.HEIGHT);
            for (int i = 0; i < data.Length; ++i) data[i] = Color.White;
            fadeScreenTexture[1].SetData(data);

            Singleton.Instance.CurrentScreenState = Singleton.ScreenState.StoryMode;
            Reset();
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Singleton.Instance.CurrentScreenState = Singleton.ScreenState.Quit;
            Singleton.Instance.PreviousMouse = Singleton.Instance.CurrentMouse;

            Singleton.Instance.CurrentKey = Keyboard.GetState();
            Singleton.Instance.CurrentMouse = Mouse.GetState();
            _numScreenSprites = _screenSprites.Count;
            _numObject = _sprites.Count;

            switch (Singleton.Instance.CurrentScreenState)
            {
                case Singleton.ScreenState.TitleScreen:
                    //mediaPlay --> TitleScreenRemix
                    Singleton.Instance.mediaPlaySong = "TitleScreenRemix";

                    for (int i = 0; i < _numScreenSprites; i++)
                    {
                        if (_screenSprites[i].IsActive) _screenSprites[i].Update(gameTime, _screenSprites);
                    }
                    break;
                case Singleton.ScreenState.MenuScreen:
                    //mediaPlay --> MenuScreen
                    Singleton.Instance.mediaPlaySong = "MenuScreen";
                    
                    for (int i = 0; i < _numScreenSprites; i++)
                    {
                        if (_screenSprites[i].IsActive) _screenSprites[i].Update(gameTime, _screenSprites);
                    }
                    break;
                case Singleton.ScreenState.StoryMode:
                    Singleton.Instance.mediaPlaySong = "BossBattle1";

                    switch (Singleton.Instance.CurrentGameState)
                    {
                        case Singleton.GameState.GameEnemyAppear:
                            for (int i = 0; i < _numObject; i++)
                            {
                                if (_sprites[i].IsActive) _sprites[i].Update(gameTime, _sprites);
                            }
                            break;
                        case Singleton.GameState.GameCustomScreen:
                            //mediaPlay --> Battle1
                            Singleton.Instance.mediaPlaySong = "BossBattle1";

                            for (int i = 0; i < _numObject; i++)
                            {
                                if (_sprites[i].IsActive) _sprites[i].Update(gameTime, _sprites);
                            }
                            if (Singleton.Instance.selectChipSuccess)
                            {
                                Singleton.Instance.selectChipSuccess = false;
                                Singleton.Instance.CurrentGameState = Singleton.GameState.GameWaiting;
                            }
                            break;
                        case Singleton.GameState.GameWaiting:
                            for (int i = 0; i < _numObject; i++)
                            {
                                if (_sprites[i].IsActive) _sprites[i].Update(gameTime, _sprites);
                            }
                            break;
                        case Singleton.GameState.GamePlaying:
                            for (int i = 0; i < _numObject; i++)
                            {
                                if (_sprites[i].IsActive) _sprites[i].Update(gameTime, _sprites);
                            }
                            for (int i = 0; i < _numObject; i++)
                            {
                                if (!_sprites[i].IsActive)
                                {
                                    _sprites.RemoveAt(i);
                                    i--;
                                    _numObject--;
                                }
                            }

                            int enemies = 0;
                            foreach (int virus in Singleton.Instance.spriteMove)
                            {
                                if (virus >= 2)
                                {
                                    enemies += 1;
                                }
                            }
                            if (enemies == 0)
                            {
                                MediaPlayer.Stop();
                                //mediaPlay --> EnemyDeletedShort
                                Singleton.Instance.mediaPlaySong = "EnemyDeletedShort";
                                Singleton.Instance.CurrentGameState = Singleton.GameState.GameClear;
                            }
                            break;
                        case Singleton.GameState.GameWaitingChip:
                            for (int i = 0; i < _numObject; i++)
                            {
                                if (_sprites[i].IsActive) _sprites[i].Update(gameTime, _sprites);
                            }

                            Singleton.Instance.useChip = true;

                            break;
                        case Singleton.GameState.GameUseChip:
                            for (int i = 0; i < _numObject; i++)
                            {
                                if (_sprites[i].IsActive) _sprites[i].Update(gameTime, _sprites);
                            }
                            if (Singleton.Instance.useChipNearlySuccess)
                            {
                                //Singleton.Instance.useChipName = "";
                                Singleton.Instance.useChipSlotIn.Pop();
                                Singleton.Instance.useChipDuring = false;
                                Singleton.Instance.useChipNearlySuccess = false;
                                Singleton.Instance.useChipSuccess = true;
                            }
                            break;
                        case Singleton.GameState.GameClear:
                            for (int i = 0; i < _numObject; i++)
                            {
                                if (_sprites[i].IsActive) _sprites[i].Update(gameTime, _sprites);
                            }

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

                            for (int i = 0; i < _numObject; i++)
                            {
                                if (!_sprites[i].IsActive)
                                {
                                    _sprites.RemoveAt(i);
                                    i--;
                                    _numObject--;
                                }
                            }
                            break;
                        case Singleton.GameState.GameOver:
                            for (int i = 0; i < _numObject; i++)
                            {
                                if (_sprites[i].IsActive) _sprites[i].Update(gameTime, _sprites);
                            }
                            for (int i = 0; i < _numObject; i++)
                            {
                                if (!_sprites[i].IsActive)
                                {
                                    _sprites.RemoveAt(i);
                                    i--;
                                    _numObject--;
                                }
                            }
                            //Singleton.Instance.CurrentScreenState = Singleton.ScreenState.MenuScreen;
                            break;
                    }
                    break;
                case Singleton.ScreenState.EditFolderChip:
                    break;
                case Singleton.ScreenState.Shop:
                    break;
                case Singleton.ScreenState.Tutorial:
                    break;
                case Singleton.ScreenState.Option:
                    break;
                case Singleton.ScreenState.Credits:
                    break;
                case Singleton.ScreenState.Quit:
                    Exit();
                    break;
            }

            Singleton.Instance.PreviousKey = Singleton.Instance.CurrentKey;

            //mediaPlay
            MediaPlayer.Volume = Singleton.Instance.MasterBGMVolume;
            if (MediaPlayer.State != MediaState.Playing)
            {
                MediaPlayer.Play(Singleton.Instance.song[Singleton.Instance.mediaPlaySong]);
            }
            else if (Singleton.Instance.mediaPlaySong == "TitleScreenRemix" &&
                MediaPlayer.PlayPosition >= new TimeSpan(0, 0, 1, 1, 750))
                MediaPlayer.Play(Singleton.Instance.song["TitleScreenRemix"], new TimeSpan(0, 0, 0, 32, 400));
            else if (Singleton.Instance.mediaPlaySong == "TitleScreen" &&
                MediaPlayer.PlayPosition >= new TimeSpan(0, 0, 1, 0, 830))
                MediaPlayer.Play(Singleton.Instance.song["TitleScreen"], new TimeSpan(0, 0, 0, 34, 200));
            else if (Singleton.Instance.mediaPlaySong == "MenuScreen" &&
                MediaPlayer.PlayPosition >= new TimeSpan(0, 0, 0, 42, 438))
                MediaPlayer.Play(Singleton.Instance.song["MenuScreen"], new TimeSpan(0, 0, 0, 3, 526));
            else if (Singleton.Instance.mediaPlaySong == "PVPBattle" 
                && MediaPlayer.PlayPosition >= new TimeSpan(0, 0, 1, 4, 604))
                MediaPlayer.Play(Singleton.Instance.song["PVPBattle"], new TimeSpan(0, 0, 0, 6, 880));
            else if (Singleton.Instance.mediaPlaySong == "Battle1" 
                && MediaPlayer.PlayPosition >= new TimeSpan(0, 0, 0, 53, 034))
                MediaPlayer.Play(Singleton.Instance.song["Battle1"], new TimeSpan(0, 0, 0, 5, 985));
            else if (Singleton.Instance.mediaPlaySong == "BossBattle1"
                && MediaPlayer.PlayPosition >= new TimeSpan(0, 0, 0, 45, 020))
                MediaPlayer.Play(Singleton.Instance.song["BossBattle1"], new TimeSpan(0, 0, 0, 13, 053));
            //else if (Singleton.Instance.mediaPlaySong == "EnemyDeletedShort"
            //    && MediaPlayer.PlayPosition >= new TimeSpan(0, 0, 0, 3, 600))
            //    MediaPlayer.Play(Singleton.Instance.song["EnemyDeletedShort"], new TimeSpan(0, 0, 0, 3, 780));

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            // TODO: Add your drawing code here
            switch (Singleton.Instance.CurrentScreenState)
            {
                case Singleton.ScreenState.TitleScreen:
                    for (int i = 0; i < _numScreenSprites; i++)
                    {
                        if (_screenSprites[i].IsActive) _screenSprites[i].Draw(spriteBatch);
                    }
                    break;
                case Singleton.ScreenState.MenuScreen:
                    for (int i = 0; i < _numScreenSprites; i++)
                    {
                        if (_screenSprites[i].IsActive) _screenSprites[i].Draw(spriteBatch);
                    }
                    break;
                case Singleton.ScreenState.StoryMode:
                    for (int i = 0; i < _numObject; i++)
                    {
                        if (_sprites[i].IsActive) _sprites[i].Draw(spriteBatch);
                    }
                    break;
            }
            spriteBatch.End();
            graphics.BeginDraw();

            base.Draw(gameTime);
        }

        protected void Reset()
        {
            Singleton.Instance.useChip = false;
            Singleton.Instance.useNormalChip = false;
            Singleton.Instance.useSceneChip = false;
            Singleton.Instance.useThrowableChip = false;
            Singleton.Instance.selectChipSuccess = false;
            Singleton.Instance.newTurnCustom = false;
            Singleton.Instance.isCustomBarFull = false;
            Singleton.Instance.MasterBGMVolume = 0.6f;
            Singleton.Instance.MasterSFXVolume = 0.8f;

            Singleton.Instance.song = new Dictionary<string, Song>()
            {
                {"TitleScreen", Content.Load<Song>("bgm/TitleScreen") },
                {"TitleScreenRemix", Content.Load<Song>("bgm/TitleScreenRemix") },
                {"MenuScreen", Content.Load<Song>("bgm/MenuScreen") },
                {"PVPBattle", Content.Load<Song>("bgm/PVPBattle(Re)-RNR3") },
                {"Battle1", Content.Load<Song>("bgm/Battle1") },
                {"BossBattle1", Content.Load<Song>("bgm/TournamentBattle-EXE4.5") },
                {"EnemyDeletedShort", Content.Load<Song>("bgm/EnemyDeleted(short)") },
            };
            //titleScreenTexture
            titleScreenTexture[0] = Content.Load<Texture2D>("background/titleScreen1");
            titleScreenTexture[1] = Content.Load<Texture2D>("background/titleScreen2");
            titleScreenTexture[2] = Content.Load<Texture2D>("background/logoTitle");
            //menuScreenTexture
            menuScreenTexture[0] = Content.Load<Texture2D>("background/MenuScreenBlack");
            menuScreenTexture[1] = Content.Load<Texture2D>("background/BlackAce");
            menuScreenTexture[2] = Content.Load<Texture2D>("background/logoTitle");

            backgroundTexture[0] = Content.Load<Texture2D>("background/Space");
            panelTexture[0] = Content.Load<Texture2D>("panel/PanelsEXE5");
            playersTexture[0] = Content.Load<Texture2D>("rockman/RockmanEXE6");
            playersTexture[1] = Content.Load<Texture2D>("rockman/RockmanBusterEXE6");
            playersTexture[2] = Content.Load<Texture2D>("rockman/BlackAceSprite");
            playersTexture[3] = Content.Load<Texture2D>("rockman/BlackAceEffect");
            enemiesTexture[0] = Content.Load<Texture2D>("virus/MettonAttack");
            enemiesTexture[1] = Content.Load<Texture2D>("virus/MettFire");
            enemiesTexture[2] = Content.Load<Texture2D>("virus/Wizard");
            enemiesTexture[3] = Content.Load<Texture2D>("boss/QueenVirgo");
            Singleton.Instance.effectsTexture[0] = this.Content.Load<Texture2D>("battleEffect/busterEffect");
            Singleton.Instance.effectsTexture[1] = this.Content.Load<Texture2D>("battleEffect/chargeBuster");
            Singleton.Instance.effectsTexture[2] = this.Content.Load<Texture2D>("battleEffect/ImpacteffectHalfexplosion");
            Singleton.Instance.effectsTexture[3] = this.Content.Load<Texture2D>("battleEffect/Uninstall");
            Singleton.Instance.effectsTexture[4] = this.Content.Load<Texture2D>("battleEffect/ImpactExplosion");
            customScreenTexture[0] = Content.Load<Texture2D>("screen/CustomScreen");
            customScreenTexture[1] = Content.Load<Texture2D>("screen/CustomWindow");
            customScreenTexture[2] = Content.Load<Texture2D>("screen/EmotionEXE5");
            customScreenTexture[3] = Content.Load<Texture2D>("screen/BattleStart");
            customScreenTexture[4] = Content.Load<Texture2D>("screen/BlackAceEmblem");
            customScreenTexture[5] = Content.Load<Texture2D>("screen/CustomBomb");
            customScreenTexture[6] = Content.Load<Texture2D>("screen/PushButtonEXE3");
            chipTexture[0] = Content.Load<Texture2D>("chipAtk/chipList");
            chipTexture[1] = Content.Load<Texture2D>("chipAtk/chipIconEXE6");
            chipTexture[2] = Content.Load<Texture2D>("chipAtk/BarrierEXE6");
            chipTexture[3] = Content.Load<Texture2D>("chipAtk/chipIconEXE4");
            chipTexture[4] = Content.Load<Texture2D>("chipAtk/Recovery");
            chipTexture[5] = Content.Load<Texture2D>("chipAtk/AirShot");
            chipTexture[6] = Content.Load<Texture2D>("chipAtk/Spreader");
            chipTexture[7] = Content.Load<Texture2D>("chipAtk/Cannon");
            chipTexture[8] = Content.Load<Texture2D>("chipAtk/Throwables");
            chipTexture[9] = Content.Load<Texture2D>("chipAtk/Meteor");
            chipTexture[10] = Content.Load<Texture2D>("chipAtk/RainyNoShade");
            chipTexture[11] = Content.Load<Texture2D>("chipAtk/MagicCircle");
            chipTexture[12] = Content.Load<Texture2D>("chipAtk/MagicFreeze");
            chipTexture[13] = Content.Load<Texture2D>("chipAtk/RiverBNK48");

            // --> screenSpritesList
            _screenSprites = new List<Sprite>();
            //titleScreenSprite
            _screenSprites.Add(new TitleScreen(titleScreenTexture)
            {
                Name = "TitleScreen",
                SoundEffects = new Dictionary<string, SoundEffectInstance>()
                {
                    {"PressStart", Content.Load<SoundEffect>("sfx/PressStart").CreateInstance() },
                }
            });
            //menuScreenSprite
            _screenSprites.Add(new MenuScreen(menuScreenTexture)
            {
                Name = "MenuScreen",
                SoundEffects = new Dictionary<string, SoundEffectInstance>()
                {
                    {"PressStart", Content.Load<SoundEffect>("sfx/PressStart").CreateInstance() },
                }
            });

            // --> spriteList
            _sprites = new List<Sprite>();
            //backgroundSprite
            _sprites.Add(new BackgroundSprite(backgroundTexture)
            {
                Name = "Background",
            });
            //panelSprite
            _sprites.Add(new PanelSprite(panelTexture)
            {
                Name = "Panel",
                SoundEffects = new Dictionary<string, SoundEffectInstance>()
                {
                    {"PanelCrack", Content.Load<SoundEffect>("sfx/PanelCrack").CreateInstance() },
                }
            });
            //fadeScreen
            _sprites.Add(new FadeScreen(fadeScreenTexture)
            {
                Name = "FadeScreen",
            });
            //magicCircleSprite
            _sprites.Add(new MagicCircle(new Dictionary<string, Animation>()
            {
                { "MagicCircle", new Animation(chipTexture[11], new Rectangle(0, 0,  307, 211), 1) },
            })
            {
                Name = "MagicCircle",
                Viewport = new Rectangle(0, 0, 307, 211),
                SoundEffects = new Dictionary<string, SoundEffectInstance>()
                {
                    {"MagicCircle", Content.Load<SoundEffect>("sfx/MagicCircle").CreateInstance() },
                }
            });
            //mettonSprite
            _sprites.Add(new MettonSprite(enemiesTexture)
            {
                Name = "Metton",
                HP = 40,
                Attack = 10,
                SoundEffects = new Dictionary<string, SoundEffectInstance>()
                {
                    {"Explosion", Content.Load<SoundEffect>("sfx/VirusExplode").CreateInstance() },
                }
            });
            //mettonWaveSprite
            _sprites.Add(new MettonWaveSprite(enemiesTexture)
            {
                Name = "MettonWave",
                SoundEffects = new Dictionary<string, SoundEffectInstance>()
                {
                    {"MettonWave", Content.Load<SoundEffect>("sfx/MettWave").CreateInstance() },
                }
            });
            //mageSprite
            _sprites.Add(new Mage(new Dictionary<string, Animation>()
            {
                { "Alive", new Animation(enemiesTexture[2], new Rectangle(70, 0, 58*5 , 72), 5) },
                { "Change", new Animation(enemiesTexture[2], new Rectangle(70, 72, 58*4 , 72), 4) },
                { "Casting", new Animation(enemiesTexture[2], new Rectangle(70, 144, 58*4 , 72), 4) },
            })
            {
                Name = "Mage",
                Viewport = new Rectangle(70, 0, 58, 72),
                SoundEffects = new Dictionary<string, SoundEffectInstance>()
                {
                    {"Casting", Content.Load<SoundEffect>("sfx/SpellCasting").CreateInstance() },
                    {"Paneltolce", Content.Load<SoundEffect>("sfx/Paneltolce").CreateInstance() },
                    {"Explosion", Content.Load<SoundEffect>("sfx/VirusExplode").CreateInstance() },
                }
            });
            //wizardSprite
            _sprites.Add(new Wizard(new Dictionary<string, Animation>()
            {
                { "Alive", new Animation(enemiesTexture[1], new Rectangle(21, 0, 38*4 , 66), 4) },
                { "Casting", new Animation(enemiesTexture[1], new Rectangle(2, 66, 39*5 , 66), 5) },
            })
            {
                Name = "Wizard",
                Viewport = new Rectangle(6, 0, 40, 72),
                SoundEffects = new Dictionary<string, SoundEffectInstance>()
                {
                    {"Casting", Content.Load<SoundEffect>("sfx/SpellCasting").CreateInstance() },
                    {"Explosion", Content.Load<SoundEffect>("sfx/VirusExplode").CreateInstance() },
                }
            });
            //meteorSprite
            _sprites.Add(new MeteorSprite(new Dictionary<string, Animation>()
            {
                { "Meteor", new Animation(chipTexture[9], new Rectangle(0, 0, 60 , 49), 1) },
                { "MeteorCracked", new Animation(chipTexture[9], new Rectangle(60, 0, 60*6 , 49), 6) },
            })
            {
                Name = "Meteor",
                Position = new Vector2(800,900),
                Viewport = new Rectangle(0, 0, 60, 49),
                SoundEffects = new Dictionary<string, SoundEffectInstance>()
                {
                    {"Meteor", Content.Load<SoundEffect>("sfx/Meteor").CreateInstance() },
                }
            });
            //queenVirgoSprite
            _sprites.Add(new QueenVirgo(new Dictionary<string, Animation>()
            {
                { "Alive", new Animation(enemiesTexture[3], new Rectangle(0, 0, 200, 200), 1) },
                { "StartCasting", new Animation(enemiesTexture[3], new Rectangle(200, 0, 200, 200), 1) },
                { "Casting", new Animation(enemiesTexture[3], new Rectangle(0, 200, 200*4, 200), 4) },
                { "FinishCasting", new Animation(enemiesTexture[3], new Rectangle(0, 400, 200*3, 200), 3) },
            })
            {
                Name = "QueenVirgo",
                Viewport = new Rectangle(0, 0, 200, 200),
                SoundEffects = new Dictionary<string, SoundEffectInstance>()
                {
                    {"AquaShield", Content.Load<SoundEffect>("sfx/AquaShield").CreateInstance() },
                    {"AquaProtect", Content.Load<SoundEffect>("sfx/AttackBounce").CreateInstance() },
                    {"Defeated", Content.Load<SoundEffect>("sfx/DefeatedExplode").CreateInstance() },
                }
            });
            //aquaWaveSprite
            _sprites.Add(new AquaWave(new Dictionary<string, Animation>()
            {
                { "AquaWave", new Animation(enemiesTexture[0], new Rectangle(0, 60,  50*5, 60), 5) },
            })
            {
                Name = "AquaWave",
                Viewport = new Rectangle(0, 50, 50, 60),
                SoundEffects = new Dictionary<string, SoundEffectInstance>()
                {
                    {"AquaWave", Content.Load<SoundEffect>("sfx/AquaWave").CreateInstance() },
                }
            });
            //rainySprite
            _sprites.Add(new Rainy(new Dictionary<string, Animation>()
            {
                { "Cloudy", new Animation(chipTexture[10], new Rectangle(0, 0,  32*4, 72), 4) },
                { "Rainy", new Animation(chipTexture[10], new Rectangle(128, 0,  32*4, 72), 4) },
            })
            {
                Name = "Rainy",
                Viewport = new Rectangle(0, 0, 32, 72),
                SoundEffects = new Dictionary<string, SoundEffectInstance>()
                {
                    {"Rainy", Content.Load<SoundEffect>("sfx/Rainy").CreateInstance() },
                }
            });
            //magicFreezeSprite
            _sprites.Add(new MagicFreeze(new Dictionary<string, Animation>()
            {
                { "Freezing", new Animation(chipTexture[12], new Rectangle(0, 0, 64 * 5, 64), 5) },
                { "Frozen", new Animation(chipTexture[12], new Rectangle(0, 64, 64, 64), 1) },
                { "Break", new Animation(chipTexture[12], new Rectangle(64, 64, 64, 64), 1) },
            }) 
            {
                Name = "MagicFreeze",
                Viewport = new Rectangle(0, 0, 64, 64),
                SoundEffects = new Dictionary<string, SoundEffectInstance>()
                {
                    {"MagicFreeze", Content.Load<SoundEffect>("sfx/MagicFreeze").CreateInstance() },
                }
            });
            //barrierSprite
            _sprites.Add(new BarrierSprite(new Dictionary<string, Animation>()
            {
                { "Barrier", new Animation(chipTexture[2], new Rectangle(-1, 0, 65*4 , 63), 4) },
                { "Barrier100", new Animation(chipTexture[2], new Rectangle(-1, 63, 65*4 , 63), 4) },
                { "Barrier200", new Animation(chipTexture[2], new Rectangle(-1, 63*2, 65*4 , 63), 4) },
                { "DreamAura", new Animation(chipTexture[2], new Rectangle(0, (63*3)+1, 65*3 , 62), 3) },
            })
            {
                Name = "BarrierSprite",
                Viewport = new Rectangle(65, 62, 65, 62),
            });
            //rockmanEXE
            _sprites.Add(new RockmanEXESprite(new Dictionary<string, Animation>()
            {
                { "Alive", new Animation(playersTexture[0], new Rectangle(9, 1, 57, 57), 1) },
                { "Buster", new Animation(playersTexture[0], new Rectangle(9, 527, 43*4, 52), 4) },
                { "BombPrepare", new Animation(playersTexture[0], new Rectangle(10, 370, 49, 52), 1) },
                { "Bomb", new Animation(playersTexture[0], new Rectangle(10, 370, 49*7, 52), 7) },
                { "Dead", new Animation(playersTexture[0], new Rectangle(9, 58, 42, 57), 1) },
                { "Uninstall", new Animation(Singleton.Instance.effectsTexture[3], new Rectangle(0, 0, 75*4, 68), 4) },
            })
            {
                Name = "RockmanEXE",
                Viewport = new Rectangle(57, 0, 57, 57),
                W = Keys.W,
                S = Keys.S,
                A = Keys.A,
                D = Keys.D,
                J = Keys.J,
                K = Keys.K,
                U = Keys.U,
                SoundEffects = new Dictionary<string, SoundEffectInstance>()
                {
                    {"Barrier", Content.Load<SoundEffect>("sfx/AttackBounce").CreateInstance() },
                    {"Buster", Content.Load<SoundEffect>("sfx/Buster").CreateInstance() },
                    {"Charging", Content.Load<SoundEffect>("sfx/BusterCharging").CreateInstance() },
                    {"Charged", Content.Load<SoundEffect>("sfx/BusterCharged").CreateInstance() },
                    {"BusterHit", Content.Load<SoundEffect>("sfx/BusterHit").CreateInstance() },
                    {"Damaged", Content.Load<SoundEffect>("sfx/Damaged").CreateInstance() },
                    {"UseChip", Content.Load<SoundEffect>("sfx/UseChip").CreateInstance() },
                    {"LowHP", Content.Load<SoundEffect>("sfx/RedHP").CreateInstance() },
                    {"Deleted", Content.Load<SoundEffect>("sfx/Deleted").CreateInstance() },
                }
            });
            //blackAce
            _sprites.Add(new BlackAce(new Dictionary<string, Animation>()
            {
                { "Alive", new Animation(playersTexture[2], new Rectangle(0, 0, 80*4, 80), 4) },
                { "NormalBuster", new Animation(playersTexture[2], new Rectangle(0, 80, 80*4, 80), 4) },
                { "Buster", new Animation(playersTexture[2], new Rectangle(0, 160, 80*4, 80), 4) },
                { "BombPrepare", new Animation(playersTexture[2], new Rectangle(0, 160, 80, 80), 1) },
                { "Bomb", new Animation(playersTexture[2], new Rectangle(0, 160, 80*4, 80), 4) },
                { "Sword", new Animation(playersTexture[2], new Rectangle(0, 240, 80*7, 80), 7) },
                { "Dead", new Animation(playersTexture[2], new Rectangle(0, 0, 80*4, 80), 4) },
                { "Uninstall", new Animation(Singleton.Instance.effectsTexture[3], new Rectangle(0, 0, 75*4, 68), 4) },
            })
            {
                Name = "BlackAce",
                Viewport = new Rectangle(0, 0, 80, 80),
                W = Keys.W,
                S = Keys.S,
                A = Keys.A,
                D = Keys.D,
                J = Keys.J,
                K = Keys.K,
                U = Keys.U,
                SoundEffects = new Dictionary<string, SoundEffectInstance>()
                {
                    {"Barrier", Content.Load<SoundEffect>("sfx/AttackBounce").CreateInstance() },
                    {"Buster", Content.Load<SoundEffect>("sfx/Buster").CreateInstance() },
                    {"Charging", Content.Load<SoundEffect>("sfx/BusterCharging").CreateInstance() },
                    {"Charged", Content.Load<SoundEffect>("sfx/BusterCharged").CreateInstance() },
                    {"BusterHit", Content.Load<SoundEffect>("sfx/BusterHit").CreateInstance() },
                    {"Damaged", Content.Load<SoundEffect>("sfx/Damaged").CreateInstance() },
                    {"UseChip", Content.Load<SoundEffect>("sfx/UseChip").CreateInstance() },
                    {"LowHP", Content.Load<SoundEffect>("sfx/RedHP").CreateInstance() },
                    {"Deleted", Content.Load<SoundEffect>("sfx/Deleted").CreateInstance() },
                }
            });
            //blackAceEffect
            _sprites.Add(new BlackAceEffect(new Dictionary<string, Animation>()
            {
                { "Changing", new Animation(playersTexture[3], new Rectangle(1, 0, 65*2, 82), 2) },
                { "Changed", new Animation(playersTexture[3], new Rectangle(2, 82, 70*2, 82), 2) },
            })
            {
                Name = "BlackAceEffect",
                Viewport = new Rectangle(0, 0, 64, 82),
                SoundEffects = new Dictionary<string, SoundEffectInstance>()
                {
                    
                }
            });
            //busterRockman
            _sprites.Add(new Buster(new Dictionary<string, Animation>()
            {
                { "NormalBuster", new Animation(playersTexture[1], new Rectangle(7, 3, 20*4, 7), 4) },
            })
            {
                Name = "RockmanBuster",
                Viewport = new Rectangle(8, 4, 19, 7),
            });
            //cannonSprite
            _sprites.Add(new CannonSprite(new Dictionary<string, Animation>()
            {
                { "Cannon", new Animation(chipTexture[7], new Rectangle(-1, 0, 65*8, 60), 8) },
                { "HiCannon", new Animation(chipTexture[7], new Rectangle(-1, 60, 65*8, 60), 8) },
                { "MegaCannon", new Animation(chipTexture[7], new Rectangle(-1, 120, 65*8, 60), 8) },
                { "DarkCannon", new Animation(chipTexture[7], new Rectangle(-1, 180, 65*11, 60), 11) },
            })
            {
                Name = "CannonSprite",
                Viewport = new Rectangle(0, 0, 65, 60),
            });
            //airShotSprite
            _sprites.Add(new AirShotSprite(new Dictionary<string, Animation>()
            {
                { "AirShot", new Animation(chipTexture[5], new Rectangle(0, 2, 50*3, 42), 3) },
            })
            {
                Name = "AirShotSprite",
                Viewport = new Rectangle(0, 2, 50, 42),
            });
            //spreaderSprite
            _sprites.Add(new SpreaderSprite(new Dictionary<string, Animation>()
            {
                { "Spreader", new Animation(chipTexture[6], new Rectangle(0, 0, 55*5, 32), 5) },
                { "DarkSpread", new Animation(chipTexture[6], new Rectangle(0, 32, 55*6, 32), 6) },
            })
            {
                Name = "SpreaderSprite",
                Viewport = new Rectangle(0, 0, 55, 32),
            });
            //spreaderEffect
            _sprites.Add(new SpreaderEffect(new Dictionary<string, Animation>()
            {
                { "Spreader", new Animation(chipTexture[6], new Rectangle(0, 64, 55*6, 32), 6) },
                { "DarkSpread", new Animation(chipTexture[6], new Rectangle(0, 96, 55*6, 32), 6) },
            })
            {
                Name = "SpreaderEffect",
                Viewport = new Rectangle(0, 64, 55, 32),
            });
            //throwableSprite
            _sprites.Add(new ThrowableSprite(new Dictionary<string, Animation>()
            {
                { "MiniBomb", new Animation(chipTexture[8], new Rectangle(22, 4, 11, 11), 1) },
                { "BigBomb", new Animation(chipTexture[8], new Rectangle(41, 4, 11, 11), 1) },
                {"EnergyBomb",  new Animation(chipTexture[8], new Rectangle(0, 22, 20*5, 15), 5) },
                {"MegaEnergyBomb",  new Animation(chipTexture[8], new Rectangle(0, 22, 20*5, 15), 5) },
                {"BugBomb",  new Animation(chipTexture[8], new Rectangle(1, 43, 22, 22), 1) },
                {"SearchBomb1",  new Animation(chipTexture[8], new Rectangle(20, 150, 20*5, 16), 5) },
                {"SearchBomb2",  new Animation(chipTexture[8], new Rectangle(20, 150, 20*5, 16), 5) },
                {"SearchBomb3",  new Animation(chipTexture[8], new Rectangle(20, 150, 20*5, 16), 5) },
                {"CannonBall",  new Animation(chipTexture[8], new Rectangle(2, 4, 11, 11), 1) },
                {"BlackBomb",  new Animation(chipTexture[8], new Rectangle(1, 71, 22, 30), 1) },
                {"DarkBomb",  new Animation(chipTexture[8], new Rectangle(60, 4, 11, 11), 1) },
            })
            {
                Name = "ThrowableSprite",
                Viewport = new Rectangle(22, 4, 11, 11),
                SoundEffects = new Dictionary<string, SoundEffectInstance>()
                {
                    {"Throw", Content.Load<SoundEffect>("sfx/Throwable").CreateInstance() },
                    {"BombExplosion", Content.Load<SoundEffect>("sfx/BombExplosion").CreateInstance() },
                    {"CannonBall", Content.Load<SoundEffect>("sfx/CannonBall").CreateInstance() },
                    {"EnergyBomb", Content.Load<SoundEffect>("sfx/EnergyBomb").CreateInstance() },
                }
            });
            //recoverySprite
            _sprites.Add(new RecoverySprite(new Dictionary<string, Animation>()
            {
                { "Recovery", new Animation(chipTexture[4], new Rectangle(5, 0, 40*8 , 72), 8) },
            })
            {
                Name = "RecoverySprite",
                Viewport = new Rectangle(6, 0, 40, 72),
            });
            //impactHalfExplodeSprite
            _sprites.Add(new ImpactHalfExplodeEffect(new Dictionary<string, Animation>()
            {
                { "HalfExplode", new Animation(Singleton.Instance.effectsTexture[2], new Rectangle(0, 0, 33*9 , 52), 9) },
            })
            {
                Name = "ImpactHalfExplodeSprite",
                Viewport = new Rectangle(0, 0, 33, 52),
            });
            //impactExplosionSprite
            _sprites.Add(new ImpactExplosionEffect(new Dictionary<string, Animation>()
            {
                { "NormalExplosion", new Animation(Singleton.Instance.effectsTexture[4], new Rectangle(0, 0, 55*6, 60), 6) },
                { "DarkExplosion", new Animation(Singleton.Instance.effectsTexture[4], new Rectangle(0, 60, 55*6, 60), 6) },

            })
            {
                Name = "ImpactExplosionSprite",
                Viewport = new Rectangle(0, 0, 55, 60),
            });
            //customBar
            _sprites.Add(new CustomBar(new Dictionary<string, Animation>()
            {
                { "CustomBar", new Animation(customScreenTexture[0], new Rectangle(440, 117, 148, 15), 1) },
            })
            {
                Name = "CustomBar",
                Viewport = new Rectangle(446, 117, 137, 15),
                Position = new Vector2(Singleton.WIDTH / 3, 10),
                SoundEffects = new Dictionary<string, SoundEffectInstance>()
                {
                    {"FullCustom", Content.Load<SoundEffect>("sfx/CustBarFull").CreateInstance() },
                }
            });
            //Bar
            _sprites.Add(new CustomBar(customScreenTexture)
            {
                Name = "Bar",
                Viewport = new Rectangle(452, 139, 8, 8),
                SoundEffects = new Dictionary<string, SoundEffectInstance>()
                {
                    {"FullCustom", Content.Load<SoundEffect>("sfx/CustBarFull").CreateInstance() },
                }
            });
            //customBomb
            _sprites.Add(new CustomBomb(new Dictionary<string, Animation>()
            {
                { "CustomBomb", new Animation(customScreenTexture[5], new Rectangle(15, 0, 144, 15), 1) },
            })
            {
                Name = "CustomBomb",
                Viewport = new Rectangle(15, 0, 144, 15),
                Position = new Vector2(Singleton.WIDTH / 3, 10),
                SoundEffects = new Dictionary<string, SoundEffectInstance>()
                {
                    {"FullCustom", Content.Load<SoundEffect>("sfx/CustBarFull").CreateInstance() },
                }
            });
            //BombChargeBar
            _sprites.Add(new CustomBomb(customScreenTexture)
            {
                Name = "BombChargeBar",
                Viewport = new Rectangle(30, 21, 5, 5),
                SoundEffects = new Dictionary<string, SoundEffectInstance>()
                {
                    {"FullCustom", Content.Load<SoundEffect>("sfx/CustBarFull").CreateInstance() },
                }
            });
            //customScreen
            _sprites.Add(new CustomScreen(new Dictionary<string, Animation>()
            {
                { "Screen", new Animation(customScreenTexture[0], new Rectangle(104, 7, 120, 160), 1) },
            })
            {
                Name = "CustomScreen",
                Position = new Vector2(0, 0),
                Viewport = new Rectangle(104, 6, 160, 120),
                SoundEffects = new Dictionary<string, SoundEffectInstance>()
                {
                    {"Custom", Content.Load<SoundEffect>("sfx/CustomScreenOpen").CreateInstance() },
                }
            });
            //customWindow
            _sprites.Add(new CustomWindow(new Dictionary<string, Animation>()
            {
                { "Standard", new Animation(customScreenTexture[1], new Rectangle(9, 255, 89, 105), 1) },
                { "Mega", new Animation(customScreenTexture[1], new Rectangle(107, 255, 89, 105), 1) },
                { "Giga", new Animation(customScreenTexture[1], new Rectangle(205, 255, 89, 105), 1) },
                { "Dark", new Animation(customScreenTexture[1], new Rectangle(303, 255, 89, 105), 1) },
            })
            {
                Name = "CustomWindow",
                Position = new Vector2(0, 0),
                Viewport = new Rectangle(9, 255, 89, 105),
            });
            //blackAceEmblem
            _sprites.Add(new BlackAceEmblem(new Dictionary<string, Animation>()
            {
                { "Available", new Animation(customScreenTexture[4], new Rectangle(0, 0, 50, 50), 1) },
                { "Disable", new Animation(customScreenTexture[4], new Rectangle(50, 0, 50, 50), 1) },
            })
            {
                Name = "BlackAceEmblem",
                Position = new Vector2(285, 400),
                Viewport = new Rectangle(0, 0, 50, 50),
            });
            //battleStart
            _sprites.Add(new BattleStart(new Dictionary<string, Animation>()
            {
                { "BattleStart", new Animation(customScreenTexture[3], new Rectangle(0, 0, 140*3, 20), 3) },
                { "BattleStartFreeze", new Animation(customScreenTexture[3], new Rectangle(280, 0, 140, 20), 1) },
                { "EnemyDeleted", new Animation(customScreenTexture[3], new Rectangle(0, 20, 140*3, 20), 3)},
                { "EnemyDeletedFreeze", new Animation(customScreenTexture[3], new Rectangle(280, 20, 140, 20), 1) },
            })
            {
                Name = "BattleStart",
                Position = new Vector2(Singleton.WIDTH/3, Singleton.HEIGHT/4),
                Viewport = new Rectangle(0, 0, 280, 20),
            });
            //gaugeHPandNoise
            _sprites.Add(new HealthGauge(customScreenTexture)
            {
                Name = "HealthNoiseGauge",
            });
            ////emotionPlayer
            //_sprites.Add(new EmotionPlayer(customScreenTexture)
            //{
            //    Name = "EmotionPlayer",
            //});
            //chipType
            _sprites.Add(new ChipType(new Dictionary<string, Animation>()
            {
                { "Fire", new Animation(customScreenTexture[1], new Rectangle(204, 47, 14, 14), 1) },
                { "Aqua", new Animation(customScreenTexture[1], new Rectangle(204+(16*1), 47, 14, 14), 1) },
                { "Elec", new Animation(customScreenTexture[1], new Rectangle(204+(16*2), 47, 14, 14), 1) },
                { "Wood", new Animation(customScreenTexture[1], new Rectangle(204+(16*3), 47, 14, 14), 1) },
                { "Sword", new Animation(customScreenTexture[1], new Rectangle(204+(16*4), 47, 14, 14), 1) },
                { "Wind", new Animation(customScreenTexture[1], new Rectangle(204+(16*5), 47, 14, 14), 1) },
                { "Scope", new Animation(customScreenTexture[1], new Rectangle(204, 64, 14, 14), 1) },
                { "Box", new Animation(customScreenTexture[1], new Rectangle(204+(16*1), 64, 14, 14), 1) },
                { "Number", new Animation(customScreenTexture[1], new Rectangle(204+(16*2), 64, 14, 14), 1) },
                { "Break", new Animation(customScreenTexture[1], new Rectangle(204+(16*3), 64, 14, 14), 1) },
                { "Normal", new Animation(customScreenTexture[1], new Rectangle(204+(16*4), 64, 14, 14), 1) },
            })
            {
                Name = "ChipType",
                Position = new Vector2(75, 219),
                Viewport = new Rectangle(9, 255, 89, 105),
            });
            //ChipIcon
            _sprites.Add(new ChipIcon(chipTexture)
            {
                Name = "ChipIcon",
            });
            //chipSelect
            _sprites.Add(new ChipSelect(new Dictionary<string, Animation>()
            {
                { "Select", new Animation(customScreenTexture[1], new Rectangle(330, 75, 30*2, 22), 2) },
            })
            {
                Name = "ChipSelect",
                Viewport = new Rectangle(330, 75, 30, 22),
                W = Keys.W,
                S = Keys.S,
                A = Keys.A,
                D = Keys.D,
                J = Keys.J,
                K = Keys.K,
                SoundEffects = new Dictionary<string, SoundEffectInstance>()
                {
                    {"BlackAceSelected", Content.Load<SoundEffect>("sfx/BlackAceSelected").CreateInstance() },
                    {"ChipSelect", Content.Load<SoundEffect>("sfx/ChipSelect").CreateInstance() },
                    {"ChipChoose", Content.Load<SoundEffect>("sfx/ChipChoose").CreateInstance() },
                    {"ChipCancel", Content.Load<SoundEffect>("sfx/ChipCancel").CreateInstance() },
                    {"ChipConfirm", Content.Load<SoundEffect>("sfx/ChipConfirm").CreateInstance() },
                }
            });
            //chipCannon
            _sprites.Add(new Cannon(chipTexture)
            {
                Name = "CannonChip",
                Viewport = new Rectangle(392, 0, 56, 47),
                SoundEffects = new Dictionary<string, SoundEffectInstance>()
                {
                    {"Cannon", Content.Load<SoundEffect>("sfx/Cannon").CreateInstance() },
                }
            });
            //chipAirShot
            _sprites.Add(new AirShot(chipTexture)
            {
                Name = "AirShotChip",
                Viewport = new Rectangle(168, 0, 56, 47),
                SoundEffects = new Dictionary<string, SoundEffectInstance>()
                {
                    {"AirShot", Content.Load<SoundEffect>("sfx/Spreader").CreateInstance() },
                }
            });
            //chipSpreader
            _sprites.Add(new Spreader(chipTexture)
            {
                Name = "SpreaderChip",
                Viewport = new Rectangle(392, 0, 56, 47),
                SoundEffects = new Dictionary<string, SoundEffectInstance>()
                {
                    {"Spreader", Content.Load<SoundEffect>("sfx/Spreader").CreateInstance() },
                }
            });
            //chipBomb
            _sprites.Add(new Throwable(chipTexture)
            {
                Name = "ThrowableChip",
                Viewport = new Rectangle(0, 48, 56, 47),
            });
            //chipRecovery
            _sprites.Add(new Recovery(chipTexture)
            {
                Name = "RecoveryChip",
                Viewport = new Rectangle(0, 145, 56, 48),
                SoundEffects = new Dictionary<string, SoundEffectInstance>()
                {
                    {"Recovery", Content.Load<SoundEffect>("sfx/Recover").CreateInstance() },
                }
            });
            //chipPanel
            _sprites.Add(new PanelChip(chipTexture)
            {
                Name = "PanelChip",
                Viewport = new Rectangle(448, 144, 56, 47),
                SoundEffects = new Dictionary<string, SoundEffectInstance>()
                {
                    {"PanelPower", Content.Load<SoundEffect>("sfx/PanelPower").CreateInstance() },
                }
            });
            //chipBarrier
            _sprites.Add(new Barrier(chipTexture)
            {
                Name = "BarrierChip",
                Viewport = new Rectangle(224, 192, 56, 47),
                SoundEffects = new Dictionary<string, SoundEffectInstance>()
                {
                    {"Barrier", Content.Load<SoundEffect>("sfx/Barrier").CreateInstance() },
                }
            });
            //chipCrackOut
            _sprites.Add(new CrackOut(chipTexture)
            {
                Name = "CrackOutChip",
                Viewport = new Rectangle(112, 240, 56, 47),
                SoundEffects = new Dictionary<string, SoundEffectInstance>()
                {
                    {"CrackOut", Content.Load<SoundEffect>("sfx/CrackOut").CreateInstance() },
                }
            });
            //chipRiverBNK48
            _sprites.Add(new RiverBNK(chipTexture)
            {
                Name = "RiverBNK48",
                Viewport = new Rectangle(0, 0, 168, 144),
                Position = new Vector2(16 * 3, 24 * 3 - 2),
                SoundEffects = new Dictionary<string, SoundEffectInstance>()
                {
                    {"MagicCircle", Content.Load<SoundEffect>("sfx/MagicCircle").CreateInstance() },
                    {"MagicFreeze", Content.Load<SoundEffect>("sfx/MagicFreeze").CreateInstance() },
                }
            });
            ////chipBlackEndGalaxy
            //_sprites.Add(new BlackEndGalaxy(chipTexture)
            //{
            //    Name = "BlackEndGalaxy",
            //    Viewport = new Rectangle(56, 240, 56, 47),
            //    SoundEffects = new Dictionary<string, SoundEffectInstance>()
            //    {
            //        {"BlackEndGalaxy", Content.Load<SoundEffect>("sfx/CrackOut").CreateInstance() },
            //    }
            //});
            //chipBlackAce
            _sprites.Add(new BlackAceChip(chipTexture)
            {
                Name = "BlackAceChip",
                Viewport = new Rectangle(0, 432, 56, 47),
                SoundEffects = new Dictionary<string, SoundEffectInstance>()
                {
                    {"Finalize!", Content.Load<SoundEffect>("sfx/Finalize!").CreateInstance() },
                    {"BlackAce!", Content.Load<SoundEffect>("sfx/BlackAce!").CreateInstance() },
                    {"FinalizeChanging", Content.Load<SoundEffect>("sfx/FinalizeChanging").CreateInstance() },
                    {"FinalizeChanged", Content.Load<SoundEffect>("sfx/FinalizeChanged").CreateInstance() },
                }
            });
            //fadeAppearWhiteScreen
            _sprites.Add(new AppearScreen(fadeScreenTexture)
            {
                Name = "FadeWhiteScreen",
                Viewport = new Rectangle(0, 0, 1200, 800),
                SoundEffects = new Dictionary<string, SoundEffectInstance>()
                {
                    //to do
                }
            });

            Singleton.Instance._font = Content.Load<SpriteFont>("RockmanFont");
        }
    }
}
