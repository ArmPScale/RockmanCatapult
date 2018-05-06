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
    class TitleScreen : Background
    {
        Vector2 bgScroll1, bgScroll2;
        int bg1 = 0, bg2 = 0;
        Color textButtonColor;
        bool isPressStart = false;

        public TitleScreen(Texture2D[] texture)
            : base(texture)
        {
            alpha = 255;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            switch (Singleton.Instance.CurrentScreenState)
            {
                case Singleton.ScreenState.TitleScreen:
                    //bgScrolling
                    bg1 -= 3;
                    bgScroll1 = new Vector2(bg1, 0);
                    if (bg1 <= -(Singleton.WIDTH - 1))
                    {
                        bg1 = 0;
                    }
                    //bgScrolling2
                    bg2 -= 8;
                    bgScroll2 = new Vector2(bg2, 0);
                    if (bg2 <= -(Singleton.WIDTH - 1))
                    {
                        bg2 = 0;
                    }
                    //fadeIn
                    if (!isPressStart && alpha >= 0)
                    {
                        alpha -= 10;
                        fade = new Color(0, 0, 0, alpha);
                    }
                    //textButton
                    if ((Singleton.Instance.CurrentMouse.X >= 400 && Singleton.Instance.CurrentMouse.X <= 825) &&
                        (Singleton.Instance.CurrentMouse.Y >= 600 && Singleton.Instance.CurrentMouse.Y <= 635))
                    {
                        textButtonColor = new Color(247, 159, 47);
                        if (!isPressStart && Singleton.Instance.PreviousMouse.LeftButton == ButtonState.Released &&
                       Singleton.Instance.CurrentMouse.LeftButton == ButtonState.Pressed)
                        {
                            SoundEffects["PressStart"].Volume = 0.5f;
                            SoundEffects["PressStart"].Play();
                            isPressStart = true;
                        }
                    }
                    else
                    {
                        textButtonColor = Color.WhiteSmoke;
                    }
                    //fadeOut
                    if (isPressStart && alpha < 255)
                    {
                        alpha += 5;
                        fade = new Color(0, 0, 0, alpha);
                    }
                    else if (alpha >= 255)
                    {
                        MediaPlayer.Stop();
                        Singleton.Instance.mediaPlaySong = "MenuScreen";
                        Singleton.Instance.CurrentScreenState = Singleton.ScreenState.MenuScreen;
                    }
                    break;
            }
            base.Update(gameTime, sprites);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (Singleton.Instance.CurrentScreenState)
            {
                case Singleton.ScreenState.TitleScreen:
                    //drawbgBack
                    spriteBatch.Draw(_texture[0], bgScroll1, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                    //drawbgFront
                    spriteBatch.Draw(_texture[1], bgScroll2, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                    //drawLogo
                    spriteBatch.Draw(_texture[2], new Vector2(Singleton.WIDTH / 4, Singleton.HEIGHT / 10),
                        null, Color.White, 0f, Vector2.Zero, scale - 0.3f, SpriteEffects.None, 0f);
                    //drawTextButton
                    spriteBatch.DrawString(Singleton.Instance._font, "Press Button Here", new Vector2(400, 600),
                        textButtonColor, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                    //drawFadeBlack
                    spriteBatch.Draw(_texture[0], new Vector2(0,0), null, fade, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                    break;
            }
            base.Draw(spriteBatch);
        }
    }
}
