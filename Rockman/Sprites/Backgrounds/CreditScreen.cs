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
        Color[] colors = { Color.WhiteSmoke, Color.DarkOrange };
        Vector2 offset;
        string[] stringPieces;
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
                            stringPieces = new string[]{ "Background Musics ", "by KokiRemix from YouTube"};
                            offset = Vector2.Zero;
                            for (int x = 0; x < stringPieces.Length; x++)
                            {
                                spriteBatch.DrawString(Singleton.Instance._font, stringPieces[x], new Vector2(200, 300) + offset, colors[x], 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                                offset.X += Singleton.Instance._font.MeasureString(stringPieces[x]).X + 150;
                            }
                            //drawTextSFX
                            stringPieces = new string[]{ "Sound Effects ", "by Rockman EXE from Capcom" };
                            offset = Vector2.Zero;
                            for (int x = 0; x < stringPieces.Length; x++)
                            {
                                spriteBatch.DrawString(Singleton.Instance._font, stringPieces[x], new Vector2(200, 400) + offset, colors[x], 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                                offset.X += Singleton.Instance._font.MeasureString(stringPieces[x]).X + 150;
                            }
                            //drawTextSprite
                            stringPieces = new string[] { "Sprites ", "by Rockman EXE from Capcom" };
                            offset = Vector2.Zero;
                            for (int x = 0; x < stringPieces.Length; x++)
                            {
                                spriteBatch.DrawString(Singleton.Instance._font, stringPieces[x], new Vector2(200, 500) + offset, colors[x], 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                                offset.X += Singleton.Instance._font.MeasureString(stringPieces[x]).X + 150;
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
