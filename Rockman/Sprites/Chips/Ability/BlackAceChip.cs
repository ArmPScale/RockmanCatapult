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
    class BlackAceChip : Chip
    {
        Dictionary<string, Rectangle> rectChipBlackAceImg = new Dictionary<string, Rectangle>()
        {
            {"BlackAce",  new Rectangle(0, 432, 56, 47) },
        };

        public BlackAceChip(Texture2D[] texture)
             : base(texture)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.GamePlaying:
                    if (Singleton.Instance.useChipSlotIn.Count != 0 &&
                        rectChipBlackAceImg.ContainsKey(Singleton.Instance.useChipSlotIn.Peek()))
                    {
                        Singleton.Instance.useSceneChip = true;
                        //Singleton.Instance.useChipName = Singleton.Instance.useChipSlotIn.Peek();
                    }
                    break;
                case Singleton.GameState.GameUseChip:
                    if (Singleton.Instance.useChipDuring &&
                        rectChipBlackAceImg.ContainsKey(Singleton.Instance.useChipSlotIn.Peek()))
                    {
                        //to do
                        Singleton.Instance.playerMove[Singleton.Instance.currentPlayerPoint.X, Singleton.Instance.currentPlayerPoint.Y] = 0;
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
                    if (rectChipBlackAceImg.ContainsKey(chipCustomImg[Singleton.Instance.currentChipSelect.X]))
                    {
                        Singleton.Instance.chipClass = "Standard";
                        Singleton.Instance.chipType = "Normal";
                        //drawChipName
                        spriteBatch.DrawString(Singleton.Instance._font, chipCustomImg[Singleton.Instance.currentChipSelect.X], new Vector2(50, 40), Color.WhiteSmoke, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                        //drawChipImg
                        spriteBatch.Draw(_texture[0], new Vector2(16 * 3, 24 * 3 - 2),
                            rectChipBlackAceImg[chipCustomImg[Singleton.Instance.currentChipSelect.X]],
                            Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                    }
                    break;
            }
            base.Draw(spriteBatch);
        }
    }
}
