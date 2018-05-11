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
    class MagicCircle : Sprite
    {
        private float _magicCircleCoolDown = 0;
        private Point _currentMagicCircle;
        private bool _isDamaged = true;

        public MagicCircle(Dictionary<string, Animation> animations)
            : base(animations)
        {
            _animations = animations;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.GamePlaying:
                    if (Singleton.Instance.bossAttack[_currentMagicCircle.X, _currentMagicCircle.Y] == 3)
                    {
                        _magicCircleCoolDown += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (_magicCircleCoolDown > 0.2f && _magicCircleCoolDown < 0.3f)
                        {
                            SoundEffects["MagicCircle"].Volume = Singleton.Instance.MasterSFXVolume;
                            SoundEffects["MagicCircle"].Play();
                            for (int i = 0; i < 3; i++)
                            {
                                for (int j = 0; j < 10; j++)
                                {
                                    if (Singleton.Instance.panelYellow[i, j] == 3) Singleton.Instance.panelYellow[i, j] = 0;
                                }
                            }
                        }
                        else if (_magicCircleCoolDown < 1f)
                        {
                            //magicFreezeStart
                            Singleton.Instance.bossAttack[_currentMagicCircle.X, _currentMagicCircle.Y + 1] = 4;
                            Singleton.Instance.bossAttack[_currentMagicCircle.X, _currentMagicCircle.Y + 2] = 4;
                            Singleton.Instance.bossAttack[_currentMagicCircle.X + 1, _currentMagicCircle.Y] = 4;
                            Singleton.Instance.bossAttack[_currentMagicCircle.X + 1, _currentMagicCircle.Y + 1] = 4;
                            Singleton.Instance.bossAttack[_currentMagicCircle.X + 1, _currentMagicCircle.Y + 2] = 4;
                            Singleton.Instance.bossAttack[_currentMagicCircle.X + 2, _currentMagicCircle.Y] = 4;
                            Singleton.Instance.bossAttack[_currentMagicCircle.X + 2, _currentMagicCircle.Y + 1] = 4;
                            Singleton.Instance.bossAttack[_currentMagicCircle.X + 2, _currentMagicCircle.Y + 2] = 4;
                        }
                        else if (_magicCircleCoolDown > 1.5f && _magicCircleCoolDown < 1.6f)
                        {
                            //atkTakePlayer
                            if (Singleton.Instance.bossAttack[_currentMagicCircle.X, _currentMagicCircle.Y] == 3 && 
                                Singleton.Instance.playerMove[_currentMagicCircle.X, _currentMagicCircle.Y] > 0 && _isDamaged)
                            {
                                Singleton.Instance.enemyAtk = 200;
                                Singleton.Instance.isDamaged = _isDamaged;
                                _isDamaged = false;
                            }
                        }
                        else if (_magicCircleCoolDown > 2f)
                        {
                            _isDamaged = true;
                            _magicCircleCoolDown = 0f;
                            Singleton.Instance.bossAttack[_currentMagicCircle.X, _currentMagicCircle.Y] = 0;
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
                    if (Singleton.Instance.bossAttack[i, j] == 3)
                    {
                        _currentMagicCircle = new Point(i, j);
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
                                new Vector2((TILESIZEX * j * 2) + (screenStageX + 25),
                                    (TILESIZEY * i * 2) + (screenStageY + 0)),
                                1f);
                        }
                    }
                }
            }
            base.Draw(spriteBatch);
        }
    }
}
