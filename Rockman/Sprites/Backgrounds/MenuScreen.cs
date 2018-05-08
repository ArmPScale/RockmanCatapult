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
{    class MenuScreen : Background
    {
        Color fadeBA;
        Color[] textButtonColor = new Color[7]
        {
            Color.WhiteSmoke,
            Color.WhiteSmoke,
            Color.WhiteSmoke,
            Color.WhiteSmoke,
            Color.WhiteSmoke,
            Color.WhiteSmoke,
            Color.WhiteSmoke,
        };
        int alphaBA = 0;
        bool isFadeIn = true, isClicked = false;

        public enum MenuState
        {
            StoryMode,
            EditFolderChip,
            Shop,
            Tutorial,
            Option,
            Credits,
            Quit
        }
        public static MenuState CurrentMenuState;

        public MenuScreen(Texture2D[] texture)
            : base(texture)
        {
            alpha = 255;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            switch (Singleton.Instance.CurrentScreenState)
            {
                case Singleton.ScreenState.MenuScreen:
                    //fadeScreen
                    if (alpha >= 0)
                    {
                        alpha -= 8;
                        fade = new Color(0, 0, 0, alpha);
                    }
                    //fadeIn/Out
                    if (!isFadeIn)
                    {
                        alphaBA -= 1;
                        fadeBA = new Color(255, 255, 255, alphaBA);
                        if (alphaBA <= 0)
                        {
                            isFadeIn = true;
                        }
                    }
                    else if (isFadeIn)
                    {
                        alphaBA += 1;
                        fadeBA = new Color(255, 255, 255, alphaBA);
                        if (alphaBA >= 140)
                        {
                            isFadeIn = false;
                        }
                    }
                    //checkClick
                    if (Singleton.Instance.PreviousMouse.LeftButton == ButtonState.Released &&
                       Singleton.Instance.CurrentMouse.LeftButton == ButtonState.Pressed)
                    {
                        isClicked = true;
                    }
                    else
                    {
                        isClicked = false;
                    }

                    //textButton
                    if ((Singleton.Instance.CurrentMouse.X >= 60 && Singleton.Instance.CurrentMouse.X <= 305) &&
                    (Singleton.Instance.CurrentMouse.Y >= 209 && Singleton.Instance.CurrentMouse.Y <= 244))
                    {
                        textButtonColor[0] = new Color(247, 159, 47);
                        if (isClicked)
                        {
                            SoundEffects["PressStart"].Volume = Singleton.Instance.MasterSFXVolume;
                            SoundEffects["PressStart"].Play();
                            //MediaPlayer.Stop();
                            Singleton.Instance.mediaPlaySong = "Battle1";
                            Singleton.Instance.CurrentScreenState = Singleton.ScreenState.StoryMode;
                            Singleton.Instance.CurrentGameState = Singleton.GameState.GameEnemyAppear;
                        }
                    }
                    else if ((Singleton.Instance.CurrentMouse.X >= 60 && Singleton.Instance.CurrentMouse.X <= 316) &&
                    (Singleton.Instance.CurrentMouse.Y >= 270 && Singleton.Instance.CurrentMouse.Y <= 305))
                    {
                        textButtonColor[1] = new Color(247, 159, 47);
                        if (isClicked)
                        {
                            SoundEffects["PressStart"].Volume = Singleton.Instance.MasterSFXVolume;
                            SoundEffects["PressStart"].Play();
                        }
                    }
                    else if ((Singleton.Instance.CurrentMouse.X >= 60 && Singleton.Instance.CurrentMouse.X <= 163) &&
                    (Singleton.Instance.CurrentMouse.Y >= 331 && Singleton.Instance.CurrentMouse.Y <= 366))
                    {
                        textButtonColor[2] = new Color(247, 159, 47);
                        if (isClicked)
                        {
                            SoundEffects["PressStart"].Volume = Singleton.Instance.MasterSFXVolume;
                            SoundEffects["PressStart"].Play();
                        }
                    }
                    else if ((Singleton.Instance.CurrentMouse.X >= 60 && Singleton.Instance.CurrentMouse.X <= 254) &&
                    (Singleton.Instance.CurrentMouse.Y >= 392 && Singleton.Instance.CurrentMouse.Y <= 427))
                    {
                        textButtonColor[3] = new Color(247, 159, 47);
                        if (isClicked)
                        {
                            SoundEffects["PressStart"].Volume = Singleton.Instance.MasterSFXVolume;
                            SoundEffects["PressStart"].Play();
                        }
                    }
                    else if ((Singleton.Instance.CurrentMouse.X >= 60 && Singleton.Instance.CurrentMouse.X <= 201) &&
                    (Singleton.Instance.CurrentMouse.Y >= 453 && Singleton.Instance.CurrentMouse.Y <= 488))
                    {
                        textButtonColor[4] = new Color(247, 159, 47);
                        if (isClicked)
                        {
                            SoundEffects["PressStart"].Volume = Singleton.Instance.MasterSFXVolume;
                            SoundEffects["PressStart"].Play();
                        }
                    }
                    else if ((Singleton.Instance.CurrentMouse.X >= 60 && Singleton.Instance.CurrentMouse.X <= 228) &&
                    (Singleton.Instance.CurrentMouse.Y >= 514 && Singleton.Instance.CurrentMouse.Y <= 549))
                    {
                        textButtonColor[5] = new Color(247, 159, 47);
                        if (isClicked)
                        {
                            SoundEffects["PressStart"].Volume = Singleton.Instance.MasterSFXVolume;
                            SoundEffects["PressStart"].Play();
                        }
                    }
                    else if ((Singleton.Instance.CurrentMouse.X >= 60 && Singleton.Instance.CurrentMouse.X <= 147) &&
                    (Singleton.Instance.CurrentMouse.Y >= 575 && Singleton.Instance.CurrentMouse.Y <= 610))
                    {
                        textButtonColor[6] = new Color(247, 159, 47);
                        if (isClicked)
                        {
                            Singleton.Instance.CurrentScreenState = Singleton.ScreenState.Quit;
                        }
                    }
                    else
                    {
                        textButtonColor = new Color[7]
                        {
                            Color.WhiteSmoke,
                            Color.WhiteSmoke,
                            Color.WhiteSmoke,
                            Color.WhiteSmoke,
                            Color.WhiteSmoke,
                            Color.WhiteSmoke,
                            Color.WhiteSmoke,
                        };
                    }
                    break;
            }
            base.Update(gameTime, sprites);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (Singleton.Instance.CurrentScreenState)
            {
                case Singleton.ScreenState.MenuScreen:
                    //drawMenuScreen
                    spriteBatch.Draw(_texture[0], new Vector2(0,0), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                    //drawBlackAce
                    spriteBatch.Draw(_texture[1], new Vector2(Singleton.WIDTH/2, Singleton.HEIGHT / 5), null, fadeBA, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
                    //drawLogo
                    spriteBatch.Draw(_texture[2], new Vector2(Singleton.WIDTH / 2 - 80, Singleton.HEIGHT / 10 - 80),
                        null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

                    //drawTextStoryModeButton
                    spriteBatch.DrawString(Singleton.Instance._font, "Story Mode", new Vector2(60, 209),
                        textButtonColor[0], 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                    //drawTextEditFolderButton
                    spriteBatch.DrawString(Singleton.Instance._font, "Edit Folder", new Vector2(60, 270),
                        textButtonColor[1], 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                    //drawTextShopButton
                    spriteBatch.DrawString(Singleton.Instance._font, "Shop", new Vector2(60, 331),
                        textButtonColor[2], 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                    //drawTextTutorialButton
                    spriteBatch.DrawString(Singleton.Instance._font, "Tutorial", new Vector2(60, 392),
                        textButtonColor[3], 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                    //drawTextOptionButton
                    spriteBatch.DrawString(Singleton.Instance._font, "Option", new Vector2(60, 453),
                        textButtonColor[4], 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                    //drawTextCreditsButton
                    spriteBatch.DrawString(Singleton.Instance._font, "Credits", new Vector2(60, 514),
                        textButtonColor[5], 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                    //drawTextQuitButton
                    spriteBatch.DrawString(Singleton.Instance._font, "Quit", new Vector2(60, 575),
                        textButtonColor[6], 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);

                    //drawFadeBlack
                    spriteBatch.Draw(_texture[0], new Vector2(0, 0), null, fade, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

                    break;
            }
            base.Draw(spriteBatch);
        }
    }
}
