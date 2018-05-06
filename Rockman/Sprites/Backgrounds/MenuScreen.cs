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
    class MenuScreen : Background
    {
        Color fade, fadeBA;
        int alpha = 255, alphaBA = 0;
        bool isFadeIn = true;
        public MenuScreen(Texture2D[] texture)
            : base(texture)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            switch (Singleton.Instance.CurrentScreenState)
            {
                case Singleton.ScreenState.MenuScreen:
                    //fadeScreen
                    if (alpha >= 0)
                    {
                        alpha -= 5;
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
                    //drawFadeBlack
                    spriteBatch.Draw(_texture[0], new Vector2(0, 0), null, fade, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

                    break;
            }
            base.Draw(spriteBatch);
        }
    }
}
