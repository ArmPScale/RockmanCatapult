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
    class CustomBar : Screen
    {
        private float _timer, _delayBar = 32f;
        Rectangle rectBar = new Rectangle(423, 31, 0, 23);
        bool isNotPlayed = true;

        public CustomBar(Texture2D[] texture) :
            base(texture)
        {
            _texture = texture;
        }

        public CustomBar(Dictionary<string, Animation> animations) :
            base(animations)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.GameCustomScreen:
                    _timer = 0f; rectBar.Width = 0;
                    isNotPlayed = true;
                    break;
                case Singleton.GameState.GamePlaying:
                    _timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    if (rectBar.Width > 384)
                    {
                        if (isNotPlayed)
                        {
                            SoundEffects["FullCustom"].Volume = Singleton.Instance.MasterSFXVolume;
                            SoundEffects["FullCustom"].Play();
                            isNotPlayed = false;
                        }
                        Singleton.Instance.isCustomBarFull = true;
                    }
                    else if (_timer > _delayBar)
                    {
                        rectBar.Width += 1;
                        _timer = 0;
                    } 
                    break;
            }
            base.Update(gameTime, sprites);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.GamePlaying:
                    if (_animationManager == null)
                    {
                        //drawBar
                        if (rectBar.Width > 384)
                        {
                            Viewport = new Rectangle(452, 151, 8, 8);
                        }
                        else
                        {
                            Viewport = new Rectangle(452, 139, 8, 8);
                        }
                        spriteBatch.Draw(_texture[0], rectBar, Viewport, Color.White);
                    }
                    else _animationManager.Draw(spriteBatch, Position, scale);
                    break;
            }
            base.Draw(spriteBatch);
        }
    }
}
