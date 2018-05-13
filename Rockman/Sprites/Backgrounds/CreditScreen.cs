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
{    class CreditScreen : Background
    {
        public bool isClicked = false;
        Color backButtonColor = Color.WhiteSmoke;
        float numberSelectedBGM = 0f , numberSelectedSFX = 0f;
        public int[] positionNumber = new int[6]
        {
            700,750,800,850,900,950
        };
        public Color[] textBGMColor = new Color[6]
        {
            Color.DarkGray,
            Color.DarkGray,
            Color.DarkGray,
            Color.DarkGray,
            Color.DarkGray,
            Color.DarkGray,
        };
        public Color[] textSFXColor = new Color[6]
        {
            Color.DarkGray,
            Color.DarkGray,
            Color.DarkGray,
            Color.DarkGray,
            Color.DarkGray,
            Color.DarkGray,
        };

        public CreditScreen(Texture2D[] texture)
            : base(texture)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            //stateOfMenu
            switch (Singleton.Instance.CurrentMenuState)
            {
                case Singleton.MenuState.MainMenu:
                    alpha = 255;
                    break;
                case Singleton.MenuState.Credits:
                    //Escape
                    if (Singleton.Instance.CurrentKey.IsKeyDown(Keys.Escape) && Singleton.Instance.PreviousKey.IsKeyUp(Keys.Escape))
                        Singleton.Instance.CurrentMenuState = Singleton.MenuState.MainMenu;
                    //fadeScreen
                    if (alpha >= 0)
                    {
                        alpha -= 8;
                        fade = new Color(0, 0, 0, alpha);
                    }
                    //checkClick
                    if (alpha <= 0 && Singleton.Instance.PreviousMouse.LeftButton == ButtonState.Released &&
                       Singleton.Instance.CurrentMouse.LeftButton == ButtonState.Pressed)
                        isClicked = true;
                    else isClicked = false;

                    //initialNumberSelected
                    numberSelectedBGM = Singleton.Instance.MasterBGMVolume / 0.2f;
                    textBGMColor[(int)numberSelectedBGM] = new Color(247, 159, 47);

                    numberSelectedSFX = Singleton.Instance.MasterSFXVolume / 0.2f;
                    textSFXColor[(int)numberSelectedSFX] = new Color(247, 159, 47);

                    for (int i = 0; i < positionNumber.Length; i++)
                    {
                        //textBGMNumber
                        if ((Singleton.Instance.CurrentMouse.X >= positionNumber[i] && Singleton.Instance.CurrentMouse.X <= positionNumber[i] + 25) &&
                            (Singleton.Instance.CurrentMouse.Y >= 300 && Singleton.Instance.CurrentMouse.Y <= 325))
                        {
                            if (isClicked)
                            {
                                SoundEffects["PressStart"].Volume = Singleton.Instance.MasterSFXVolume;
                                SoundEffects["PressStart"].Play();
                                textBGMColor = new Color[6]
                                {
                                    Color.DarkGray,
                                    Color.DarkGray,
                                    Color.DarkGray,
                                    Color.DarkGray,
                                    Color.DarkGray,
                                    Color.DarkGray,
                                };
                                textBGMColor[i] = new Color(247, 159, 47);
                                Singleton.Instance.MasterBGMVolume = 0.2f*i; 
                            }
                            else if (textBGMColor[i] != new Color(247, 159, 47))
                            {
                                textBGMColor[i] = Color.WhiteSmoke;
                            }
                        }
                        else if(textBGMColor[i] != new Color(247, 159, 47))
                        {
                            textBGMColor[i] = Color.DarkGray;
                        }
                        //textSFXNumber
                        if ((Singleton.Instance.CurrentMouse.X >= positionNumber[i] && Singleton.Instance.CurrentMouse.X <= positionNumber[i] + 25) &&
                            (Singleton.Instance.CurrentMouse.Y >= 400 && Singleton.Instance.CurrentMouse.Y <= 425))
                        {
                            if (isClicked)
                            {
                                SoundEffects["PressStart"].Volume = Singleton.Instance.MasterSFXVolume;
                                SoundEffects["PressStart"].Play();
                                textSFXColor = new Color[6]
                                {
                                    Color.DarkGray,
                                    Color.DarkGray,
                                    Color.DarkGray,
                                    Color.DarkGray,
                                    Color.DarkGray,
                                    Color.DarkGray,
                                };
                                textSFXColor[i] = new Color(247, 159, 47);
                                Singleton.Instance.MasterSFXVolume = 0.2f * i;
                            }
                            else if (textSFXColor[i] != new Color(247, 159, 47))
                            {
                                textSFXColor[i] = Color.WhiteSmoke;
                            }
                        }
                        else if (textSFXColor[i] != new Color(247, 159, 47))
                        {
                            textSFXColor[i] = Color.DarkGray;
                        }
                    }

                    //backButton
                    if ((Singleton.Instance.CurrentMouse.X >= 925 && Singleton.Instance.CurrentMouse.X <= 925 + 98) &&
                            (Singleton.Instance.CurrentMouse.Y >= 700 && Singleton.Instance.CurrentMouse.Y <= 700 + 34))
                    {
                        backButtonColor = new Color(247, 159, 47);
                        if (isClicked)
                        {
                            Singleton.Instance.CurrentMenuState = Singleton.MenuState.MainMenu;
                            backButtonColor = Color.WhiteSmoke;
                        }
                    }
                    else
                    {
                        backButtonColor = Color.WhiteSmoke;
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
                    switch (Singleton.Instance.CurrentMenuState)
                    {
                        case Singleton.MenuState.Credits:
                            //drawTextPractice
                            spriteBatch.DrawString(Singleton.Instance._font, "Credits", new Vector2(60, 144),
                                Color.WhiteSmoke, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
                            //drawTextBGM
                            spriteBatch.DrawString(Singleton.Instance._font, "Background Music", new Vector2(200, 300),
                                Color.WhiteSmoke, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                            //drawTextSFX
                            spriteBatch.DrawString(Singleton.Instance._font, "Sound Effect", new Vector2(200, 400),
                                Color.WhiteSmoke, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                            for (int i = 0;i< positionNumber.Length;i++)
                            {
                                //drawTextBGMNumber
                                spriteBatch.DrawString(Singleton.Instance._font, string.Format("{0}", i), new Vector2(positionNumber[i], 300),
                                    textBGMColor[i], 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                                //drawTextSFXNumber
                                spriteBatch.DrawString(Singleton.Instance._font, string.Format("{0}", i), new Vector2(positionNumber[i], 400),
                                    textSFXColor[i], 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                            }
                            
                            //drawTextBackButton
                            spriteBatch.DrawString(Singleton.Instance._font, "Back", new Vector2(925, 700),
                                backButtonColor, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);

                            //drawFadeBlack
                            spriteBatch.Draw(_texture[0], new Vector2(0, 0), null, fade, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                            break;
                    }
                    break;
            }
            base.Draw(spriteBatch);
        }
    }
}
