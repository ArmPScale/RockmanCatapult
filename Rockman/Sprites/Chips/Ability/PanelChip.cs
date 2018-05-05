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
    class PanelChip : Chip
    {
        Dictionary<string, Rectangle> rectChipPanelImg = new Dictionary<string, Rectangle>()
        {
            {"PanelReturn",  new Rectangle(448, 144, 56, 47) },
            {"HolyPanel",  new Rectangle(448 + (56*1), 144, 56, 47) },
            {"Sanctuary",  new Rectangle(280, 288, 56, 47) },
            {"DarkStage",  new Rectangle(280, 336, 56, 47) },
        };

        public PanelChip(Texture2D[] texture)
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
                        rectChipPanelImg.ContainsKey(Singleton.Instance.useChipSlotIn.Peek()))
                    {
                        Singleton.Instance.useSceneChip = true;
                    }
                    break;
                case Singleton.GameState.GameUseChip:
                    if (Singleton.Instance.useChipDuring &&
                        rectChipPanelImg.ContainsKey(Singleton.Instance.useChipSlotIn.Peek()))
                    {
                        SoundEffects["PanelPower"].Volume = Singleton.Instance.MasterSFXVolume;
                        SoundEffects["PanelPower"].Play();
                        if(Singleton.Instance.useChipSlotIn.Peek() == "PanelReturn")
                        {
                            Singleton.Instance.panelStage = new int[3, 10]
                            {
                                { 0,0,0,0,0,0,0,0,0,0},
                                { 0,0,0,0,0,0,0,0,0,0},
                                { 0,0,0,0,0,0,0,0,0,0},
                            };
                            Singleton.Instance.panelElement = new int[3, 10]
                            {
                                { 0,0,0,0,0,0,0,0,0,0},
                                { 0,0,0,0,0,0,0,0,0,0},
                                { 0,0,0,0,0,0,0,0,0,0},
                            };
                        }
                        else if(Singleton.Instance.useChipSlotIn.Peek() == "HolyPanel")
                        {
                            if (Singleton.Instance.panelStage[Singleton.Instance.currentPlayerPoint.X, Singleton.Instance.currentPlayerPoint.Y] < 1)
                            {
                                Singleton.Instance.panelElement[Singleton.Instance.currentPlayerPoint.X, Singleton.Instance.currentPlayerPoint.Y] = 1;
                            }
                        }
                        else if (Singleton.Instance.useChipSlotIn.Peek() == "Sanctuary")
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                for (int j = 0; j < 10; j++)
                                {
                                    if (Singleton.Instance.panelBoundary[i, j] == 0 &&
                                        Singleton.Instance.panelStage[i,j] < 1)
                                    {
                                        Singleton.Instance.panelElement[i, j] = 1;
                                    }
                                }
                            }
                        }
                        else if (Singleton.Instance.useChipSlotIn.Peek() == "DarkStage")
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                for (int j = 0; j < 10; j++)
                                {
                                    if (Singleton.Instance.panelBoundary[i, j] == 0 &&
                                        Singleton.Instance.panelStage[i, j] < 1)
                                    {
                                        Singleton.Instance.panelElement[i, j] = 0;
                                        Singleton.Instance.panelStage[i, j] = 1;
                                    }
                                    if (Singleton.Instance.panelBoundary[i, j] == 1 &&
                                        Singleton.Instance.panelStage[i, j] < 1)
                                    {
                                        Singleton.Instance.panelElement[i, j] = 4;
                                    }
                                }
                            }
                            Singleton.Instance.chooseEmotionPlayer = "DarkEmotion";
                            Singleton.Instance.statusBugHP = true;
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
                    if (rectChipPanelImg.ContainsKey(chipCustomImg[Singleton.Instance.currentChipSelect.X]))
                    {
                        if(chipCustomImg[Singleton.Instance.currentChipSelect.X] == "Sanctuary")
                        {
                            Singleton.Instance.chipClass = "Mega";
                        }
                        else if (chipCustomImg[Singleton.Instance.currentChipSelect.X] == "DarkStage")
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
                            rectChipPanelImg[chipCustomImg[Singleton.Instance.currentChipSelect.X]],
                            Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                    }
                    break;
            }
            base.Draw(spriteBatch);
        }
    }
}
