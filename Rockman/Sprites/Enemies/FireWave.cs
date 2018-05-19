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
    class FireWave : Sprite
    {
        private float _fireWaveCoolDown = 0f , _damageCoolDown = 0f;
        private Point _currentFireWave;
        private bool _isDamaged = true;

        public FireWave(Dictionary<string, Animation> animations)
            : base(animations)
        {
            _animations = animations;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.GameEnemyAppear:
                    _fireWaveCoolDown = 0; _damageCoolDown = 0;
                    _animationManager.Play(_animations["FireWave"]);
                    _isDamaged = true;
                    break;
                case Singleton.GameState.GamePlaying:
                    if (Singleton.Instance.bossAttack[_currentFireWave.X, _currentFireWave.Y] == 6)
                    {
                        _fireWaveCoolDown += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (_fireWaveCoolDown < 0.05f)
                        {
                            _animationManager.Play(_animations["FireWave"]);
                            SoundEffects["NoiseCharge"].Volume = Singleton.Instance.MasterSFXVolume;
                            SoundEffects["NoiseCharge"].Play();
                        }
                        if (_fireWaveCoolDown > 0.2f && _fireWaveCoolDown < 1.0f)
                        {
                            SoundEffects["NoiseWave"].Volume = Singleton.Instance.MasterSFXVolume;
                            SoundEffects["NoiseWave"].Play();
                            //atkTakePlayer
                            for (int i = 0; i < 3; i++)
                            {
                                for (int j = 0; j < 10; j++)
                                {
                                    if (Singleton.Instance.bossAttack[i, j] == 6 && Singleton.Instance.playerMove[i, j] > 0 && _isDamaged)
                                    {
                                        Singleton.Instance.enemyAtk = 20;
                                        Singleton.Instance.isDamaged = _isDamaged;
                                        _isDamaged = false;
                                    }
                                }
                            }
                            if (_fireWaveCoolDown < 1.0f)
                            {
                                _damageCoolDown += (float)gameTime.ElapsedGameTime.TotalSeconds;
                                if (_damageCoolDown > 0.1f)
                                {
                                    _isDamaged = true;
                                    _damageCoolDown = 0f;
                                }
                            }
                        }
                        else if (_fireWaveCoolDown > 1.0f)
                        {
                            _fireWaveCoolDown = 0; _damageCoolDown = 0;
                            _isDamaged = true;
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
                    if (Singleton.Instance.bossAttack[i, j] == 6)
                    {
                        _currentFireWave = new Point(i, j);
                        if (_animationManager == null)
                        {
                            spriteBatch.Draw(_texture[0],
                                            Position,
                                            Viewport,
                                            Color.White);
                        }
                        else
                        {
                            if (_fireWaveCoolDown > 0.2f)
                            {
                                _animationManager.Draw(spriteBatch,
                                new Vector2((TILESIZEX * j * 2) + (screenStageX + Position.X - 30),
                                    (TILESIZEY * i * 2) + (screenStageY - Position.Y - 140)),
                                4f);
                            }
                        }
                    }
                }
            }
            base.Draw(spriteBatch);
        }
    }
}
