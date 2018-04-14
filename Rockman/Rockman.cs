using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using Rockman.Sprites;
using Rockman.Models;

namespace Rockman
{
    public class Rockman : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private List<Sprite> _sprites;
        Texture2D[] rockmanEXETexture, panelTexture, mettonTexture, backgroundTexture;

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
            backgroundTexture = new Texture2D[10];
            panelTexture = new Texture2D[6];
            rockmanEXETexture = new Texture2D[10];
            mettonTexture = new Texture2D[10];
            Singleton.Instance.effectsTexture = new Texture2D[10];

            Singleton.Instance.soundEffects = new List<SoundEffect>();

            IsMouseVisible = true;

            Singleton.Instance.panelBoundary = new int[3, 10]
            {
                { 0,0,0,0,2,2,1,1,1,1},
                { 0,0,0,0,2,2,1,1,1,1},
                { 0,0,0,0,2,2,1,1,1,1},
            };
            Singleton.Instance.panelStage = new int[3, 10]
            {
                { 3,1,0,0,3,3,0,0,0,0},
                { 1,0,0,0,3,3,0,0,0,0},
                { 0,0,0,0,3,3,0,0,0,0},
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
            Singleton.Instance.chipAttack = new int[3, 6]
            {
                { 0,0,0,0,0,0},
                { 0,0,0,0,0,0},
                { 0,0,0,0,0,0},
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

            backgroundTexture[0] = this.Content.Load<Texture2D>("background/space");
            panelTexture[0] = this.Content.Load<Texture2D>("panel/panelStage");
            rockmanEXETexture[0] = this.Content.Load<Texture2D>("rockman/RockmanEXEBattle1");
            mettonTexture[0] = this.Content.Load<Texture2D>("virus/MettonAttack");
            Singleton.Instance.effectsTexture[0] = this.Content.Load<Texture2D>("battleEffect/busterEffect");
            Singleton.Instance.effectsTexture[1] = this.Content.Load<Texture2D>("battleEffect/chargeBuster");
            _sprites = new List<Sprite>()
            {
                new BackgroundSprite(backgroundTexture)
                {
                    Name = "Background",
                },
                new PanelSprite(panelTexture)
                {
                    Name = "Panel",
                },
                new MettonSprite(mettonTexture)
                {
                    Name = "Metton",
                    HP = 40, Attack = 10,
                },
                new RockmanEXESprite(rockmanEXETexture)
                {
                    Name = "RockmanEXE",
                    HP = 100, Attack = 1,
                    W = Keys.W, S = Keys.S, A = Keys.A, D = Keys.D,
                    J = Keys.J, K = Keys.K,
                },
                new MettonWaveSprite(mettonTexture)
                {
                    Name = "MettonWave",
                },
            };
            //_sprites.Add(new MettonSprite(new Dictionary<string, Animation>()
            //{
            //    {"MettonAtk", new Animation(mettonTexture[0], new Rectangle(50, 0, 50, 60), 14)   },
            //})
            //{
            //    Name = "Metton",
            //    HP = 40,
            //    Attack = 10,
            //    Viewport = new Rectangle(50, 0, 50, 60),
            //});

            //MediaPlayer.Play(Singleton.Instance.song);
            Singleton.Instance._font = Content.Load<SpriteFont>("RockmanFont");
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

            switch (Singleton.Instance.CurrentGameState)
            {
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
                case Singleton.GameState.GameClear:
                    
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
            Singleton.Instance.MasterBGMVolume = 0.15f;
            Singleton.Instance.MasterSFXVolume = 0.05f;
        }
    }
}
