using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Rockman.Sprites.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rockman.Sprites
{
    class AppearScreen : Screen
    {
        private float _timer = 0f;

        public AppearScreen(Texture2D[] texture)
            : base(texture)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.GameEnemyAppear:
                    _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (_timer < 0.05f)
                    {
                        SoundEffects["GoIntoBattle"].Volume = Singleton.Instance.MasterSFXVolume;
                        SoundEffects["GoIntoBattle"].Play();
                    }
                    //gotoGameCustomScreen
                    else if (_timer > 3f)
                    {
                        _timer = 0f;
                        MediaPlayer.Stop();
                        Singleton.Instance.CurrentGameState = Singleton.GameState.GameCustomScreen;
                    }
                    //fadeScreen
                    if (fade.A < 255)
                    {
                        fade.R += 15;
                        fade.G += 15;
                        fade.B += 15;
                        fade.A += 15;
                    }
                    break;
                case Singleton.GameState.GameCustomScreen:
                    fade *= 0.96f;
                    break;
            }
                
            base.Update(gameTime, sprites);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.GameEnemyAppear:
                    spriteBatch.Draw(_texture[0], new Vector2(0, 0), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                    spriteBatch.Draw(_texture[1], new Vector2(0, 0), null, fade, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                    break;
                case Singleton.GameState.GameCustomScreen:
                    spriteBatch.Draw(_texture[1], new Vector2(0, 0), null, fade, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                    break;
            }
            base.Draw(spriteBatch);
        }
    }
}
