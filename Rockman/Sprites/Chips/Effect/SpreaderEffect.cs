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
    class SpreaderEffect : Chip
    {
        private float _spreaderEffectCoolDown = 0;
        public SpreaderEffect(Dictionary<string, Animation> animations)
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
                            if (Singleton.Instance.chipEffect[i, j] == 1)
                            {
                                _spreaderEffectCoolDown += (float)gameTime.ElapsedGameTime.TotalSeconds;
                                _animationManager.Play(_animations[Singleton.Instance.drawChipEffectName]);
                                if (_spreaderEffectCoolDown > 0.15f * 9)
                                {
                                    _spreaderEffectCoolDown = 0;
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
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 10; j++)
                        {
                            if (_animationManager == null)
                            {
                                spriteBatch.Draw(_texture[0],
                                                Position,
                                                Viewport,
                                                Color.White);
                            }
                            else
                            {
                                if (Singleton.Instance.chipEffect[i, j] == 1)
                                {
                                    _animationManager.Draw(spriteBatch, new Vector2((TILESIZEX * j * 2) + (screenStageX - 20), (TILESIZEY * i * 2) + (screenStageY - 20)), scale);
                                }
                            }
                        }
                    }  
                    break;
            }
            base.Draw(spriteBatch);
        }
    }
}
