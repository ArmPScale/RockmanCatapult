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
    class RiverBNK : Chip
    {
        private float _magicCircleCoolDown = 0f, _magicFreezeCoolDown = 0f;
        private bool isDamaged = true;
        Dictionary<string, Rectangle> rectChipRiverImg = new Dictionary<string, Rectangle>()
        {
            {"CherprangRiver",  new Rectangle(0, 0, 168, 144) },
            {"JaneRiver",  new Rectangle(168, 0, 168, 144) },
        };

        Dictionary<string, int> riverAtk = new Dictionary<string, int>()
        {
            {"CherprangRiver",  400 },
            {"JaneRiver",  400 },
        };

        public RiverBNK(Texture2D[] texture)
             : base(texture)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.GamePlaying:
                    if (Singleton.Instance.useChipSlotIn.Count != 0 &&
                        riverAtk.ContainsKey(Singleton.Instance.useChipSlotIn.Peek()))
                    {
                        Singleton.Instance.useSceneChip = true;
                        //Singleton.Instance.useChipName = Singleton.Instance.useChipSlotIn.Peek();
                    }
                    break;
                case Singleton.GameState.GameUseChip:
                    if (Singleton.Instance.useChipDuring &&
                        riverAtk.ContainsKey(Singleton.Instance.useChipSlotIn.Peek()))
                    {
                        if (Singleton.Instance.useChipName == "CherprangRiver" || Singleton.Instance.useChipName == "JaneRiver")
                        {
                            _magicCircleCoolDown += (float)gameTime.ElapsedGameTime.TotalSeconds;
                            _magicFreezeCoolDown += (float)gameTime.ElapsedGameTime.TotalSeconds;

                            if (_magicCircleCoolDown > 0.2f && _magicCircleCoolDown < 0.3f)
                            {
                                SoundEffects["MagicCircle"].Volume = Singleton.Instance.MasterSFXVolume;
                                SoundEffects["MagicCircle"].Play();
                                Singleton.Instance.chipEffect[0, 6] = 5;
                            }
                            else if (_magicCircleCoolDown < 1f)
                            {
                                //magicFreezeStart
                                Singleton.Instance.chipEffect[0, 7] = 6;
                                Singleton.Instance.chipEffect[0, 8] = 6;
                                Singleton.Instance.chipEffect[1, 6] = 6;
                                Singleton.Instance.chipEffect[1, 7] = 6;
                                Singleton.Instance.chipEffect[1, 8] = 6;
                                Singleton.Instance.chipEffect[2, 6] = 6;
                                Singleton.Instance.chipEffect[2, 7] = 6;
                                Singleton.Instance.chipEffect[2, 8] = 6;
                            }
                            else if (_magicCircleCoolDown > 3f)
                            {
                                _magicCircleCoolDown = 0f;
                                isDamaged = true;
                                Singleton.Instance.useChipNearlySuccess = true;
                            }

                            //_magicFreezeCoolDown
                            if (isDamaged && _magicFreezeCoolDown > 1.9f)
                            {
                                Singleton.Instance.spriteHP[0, 6] -= riverAtk[Singleton.Instance.useChipSlotIn.Peek()];
                                Singleton.Instance.spriteHP[0, 7] -= riverAtk[Singleton.Instance.useChipSlotIn.Peek()];
                                Singleton.Instance.spriteHP[0, 8] -= riverAtk[Singleton.Instance.useChipSlotIn.Peek()];
                                Singleton.Instance.spriteHP[1, 6] -= riverAtk[Singleton.Instance.useChipSlotIn.Peek()];
                                Singleton.Instance.spriteHP[1, 7] -= riverAtk[Singleton.Instance.useChipSlotIn.Peek()];
                                Singleton.Instance.spriteHP[1, 8] -= riverAtk[Singleton.Instance.useChipSlotIn.Peek()];
                                Singleton.Instance.spriteHP[2, 6] -= riverAtk[Singleton.Instance.useChipSlotIn.Peek()];
                                Singleton.Instance.spriteHP[2, 7] -= riverAtk[Singleton.Instance.useChipSlotIn.Peek()];
                                Singleton.Instance.spriteHP[2, 8] -= riverAtk[Singleton.Instance.useChipSlotIn.Peek()];
                                isDamaged = false;
                            }
                            else if (_magicFreezeCoolDown > 2.9f)
                            {
                                for (int i = 0; i < 3; i++)
                                {
                                    for (int j = 0; j < 10; j++)
                                    {
                                        if (Singleton.Instance.chipEffect[i, j] == 6) Singleton.Instance.chipEffect[i, j] = 0;
                                    }
                                }
                                Singleton.Instance.chipEffect[0, 6] = 0;
                                _magicFreezeCoolDown = 0;
                            }

                        }
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
                    if (rectChipRiverImg.ContainsKey(chipCustomImg[Singleton.Instance.currentChipSelect.X]))
                    {
                        Singleton.Instance.chipClass = "Giga";
                        Singleton.Instance.chipType = "Aqua";
                        //drawChipName
                        spriteBatch.DrawString(Singleton.Instance._font, chipCustomImg[Singleton.Instance.currentChipSelect.X], new Vector2(50, 40), Color.WhiteSmoke, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                        //drawChipImg
                        spriteBatch.Draw(_texture[13], new Vector2(16 * 3, 24 * 3 - 2),
                            rectChipRiverImg[chipCustomImg[Singleton.Instance.currentChipSelect.X]],
                            Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                        //drawChipAtk
                        spriteBatch.DrawString(Singleton.Instance._font, "" + riverAtk[chipCustomImg[Singleton.Instance.currentChipSelect.X]], new Vector2(150, 220), Color.WhiteSmoke, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                    }
                    break;
            }
            base.Draw(spriteBatch);
        }
    }
}
