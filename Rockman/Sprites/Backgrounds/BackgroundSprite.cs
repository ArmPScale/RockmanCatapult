using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rockman.Sprites
{
    class BackgroundSprite : Sprite
    {
        public BackgroundSprite(Texture2D[] texture)
            : base(texture)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            Position.X -= 0.75f;
            if(Position.X <= -(Singleton.WIDTH))
            {
                Position.X = 0;
            }
            base.Update(gameTime, sprites);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (Singleton.Instance.CurrentScreenState)
            {
                case Singleton.ScreenState.StoryMode:
                    spriteBatch.Draw(_texture[0], Position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                    break;
            }
            base.Draw(spriteBatch);
        }
    }
}
