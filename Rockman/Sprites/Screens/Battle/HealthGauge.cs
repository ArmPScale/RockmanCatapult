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
    class HealthGauge : Screen
    {
        int HP = 0, Noise = 0;
        Vector2 NoisePosition; 
        Dictionary<string, Rectangle> rectGaugeImg = new Dictionary<string, Rectangle>()
        {
            {"HealthGauge",  new Rectangle(220, 188, 56, 19) },
            {"GreenGauge",  new Rectangle(220, 217, 56, 19) },
            {"OrangeGauge",  new Rectangle(283, 2, 44, 16) },
            {"RedGauge",  new Rectangle(346, 2, 44, 16) },
        };

        public HealthGauge(Texture2D[] texture) : base(texture)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            switch (Singleton.Instance.CurrentScreenState)
            {
                case Singleton.ScreenState.StoryMode:
                    HP = Singleton.Instance.HeroHP;
                    Noise = Singleton.Instance.NoisePercent;
                    switch (Singleton.Instance.CurrentGameState)
                    {
                        case Singleton.GameState.GameCustomScreen:
                            Position = new Vector2(375, 10);
                            NoisePosition = new Vector2(375, 70);
                            break;
                        case Singleton.GameState.GamePlaying:
                            Position = new Vector2(0, 10);
                            NoisePosition = new Vector2(0, 70);

                            break;
                    }
                    break;
            }
            base.Update(gameTime, sprites);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.GameCustomScreen:
                    //drawHealthGauge
                    spriteBatch.Draw(_texture[1], Position, rectGaugeImg["HealthGauge"], Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                    //drawHeroHP
                    if (Singleton.Instance.maxHeroHP / 4 >= HP)
                    {
                        spriteBatch.DrawString(Singleton.Instance._font, string.Format("{0}", HP),
                            new Vector2(430, 18), Color.OrangeRed, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                    }
                    else
                    {
                        spriteBatch.DrawString(Singleton.Instance._font, string.Format("{0}", HP), 
                            new Vector2(430, 18), Color.White, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                    }

                    //drawNoiseGauge
                    //if (Noise < 50)
                    //{
                    //    spriteBatch.Draw(_texture[1], NoisePosition, rectGaugeImg["GreenGauge"], 
                    //        Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);

                    //}
                    //else if (Noise < 200)
                    //{
                    //    spriteBatch.Draw(_texture[1], NoisePosition, rectGaugeImg["OrangeGauge"],
                    //       Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                    //}
                    //else
                    //{
                    //    spriteBatch.Draw(_texture[1], NoisePosition, rectGaugeImg["RedGauge"],
                    //       Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                    //}
                    //spriteBatch.DrawString(Singleton.Instance._font, string.Format("{0}", Noise),
                    //        new Vector2(430, 78), Color.White, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                    break;
                case Singleton.GameState.GamePlaying:
                    //drawHealthGauge
                    spriteBatch.Draw(_texture[1], Position, rectGaugeImg["HealthGauge"], Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                    //drawHeroHP
                    if (Singleton.Instance.maxHeroHP / 4 >= HP)
                    {
                        spriteBatch.DrawString(Singleton.Instance._font, string.Format("{0}", HP),
                            new Vector2(55, 18), Color.OrangeRed, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                    }
                    else
                    {
                        spriteBatch.DrawString(Singleton.Instance._font, string.Format("{0}", HP),
                            new Vector2(55, 18), Color.White, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                    }

                    ////drawNoiseGauge
                    //if (Noise < 50)
                    //{
                    //    spriteBatch.Draw(_texture[1], NoisePosition, rectGaugeImg["GreenGauge"],
                    //        Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);

                    //}
                    //else if (Noise < 200)
                    //{
                    //    spriteBatch.Draw(_texture[1], NoisePosition, rectGaugeImg["OrangeGauge"],
                    //       Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                    //}
                    //else
                    //{
                    //    spriteBatch.Draw(_texture[1], NoisePosition, rectGaugeImg["RedGauge"],
                    //       Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                    //}
                    //spriteBatch.DrawString(Singleton.Instance._font, string.Format("{0}", Noise),
                    //        new Vector2(55, 78), Color.White, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                    break;
            }
            base.Draw(spriteBatch);
        }
    }
}
