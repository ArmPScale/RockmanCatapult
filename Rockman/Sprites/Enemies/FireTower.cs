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
    class FireTower : Sprite
    {
        private float _fireTowerCoolDown = 0f;
        private Point _currentFireWave;
        private bool _isDamaged = true;

        public FireTower(Dictionary<string, Animation> animations)
            : base(animations)
        {
            _animations = animations;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.GameEnemyAppear:
                    _fireTowerCoolDown = 0;
                    _animationManager.Play(_animations["FireTowerFirst"]);
                    _isDamaged = true;
                    break;
                case Singleton.GameState.GamePlaying:
                    if (Singleton.Instance.bossAttack[_currentFireWave.X, _currentFireWave.Y] == 7)
                    {
                        _fireTowerCoolDown += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (_fireTowerCoolDown < 0.05f)
                        {
                            _animationManager.Play(_animations["FireTowerFirst"]);
                            SoundEffects["DragonBreathe"].Volume = Singleton.Instance.MasterSFXVolume;
                            SoundEffects["DragonBreathe"].Play();
                        }
                        if (_fireTowerCoolDown > 0.2f && _fireTowerCoolDown < 1.0f)
                        {
                            _animationManager.Play(_animations["FireTower"]);
                            //atkTakePlayer
                            for (int i = 0; i < 3; i++)
                            {
                                for (int j = 0; j < 10; j++)
                                {
                                    if (Singleton.Instance.bossAttack[i, j] == 7 && Singleton.Instance.playerMove[i, j] > 0 && _isDamaged)
                                    {
                                        Singleton.Instance.enemyAtk = 200;
                                        Singleton.Instance.isDamaged = _isDamaged;
                                        _isDamaged = false;
                                    }
                                }
                            }
                        }
                        else if (_fireTowerCoolDown > 1.0f)
                        {
                            _fireTowerCoolDown = 0;
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
                    if (Singleton.Instance.bossAttack[i, j] == 7)
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
                            _animationManager.Draw(spriteBatch,
                               new Vector2((TILESIZEX * j * 2) + (screenStageX + Position.X - 30),
                                   (TILESIZEY * i * 2) + (screenStageY - Position.Y - 140)),
                               4f);
                        }
                    }
                }
            }
            base.Draw(spriteBatch);
        }
    }
}
