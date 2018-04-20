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
    class Spreader : Chip
    {
        Dictionary<string, Rectangle> rectChipSpreaderImg = new Dictionary<string, Rectangle>()
        {
            {"SpreadGun1",  new Rectangle(392, 0, 56, 47) },
            {"SpreadGun2",  new Rectangle(448, 0, 56, 47) },
            {"SpreadGun3",  new Rectangle(504, 0, 56, 47) },
            {"DarkSpread",  new Rectangle(112, 336, 56, 47) },
        };
        Dictionary<string, int> spreaderAtk = new Dictionary<string, int>()
        {
            {"SpreadGun1",  30 },
            {"SpreadGun2",  60 },
            {"SpreadGun3",  90 },
            {"DarkSpread",  400 },
        };

        public Spreader(Texture2D[] texture)
             : base(texture)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.GamePlaying:
                    if (Singleton.Instance.useChipSlotIn.Count != 0 &&
                        Singleton.Instance.useNormalChip == true &&
                        rectChipSpreaderImg.ContainsKey(Singleton.Instance.useChipSlotIn.Peek()))
                    {
                        //animateUseChipNormal
                        Singleton.Instance.choosePlayerAnimate = "Buster";
                        Singleton.Instance.CurrentPlayerState = Singleton.PlayerState.UseChipNormal;
                        //useChipAirShot
                        SoundEffects["Spreader"].Volume = Singleton.Instance.MasterSFXVolume;
                        SoundEffects["Spreader"].Play();
                        if (Singleton.Instance.useChipSlotIn.Peek() == "DarkSpread")
                        {
                            Singleton.Instance.useChipName = "DarkSpread";
                            Singleton.Instance.currentChipCoolDown = 0.35f;
                            Singleton.Instance.chooseEmotionPlayer = "DarkEmotion";
                            Singleton.Instance.statusBugHP = true;
                        }
                        else
                        {
                            Singleton.Instance.useChipName = "Spreader";
                            Singleton.Instance.currentChipCoolDown = 0.3f;
                        }
                        for (int k = Singleton.Instance.currentPlayerPoint.Y; k < 10; k++)
                        {
                            if (Singleton.Instance.spriteMove[Singleton.Instance.currentPlayerPoint.X, k] > 1)
                            {
                                Singleton.Instance.drawChipEffectName = Singleton.Instance.useChipName;
                                Singleton.Instance.spriteHP[Singleton.Instance.currentPlayerPoint.X, k] -= spreaderAtk[Singleton.Instance.useChipSlotIn.Peek()];
                                if (Singleton.Instance.currentPlayerPoint.X - 1 >= 0)
                                {
                                    Singleton.Instance.chipEffect[Singleton.Instance.currentPlayerPoint.X - 1, k - 1] = 1;
                                    Singleton.Instance.chipEffect[Singleton.Instance.currentPlayerPoint.X - 1, k] = 1;
                                    if (k + 1 < 10) Singleton.Instance.chipEffect[Singleton.Instance.currentPlayerPoint.X - 1, k + 1] = 1;
                                }
                                if (Singleton.Instance.currentPlayerPoint.X + 1 < 3)
                                {
                                    Singleton.Instance.chipEffect[Singleton.Instance.currentPlayerPoint.X + 1, k - 1] = 1;
                                    Singleton.Instance.chipEffect[Singleton.Instance.currentPlayerPoint.X + 1, k] = 1;
                                    if (k + 1 < 10) Singleton.Instance.chipEffect[Singleton.Instance.currentPlayerPoint.X + 1, k + 1] = 1;
                                }
                                Singleton.Instance.chipEffect[Singleton.Instance.currentPlayerPoint.X, k - 1] = 1;
                                Singleton.Instance.chipEffect[Singleton.Instance.currentPlayerPoint.X, k] = 1;
                                if (k + 1 < 10) Singleton.Instance.chipEffect[Singleton.Instance.currentPlayerPoint.X, k + 1] = 1;

                                break;
                            }
                        }
                        Singleton.Instance.useChipSlotIn.Pop();
                        Singleton.Instance.useNormalChip = false;
                    }
                    break;
            }
            base.Update(gameTime, sprites);
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.GameCustomScreen:
                    if (rectChipSpreaderImg.ContainsKey(chipCustomImg[Singleton.Instance.currentChipSelect.X]))
                    {
                        if (chipCustomImg[Singleton.Instance.currentChipSelect.X] == "DarkSpread")
                        {
                            Singleton.Instance.chipClass = "Dark";
                        }
                        else
                        {
                            Singleton.Instance.chipClass = "Standard";
                        }
                        Singleton.Instance.chipType = "Normal";
                        //drawChipName
                        spriteBatch.DrawString(Singleton.Instance._font, chipCustomImg[Singleton.Instance.currentChipSelect.X], new Vector2(50, 40), Color.WhiteSmoke, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                        //drawChipImg
                        spriteBatch.Draw(_texture[0], new Vector2(16 * 3, 24 * 3 - 2),
                            rectChipSpreaderImg[chipCustomImg[Singleton.Instance.currentChipSelect.X]],
                            Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                        //drawChipAtk
                        spriteBatch.DrawString(Singleton.Instance._font, "" + spreaderAtk[chipCustomImg[Singleton.Instance.currentChipSelect.X]], new Vector2(150, 220), Color.WhiteSmoke, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                    }
                    break;
            }
            base.Draw(spriteBatch);
        }
    }
}
