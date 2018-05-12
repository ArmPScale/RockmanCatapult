using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rockman.Models;

namespace Rockman.Sprites.Screens
{
    class BattleStart : Screen
    {
        private float _timer;

        public BattleStart(Texture2D[] texture) : base(texture)
        {
        }

        public BattleStart(Dictionary<string, Animation> animations)
            : base(animations)
        {
        }
        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.GameWaiting:
                    _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (_timer > 0.3f)
                    {
                        _animationManager.Play(_animations["BattleStartFreeze"]);
                        if (_timer > 1.5f)
                        {
                            _timer = 0f;
                            Singleton.Instance.CurrentGameState = Singleton.GameState.GamePlaying;
                        }
                    }
                    else
                    {
                        _animationManager.Play(_animations["BattleStart"]);
                    }
                    break;
                case Singleton.GameState.GameClear:
                    _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (_timer > 0.3f)
                    {
                        _animationManager.Play(_animations["EnemyDeletedFreeze"]);
                        if (_timer > 1.5f)
                        {
                            _timer = 0f;
                        }
                    }
                    else
                    {
                        _animationManager.Play(_animations["EnemyDeleted"]);
                    }
                    break;
            }
            _animationManager.Update(gameTime);
            base.Update(gameTime, sprites);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.GameWaiting:
                    if (_animationManager == null)
                    {
                        spriteBatch.Draw(_texture[0],
                                        Position,
                                        Viewport,
                                        Color.White);
                    }
                    else
                    {
                        _animationManager.Draw(spriteBatch,Position,scale);
                    }
                    break;
                case Singleton.GameState.GameClear:
                    if (_animationManager == null)
                    {
                        spriteBatch.Draw(_texture[0],
                                        Position,
                                        Viewport,
                                        Color.White);
                    }
                    else
                    {
                        _animationManager.Draw(spriteBatch, Position, scale);
                    }
                    break;
            }
            base.Draw(spriteBatch);
        }
    }
}
