using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rockman.Models;

namespace Rockman.Sprites.Chips
{
    class Buster : Chip
    {
        private float _busterCoolDown;
        private bool _isBusterShot;
        public Buster(Dictionary<string, Animation> animations) 
            : base(animations)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.GamePlaying:
                    switch (Singleton.Instance.CurrentPlayerState)
                    {
                        case Singleton.PlayerState.BusterShot:
                            _busterCoolDown += (float)gameTime.ElapsedGameTime.TotalSeconds;
                            _animationManager.Play(_animations["NormalBuster"]);
                            if(_busterCoolDown < 0.1f)
                            {
                                _isBusterShot = true;
                            }
                            else if (_busterCoolDown > 0.3f)
                            {
                                _busterCoolDown = 0f;
                                _isBusterShot = false;
                            }
                            _animationManager.Update(gameTime);
                            break;
                    }
                    break;
                case Singleton.GameState.GameUseChip:
                    _busterCoolDown += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (_busterCoolDown > 0.01f)
                    {
                        _busterCoolDown = 0f;
                        _animationManager.Update(gameTime);
                    }
                    break;
                case Singleton.GameState.GameClear:
                    _busterCoolDown += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (_busterCoolDown > 0.3f)
                    {
                        _busterCoolDown = 0f;
                        _isBusterShot = false;
                    }
                    _animationManager.Update(gameTime);
                    break;
            }
            base.Update(gameTime, sprites);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.GamePlaying:
                    switch (Singleton.Instance.CurrentPlayerState)
                    {
                        case Singleton.PlayerState.BusterShot:
                            if (_animationManager == null)
                            {
                                spriteBatch.Draw(_texture[0],
                                                Position,
                                                Viewport,
                                                Color.White);
                            }
                            else
                            {
                                if (Singleton.Instance.playerMove[Singleton.Instance.currentPlayerPoint.X, Singleton.Instance.currentPlayerPoint.Y] < 10)
                                {
                                    _animationManager.Draw(spriteBatch,
                                    new Vector2((TILESIZEX * Singleton.Instance.currentPlayerPoint.Y * 2) + (screenStageX + 95), (TILESIZEY * Singleton.Instance.currentPlayerPoint.X * 2) + (screenStageY - 50)),
                                    scale);
                                }
                            }
                            break;
                    }
                    break;
                case Singleton.GameState.GameUseChip:
                    if (Singleton.Instance.choosePlayerAnimate == "Buster" &&
                        Singleton.Instance.playerMove[Singleton.Instance.currentPlayerPoint.X, Singleton.Instance.currentPlayerPoint.Y] == 1)
                    {
                        _animationManager.Draw(spriteBatch,
                        new Vector2((TILESIZEX * Singleton.Instance.currentPlayerPoint.Y * 2) + (screenStageX + 95), (TILESIZEY * Singleton.Instance.currentPlayerPoint.X * 2) + (screenStageY - 50)),
                        scale);
                    }
                    break;
                case Singleton.GameState.GameClear:
                    if (_isBusterShot && Singleton.Instance.playerMove[Singleton.Instance.currentPlayerPoint.X, Singleton.Instance.currentPlayerPoint.Y] < 10)
                        _animationManager.Draw(spriteBatch,
                                new Vector2((TILESIZEX * Singleton.Instance.currentPlayerPoint.Y * 2) + (screenStageX + 95), (TILESIZEY * Singleton.Instance.currentPlayerPoint.X * 2) + (screenStageY - 50)),
                                scale);
                    break;
            }
            base.Draw(spriteBatch);
        }
    }
}
