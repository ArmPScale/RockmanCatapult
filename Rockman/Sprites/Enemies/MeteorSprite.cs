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
    class MeteorSprite : Sprite
    {
        private float _meteorCoolDown = 0;
        private Point _currentMeteor;
        private bool _isDamaged = true;

        public MeteorSprite(Dictionary<string, Animation> animations)
            : base(animations)
        {
            _animations = animations;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.GamePlaying:
                    if (Singleton.Instance.virusAttack[_currentMeteor.X, _currentMeteor.Y] == 4)
                    {
                        _meteorCoolDown += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (_meteorCoolDown < 0.05f && _meteorCoolDown < 0.1f)
                        {
                            _animationManager.Play(_animations["Meteor"]);

                            SoundEffects["Meteor"].Volume = Singleton.Instance.MasterSFXVolume;
                            SoundEffects["Meteor"].Play();
                        }
                        else if (_meteorCoolDown > 0.1f)
                        {
                            if (Position.Y >= 100)
                            {
                                Position.X -= 50;
                                Position.Y -= 50;
                            }
                            else if (Position.Y < 100)
                            {
                                _animationManager.Play(_animations["MeteorCracked"]);
                                //atkTakePlayer
                                if (Singleton.Instance.playerMove[_currentMeteor.X, _currentMeteor.Y] > 0 && _isDamaged)
                                {
                                    Singleton.Instance.enemyAtk = 30;
                                    Singleton.Instance.isDamaged = _isDamaged;
                                    _isDamaged = false;
                                }
                                if (_meteorCoolDown > 0.92f)
                                {
                                    _meteorCoolDown = 0;
                                    Position = new Vector2(800, 900);
                                    _animationManager.Play(_animations["Meteor"]);
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
                    if (Singleton.Instance.virusAttack[i, j] == 4)
                    {
                        _currentMeteor = new Point(i, j);
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
                                new Vector2((TILESIZEX * j * 2) + (screenStageX + Position.X + 20),
                                    (TILESIZEY * i * 2) + (screenStageY - Position.Y - 20)),
                                scale);

                        }
                    }
                }
            }
            base.Draw(spriteBatch);
        }
    }
}
