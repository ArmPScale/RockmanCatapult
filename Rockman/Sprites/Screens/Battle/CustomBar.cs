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
                    rectBar.Width = 0;
                    isNotPlayed = true;
                    break;
                case Singleton.GameState.GamePlaying:
                    if(rectBar.Width > 384)
                    {
                        if(isNotPlayed)
                        {
                            SoundEffects["FullCustom"].Volume = Singleton.Instance.MasterSFXVolume;
                            SoundEffects["FullCustom"].Play();
                            isNotPlayed = false;
                        }
                        Singleton.Instance.isCustomBarFull = true;
                    }
                    else rectBar.Width += 1;
                    break;
            }
            base.Update(gameTime, sprites);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.GameWaiting:
                    if (_animationManager == null)
                    {
                        //drawBar
                        spriteBatch.Draw(_texture[0], rectBar, Viewport, Color.White);
                    }
                    else _animationManager.Draw(spriteBatch, Position, scale);
                    break;
                case Singleton.GameState.GamePlaying:
                    if (_animationManager == null)
                    {
                        //drawBar
                        spriteBatch.Draw(_texture[0], rectBar, Viewport, Color.White);
                    }
                    else _animationManager.Draw(spriteBatch, Position, scale);
                    break;
            }
            base.Draw(spriteBatch);
        }
    }
}
