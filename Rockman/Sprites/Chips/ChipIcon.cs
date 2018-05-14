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
            {"Cannon",  new Rectangle(22, 2, 14, 14) },
            {"HiCannon",  new Rectangle(22+(19*1), 2, 14, 14) },
            {"MegaCannon",  new Rectangle(22+(19*2), 2, 14, 14) },
            {"AirShot",  new Rectangle(79, 2, 14, 14) },
            {"SpreadGun1",  new Rectangle(174, 2, 14, 14) },
            {"SpreadGun2",  new Rectangle(174+(19*1), 2, 14, 14) },
            {"SpreadGun3",  new Rectangle(174+(19*2), 2, 14, 14) },
            {"MiniBomb",  new Rectangle(117, 56, 14, 14) },
            {"BigBomb",  new Rectangle(193, 218, 14, 14) },
            {"EnergyBomb",  new Rectangle(117+(19*1), 56, 14, 14) },
            {"MegaEnergyBomb",  new Rectangle(117+(19*2), 56, 14, 14) },
            {"BlackBomb",  new Rectangle(117+(19*6), 56, 14, 14) },
            {"BugBomb",  new Rectangle(60, 74, 14, 14) },
            {"Recovery10",  new Rectangle(193, 164, 14, 14) },
            {"Recovery30",  new Rectangle(212, 164, 14, 14) },
            {"Recovery50",  new Rectangle(231, 164, 14, 14) },
            {"Recovery80",  new Rectangle(250, 164, 14, 14) },
            {"Recovery120",  new Rectangle(192+(19*4), 164, 14, 14) },
            {"Recovery150",  new Rectangle(192+(19*5), 164, 14, 14) },
            {"Recovery200",  new Rectangle(3+(19*0), 164+18, 14, 14) },
            {"Recovery300",  new Rectangle(3+(19*1), 164+18, 14, 14) },
            {"PanelReturn",  new Rectangle(117, 182, 14, 14) },
            {"HolyPanel",  new Rectangle(117+(19*2), 182, 14, 14) },
            {"Sanctuary",  new Rectangle(117+(19*3), 182, 14, 14) },
            {"Barrier",  new Rectangle(41, 200, 14, 14) },
            {"Barrier100",  new Rectangle(41+(19*1), 200, 14, 14) },
            {"Barrier200",  new Rectangle(41+(19*2), 200, 14, 14) },
            {"DreamAura",  new Rectangle(41+(19*4), 200, 14, 14) },
            {"CherprangRiver",  new Rectangle(193, 254, 14, 14) },
            {"JaneRiver",  new Rectangle(193, 254, 14, 14) },
            {"BlackAce",  new Rectangle(136, 254, 14, 14) },
        };
        Dictionary<string, Rectangle> rectChipIconEXE4Img = new Dictionary<string, Rectangle>()
        {
            {"CannonBall",  new Rectangle(256, 17, 14, 14) },
            {"SearchBomb1",  new Rectangle(144, 97, 14, 14) },
            {"SearchBomb2",  new Rectangle(144+(16*1), 97, 14, 14) },
            {"SearchBomb3",  new Rectangle(144+(16*2), 97, 14, 14) },
            {"CrackOut",  new Rectangle(240, 49, 14, 14) },
            {"DoubleCrack",  new Rectangle(240+(16*1), 49, 14, 14) },
            {"TripleCrack",  new Rectangle(240+(16*2), 49, 14, 14) },
            {"DarkCannon",  new Rectangle(128, 113, 14, 14) },
            {"DarkSpread",  new Rectangle(160, 113, 14, 14) },
            {"DarkBomb",  new Rectangle(176, 113, 14, 14) },
            {"DarkRecovery",  new Rectangle(224, 113, 14, 14) },
            {"DarkStage",  new Rectangle(240, 113, 14, 14) },
            {"FinalGun",  new Rectangle(240, 113, 14, 14) },

        };
        Dictionary<string, int> allChipIconAtk = new Dictionary<string, int>()
        {
            {"Cannon",  40 },
            {"HiCannon",  100 },
            {"MegaCannon",  180 },
            {"AirShot",  20 },
            {"SpreadGun1",  30 },
            {"SpreadGun2",  60 },
            {"SpreadGun3",  90 },
            {"MiniBomb",  50 },
            {"BigBomb",  140 },
            {"EnergyBomb",  40 },
            {"MegaEnergyBomb",  60 },
            {"BlackBomb",  250 },
            {"CannonBall",  140 },
            {"SearchBomb1",  80 },
            {"SearchBomb2",  110 },
            {"SearchBomb3",  140 },
            {"DarkCannon",  Singleton.Instance.maxHeroHP - Singleton.Instance.HeroHP},
            {"DarkSpread",  400 },
            {"DarkBomb",  200 },
            {"CherprangRiver",  400 },
            {"JaneRiver",  400 },
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
                case Singleton.GameState.GamePlaying:
                    if (Singleton.Instance.maxHeroHP - Singleton.Instance.HeroHP < 1000) allChipIconAtk["DarkCannon"] = Singleton.Instance.maxHeroHP - Singleton.Instance.HeroHP;
                    break;
            }
            base.Update(gameTime, sprites);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 1; i < 6; i++)
            {
                Color[] colors = { Color.WhiteSmoke, Color.DarkOrange };
                Vector2 startPosition = new Vector2(60, 755);
                Vector2 offset = Vector2.Zero;
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
                    case Singleton.GameState.GameWaiting:
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
                            //chipNameAndAtk
                            if (allChipIconAtk.ContainsKey(Singleton.Instance.useChipSlotIn.Peek()))
                            {
                                string[] stringPieces = { Singleton.Instance.useChipSlotIn.Peek(), "" + allChipIconAtk[Singleton.Instance.useChipSlotIn.Peek()] };
                                for (int x = 0; x < stringPieces.Length; x++)
                                {
                                    spriteBatch.DrawString(Singleton.Instance._font, stringPieces[x], startPosition + offset, colors[x], 0f, Vector2.Zero, 1.2f, SpriteEffects.None, 0f);
                                    offset.X += Singleton.Instance._font.MeasureString(stringPieces[x]).X + 40;
                                }
                            }
                            else
                            {
                                spriteBatch.DrawString(Singleton.Instance._font, Singleton.Instance.useChipSlotIn.Peek(), startPosition, Color.WhiteSmoke, 0f, Vector2.Zero, 1.2f, SpriteEffects.None, 0f);
                            }
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
                            //chipNameAndAtk
                            if (allChipIconAtk.ContainsKey(Singleton.Instance.useChipSlotIn.Peek()))
                            {
                                string[] stringPieces = { Singleton.Instance.useChipSlotIn.Peek(), "" + allChipIconAtk[Singleton.Instance.useChipSlotIn.Peek()] };
                                for (int x = 0; x < stringPieces.Length; x++)
                                {
                                    spriteBatch.DrawString(Singleton.Instance._font, stringPieces[x], startPosition + offset, colors[x], 0f, Vector2.Zero, 1.2f, SpriteEffects.None, 0f);
                                    offset.X += Singleton.Instance._font.MeasureString(stringPieces[x]).X + 40;
                                }
                            }
                            else
                            {
                                spriteBatch.DrawString(Singleton.Instance._font, Singleton.Instance.useChipSlotIn.Peek(), startPosition, Color.WhiteSmoke, 0f, Vector2.Zero, 1.2f, SpriteEffects.None, 0f);
                            }
                        }
                        break;
                }
            }
            base.Draw(spriteBatch);
        }
    }
}
