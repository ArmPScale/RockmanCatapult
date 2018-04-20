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
    class EmotionPlayer : Screen
    {
        Dictionary<string, Rectangle> rectEmotionPlayerImg = new Dictionary<string, Rectangle>()
        {
            {"NormalEmotion",  new Rectangle(2, 2, 44, 16) },
            {"AngryEmotion",  new Rectangle(2+(47*1), 2, 44, 16) },
            {"TireEmotion",  new Rectangle(2+(47*2), 2, 44, 16) },
            {"FullSyncEmotion",  new Rectangle(2+(47*3), 2, 44, 16) },
            {"DarkEmotion",  new Rectangle(2+(47*4), 2, 44, 16) },
            {"HurtEmotion",  new Rectangle(2+(47*5), 2, 44, 16) },
        };

        public EmotionPlayer(Texture2D[] texture) : base(texture)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.GameCustomScreen:
                    Position = new Vector2(375, 70);

                    break;
                case Singleton.GameState.GamePlaying:
                    Position = new Vector2(10, 70);

                    break;
            }
            base.Update(gameTime, sprites);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.GameCustomScreen:
                    spriteBatch.Draw(_texture[2], Position, rectEmotionPlayerImg[Singleton.Instance.chooseEmotionPlayer], Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                    break;
                case Singleton.GameState.GamePlaying:
                    spriteBatch.Draw(_texture[2], Position, rectEmotionPlayerImg[Singleton.Instance.chooseEmotionPlayer], Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                    break;
            }
            base.Draw(spriteBatch);
        }
    }
}
