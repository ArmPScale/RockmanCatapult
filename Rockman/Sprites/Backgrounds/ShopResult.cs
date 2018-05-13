using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Rockman.Sprites.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rockman.Sprites
{    class ShopResult : Background
    {
        public bool isClicked = false, isPutInYouGot = false;
        Color backButtonColor, fadeChipImg = new Color(255,255,255,255);
        int[] positionChipImgX = new int[3]
        {
            298,516,734
        };
        string[] chipNameYouGot = new string[3]
        {
            "","",""
        };
        Dictionary<string, Rectangle> rectChipImg = new Dictionary<string, Rectangle>()
        {
            {"Cannon",  new Rectangle(0, 0, 56, 47) },
            {"HiCannon",  new Rectangle(56, 0, 56, 47) },
            {"MegaCannon",  new Rectangle(112, 0, 56, 47) },
            {"AirShot",  new Rectangle(168, 0, 56, 47) },
            {"SpreadGun1",  new Rectangle(392, 0, 56, 47) },
            {"SpreadGun2",  new Rectangle(448, 0, 56, 47) },
            {"SpreadGun3",  new Rectangle(504, 0, 56, 47) },
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
            {"Recovery10",  new Rectangle(0, 145, 56, 47) },
            {"Recovery30",  new Rectangle(56, 145, 56, 47) },
            {"Recovery50",  new Rectangle(56*2, 145, 56, 47) },
            {"Recovery80",  new Rectangle(56*3, 145, 56, 47) },
            {"Recovery120",  new Rectangle(56*4, 145, 56, 47) },
            {"Recovery150",  new Rectangle(56*5, 145, 56, 47) },
            {"Recovery200",  new Rectangle(56*6, 145, 56, 47) },
            {"Recovery300",  new Rectangle(56*7, 145, 56, 47) },
            {"Barrier",  new Rectangle(224, 192, 56, 47) },
            {"Barrier100",  new Rectangle(224 + (56*1), 192, 56, 47) },
            {"Barrier200",  new Rectangle(224 + (56*2), 192, 56, 47) },
            {"PanelReturn",  new Rectangle(448, 144, 56, 47) },
            {"HolyPanel",  new Rectangle(448 + (56*1), 144, 56, 47) },
            {"Sanctuary",  new Rectangle(280, 288, 56, 47) },
            {"CrackOut",  new Rectangle(112, 240, 56, 47) },
            {"DoubleCrack",  new Rectangle(168, 240, 56, 47) },
            {"TripleCrack",  new Rectangle(224, 240, 56, 47) },
            {"DreamAura",  new Rectangle(56, 288, 56, 47) },
            {"DarkCannon",  new Rectangle(0, 336, 56, 47) },
            {"DarkBomb",  new Rectangle(56, 336, 56, 47) },
            {"DarkSpread",  new Rectangle(112, 336, 56, 47) },
            {"DarkRecovery",  new Rectangle(168, 336, 56, 47) },
            {"DarkStage",  new Rectangle(280, 336, 56, 47) },
            {"FinalGun",  new Rectangle(0, 384, 56, 47) },
        };
        Dictionary<string, Rectangle> rectChipRiverImg = new Dictionary<string, Rectangle>()
        {
            {"CherprangRiver",  new Rectangle(0, 0, 168, 144) },
            {"JaneRiver",  new Rectangle(168, 0, 168, 144) },
        };
        private List<string> _boosterPack1 = new List<string>()
        {
            "Cannon","Cannon","Cannon","Cannon","Cannon","AirShot","AirShot","AirShot","AirShot","AirShot",
            "SpreadGun1","SpreadGun1","SpreadGun1","SpreadGun1","SpreadGun1","MiniBomb","MiniBomb","MiniBomb","MiniBomb","MiniBomb",
            "SearchBomb1","SearchBomb1","SearchBomb1","Recovery10","Recovery10","Recovery10","Recovery10","Recovery10","Recovery30","Recovery30",
            "Barrier","Barrier","Barrier","HolyPanel","HolyPanel","PanelReturn","CrackOut","CrackOut","CrackOut","DoubleCrack",
            "DoubleCrack","DoubleCrack","TripleCrack","TripleCrack","HiCannon","HiCannon","SpreadGun2","SpreadGun2","EnergyBomb","EnergyBomb",
            "CannonBall","CannonBall","Recovery50","Recovery50","Barrier100","Barrier100","Barrier200","Recovery80","Sanctuary","DreamAura",
            "FinalGun","JaneRiver","CherprangRiver"
        };

        public ShopResult(Texture2D[] texture)
            : base(texture)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            //stateOfMenu
            switch (Singleton.Instance.CurrentMenuState)
            {
                case Singleton.MenuState.Shop:
                    if (Singleton.Instance.isGetChipResult)
                    {
                        //shuffleInQueue
                        if (Singleton.Instance.nextChipInPack.Count == 0)
                        {
                            _boosterPack1.Shuffle();
                            Singleton.Instance.nextChipInPack = new Queue<string>(_boosterPack1);
                        }
                        //putChipInYouGot
                        if (!isPutInYouGot)
                        {
                            SoundEffects["GotItem"].Volume = Singleton.Instance.MasterSFXVolume;
                            SoundEffects["GotItem"].Play();
                            for (int i = 0; i < chipNameYouGot.Length; i++)
                            {
                                //checkAlreadyHave
                                if (Singleton.Instance.allChipDict.ContainsKey(Singleton.Instance.nextChipInPack.Peek()))
                                {
                                    Singleton.Instance.allChipDict[Singleton.Instance.nextChipInPack.Peek()] += 1;
                                }
                                else
                                {
                                    Singleton.Instance.allChipDict.Add(Singleton.Instance.nextChipInPack.Peek(), 1);
                                }
                                chipNameYouGot[i] = Singleton.Instance.nextChipInPack.Dequeue();
                            }
                            isPutInYouGot = true;
                            Console.WriteLine(Singleton.Instance.nextChipInPack.Count);
                            Console.WriteLine(Singleton.Instance.allChipDict);
                        }
                        fadeChipImg *= 0.96f;
                        //fadeScreen
                        if (alpha <= 120)
                        {
                            alpha += 4;
                            fade = new Color(0, 0, 0, alpha);
                        }
                        //checkClick
                        if (alpha >= 120 && Singleton.Instance.PreviousMouse.LeftButton == ButtonState.Released &&
                           Singleton.Instance.CurrentMouse.LeftButton == ButtonState.Pressed)
                            isClicked = true;
                        else isClicked = false;

                        if ((Singleton.Instance.CurrentMouse.X >= 825 && Singleton.Instance.CurrentMouse.X <= 825 + 95) &&
                                (Singleton.Instance.CurrentMouse.Y >= 580 && Singleton.Instance.CurrentMouse.Y <= 580 + 30))
                        {
                            backButtonColor = new Color(247, 159, 47);
                            //backButton
                            if (isClicked)
                            {
                                isPutInYouGot = false;
                                alpha = 0;
                                fadeChipImg = new Color(255, 255, 255, 255);
                                backButtonColor = Color.WhiteSmoke;
                                Singleton.Instance.isGetChipResult = false;
                            }
                        }
                        else
                        {
                            backButtonColor = Color.WhiteSmoke;
                        }
                    }
                    break;
            }
            base.Update(gameTime, sprites);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (Singleton.Instance.CurrentScreenState)
            {
                case Singleton.ScreenState.MenuScreen:
                    switch (Singleton.Instance.CurrentMenuState)
                    {
                        case Singleton.MenuState.Shop:
                            if (Singleton.Instance.isGetChipResult)
                            {
                                //drawFadeBlack
                                spriteBatch.Draw(_texture[0], new Vector2(0, 0), null, fade, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

                                //drawResultShop
                                spriteBatch.Draw(_texture[3], new Vector2(240, 160), null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                                //drawTextResult
                                spriteBatch.DrawString(Singleton.Instance._font, "Result", new Vector2(300, 168),
                                    Color.WhiteSmoke, 0f, Vector2.Zero, 1.2f, SpriteEffects.None, 0f);
                                //drawTextDescription
                                spriteBatch.DrawString(Singleton.Instance._font, "You Got", new Vector2(525, 250),
                                    Color.WhiteSmoke, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);

                                //drawChipImg
                                for (int i=0;i< positionChipImgX.Length; i++)
                                {
                                    if (chipNameYouGot[i] == "JaneRiver" ||
                                        chipNameYouGot[i] == "CherprangRiver")
                                    {
                                        spriteBatch.Draw(_texture[5], new Vector2(positionChipImgX[i], 350),
                                        rectChipRiverImg[chipNameYouGot[i]],
                                        Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                                    }
                                    else
                                    {
                                        spriteBatch.Draw(_texture[4], new Vector2(positionChipImgX[i], 350),
                                        rectChipImg[chipNameYouGot[i]],
                                        Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                                    }
                                }
                                //drawChipName
                                for (int i = 0; i < positionChipImgX.Length; i++)
                                {
                                    if (chipNameYouGot[i] == "DreamAura" ||
                                        chipNameYouGot[i] == "Barrier200" ||
                                        chipNameYouGot[i] == "PanelReturn" ||
                                        chipNameYouGot[i] == "Recovery80" ||
                                        chipNameYouGot[i] == "Sanctuary" ||
                                        chipNameYouGot[i] == "FinalGun" ||
                                        chipNameYouGot[i] == "JaneRiver" ||
                                        chipNameYouGot[i] == "CherprangRiver")
                                    {
                                        spriteBatch.DrawString(Singleton.Instance._font, chipNameYouGot[i], new Vector2(positionChipImgX[i], 500),
                                        new Color(247, 159, 47), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                                    }
                                    else
                                    {
                                        spriteBatch.DrawString(Singleton.Instance._font, chipNameYouGot[i], new Vector2(positionChipImgX[i], 500),
                                        Color.WhiteSmoke, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                                    }
                                }
                                //drawAlphaChipImg
                                for (int i = 0; i < positionChipImgX.Length; i++)
                                {
                                    spriteBatch.Draw(_texture[0], new Vector2(positionChipImgX[i], 350),
                                         new Rectangle(0, 0, 56, 47),
                                        fadeChipImg, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                                }
                                //drawTextCloseButton
                                spriteBatch.DrawString(Singleton.Instance._font, "Close", new Vector2(825, 580),
                                    backButtonColor, 0f, Vector2.Zero, 1.2f, SpriteEffects.None, 0f);
                            }
                            break;
                    }
                    break;
            }
            base.Draw(spriteBatch);
        }
    }
}
