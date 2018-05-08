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

        public AppearScreen(Texture2D[] texture)
            : base(texture)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.GameEnemyAppear:
                    //fadeScreen
                    fade.R += 5;
                    fade.G += 5;
                    fade.B += 5;
                    fade.A += 5;
                    if (fade.A >= 255)
                    {
                        MediaPlayer.Stop();
                        Singleton.Instance.CurrentGameState = Singleton.GameState.GameCustomScreen;
                    }
                    Console.WriteLine(fade.A);
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
