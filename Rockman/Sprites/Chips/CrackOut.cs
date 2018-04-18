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
        Dictionary<string, Rectangle> rectChipCrackImg = new Dictionary<string, Rectangle>()
        {
            {"CrackOut",  new Rectangle(112, 240, 56, 47) },
            {"DoubleCrack",  new Rectangle(168, 240, 56, 47) },
            {"TripleCrack",  new Rectangle(224, 240, 56, 47) },
        };
        Dictionary<string, int> crackOutPanel = new Dictionary<string, int>()
        {
            {"CrackOut",  1 },
            {"DoubleCrack",  2 },
            {"TripleCrack",  3 },
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
                    if (Singleton.Instance.useChipSlotIn.Count != 0 &&
                        Singleton.Instance.useNormalChip == true &&
                        rectChipCrackImg.ContainsKey(Singleton.Instance.useChipSlotIn.Peek()))
                    {
                        SoundEffects["CrackOut"].Volume = Singleton.Instance.MasterSFXVolume;
                        SoundEffects["CrackOut"].Play();
                        if (Singleton.Instance.useChipSlotIn.Peek() == "DoubleCrack")
                        {
                            Singleton.Instance.panelStage[Singleton.Instance.currentPlayerPoint.X, Singleton.Instance.currentPlayerPoint.Y + 1] = 2;
                            Singleton.Instance.panelStage[Singleton.Instance.currentPlayerPoint.X, Singleton.Instance.currentPlayerPoint.Y + 2] = 2;
                        }
                        else if (Singleton.Instance.useChipSlotIn.Peek() == "TripleCrack")
                        {
                            Singleton.Instance.panelStage[Singleton.Instance.currentPlayerPoint.X, Singleton.Instance.currentPlayerPoint.Y + 1] = 2;
                        }
                        else
                        {
                            Singleton.Instance.panelStage[Singleton.Instance.currentPlayerPoint.X, Singleton.Instance.currentPlayerPoint.Y + 1] = 2;
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
