using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rockman.Models;

namespace Rockman.Sprites.Chips
{
    class Chip : Sprite
    {
        #region PHYSICS_VARIABLES
        public float GRAVITY = 981;
        public Vector2 Acceleration = new Vector2(100, 100);
        public Vector2 Velocity = new Vector2(1200, -2000);

        #endregion

        public String[] chipCustomImg = new string[6];
        public String[] chipCustomImgCopy = new string[6];

        public Chip(Texture2D[] texture) 
            : base(texture)
        {
            chipCustomImg = Singleton.Instance.chipCustomSelect;
        }

        public Chip(Dictionary<string, Animation> animations) 
            : base(animations)
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
    }
}
