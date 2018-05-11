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
    class ImpactExplosionEffect : Sprite
    {
        private float _impactExplosionCoolDown = 0;
        private Point _currentExplosion;

        public ImpactExplosionEffect(Dictionary<string, Animation> animations)
            : base(animations)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            switch (Singleton.Instance.CurrentScreenState)
            {
                case Singleton.ScreenState.StoryMode:
                    if (Singleton.Instance.chipEffect[_currentExplosion.X, _currentExplosion.Y] == 3)
                    {
                        _impactExplosionCoolDown += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        _animationManager.Play(_animations["NormalExplosion"]);
                        if (_impactExplosionCoolDown > Singleton.Instance.currentChipCoolDown)
                        {
                            _impactExplosionCoolDown = 0;
                            Singleton.Instance.chipEffect = new int[3, 10]
                            {
                                        { 0,0,0,0,0,0,0,0,0,0},
                                        { 0,0,0,0,0,0,0,0,0,0},
                                        { 0,0,0,0,0,0,0,0,0,0},
                            };
                            Singleton.Instance.drawChipEffectName = "";
                        }
                    }
                    if (Singleton.Instance.chipEffect[_currentExplosion.X, _currentExplosion.Y] == 4)
                    {
                        _impactExplosionCoolDown += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        _animationManager.Play(_animations["DarkExplosion"]);
                        if (_impactExplosionCoolDown > Singleton.Instance.currentChipCoolDown)
                        {
                            _impactExplosionCoolDown = 0;
                            Singleton.Instance.chipEffect = new int[3, 10]
                            {
                                        { 0,0,0,0,0,0,0,0,0,0},
                                        { 0,0,0,0,0,0,0,0,0,0},
                                        { 0,0,0,0,0,0,0,0,0,0},
                            };
                            Singleton.Instance.drawChipEffectName = "";
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
                    if (Singleton.Instance.chipEffect[i, j] == 3 ||
                        Singleton.Instance.chipEffect[i, j] == 4)
                    {
                        _currentExplosion = new Point(i, j);
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
                                    _animationManager.Draw(spriteBatch, new Vector2((TILESIZEX * j * 2) + (screenStageX - 20), 
                                        (TILESIZEY * i * 2) + (screenStageY - 80)), scale);
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
