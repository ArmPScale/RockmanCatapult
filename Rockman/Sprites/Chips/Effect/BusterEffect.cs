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
    class BusterEffect : Sprite
    {
        private float _busterCoolDown = 0;
        private Point _currentBuster;
        private int posBusterX = 0, posBusterY = 0;
        Random random = new Random();

        public BusterEffect(Dictionary<string, Animation> animations)
            : base(animations)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            switch (Singleton.Instance.CurrentScreenState)
            {
                case Singleton.ScreenState.StoryMode:
                    if (Singleton.Instance.chipEffect[_currentBuster.X, _currentBuster.Y] == 7)
                    {
                        _busterCoolDown += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        _animationManager.Play(_animations["NormalBuster"]);
                        if (_busterCoolDown < 0.02f)
                        {
                            posBusterX = random.Next(0, 30);
                            posBusterY = random.Next(0, 50);
                        }
                        else if (_busterCoolDown > 0.1f)
                        {
                            Singleton.Instance.chipEffect[_currentBuster.X, _currentBuster.Y] = 0;
                            _busterCoolDown = 0;
                        }
                    }
                    else if (Singleton.Instance.chipEffect[_currentBuster.X, _currentBuster.Y] == 8)
                    {
                        _busterCoolDown += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        _animationManager.Play(_animations["ChargeBuster"]);
                        if (_busterCoolDown < 0.02f)
                        {
                            posBusterX = random.Next(0, 10);
                            posBusterY = random.Next(0, 30);
                        }
                        else if (_busterCoolDown > 0.24f)
                        {
                            Singleton.Instance.chipEffect[_currentBuster.X, _currentBuster.Y] = 0;
                            _busterCoolDown = 0;
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
                    if (Singleton.Instance.chipEffect[i, j] == 7 || 
                        Singleton.Instance.chipEffect[i, j] == 8)
                    {
                        _currentBuster = new Point(i, j);
                        switch (Singleton.Instance.CurrentScreenState)
                        {
                            case Singleton.ScreenState.StoryMode:

                                if (_animationManager == null)
                                {
                                    spriteBatch.Draw(_texture[0],
                                                    Position,
                                                    Viewport,
                                                    Color.White);
                                }
                                else
                                {
                                    _animationManager.Draw(spriteBatch, new Vector2((TILESIZEX * j * 2) + (screenStageX - 20) - posBusterX, 
                                        (TILESIZEY * i * 2) + (screenStageY - 80) - posBusterY) 
                                        , scale);
                                }
                                break;
                        }
                    }
                }
            }
            base.Draw(spriteBatch);
        }
    }
}
