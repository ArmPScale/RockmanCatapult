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
    class Throwable : Chip
    {
        private bool isPressed = false;

        Dictionary<string, Rectangle> rectChipThrowableImg = new Dictionary<string, Rectangle>()
        {
            {"MiniBomb",  new Rectangle(0, 48, 56, 47) },
            {"BigBomb",  new Rectangle(0 + (56*1), 48, 56, 47) },
            {"EnergyBomb",  new Rectangle(0 + (56*2), 48, 56, 47) },
            {"MegaEnergyBomb",  new Rectangle(0 + (56*3), 48, 56, 47) },
            {"BugBomb",  new Rectangle(0 + (56*4), 48, 56, 47) },
            {"SearchBomb1",  new Rectangle(0 + (56*5), 48, 56, 47) },
            {"SearchBomb2",  new Rectangle(0 + (56*6), 48, 56, 47) },
            {"SearchBomb3",  new Rectangle(0 + (56*7), 48, 56, 47) },
            {"CannonBall",  new Rectangle(0 + (56*8), 48, 56, 47) },
            {"BlackBomb",  new Rectangle(0 + (56*9), 48, 56, 47) },
            {"DarkBomb",  new Rectangle(56, 336, 56, 47) },
        };
        Dictionary<string, int> throwableAtk = new Dictionary<string, int>()
        {
            {"MiniBomb",  50 },
            {"BigBomb",  140 },
            {"EnergyBomb",  40 },
            {"MegaEnergyBomb",  60 },
            {"BugBomb",  0},
            {"SearchBomb1",  80 },
            {"SearchBomb2",  110 },
            {"SearchBomb3",  140 },
            {"CannonBall",  140 },
            {"BlackBomb",  250 },
            {"DarkBomb",  200 },
        };

        public Throwable(Texture2D[] texture)
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
                        throwableAtk.ContainsKey(Singleton.Instance.useChipSlotIn.Peek()))
                    {
                        Singleton.Instance.useSceneChip = true;
                        //Singleton.Instance.useChipName = Singleton.Instance.useChipSlotIn.Peek();
                    }
                    break;
                case Singleton.GameState.GameUseChip:
                    if (Singleton.Instance.useChipDuring &&
                        throwableAtk.ContainsKey(Singleton.Instance.useChipSlotIn.Peek()))
                    {
                        // to do
                        if(!isPressed && 
                            Singleton.Instance.CurrentKey.IsKeyDown(Keys.K) && Singleton.Instance.PreviousKey.IsKeyUp(Keys.K))
                        {
                            isPressed = true;
                            Singleton.Instance.useThrowableChip = true;
                            Singleton.Instance.playerChipAtk = throwableAtk[Singleton.Instance.useChipSlotIn.Peek()];
                        }
                        if (Singleton.Instance.useChipNearlySuccess)
                        {
                            isPressed = false;
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
                    if (rectChipThrowableImg.ContainsKey(chipCustomImg[Singleton.Instance.currentChipSelect.X]))
                    {
                        if(chipCustomImg[Singleton.Instance.currentChipSelect.X] == "DarkBomb")
                        {
                            Singleton.Instance.chipType = "Normal";
                            Singleton.Instance.chipClass = "Dark";
                        }
                        else if (chipCustomImg[Singleton.Instance.currentChipSelect.X] == "CannonBall")
                        {
                            Singleton.Instance.chipType = "Break";
                            Singleton.Instance.chipClass = "Standard";
                        }
                        else if (chipCustomImg[Singleton.Instance.currentChipSelect.X] == "BlackBomb")
                        {
                            Singleton.Instance.chipType = "Fire";
                            Singleton.Instance.chipClass = "Standard";
                        }
                        else
                        {
                            Singleton.Instance.chipType = "Normal";
                            Singleton.Instance.chipClass = "Standard";
                        }
                        //drawChipName
                        spriteBatch.DrawString(Singleton.Instance._font, chipCustomImg[Singleton.Instance.currentChipSelect.X], new Vector2(50, 40), Color.WhiteSmoke, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                        //drawChipImg
                        spriteBatch.Draw(_texture[0], new Vector2(16 * 3, 24 * 3 - 2),
                            rectChipThrowableImg[chipCustomImg[Singleton.Instance.currentChipSelect.X]],
                            Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                        //drawChipAtk
                        spriteBatch.DrawString(Singleton.Instance._font, "" + throwableAtk[chipCustomImg[Singleton.Instance.currentChipSelect.X]], new Vector2(150, 220), Color.WhiteSmoke, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                    }
                    break;
            }
            base.Draw(spriteBatch);
        }
    }
}
