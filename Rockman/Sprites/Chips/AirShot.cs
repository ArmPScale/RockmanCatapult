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
    class AirShot : Chip
    {
        Dictionary<string, Rectangle> rectChipAirShotImg = new Dictionary<string, Rectangle>()
        {
            {"AirShot",  new Rectangle(168, 0, 56, 47) },
        };
        Dictionary<string, int> airShotAtk = new Dictionary<string, int>()
        {
            {"AirShot",  20 },
        };

        public AirShot(Texture2D[] texture)
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
                        rectChipAirShotImg.ContainsKey(Singleton.Instance.useChipSlotIn.Peek()))
                    {
                        //animateUseChipNormal
                        Singleton.Instance.choosePlayerAnimate = "Buster";
                        Singleton.Instance.currentChipCoolDown = 0.2f;
                        Singleton.Instance.useChipName = "AirShot";
                        Singleton.Instance.CurrentPlayerState = Singleton.PlayerState.UseChipNormal;
                        //useChipAirShot
                        SoundEffects["AirShot"].Volume = Singleton.Instance.MasterSFXVolume;
                        SoundEffects["AirShot"].Play();
                        for (int k = Singleton.Instance.currentPlayerPoint.Y; k < 10; k++)
                        {
                            if (Singleton.Instance.spriteMove[Singleton.Instance.currentPlayerPoint.X, k] > 1)
                            {
                                Singleton.Instance.spriteHP[Singleton.Instance.currentPlayerPoint.X, k] -= airShotAtk[Singleton.Instance.useChipSlotIn.Peek()];
                                //impactHalfExplode
                                Singleton.Instance.chipEffect[Singleton.Instance.currentPlayerPoint.X, k] = 2;
                                if (k + 1 < 10)
                                {
                                    Singleton.Instance.spriteMove[Singleton.Instance.currentPlayerPoint.X, k + 1] = Singleton.Instance.spriteMove[Singleton.Instance.currentPlayerPoint.X, k];
                                    Singleton.Instance.spriteMove[Singleton.Instance.currentPlayerPoint.X, k] = 0;
                                    Singleton.Instance.spriteHP[Singleton.Instance.currentPlayerPoint.X, k + 1] = Singleton.Instance.spriteHP[Singleton.Instance.currentPlayerPoint.X, k];
                                    Singleton.Instance.spriteHP[Singleton.Instance.currentPlayerPoint.X, k] = 0;
                                }
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
                    if (rectChipAirShotImg.ContainsKey(chipCustomImg[Singleton.Instance.currentChipSelect.X]))
                    {
                        Singleton.Instance.chipClass = "Standard";
                        Singleton.Instance.chipType = "Wind";
                        //drawChipName
                        spriteBatch.DrawString(Singleton.Instance._font, chipCustomImg[Singleton.Instance.currentChipSelect.X], new Vector2(50, 40), Color.WhiteSmoke, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                        //drawChipImg
                        spriteBatch.Draw(_texture[0], new Vector2(16 * 3, 24 * 3 - 2),
                            rectChipAirShotImg[chipCustomImg[Singleton.Instance.currentChipSelect.X]],
                            Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                        //drawChipAtk
                        spriteBatch.DrawString(Singleton.Instance._font, "" + airShotAtk[chipCustomImg[Singleton.Instance.currentChipSelect.X]], new Vector2(150, 220), Color.WhiteSmoke, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                    }
                    break;
            }
            base.Draw(spriteBatch);
        }
    }
}
