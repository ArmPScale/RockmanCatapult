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
                            for (int i = 0; i < 3; i++)
                            {
                                for (int j = 0; j < 10; j++)
                                {
                                    if (Singleton.Instance.bossAttack[i, j] == 3) Singleton.Instance.bossAttack[i, j] = 4;
                                }
                            }
                        }
                        else if (_magicCircleCoolDown > 2f)
                        {
                            _magicCircleCoolDown = 0f;
                            for (int i = 0; i < 3; i++)
                            {
                                for (int j = 0; j < 10; j++)
                                {
                                    if (Singleton.Instance.bossAttack[i, j] == 3) Singleton.Instance.bossAttack[i, j] = 0;
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
                                new Vector2((TILESIZEX * j * 2) + (screenStageX - 100),
                                    (TILESIZEY * i * 2) + (screenStageY - 70)),
                                1f);
                        }
                    }
                }
            }
            base.Draw(spriteBatch);
        }
    }
}
