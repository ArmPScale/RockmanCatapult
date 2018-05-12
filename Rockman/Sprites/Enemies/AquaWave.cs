using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rockman.Models;

namespace Rockman.Sprites
{
    class AquaWave : Sprite
    {
        private float _aquaWaveCoolDown = 0;
        private Point _currentAquaWave;
        private bool _isDamaged = true;

        public AquaWave(Dictionary<string, Animation> animations)
            : base(animations)
        {
            _animations = animations;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.GameEnemyAppear:
                    _aquaWaveCoolDown = 0;
                    break;
                case Singleton.GameState.GamePlaying:
                    if (Singleton.Instance.bossAttack[_currentAquaWave.X, _currentAquaWave.Y] == 1)
                    {
                        _aquaWaveCoolDown += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        //atkTakePlayer
                        if (Singleton.Instance.playerMove[_currentAquaWave.X, _currentAquaWave.Y] > 0 && _isDamaged)
                        {
                            Singleton.Instance.enemyAtk = 40;
                            Singleton.Instance.isDamaged = _isDamaged;
                            _isDamaged = false;
                        }
                        if (_aquaWaveCoolDown > 0.15f)
                        {
                            SoundEffects["AquaWave"].Volume = Singleton.Instance.MasterSFXVolume;
                            SoundEffects["AquaWave"].Play();
                            if (_currentAquaWave.Y > 0 && Singleton.Instance.panelStage[_currentAquaWave.X, _currentAquaWave.Y - 1] <= 1)
                            {
                                Singleton.Instance.panelYellow[_currentAquaWave.X, _currentAquaWave.Y - 1] = Singleton.Instance.bossAttack[_currentAquaWave.X, _currentAquaWave.Y]; 
                                Singleton.Instance.bossAttack[_currentAquaWave.X, _currentAquaWave.Y - 1] = Singleton.Instance.bossAttack[_currentAquaWave.X, _currentAquaWave.Y];
                            }
                            Singleton.Instance.panelYellow[_currentAquaWave.X, _currentAquaWave.Y] = 0;
                            Singleton.Instance.bossAttack[_currentAquaWave.X, _currentAquaWave.Y] = 0;
                            _isDamaged = true;
                            _aquaWaveCoolDown = 0;
                        }
                    }
                    _animationManager.Update(gameTime);
                    break;
            }
            base.Update(gameTime, sprites);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (Singleton.Instance.bossAttack[i, j] == 1)
                    {
                        _currentAquaWave = new Point(i, j);
                        if (_animationManager == null)
                        {
                            spriteBatch.Draw(_texture[0],
                                            Position,
                                            Viewport,
                                            Color.White);
                        }
                        else
                        {
                            _animationManager.Draw(spriteBatch,
                                new Vector2((TILESIZEX * j * 2) + (screenStageX - 10),
                                    (TILESIZEY * i * 2) + (screenStageY - 120)),
                                scale);
                        }
                    }
                }
            }
            base.Draw(spriteBatch);
        }
    }
}
