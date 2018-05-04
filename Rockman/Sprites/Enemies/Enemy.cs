using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Rockman.Managers;
using Rockman.Models;

namespace Rockman.Sprites
{
    class Enemy : Sprite
    {
 
        public Enemy(Texture2D[] texture)
            : base(texture)
        {
        }

        public Enemy(Dictionary<string, Animation> animations)
            : base(animations)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {

        }

        public override void Reset()
        {
        }

    }
}
