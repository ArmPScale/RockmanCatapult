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
        Vector2 bgScroll;
        int n = 0;

        public BackgroundSprite(Texture2D[] texture)
            : base(texture)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            n--;
            bgScroll = new Vector2(n, 0);
            if(n <= -(Singleton.WIDTH) * scale)
            {
                n = 0;
            }
            base.Update(gameTime, sprites);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (Singleton.Instance.CurrentScreenState)
            {
                case Singleton.ScreenState.StoryMode:
                    spriteBatch.Draw(_texture[0], bgScroll, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                    break;
            }
            base.Draw(spriteBatch);
        }
    }
}
