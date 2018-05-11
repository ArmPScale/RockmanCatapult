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
        public static float GRAVITY = 981;
        public static Vector2 Acceleration = new Vector2(75, 75);
        public static Vector2 Velocity = new Vector2(0, 0);

        #endregion

        public String[] chipCustomImg = new string[7];
        public String[] chipCustomImgCopy = new string[7];

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
