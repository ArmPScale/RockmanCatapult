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
            {"Recovery10",  new Rectangle(193, 164, 14, 14) },
            {"Recovery30",  new Rectangle(212, 164, 14, 14) },
            {"Recovery50",  new Rectangle(231, 164, 14, 14) },
            {"Recovery80",  new Rectangle(250, 164, 14, 14) },
            {"Recovery120",  new Rectangle(192+(19*4), 164, 14, 14) },
            {"Recovery150",  new Rectangle(192+(19*5), 164, 14, 14) },
            {"Recovery200",  new Rectangle(3+(19*0), 164+18, 14, 14) },
            {"Recovery300",  new Rectangle(3+(19*1), 164+18, 14, 14) },
            {"Barrier",  new Rectangle(41, 200, 14, 14) },
            {"Barrier100",  new Rectangle(41+(19*1), 200, 14, 14) },
            {"Barrier200",  new Rectangle(41+(19*2), 200, 14, 14) },
            {"DreamAura",  new Rectangle(41+(19*4), 200, 14, 14) },
        };
        Dictionary<string, Rectangle> rectChipIconEXE4Img = new Dictionary<string, Rectangle>()
        {
            {"CrackOut",  new Rectangle(240, 49, 14, 14) },
            {"DoubleCrack",  new Rectangle(240+(16*1), 49, 14, 14) },
            {"TripleCrack",  new Rectangle(240+(16*2), 49, 14, 14) },
            {"DarkRecovery",  new Rectangle(224, 113, 14, 14) },
        };

        public ChipIcon(Texture2D[] texture)
            : base(texture)
        {
            _texture = texture;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {

            base.Update(gameTime, sprites);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 1; i < 6; i++)
            {
                switch (Singleton.Instance.CurrentGameState)
                {
                    case Singleton.GameState.GameCustomScreen:
                        //chipEXE6
                        if (rectChipIconImg.ContainsKey(Singleton.Instance.chipCustomSelect[i-1]))
                        {
                            spriteBatch.Draw(_texture[1], new Vector2((48 * i) - 20, 105 * 3),
                                rectChipIconImg[Singleton.Instance.chipCustomSelect[i - 1]],
                                Color.White, 0f, Vector2.Zero, 2.75f, SpriteEffects.None, 0f);
                        }
                        if (rectChipIconImg.ContainsKey(Singleton.Instance.chipStackImg[i - 1]))
                        {
                            spriteBatch.Draw(_texture[1], new Vector2((98 * 3)-1, (24 * 2 * i) + 26),
                                rectChipIconImg[Singleton.Instance.chipStackImg[i - 1]],
                                Color.White, 0f, Vector2.Zero, 2.75f, SpriteEffects.None, 0f);
                        }
                        //chipEXE4
                        if (rectChipIconEXE4Img.ContainsKey(Singleton.Instance.chipCustomSelect[i - 1]))
                        {
                            spriteBatch.Draw(_texture[3], new Vector2((48 * i) - 20, 105 * 3),
                                rectChipIconEXE4Img[Singleton.Instance.chipCustomSelect[i - 1]],
                                Color.White, 0f, Vector2.Zero, 2.75f, SpriteEffects.None, 0f);
                        }
                        if (rectChipIconEXE4Img.ContainsKey(Singleton.Instance.chipStackImg[i - 1]))
                        {
                            spriteBatch.Draw(_texture[3], new Vector2((98 * 3)-1, (24 * 2 * i) + 26),
                                rectChipIconEXE4Img[Singleton.Instance.chipStackImg[i - 1]],
                                Color.White, 0f, Vector2.Zero, 2.75f, SpriteEffects.None, 0f);
                        }
                        break;
                    case Singleton.GameState.GamePlaying:
                        if (Singleton.Instance.useChipSlotIn.Count != 0)
                        {
                            if (rectChipIconImg.ContainsKey(Singleton.Instance.useChipSlotIn.Peek()))
                            {
                                spriteBatch.Draw(_texture[1], new Vector2(10, 750),
                                    rectChipIconImg[Singleton.Instance.useChipSlotIn.Peek()],
                                    Color.White, 0f, Vector2.Zero, 2.75f, SpriteEffects.None, 0f);
                            }
                            else if (rectChipIconEXE4Img.ContainsKey(Singleton.Instance.useChipSlotIn.Peek()))
                            {
                                spriteBatch.Draw(_texture[3], new Vector2(10, 750),
                                    rectChipIconEXE4Img[Singleton.Instance.useChipSlotIn.Peek()],
                                    Color.White, 0f, Vector2.Zero, 2.75f, SpriteEffects.None, 0f);
                            }
                            spriteBatch.DrawString(Singleton.Instance._font, Singleton.Instance.useChipSlotIn.Peek(), new Vector2(60, 755), Color.WhiteSmoke, 0f, Vector2.Zero, 1.2f, SpriteEffects.None, 0f);
                        }
                        break;
                }
            }
            base.Draw(spriteBatch);
        }
    }
}
