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
    class Screen : Sprite
    {
        public enum CustomState
        {
            Open,
            Wait,
            Close
        }
        public static CustomState CurrentCustomState;

        public Screen(Texture2D[] texture) : base(texture)
        {
        }

        public Screen(Dictionary<string, Animation> animations) : base(animations)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {

            base.Update(gameTime, sprites);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void Reset()
        {
            base.Reset();
        }
        public static void setState(CustomState newState)
        {
            CurrentCustomState = newState;
        }
    }
}
