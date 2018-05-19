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
    class CrimsonHeadJet : Sprite
    {
        private float _headJetCoolDown = 0;
        private Point _currentHeadJet;
        private bool _isDamaged = true;
        public Random random = new Random();
        int randomNum = 0;
        public CrimsonHeadJet(Dictionary<string, Animation> animations)
            : base(animations)
        {
            _animations = animations;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.GameEnemyAppear:
                    _headJetCoolDown = 0;
                    _animationManager.Play(_animations["RedHead"]);
                    Position = new Vector2(300, 400);
                    _isDamaged = true;
                    _animationManager.Update(gameTime);
                    break;
                case Singleton.GameState.GamePlaying:
                    if (Singleton.Instance.bossAttack[_currentHeadJet.X, _currentHeadJet.Y] == 5)
                    {
                        _headJetCoolDown += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        randomNum = random.Next(1, 3);
                        if (_headJetCoolDown < 0.05f)
                        {
                            if (randomNum == 1) _animationManager.Play(_animations["RedHead"]);
                            else _animationManager.Play(_animations["BlackHead"]);
                            SoundEffects["HeadJet"].Volume = Singleton.Instance.MasterSFXVolume;
                            SoundEffects["HeadJet"].Play();
                        }
                        else if (_headJetCoolDown > 0.05f)
                        {
                            if (Position.Y >= 100)
                            {
                                Position.X -= 50;
                                Position.Y -= 50;
                            }
                            else if (Position.Y < 51)
                            {
                                if (_headJetCoolDown > 0.2f && _headJetCoolDown < 0.4f)
                                {
                                    _animationManager.Play(_animations["Fire"]);
                                    SoundEffects["HeadJetFall"].Volume = Singleton.Instance.MasterSFXVolume;
                                    SoundEffects["HeadJetFall"].Play();
                                    //atkTakePlayer
                                    for (int i = 0; i < 3; i++)
                                    {
                                        for (int j = 0; j < 10; j++)
                                        {
                                            if (Singleton.Instance.bossAttack[i, j] == 5 && Singleton.Instance.playerMove[i, j] > 0 && _isDamaged)
                                            {
                                                Singleton.Instance.enemyAtk = 50;
                                                Singleton.Instance.isDamaged = _isDamaged;
                                                _isDamaged = false;
                                            }
                                        }
                                    }
                                }
                                if (_headJetCoolDown > 0.68f)
                                {
                                    _headJetCoolDown = 0;
                                    Position = new Vector2(300, 400);
                                    _animationManager.Play(_animations["RedHead"]);
                                    _isDamaged = true;
                                }
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
                    if (Singleton.Instance.bossAttack[i, j] == 5)
                    {
                        _currentHeadJet = new Point(i, j);
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
                                new Vector2((TILESIZEX * j * 2) + (screenStageX + Position.X + 30),
                                    (TILESIZEY * i * 2) + (screenStageY - Position.Y - 150)),
                                2f);
                        }
                    }
                }
            }
            base.Draw(spriteBatch);
        }
    }
}
