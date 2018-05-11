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
    class MagicFreeze : Sprite
    {
        private float _magicFreezeCoolDown = 0;
        private Point _currentMagicFreeze;
        private bool _isDamaged = true;

        public MagicFreeze(Dictionary<string, Animation> animations)
            : base(animations)
        {
            _animations = animations;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.GamePlaying:
                    if (Singleton.Instance.bossAttack[_currentMagicFreeze.X, _currentMagicFreeze.Y] == 4)
                    {
                        _magicFreezeCoolDown += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        
                        if (_magicFreezeCoolDown > 1.3f)
                        {
                            if (_magicFreezeCoolDown < 1.4f)
                            {
                                SoundEffects["MagicFreeze"].Volume = Singleton.Instance.MasterSFXVolume;
                                SoundEffects["MagicFreeze"].Play();
                                _animationManager.Play(_animations["Freezing"]);
                            }
                            else if (_magicFreezeCoolDown < 1.6f)
                            {
                                _animationManager.Play(_animations["Frozen"]);
                                //atkTakePlayer
                                for (int i = 0; i < 3; i++)
                                {
                                    for (int j = 0; j < 10; j++)
                                    {
                                        if (Singleton.Instance.bossAttack[i, j] == 4 && Singleton.Instance.playerMove[i, j] > 0 && _isDamaged)
                                        {
                                            Singleton.Instance.enemyAtk = 200;
                                            Singleton.Instance.isDamaged = _isDamaged;
                                            _isDamaged = false;
                                        }
                                    }
                                }
                            }
                            else if (_magicFreezeCoolDown > 2.5f)
                            {
                                for (int i = 0; i < 3; i++)
                                {
                                    for (int j = 0; j < 10; j++)
                                    {
                                        if (Singleton.Instance.bossAttack[i, j] == 4) Singleton.Instance.bossAttack[i, j] = 0;
                                    }
                                }
                                _isDamaged = true;
                                _magicFreezeCoolDown = 0;
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
                    if (Singleton.Instance.bossAttack[i, j] == 4 && i == 1)
                    {
                        _currentMagicFreeze = new Point(i, j);
                        if (_animationManager == null)
                        {
                            spriteBatch.Draw(_texture[0],
                                            Position,
                                            Viewport,
                                            Color.White);
                        }
                        else
                        {
                            if (_magicFreezeCoolDown > 1.3f)
                            {
                                _animationManager.Draw(spriteBatch,
                                new Vector2((TILESIZEX * j * 2) + (screenStageX - 90),
                                    (TILESIZEY * i * 2) + (screenStageY - 180)),
                                scale + 1f);
                            }
                        }
                    }
                }
            }
            base.Draw(spriteBatch);
        }
    }
}
