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
    class Barrier : Chip
    {
        Dictionary<string, Rectangle> rectChipBarImg = new Dictionary<string, Rectangle>()
        {
            {"Barrier",  new Rectangle(224, 192, 56, 47) },
            {"Barrier100",  new Rectangle(224 + (56*1), 192, 56, 47) },
            {"Barrier200",  new Rectangle(224 + (56*2), 192, 56, 47) },
            {"DreamAura",  new Rectangle(56, 288, 56, 47) },
        };
        Dictionary<string, int> barrierDef = new Dictionary<string, int>()
        {
            {"Barrier",  10 },
            {"Barrier100",  100 },
            {"Barrier200",  200 },
            {"DreamAura",  200 },
        };

        public Barrier(Texture2D[] texture)
            : base(texture)
        {
            _texture = texture;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.GamePlaying:
                    if (Singleton.Instance.useChipSlotIn.Count != 0 &&
                        barrierDef.ContainsKey(Singleton.Instance.useChipSlotIn.Peek()))
                    {
                        Singleton.Instance.useSceneChip = true;
                        //Singleton.Instance.useChipName = Singleton.Instance.useChipSlotIn.Peek();
                    }
                    break;
                case Singleton.GameState.GameUseChip:
                    if (Singleton.Instance.useChipDuring &&
                        barrierDef.ContainsKey(Singleton.Instance.useChipSlotIn.Peek()))
                    {
                        SoundEffects["Barrier"].Volume = Singleton.Instance.MasterSFXVolume;
                        SoundEffects["Barrier"].Play();
                        if(Singleton.Instance.useChipSlotIn.Peek() == "DreamAura")
                        {
                            if (Singleton.Instance.HeroBarrier > 0) Singleton.Instance.HeroBarrier = 0;
                            Singleton.Instance.HeroAura = barrierDef[Singleton.Instance.useChipSlotIn.Peek()];
                        }
                        else
                        {
                            if (Singleton.Instance.HeroAura > 0) Singleton.Instance.HeroAura = 0;
                            Singleton.Instance.HeroBarrier = barrierDef[Singleton.Instance.useChipSlotIn.Peek()];
                        }
                        Singleton.Instance.useChipNearlySuccess = true;
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
                    if (rectChipBarImg.ContainsKey(chipCustomImg[Singleton.Instance.currentChipSelect.X]))
                    {
                        if(chipCustomImg[Singleton.Instance.currentChipSelect.X] == "DreamAura")
                        {
                            Singleton.Instance.chipClass = "Mega";
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
                            rectChipBarImg[chipCustomImg[Singleton.Instance.currentChipSelect.X]],
                            Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                    }
                    break;
            }
            base.Draw(spriteBatch);
        }
    }
}
