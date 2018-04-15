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
    class CustomScreen : Sprite
    {

        public CustomScreen(Texture2D[] texture)
            : base(texture)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {

            base.Update(gameTime, sprites);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(_texture[0], bgScroll, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            base.Draw(spriteBatch);
        }
    }
}
