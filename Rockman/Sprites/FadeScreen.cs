using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rockman.Sprites
{
    class FadeScreen : Sprite
    {
        Color fade, fadeChipName;
        int alpha = 0 , alphaChip = 0;
        private float _textAnimate, _fadeAnimate;

        public FadeScreen(Texture2D[] texture)
            : base(texture)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            if (Singleton.Instance.useChip)
            {
                if (alpha <= 100)
                {
                    alpha += 2;
                    fade = new Color(0, 0, 0, alpha);
                }
                _textAnimate += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (alpha <= 50)
                {
                    alphaChip += 1;
                    if (alphaChip <= 100) fadeChipName = new Color((byte)255, (byte)255, (byte)255, alphaChip);
                }
                if (_textAnimate > 2.5f)
                {
                    Singleton.Instance.useChip = false;
                    Singleton.Instance.useChipDuring = true;
                    Singleton.Instance.CurrentGameState = Singleton.GameState.GameUseChip;
                }
            }
            else if (Singleton.Instance.useChipSuccess)
            {
                _fadeAnimate += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (alpha > 0)
                {
                    alpha -= 2;
                    fade = new Color(0, 0, 0, alpha);
                }
                if (_fadeAnimate > 1.3f)
                {
                    _textAnimate = 0; _fadeAnimate = 0;
                    Singleton.Instance.useChipSuccess = false;
                    Singleton.Instance.CurrentGameState = Singleton.GameState.GamePlaying;
                }
            }
            base.Update(gameTime, sprites);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Singleton.Instance.useChip || Singleton.Instance.useChipDuring || Singleton.Instance.useChipSuccess)
            {
                spriteBatch.Draw(_texture[0], new Vector2(0, 0), null, fade, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                if(_textAnimate < 2f) { 
                spriteBatch.DrawString(Singleton.Instance._font, Singleton.Instance.useChipName, new Vector2(Singleton.WIDTH / 6, Singleton.HEIGHT / 3), 
                    fadeChipName, 
                    0f, 
                    Vector2.Zero, 
                    1.3f, 
                    SpriteEffects.None, 
                    0f);
                }
            }
            base.Draw(spriteBatch);
        }
    }
}
