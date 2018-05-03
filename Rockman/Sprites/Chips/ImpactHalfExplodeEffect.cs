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
    class ImpactHalfExplodeEffect : Sprite
    {
        private float _impactHalfExplodeCoolDown = 0;
        private bool isExplode = false;
        public ImpactHalfExplodeEffect(Dictionary<string, Animation> animations)
            : base(animations)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.GamePlaying:
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 10; j++)
                        {
                            if (Singleton.Instance.chipEffect[i, j] == 2)
                            {
                                _impactHalfExplodeCoolDown += (float)gameTime.ElapsedGameTime.TotalSeconds;
                                _animationManager.Play(_animations["HalfExplode"]);
                                if (_impactHalfExplodeCoolDown > Singleton.Instance.currentChipCoolDown)
                                {
                                    _impactHalfExplodeCoolDown = 0;
                                    Singleton.Instance.chipEffect = new int[3, 10]
                                    {
                                        { 0,0,0,0,0,0,0,0,0,0},
                                        { 0,0,0,0,0,0,0,0,0,0},
                                        { 0,0,0,0,0,0,0,0,0,0},
                                    };
                                    Singleton.Instance.drawChipEffectName = "";
                                }
                            }
                        }
                    }
                    break;
            }
            _animationManager.Update(gameTime);
            base.Update(gameTime, sprites);
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (_impactHalfExplodeCoolDown > Singleton.Instance.currentChipAtkTime && 
                        Singleton.Instance.chipEffect[i, j] == 2)
                    {
                        switch (Singleton.Instance.CurrentGameState)
                        {
                            case Singleton.GameState.GamePlaying:

                                if (_animationManager == null)
                                {
                                    spriteBatch.Draw(_texture[0],
                                                    Position,
                                                    Viewport,
                                                    Color.White);
                                }
                                else
                                {
                                    _animationManager.Draw(spriteBatch, new Vector2((TILESIZEX * j * 2) + (screenStageX - 0), (TILESIZEY * i * 2) + (screenStageY - 50)), scale);
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
