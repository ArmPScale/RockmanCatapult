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

        private List<Sprite> _sprites, _chipSprites, _outSprites;
        Texture2D[] rockmanEXETexture, panelTexture, mettonTexture, backgroundTexture, fadeScreenTexture, chipTexture, customScreenTexture;
        private int _numObject;

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

            backgroundTexture = new Texture2D[10];
            panelTexture = new Texture2D[6];
            rockmanEXETexture = new Texture2D[10];
            mettonTexture = new Texture2D[10];
            fadeScreenTexture = new Texture2D[1];
            customScreenTexture = new Texture2D[5];
            chipTexture = new Texture2D[10];
            Singleton.Instance.effectsTexture = new Texture2D[10];
            Singleton.Instance.soundEffects = new List<SoundEffect>();
            Singleton.Instance.chipSlotIn = new Stack<string>();
            Singleton.Instance.indexChipSlotIn = new Stack<int>();
            Singleton.Instance.useChipSlotIn = new Stack<string>();
            Singleton.Instance.chipStackImg = new string[6]
            {
                "","","","","",""
            };
            Singleton.Instance.chipSelect = new int[6]
            {
                1,0,0,0,0,0
            };
            Singleton.Instance.chipCustomSelect = new string[6]
            {
                "","","","","","NoChip"
            };
            //shuffleBattleChipInFolder
            Singleton.Instance.folderList = new List<string>()
            {
                "DarkRecovery","DoubleCrack","AirShot","Recovery120","Recovery300",
                "DreamAura","Barrier","Barrier100","TripleCrack","AirShot","AirShot"
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
                { 1,0,0,0,0,0,0,0,0,0},
                { 0,0,0,0,0,0,0,0,0,0},
            };
            Singleton.Instance.spriteMove = new int[3, 10]
            {
                { 0,0,0,0,0,0,0,0,0,0},
                { 0,1,0,0,0,0,0,0,2,0},
                { 0,0,0,0,0,0,0,0,0,0},
            };
            Singleton.Instance.spriteHP = new int[3, 10]
            {
                { 0,0,0,0,0,0,0,0,0,0},
                { 0,0,0,0,0,0,0,0,70,0},
                { 0,0,0,0,0,0,0,0,0,0},
            };
            Singleton.Instance.chipAttack = new int[3, 10]
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
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Singleton.Instance.song = this.Content.Load<Song>("bgm/BattleStart-EXE5");
            //Singleton.Instance.song = this.Content.Load<Song>("bgm/TournamentBattle-EXE4.5");
            Singleton.Instance.song = this.Content.Load<Song>("bgm/PVPBattle(Re)-RNR3");
            Singleton.Instance.soundEffects.Add(this.Content.Load<SoundEffect>("sfx/Buster"));
            Singleton.Instance.soundEffects.Add(this.Content.Load<SoundEffect>("sfx/BusterCharging"));
            Singleton.Instance.soundEffects.Add(this.Content.Load<SoundEffect>("sfx/BusterCharged"));
            Singleton.Instance.soundEffects.Add(this.Content.Load<SoundEffect>("sfx/Cannon"));
            Singleton.Instance.soundEffects.Add(this.Content.Load<SoundEffect>("sfx/MettWave"));
            Singleton.Instance.soundEffects.Add(this.Content.Load<SoundEffect>("sfx/DefeatedExplode"));
            Singleton.Instance.soundEffects.Add(this.Content.Load<SoundEffect>("sfx/Deleted"));
            Singleton.Instance.soundEffects.Add(this.Content.Load<SoundEffect>("sfx/UseChip"));

            fadeScreenTexture[0] = new Texture2D(graphics.GraphicsDevice, Singleton.WIDTH, Singleton.HEIGHT);
            Color[] data = new Color[Singleton.WIDTH * Singleton.HEIGHT];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.Black;
            fadeScreenTexture[0].SetData(data);

            Reset();
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Singleton.Instance.CurrentKey = Keyboard.GetState();
            _numObject = _sprites.Count;
            //Singleton.Instance.CurrentGameState = Singleton.GameState.GamePlaying;

            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.GameCustomScreen:
                    for (int i = 0; i < _numObject; i++)
                    {
                        if (_sprites[i].IsActive) _sprites[i].Update(gameTime, _sprites);
                    }
                    if (Singleton.Instance.selectChipSuccess)
                    {
                        Singleton.Instance.selectChipSuccess = false;
                        Singleton.Instance.CurrentGameState = Singleton.GameState.GamePlaying;
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
                    for (int i = 0; i < 3; i++)
                    {
                        if (_sprites[i].IsActive) _sprites[i].Update(gameTime, _sprites);
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
                    break;
            }
            Singleton.Instance.PreviousKey = Singleton.Instance.CurrentKey;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            // TODO: Add your drawing code here
            for (int i = 0; i < _numObject; i++)
            {
                if (_sprites[i].IsActive) _sprites[i].Draw(spriteBatch);
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
            Singleton.Instance.selectChipSuccess = false;
            Singleton.Instance.newTurnCustom = false;
            Singleton.Instance.isCustomBarFull = false;
            Singleton.Instance.MasterBGMVolume = 0.5f;
            Singleton.Instance.MasterSFXVolume = 1f;

            backgroundTexture[0] = Content.Load<Texture2D>("background/space");
            panelTexture[0] = Content.Load<Texture2D>("panel/panelStage");
            rockmanEXETexture[0] = Content.Load<Texture2D>("rockman/RockmanEXE6");
            rockmanEXETexture[1] = Content.Load<Texture2D>("rockman/RockmanBusterEXE6");
            mettonTexture[0] = Content.Load<Texture2D>("virus/MettonAttack");
            Singleton.Instance.effectsTexture[0] = this.Content.Load<Texture2D>("battleEffect/busterEffect");
            Singleton.Instance.effectsTexture[1] = this.Content.Load<Texture2D>("battleEffect/chargeBuster");
            customScreenTexture[0] = Content.Load<Texture2D>("screen/CustomScreen");
            customScreenTexture[1] = Content.Load<Texture2D>("screen/CustomWindow");
            customScreenTexture[2] = Content.Load<Texture2D>("screen/EmotionEXE5");
            chipTexture[0] = Content.Load<Texture2D>("chipAtk/chipList");
            chipTexture[1] = Content.Load<Texture2D>("chipAtk/chipIconEXE6");
            chipTexture[2] = Content.Load<Texture2D>("chipAtk/BarrierEXE6");
            chipTexture[3] = Content.Load<Texture2D>("chipAtk/chipIconEXE4");
            chipTexture[4] = Content.Load<Texture2D>("chipAtk/Recovery");
            chipTexture[5] = Content.Load<Texture2D>("chipAtk/AirShot");

            _sprites = new List<Sprite>()
            {
                new BackgroundSprite(backgroundTexture)
                {
                    Name = "Background",
                },
                new PanelSprite(panelTexture)
                {
                    Name = "Panel",
                    SoundEffects = new Dictionary<string, SoundEffectInstance>()
                    {
                        {"PanelCrack", Content.Load<SoundEffect>("sfx/PanelCrack").CreateInstance() },
                    }
                },
                new FadeScreen(fadeScreenTexture)
                {
                    Name = "Panel",
                },
                new MettonSprite(mettonTexture)
                {
                    Name = "Metton",
                    HP = 40, Attack = 10,
                },
                new MettonWaveSprite(mettonTexture)
                {
                    Name = "MettonWave",
                },
            };
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
                { "Alive", new Animation(rockmanEXETexture[0], new Rectangle(9, 1, 57, 57), 1) },
                { "Buster", new Animation(rockmanEXETexture[0], new Rectangle(9, 527, 43*4, 52), 4) },
                { "Bomb", new Animation(rockmanEXETexture[0], new Rectangle(10, 370, 49*7, 52), 7) },
                { "Dead", new Animation(rockmanEXETexture[0], new Rectangle(9, 58, 42, 57), 1) },
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
            //busterRockman
            _sprites.Add(new Buster(new Dictionary<string, Animation>()
            {
                { "NormalBuster", new Animation(rockmanEXETexture[1], new Rectangle(7, 3, 20*4, 7), 4) },
            })
            {
                Name = "RockmanBuster",
                Viewport = new Rectangle(8, 4, 19, 7),
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
            //recoverySprite
            _sprites.Add(new RecoverySprite(new Dictionary<string, Animation>()
            {
                { "Recovery", new Animation(chipTexture[4], new Rectangle(5, 0, 40*8 , 72), 8) },
            })
            {
                Name = "RecoverySprite",
                Viewport = new Rectangle(6, 0, 40, 72),
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
            //emotionPlayer
            _sprites.Add(new EmotionPlayer(customScreenTexture)
            {
                Name = "EmotionPlayer",
            });
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
                { "Select", new Animation(customScreenTexture[0], new Rectangle(28, 10, 22, 24), 1) },
            })
            {
                Name = "ChipSelect",
                Viewport = new Rectangle(28, 35, 22, 22),
                //W = Keys.W,
                //S = Keys.S,
                A = Keys.A,
                D = Keys.D,
                J = Keys.J,
                K = Keys.K,
                SoundEffects = new Dictionary<string, SoundEffectInstance>()
                {
                    {"ChipSelect", Content.Load<SoundEffect>("sfx/ChipSelect").CreateInstance() },
                    {"ChipChoose", Content.Load<SoundEffect>("sfx/ChipChoose").CreateInstance() },
                    {"ChipCancel", Content.Load<SoundEffect>("sfx/ChipCancel").CreateInstance() },
                    {"ChipConfirm", Content.Load<SoundEffect>("sfx/ChipConfirm").CreateInstance() },
                }
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

            //MediaPlayer.Play(Singleton.Instance.song);
            Singleton.Instance._font = Content.Load<SpriteFont>("RockmanFont");
        }
    }
}
