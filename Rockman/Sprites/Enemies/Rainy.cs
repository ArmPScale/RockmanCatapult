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
    class Rainy : Sprite
    {
        private float _rainyCoolDown = 0, _atkCoolDown = 0;
        private Point _currentRainy;
        private bool _isDamaged = true;

        public Rainy(Dictionary<string, Animation> animations)
            : base(animations)
        {
            _animations = animations;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.GameEnemyAppear:
                    _rainyCoolDown = 0; _atkCoolDown = 0;
                    break;
                case Singleton.GameState.GamePlaying:
                    if (Singleton.Instance.bossAttack[_currentRainy.X, _currentRainy.Y] == 2)
                    {
                        _rainyCoolDown += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        _atkCoolDown += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        //atkTakePlayer
                        for (int i = 0; i < 3; i++)
                        {
                            for (int j = 0; j < 10; j++)
                            {
                                if (Singleton.Instance.bossAttack[i, j] == 2 && Singleton.Instance.playerMove[i, j] > 0 && _isDamaged)
                                {
                                    Singleton.Instance.enemyAtk = 15;
                                    Singleton.Instance.isDamaged = _isDamaged;
                                    _isDamaged = false;
                                }
                            }
                        }
                        if (_rainyCoolDown < 0.05f)
                        {
                            _animationManager.Play(_animations["Cloudy"]);
                            SoundEffects["Rainy"].Volume = Singleton.Instance.MasterSFXVolume;
                            SoundEffects["Rainy"].Play();
                        }
                        else if (_rainyCoolDown > 0.15f && _rainyCoolDown < 0.2f)
                        {
                            _animationManager.Play(_animations["Rainy"]);
                        }
                        else if (_rainyCoolDown > 0.2f)
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                for (int j = 0; j < 10; j++)
                                {
                                    if (Singleton.Instance.panelYellow[i, j] == 2) Singleton.Instance.panelYellow[i, j] = 0;
                                }
                            }
                            //Singleton.Instance.panelYellow[_currentRainy.X, _currentRainy.Y] = 0;
                            if (_atkCoolDown > 0.5f)
                            {
                                _isDamaged = true;
                                _atkCoolDown = 0f;
                            }
                            if (_rainyCoolDown > 1f)
                            {
                                for (int i = 0; i < 3; i++)
                                {
                                    for (int j = 0; j < 10; j++)
                                    {
                                        if (Singleton.Instance.bossAttack[i, j] == 2) Singleton.Instance.bossAttack[i, j] = 0;
                                    }
                                }
                                _rainyCoolDown = 0f;
                            }
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
                    if (Singleton.Instance.bossAttack[i, j] == 2)
                    {
                        _currentRainy = new Point(i, j);
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
                                new Vector2((TILESIZEX * j * 2) + (screenStageX + 10),
                                    (TILESIZEY * i * 2) + (screenStageY - 160)),
                                scale);
                        }
                    }
                }
            }
            base.Draw(spriteBatch);
        }
    }
}
