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
    class ChipIcon : Chip
    {
        Dictionary<string, Rectangle> rectChipIconImg = new Dictionary<string, Rectangle>()
        {
            {"Recovery10",  new Rectangle(192, 164, 16, 16) },
            {"Recovery30",  new Rectangle(211, 164, 16, 16) },
            {"Recovery50",  new Rectangle(230, 164, 16, 16) },
            {"Recovery80",  new Rectangle(249, 164, 16, 16) },
            {"Recovery120",  new Rectangle(192+(19*4), 164, 16, 16) },
            {"Recovery150",  new Rectangle(192+(19*5), 164, 16, 16) },
            {"Recovery200",  new Rectangle(2+(19*0), 164+18, 16, 16) },
            {"Recovery300",  new Rectangle(2+(19*1), 164+18, 16, 16) },
        };

        public ChipIcon(Texture2D[] texture)
            : base(texture)
        {
            _texture = texture;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.GameCustomScreen:

                    break;
            }
            base.Update(gameTime, sprites);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 1; i < 6; i++)
            {
                switch (Singleton.Instance.CurrentGameState)
                {
                    case Singleton.GameState.GameCustomScreen:
                        if (rectChipIconImg.ContainsKey(Singleton.Instance.chipCustomSelect[i-1]))
                        {
                            spriteBatch.Draw(_texture[1], new Vector2((48 * i) - 22, 104 * 3),
                                rectChipIconImg[Singleton.Instance.chipCustomSelect[i-1]],
                                Color.White, 0f, Vector2.Zero, 2.75f, SpriteEffects.None, 0f);
                        }
                        break;
                }
            }
            base.Draw(spriteBatch);
        }
    }
}
