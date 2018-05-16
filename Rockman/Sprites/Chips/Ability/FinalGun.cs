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
    class FinalGun : Chip
    {
        private float _chargeBusterCoolDown = 0f, _busterShotDown = 0f;
        private bool isScan = false;
        private List<Vector2> posEnemies = new List<Vector2>();
        public int times, indexList = 0;
        Dictionary<string, Rectangle> rectChipFinalGunImg = new Dictionary<string, Rectangle>()
        {
            {"FinalGun",  new Rectangle(0, 384, 56, 47) },
        };

        public FinalGun(Texture2D[] texture)
             : base(texture)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.GamePlaying:
                    if (Singleton.Instance.useChipSlotIn.Count != 0 &&
                        rectChipFinalGunImg.ContainsKey(Singleton.Instance.useChipSlotIn.Peek()))
                    {
                        Singleton.Instance.useSceneChip = true;
                        //Singleton.Instance.useChipName = Singleton.Instance.useChipSlotIn.Peek();
                    }
                    break;
                case Singleton.GameState.GameUseChip:
                    if (Singleton.Instance.useChipDuring &&
                        rectChipFinalGunImg.ContainsKey(Singleton.Instance.useChipSlotIn.Peek()))
                    {
                        if(!isScan)
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
                            _busterShotDown += (float)gameTime.ElapsedGameTime.TotalSeconds;
                            if (Singleton.Instance.spriteHP[(int)posEnemies[indexList].X, (int)posEnemies[indexList].Y] <= 0)
                            {
                                indexList += 1;
                                if (indexList >= posEnemies.Count) times = 12;
                            }
                            if (times < 12 && _busterShotDown > 0.5f)
                            {
                                Singleton.Instance.choosePlayerAnimate = "Buster";
                                SoundEffects["Buster"].Volume = Singleton.Instance.MasterSFXVolume;
                                SoundEffects["Buster"].Play();
                                SoundEffects["BusterHit"].Volume = Singleton.Instance.MasterSFXVolume;
                                SoundEffects["BusterHit"].Play();
                                Singleton.Instance.spriteHP[(int)posEnemies[indexList].X, (int)posEnemies[indexList].Y] -= 50;
                                Singleton.Instance.chipEffect[(int)posEnemies[indexList].X, (int)posEnemies[indexList].Y] = 8;
                                times += 1;
                                _busterShotDown = 0f;
                            }
                            else if (times >= 12)
                            {
                                Singleton.Instance.choosePlayerAnimate = "Alive";
                                times = 0;
                                indexList = 0;
                                _busterShotDown = 0;
                                isScan = false;
                                posEnemies.Clear();
                                Singleton.Instance.useChipNearlySuccess = true;
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
                    if (rectChipFinalGunImg.ContainsKey(chipCustomImg[Singleton.Instance.currentChipSelect.X]))
                    {
                        Singleton.Instance.chipClass = "Giga";
                        Singleton.Instance.chipType = "Normal";
                        //drawChipName
                        spriteBatch.DrawString(Singleton.Instance._font, chipCustomImg[Singleton.Instance.currentChipSelect.X], new Vector2(50, 40), Color.WhiteSmoke, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                        //drawChipImg
                        spriteBatch.Draw(_texture[0], new Vector2(16 * 3, 24 * 3 - 2),
                            rectChipFinalGunImg[chipCustomImg[Singleton.Instance.currentChipSelect.X]],
                            Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                        //drawChipAtk
                        spriteBatch.DrawString(Singleton.Instance._font, string.Format("{0}", 50), new Vector2(150, 220), Color.WhiteSmoke, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                    }
                    break;
            }
            base.Draw(spriteBatch);
        }
    }
}
