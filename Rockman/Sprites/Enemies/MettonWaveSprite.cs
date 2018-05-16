using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rockman.Models;
using System;
using System.Collections.Generic;

namespace Rockman.Sprites
{
    class MettonWaveSprite : Enemy
    {
        Point currentTile;
        private float _mettonWaveCoolDown = 0f;
        private bool _isDamaged = true;

        public MettonWaveSprite(Dictionary<string, Animation> animations)
           : base(animations)
        {
            _animations = animations;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.GameEnemyAppear:
                    _mettonWaveCoolDown = 0;
                    break;
                case Singleton.GameState.GamePlaying:
                    if (Singleton.Instance.virusAttack[currentTile.X, currentTile.Y] == 2)
                    {
                        _mettonWaveCoolDown += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        //atkTakePlayer
                        if (Singleton.Instance.playerMove[currentTile.X, currentTile.Y] > 0 && _isDamaged)
                        {
                            Singleton.Instance.enemyAtk = 10;
                            Singleton.Instance.isDamaged = _isDamaged;
                            _isDamaged = false;
                        }
                        if (_mettonWaveCoolDown < 0.1f)
                        {
                            SoundEffects["MettonWave"].Volume = Singleton.Instance.MasterSFXVolume;
                            SoundEffects["MettonWave"].Play();
                        }
                        else if (_mettonWaveCoolDown > 0.45f)
                        {
                            if (currentTile.Y > 0 && Singleton.Instance.panelStage[currentTile.X, currentTile.Y - 1] <= 1)
                            {
                                Singleton.Instance.panelYellow[currentTile.X, currentTile.Y - 1] = Singleton.Instance.virusAttack[currentTile.X, currentTile.Y];
                                Singleton.Instance.virusAttack[currentTile.X, currentTile.Y - 1] = Singleton.Instance.virusAttack[currentTile.X, currentTile.Y];
                            }
                            Singleton.Instance.panelYellow[currentTile.X, currentTile.Y] = 0;
                            Singleton.Instance.virusAttack[currentTile.X, currentTile.Y] = 0;
                            _isDamaged = true;
                            _mettonWaveCoolDown = 0;
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
                    if (Singleton.Instance.virusAttack[i, j] == 2)
                    {
                        currentTile = new Point(i, j);
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
