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
    class Cannon : Chip
    {
        Dictionary<string, Rectangle> rectChipCannonImg = new Dictionary<string, Rectangle>()
        {
            {"Cannon",  new Rectangle(0, 0, 56, 47) },
            {"HiCannon",  new Rectangle(56, 0, 56, 47) },
            {"MegaCannon",  new Rectangle(112, 0, 56, 47) },
            {"DarkCannon",  new Rectangle(0, 336, 56, 47) },
        };
        Dictionary<string, int> cannonAtk = new Dictionary<string, int>()
        {
            {"Cannon",  40 },
            {"HiCannon",  100 },
            {"MegaCannon",  180 },
            {"DarkCannon",  Singleton.Instance.maxHeroHP - Singleton.Instance.HeroHP},
        };

        public Cannon(Texture2D[] texture)
             : base(texture)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.GamePlaying:
                    if(Singleton.Instance.maxHeroHP - Singleton.Instance.HeroHP < 1000) cannonAtk["DarkCannon"] = Singleton.Instance.maxHeroHP - Singleton.Instance.HeroHP;
                    if (Singleton.Instance.useChipSlotIn.Count != 0 &&
                        Singleton.Instance.useNormalChip == true &&
                        rectChipCannonImg.ContainsKey(Singleton.Instance.useChipSlotIn.Peek()))
                    {
                        //animateUseChipNormal
                        Singleton.Instance.choosePlayerAnimate = "Buster";
                        Singleton.Instance.CurrentPlayerState = Singleton.PlayerState.UseChipNormal;
                        //useChipCannon
                        Singleton.Instance.useChipName = Singleton.Instance.useChipSlotIn.Peek();
                        Singleton.Instance.currentChipAtkTime = 0.5f;
                        if (Singleton.Instance.useChipSlotIn.Peek() == "DarkCannon")
                        {
                            Singleton.Instance.currentChipCoolDown = 0.95f;
                            Singleton.Instance.chooseEmotionPlayer = "DarkEmotion";
                            Singleton.Instance.statusBugHP = true;
                        }
                        else
                        {
                            Singleton.Instance.currentChipCoolDown = 0.8f;
                        }
                        SoundEffects["Cannon"].Volume = Singleton.Instance.MasterSFXVolume;
                        SoundEffects["Cannon"].Play();
                        for (int k = Singleton.Instance.currentPlayerPoint.Y; k < 10; k++)
                        {
                            if (Singleton.Instance.spriteMove[Singleton.Instance.currentPlayerPoint.X, k] > 1)
                            {
                                Singleton.Instance.drawChipEffectName = Singleton.Instance.useChipName;
                                Singleton.Instance.currentVirusGotDmgIndex = k;
                                Singleton.Instance.playerChipAtk = cannonAtk[Singleton.Instance.useChipSlotIn.Peek()];
                                //impactHalfExplode
                                Singleton.Instance.chipEffect[Singleton.Instance.currentPlayerPoint.X, k] = 2;
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
                    if (rectChipCannonImg.ContainsKey(chipCustomImg[Singleton.Instance.currentChipSelect.X]))
                    {
                        if (chipCustomImg[Singleton.Instance.currentChipSelect.X] == "DarkCannon")
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
                            rectChipCannonImg[chipCustomImg[Singleton.Instance.currentChipSelect.X]],
                            Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                        //drawChipAtk
                        spriteBatch.DrawString(Singleton.Instance._font, "" + cannonAtk[chipCustomImg[Singleton.Instance.currentChipSelect.X]], new Vector2(150, 220), Color.WhiteSmoke, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                    }
                    break;
            }
            base.Draw(spriteBatch);
        }
    }
}
