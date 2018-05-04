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
    class CrackOut : Chip
    {
        Point currentPanel;
        Dictionary<string, Rectangle> rectChipCrackImg = new Dictionary<string, Rectangle>()
        {
            {"CrackOut",  new Rectangle(112, 240, 56, 47) },
            {"DoubleCrack",  new Rectangle(168, 240, 56, 47) },
            {"TripleCrack",  new Rectangle(224, 240, 56, 47) },
        };

        public CrackOut(Texture2D[] texture)
            : base(texture)
        {
            _texture = texture;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.GamePlaying:
                    currentPanel = Singleton.Instance.currentPlayerPoint;
                    if (Singleton.Instance.useChipSlotIn.Count != 0 &&
                        Singleton.Instance.useNormalChip == true &&
                        rectChipCrackImg.ContainsKey(Singleton.Instance.useChipSlotIn.Peek()))
                    {
                        //animateUseChipNormal
                        Singleton.Instance.choosePlayerAnimate = "Bomb";
                        Singleton.Instance.currentChipCoolDown = 0.3f;
                        Singleton.Instance.CurrentPlayerState = Singleton.PlayerState.UseChipNormal;
                        //useChipCrackOut
                        SoundEffects["CrackOut"].Volume = Singleton.Instance.MasterSFXVolume;
                        SoundEffects["CrackOut"].Play();
                        if (Singleton.Instance.useChipSlotIn.Peek() == "DoubleCrack")
                        {
                            if (Singleton.Instance.currentPlayerPoint.Y + 2 < 10
                                && Singleton.Instance.panelStage[currentPanel.X, currentPanel.Y + 2] != 3)
                            {
                                if (Singleton.Instance.spriteMove[currentPanel.X, currentPanel.Y + 2] > 0)
                                {
                                    Singleton.Instance.panelStage[currentPanel.X, currentPanel.Y + 2] = 1;
                                }
                                else
                                {
                                    Singleton.Instance.panelStage[currentPanel.X, currentPanel.Y + 2] = 2;
                                    Singleton.Instance.areaCracked[currentPanel.X, currentPanel.Y + 2] = 1;
                                }
                            }
                        }
                        else if (Singleton.Instance.useChipSlotIn.Peek() == "TripleCrack")
                        {
                            if (Singleton.Instance.currentPlayerPoint.X - 1 >= 0
                                && Singleton.Instance.panelStage[currentPanel.X - 1, currentPanel.Y + 1] != 3)
                            {
                                if (Singleton.Instance.spriteMove[currentPanel.X - 1, currentPanel.Y + 1] > 0)
                                {
                                    Singleton.Instance.panelStage[currentPanel.X - 1, currentPanel.Y + 1] = 1;
                                }
                                else
                                {
                                    Singleton.Instance.panelStage[currentPanel.X - 1, currentPanel.Y + 1] = 2;
                                    Singleton.Instance.areaCracked[currentPanel.X - 1, currentPanel.Y + 1] = 1;
                                }
                            }
                            if (Singleton.Instance.currentPlayerPoint.X + 1 < 3
                                && Singleton.Instance.panelStage[currentPanel.X + 1, currentPanel.Y + 1] != 3)
                            {
                                if (Singleton.Instance.spriteMove[currentPanel.X + 1, currentPanel.Y + 1] > 0)
                                {
                                    Singleton.Instance.panelStage[currentPanel.X + 1, currentPanel.Y + 1] = 1;
                                }
                                else
                                {
                                    Singleton.Instance.panelStage[currentPanel.X + 1, currentPanel.Y + 1] = 2;
                                    Singleton.Instance.areaCracked[currentPanel.X + 1, currentPanel.Y + 1] = 1;
                                }
                            }
                        }
                        if (Singleton.Instance.currentPlayerPoint.Y + 1 < 10
                            && Singleton.Instance.panelStage[currentPanel.X, currentPanel.Y + 1] != 3)
                        {
                            if (Singleton.Instance.spriteMove[currentPanel.X, currentPanel.Y + 1] > 0)
                            {
                                Singleton.Instance.panelStage[currentPanel.X, currentPanel.Y + 1] = 1;
                            }
                            else
                            {
                                Singleton.Instance.panelStage[currentPanel.X, currentPanel.Y + 1] = 2;
                                Singleton.Instance.areaCracked[currentPanel.X, currentPanel.Y + 1] = 1;
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
                    if (rectChipCrackImg.ContainsKey(chipCustomImg[Singleton.Instance.currentChipSelect.X]))
                    {
                        Singleton.Instance.chipClass = "Standard";
                        Singleton.Instance.chipType = "Normal";
                        //drawChipName
                        spriteBatch.DrawString(Singleton.Instance._font, chipCustomImg[Singleton.Instance.currentChipSelect.X], new Vector2(50, 40), Color.WhiteSmoke, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                        //drawChipImg
                        spriteBatch.Draw(_texture[0], new Vector2(16 * 3, 24 * 3 - 2),
                            rectChipCrackImg[chipCustomImg[Singleton.Instance.currentChipSelect.X]],
                            Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                    }
                    break;
            }
            base.Draw(spriteBatch);
        }
    }
}
