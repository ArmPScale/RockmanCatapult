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
    class BEGalaxy : Chip
    {
        private float _chargeBusterCoolDown = 0f, _busterShotDown = 0f;
        private bool isScan = false, isDamaged = true;
        private List<Vector2> posEnemies = new List<Vector2>();
        Dictionary<string, Rectangle> rectChipBEGalaxyImg = new Dictionary<string, Rectangle>()
        {
            {"BlackEndGalaxy",  new Rectangle(57, 385, 56, 47) },
        };

        public BEGalaxy(Texture2D[] texture)
             : base(texture)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.GamePlaying:
                    if (Singleton.Instance.useChipSlotIn.Count != 0 &&
                        rectChipBEGalaxyImg.ContainsKey(Singleton.Instance.useChipSlotIn.Peek()))
                    {
                        Singleton.Instance.useSceneChip = true;
                        //Singleton.Instance.useChipName = Singleton.Instance.useChipSlotIn.Peek();
                    }
                    break;
                case Singleton.GameState.GameUseChip:
                    if (Singleton.Instance.useChipDuring &&
                        rectChipBEGalaxyImg.ContainsKey(Singleton.Instance.useChipSlotIn.Peek()))
                    {
                        if (Singleton.Instance.choosePlayerAnimate == "Sword")
                        {
                            // to do code
                            if (!isScan)
                            {
                                for (int i = 0; i < 10; i++)
                                {
                                    for (int j = 0; j < 3; j++)
                                    {
                                        if (Singleton.Instance.spriteMove[j, i] > 0)
                                        {
                                            posEnemies.Add(new Vector2(j, i));
                                        }
                                    }
                                }
                                isScan = true;
                            }
                            if (posEnemies.Count != 0)
                            {

                                if (isDamaged)
                                {
                                    for (int i = 0; i < posEnemies.Count; i++)
                                    {
                                        Singleton.Instance.spriteHP[(int)posEnemies[i].X, (int)posEnemies[i].Y] -= 500;
                                        Console.WriteLine(posEnemies[i]);
                                    }
                                    isDamaged = false;
                                }
                                posEnemies.Clear();
                            }
                        }
                        else if (!isDamaged)
                        {
                            isScan = false;
                            isDamaged = true;
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
                    if (rectChipBEGalaxyImg.ContainsKey(chipCustomImg[Singleton.Instance.currentChipSelect.X]))
                    {
                        Singleton.Instance.chipClass = "Giga";
                        Singleton.Instance.chipType = "Normal";
                        //drawChipName
                        spriteBatch.DrawString(Singleton.Instance._font, chipCustomImg[Singleton.Instance.currentChipSelect.X], new Vector2(50, 40), Color.WhiteSmoke, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                        //drawChipImg
                        spriteBatch.Draw(_texture[0], new Vector2(16 * 3, 24 * 3 - 2),
                            rectChipBEGalaxyImg[chipCustomImg[Singleton.Instance.currentChipSelect.X]],
                            Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                        //drawChipAtk
                        spriteBatch.DrawString(Singleton.Instance._font, string.Format("{0}", 500), new Vector2(150, 220), Color.WhiteSmoke, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                    }
                    break;
            }
            base.Draw(spriteBatch);
        }
    }
}
