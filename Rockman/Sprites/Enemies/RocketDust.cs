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
    class RocketDust : Sprite
    {
        private int posRocketX = 0, indexNewRocketY = 0;
        private float _rocketCoolDown = 0f;
        private Point _currentRocket;
        private bool _isDamaged = true;

        public RocketDust(Dictionary<string, Animation> animations)
            : base(animations)
        {
            _animations = animations;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.GameEnemyAppear:
                    _rocketCoolDown = 0;
                    _animationManager.Play(_animations["RocketAppear"]);
                    _isDamaged = true;
                    break;
                case Singleton.GameState.GamePlaying:
                    if (Singleton.Instance.spriteMove[_currentRocket.X, _currentRocket.Y] == 20)
                    {
                        _rocketCoolDown += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        //checkHP
                        if (Singleton.Instance.spriteHP[_currentRocket.X, _currentRocket.Y] <= 0)
                        {
                            posRocketX = 0; indexNewRocketY = 0;
                            _rocketCoolDown = 0;
                            Singleton.Instance.spriteMove[_currentRocket.X, _currentRocket.Y] = 0;
                        }
                        //atkTakePlayer
                        for (int i = 0; i < 3; i++)
                        {
                            for (int j = 0; j < 10; j++)
                            {
                                if (Singleton.Instance.virusAttack[i, j] == 20 && Singleton.Instance.playerMove[i, j] > 0 && _isDamaged)
                                {
                                    Singleton.Instance.enemyAtk = 20;
                                    Singleton.Instance.isDamaged = _isDamaged;
                                    _isDamaged = false;
                                }
                            }
                        }
                        if (_rocketCoolDown < 0.05f)
                        {
                            _animationManager.Play(_animations["RocketAppear"]);
                            SoundEffects["RocketAppear"].Volume = Singleton.Instance.MasterSFXVolume;
                            SoundEffects["RocketAppear"].Play();
                        }
                        else if(_rocketCoolDown < 0.2f)
                        {
                            _animationManager.Play(_animations["RocketJet"]);
                            Singleton.Instance.virusAttack[_currentRocket.X, _currentRocket.Y] = 20;
                        }
                        else if (_rocketCoolDown > 0.8f && _rocketCoolDown < 1.0f)
                        {
                            SoundEffects["RocketJet"].Volume = Singleton.Instance.MasterSFXVolume;
                            SoundEffects["RocketJet"].Play();
                            posRocketX -= 10;
                        }
                        else if (_rocketCoolDown > 1.0f && _rocketCoolDown < 1.9f)
                        {
                            posRocketX -= 10;
                            indexNewRocketY = posRocketX / (40 * 3) + 5;
                            //Console.WriteLine(indexNewRocketY);
                            if (indexNewRocketY - 1 >= 0 && _rocketCoolDown > 1.1f && _rocketCoolDown < 1.9f)
                            {
                                Singleton.Instance.virusAttack[_currentRocket.X, indexNewRocketY - 1] = 20;
                                Singleton.Instance.virusAttack[_currentRocket.X, indexNewRocketY] = 0;
                            }
                            else if(indexNewRocketY == 0)
                            {
                                Singleton.Instance.virusAttack[_currentRocket.X, indexNewRocketY] = 0;
                                //Singleton.Instance.spriteMove[_currentRocket.X, _currentRocket.Y] = 0;
                            }
                        }
                        else if (_rocketCoolDown > 5f)
                        {
                            posRocketX = 0;
                            _rocketCoolDown = 0;
                            _isDamaged = true;
                            //Singleton.Instance.spriteMove[_currentRocket.X, _currentRocket.Y] = 0;
                            //Singleton.Instance.spriteHP[_currentRocket.X, _currentRocket.Y] = 0;
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
                    if (Singleton.Instance.spriteMove[i, j] == 20)
                    {
                        _currentRocket = new Point(i, j);
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
                                new Vector2((TILESIZEX * j * 2) + (screenStageX + Position.X + 0) + posRocketX,
                                    (TILESIZEY * i * 2) + (screenStageY - Position.Y - 85)),
                                scale);
                        }
                    }
                }
            }
            base.Draw(spriteBatch);
        }
    }
}
