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
{    class StoryModeScreen : Background
    {
        public bool isClicked = false;
        Color backButtonColor;

        public StoryModeScreen(Texture2D[] texture)
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
                case Singleton.MenuState.StoryMode:
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

                    if ((Singleton.Instance.CurrentMouse.X >= 170 && Singleton.Instance.CurrentMouse.X <= 170 + _texture[1].Width) &&
                            (Singleton.Instance.CurrentMouse.Y >= 265 && Singleton.Instance.CurrentMouse.Y <= 265 + _texture[1].Height))
                    {
                        //Stage1
                        if (isClicked)
                        {
                            SoundEffects["PressStart"].Volume = Singleton.Instance.MasterSFXVolume;
                            SoundEffects["PressStart"].Play();
                            Singleton.Instance.stagesName = "Stage1Metton";
                            Singleton.Instance.CurrentScreenState = Singleton.ScreenState.StoryMode;
                            Singleton.Instance.CurrentGameState = Singleton.GameState.GameEnemyAppear;
                        }
                    }
                    else if ((Singleton.Instance.CurrentMouse.X >= 220 + _texture[1].Width && Singleton.Instance.CurrentMouse.X <= 220 + _texture[1].Width*2) &&
                            (Singleton.Instance.CurrentMouse.Y >= 265 && Singleton.Instance.CurrentMouse.Y <= 265 + _texture[1].Height))
                    {
                        //Stage2
                        if (isClicked)
                        {
                            SoundEffects["PressStart"].Volume = Singleton.Instance.MasterSFXVolume;
                            SoundEffects["PressStart"].Play();
                            Singleton.Instance.stagesName = "Stage2Meteor";
                            Singleton.Instance.CurrentScreenState = Singleton.ScreenState.StoryMode;
                            Singleton.Instance.CurrentGameState = Singleton.GameState.GameEnemyAppear;
                        }
                    }
                    else if ((Singleton.Instance.CurrentMouse.X >= 270 + _texture[2].Width * 2 && Singleton.Instance.CurrentMouse.X <= 270 + _texture[2].Width * 3) &&
                            (Singleton.Instance.CurrentMouse.Y >= 265 && Singleton.Instance.CurrentMouse.Y <= 265 + _texture[2].Height))
                    {
                        //Stage3
                        if (isClicked)
                        {
                            SoundEffects["PressStart"].Volume = Singleton.Instance.MasterSFXVolume;
                            SoundEffects["PressStart"].Play();
                            Singleton.Instance.stagesName = "Stage3Queen";
                            Singleton.Instance.CurrentScreenState = Singleton.ScreenState.StoryMode;
                            Singleton.Instance.CurrentGameState = Singleton.GameState.GameEnemyAppear;
                        }
                    }
                    else if ((Singleton.Instance.CurrentMouse.X >= 320 + _texture[2].Width * 3 && Singleton.Instance.CurrentMouse.X <= 320 + _texture[3].Width * 4) &&
                            (Singleton.Instance.CurrentMouse.Y >= 265 && Singleton.Instance.CurrentMouse.Y <= 265 + _texture[3].Height))
                    {
                        //Stage4
                        if (isClicked)
                        {
                            SoundEffects["PressStart"].Volume = Singleton.Instance.MasterSFXVolume;
                            SoundEffects["PressStart"].Play();
                            Singleton.Instance.stagesName = "Stage4Crimson";
                            Singleton.Instance.CurrentScreenState = Singleton.ScreenState.StoryMode;
                            Singleton.Instance.CurrentGameState = Singleton.GameState.GameEnemyAppear;
                        }
                    }
                    else if ((Singleton.Instance.CurrentMouse.X >= 925 && Singleton.Instance.CurrentMouse.X <= 925+98) &&
                            (Singleton.Instance.CurrentMouse.Y >= 700 && Singleton.Instance.CurrentMouse.Y <= 700+34))
                    {
                        backButtonColor = new Color(247, 159, 47);
                        //backButton
                        if (isClicked) Singleton.Instance.CurrentMenuState = Singleton.MenuState.MainMenu;
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
                        case Singleton.MenuState.StoryMode:
                            //drawTextStoryMode
                            spriteBatch.DrawString(Singleton.Instance._font, "Story Mode", new Vector2(60, 144),
                                Color.WhiteSmoke, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
                            //drawTextBackButton
                            spriteBatch.DrawString(Singleton.Instance._font, "Back", new Vector2(925, 700),
                                backButtonColor, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                            //drawStage1
                            spriteBatch.Draw(_texture[1], new Vector2(170, 265), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                            //drawStage2
                            spriteBatch.Draw(_texture[2], new Vector2(220 + _texture[1].Width, 265), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                            //drawStage3
                            spriteBatch.Draw(_texture[3], new Vector2(270 + _texture[2].Width * 2, 265), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                            //drawStage4
                            spriteBatch.Draw(_texture[4], new Vector2(320 + _texture[3].Width * 3, 265), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

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
