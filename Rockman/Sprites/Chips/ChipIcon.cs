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
            {"Recovery10",  new Rectangle(192, 163, 16, 16) },
            {"Recovery30",  new Rectangle(211, 163, 16, 16) },
            {"Recovery50",  new Rectangle(230, 163, 16, 16) },
            {"Recovery80",  new Rectangle(249, 163, 16, 16) },
            {"Recovery120",  new Rectangle(191+(19*4), 163, 16, 16) },
            {"Recovery150",  new Rectangle(191+(19*5), 163, 16, 16) },
            {"Recovery200",  new Rectangle(2+(19*0), 163+18, 16, 16) },
            {"Recovery300",  new Rectangle(2+(19*1), 163+18, 16, 16) },
            {"Barrier10",  new Rectangle(40, 199, 16, 16) },
            {"Barrier100",  new Rectangle(40+(19*1), 199, 16, 16) },
            {"Barrier200",  new Rectangle(40+(19*2), 199, 16, 16) },
            {"CrackOut",  new Rectangle(40+(19*4), 199, 16, 16) },
            {"DoubleCrack",  new Rectangle(40+(19*4), 199, 16, 16) },
            {"TripleCrack",  new Rectangle(40+(19*4), 199, 16, 16) },
            {"DreamAura",  new Rectangle(40+(19*4), 199, 16, 16) },
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
                                rectChipIconImg[Singleton.Instance.chipCustomSelect[i - 1]],
                                Color.White, 0f, Vector2.Zero, 2.75f, SpriteEffects.None, 0f);
                        }
                        if (rectChipIconImg.ContainsKey(Singleton.Instance.chipStackImg[i - 1]))
                        {
                            spriteBatch.Draw(_texture[1], new Vector2(97 * 3, (24 * 2 * i) + 22),
                                rectChipIconImg[Singleton.Instance.chipStackImg[i - 1]],
                                Color.White, 0f, Vector2.Zero, 2.75f, SpriteEffects.None, 0f);
                        }
                        break;
                }
            }
            base.Draw(spriteBatch);
        }
    }
}
