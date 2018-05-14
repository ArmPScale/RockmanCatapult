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
{    class EditScreen : Background
    {
        public bool isClicked = false, isSelectedFolder = false;
        Color backButtonColor = Color.WhiteSmoke;
        private int _positionChipIconImgX = 0, _positionChipIconImgY = 0, _chipCount = 0;
        int rowAllChip = 0, columnAllChip = 0, numAllChip = 0, positionChipFolder = 0;
        string drawFrameName = "", drawChipName = "";
        string drawFrameNameBag = "", drawChipNameBag = "";
        List<string> allChipKeyList;
        Dictionary<string, Rectangle> rectChipFrameImg = new Dictionary<string, Rectangle>()
        {
            {"Black",  new Rectangle(6, 11, 94, 127) },
            {"Standard",  new Rectangle(110, 11, 94, 127) },
            {"Mega",  new Rectangle(214, 11, 94, 127) },
            {"Giga",  new Rectangle(317, 11, 94, 127) },
            {"Dark",  new Rectangle(421, 11, 94, 127) },
            {"Information",  new Rectangle(220, 152, 80, 59) },
            {"ChipScreen",  new Rectangle(0, 0, 656, 576) },
        };
        public EditScreen(Texture2D[] texture)
            : base(texture)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            //stateOfMenu
            switch (Singleton.Instance.CurrentMenuState)
            {
                case Singleton.MenuState.MainMenu:
                    alpha = 255;
                    break;
                case Singleton.MenuState.EditFolderChip:
                    //Escape
                    if (Singleton.Instance.CurrentKey.IsKeyDown(Keys.Escape) && Singleton.Instance.PreviousKey.IsKeyUp(Keys.Escape))
                        Singleton.Instance.CurrentMenuState = Singleton.MenuState.MainMenu;
                    //fadeScreen
                    if (alpha >= 0)
                    {
                        alpha -= 8;
                        fade = new Color(0, 0, 0, alpha);
                    }
                    //checkClick
                    if (alpha <= 0 && Singleton.Instance.PreviousMouse.LeftButton == ButtonState.Released &&
                       Singleton.Instance.CurrentMouse.LeftButton == ButtonState.Pressed)
                        isClicked = true;
                    else isClicked = false;

                    allChipKeyList = new List<string>(Singleton.Instance.allChipDict.Keys);

                    //folderChipButton
                    for (int i = 1; i <= Singleton.Instance.folderList.Count / 5; i++)
                    {
                        for (int j = 1; j <= Singleton.Instance.folderList.Count / 6; j++)
                        {
                            if ((Singleton.Instance.CurrentMouse.X >= 302 + _positionChipIconImgX && Singleton.Instance.CurrentMouse.X <= 302 + _positionChipIconImgX + 42) &&
                            (Singleton.Instance.CurrentMouse.Y >= 302 + _positionChipIconImgY && Singleton.Instance.CurrentMouse.Y <= 302 + _positionChipIconImgY + 42))
                            {
                                if (isClicked)
                                {
                                    drawFrameName = chipClassDict[Singleton.Instance.folderList[_chipCount]];
                                    drawChipName = Singleton.Instance.folderList[_chipCount];
                                    positionChipFolder = _chipCount;
                                    isSelectedFolder = true;
                                }
                            }
                            _positionChipIconImgX += 50;
                            _chipCount += 1;
                        }
                        _positionChipIconImgY += 50; _positionChipIconImgX = 0;
                    }
                    _positionChipIconImgY = 0; _chipCount = 0;

                    //allChipButton
                    if (Singleton.Instance.allChipDict.Count != 0)
                    {
                        numAllChip = Singleton.Instance.allChipDict.Count;
                        rowAllChip = numAllChip / 5;
                        if (numAllChip % 5 == 0)
                        {
                            rowAllChip -= 1;
                        }
                        for (int i = 0; i <= rowAllChip; i++)
                        {
                            if (numAllChip < 5 && numAllChip % 5 != 0)
                            {
                                columnAllChip = numAllChip % 5;
                            }
                            else if (numAllChip >= 5)
                            {
                                columnAllChip = 5;
                                numAllChip -= 5;
                            }
                            for (int j = 1; j <= columnAllChip; j++)
                            {
                                if ((Singleton.Instance.CurrentMouse.X >= 628 + _positionChipIconImgX && Singleton.Instance.CurrentMouse.X <= 628 + _positionChipIconImgX + 42) &&
                                (Singleton.Instance.CurrentMouse.Y >= 302 + _positionChipIconImgY && Singleton.Instance.CurrentMouse.Y <= 302 + _positionChipIconImgY + 42))
                                {
                                    if (isClicked)
                                    {
                                        if (isSelectedFolder)
                                        {
                                            //checkAlreadyHave
                                            if (Singleton.Instance.allChipDict.ContainsKey(drawChipName))
                                            {
                                                Singleton.Instance.folderList[positionChipFolder] = allChipKeyList[_chipCount];
                                                Singleton.Instance.allChipDict[drawChipName] += 1;
                                                if (Singleton.Instance.allChipDict[allChipKeyList[_chipCount]] > 1)
                                                {
                                                    Singleton.Instance.allChipDict[allChipKeyList[_chipCount]] -= 1;
                                                }
                                                else
                                                {
                                                    Singleton.Instance.allChipDict.Remove(allChipKeyList[_chipCount]);
                                                }
                                            }
                                            else
                                            {
                                                Singleton.Instance.folderList[positionChipFolder] = allChipKeyList[_chipCount];
                                                if (Singleton.Instance.allChipDict[allChipKeyList[_chipCount]] > 1)
                                                {
                                                    Singleton.Instance.allChipDict[allChipKeyList[_chipCount]] -= 1;
                                                }
                                                else
                                                Singleton.Instance.allChipDict.Remove(allChipKeyList[_chipCount]);
                                                Singleton.Instance.allChipDict.Add(drawChipName, 1);
                                                allChipKeyList.Add(drawChipName);
                                                Console.WriteLine(Singleton.Instance.allChipDict.Count);
                                            }
                                            isSelectedFolder = false;
                                        }
                                        drawFrameNameBag = chipClassDict[allChipKeyList[_chipCount]];
                                        drawChipNameBag = allChipKeyList[_chipCount];
                                    }
                                }
                                _positionChipIconImgX += 50;
                                _chipCount += 1;
                            }
                            _positionChipIconImgY += 50; _positionChipIconImgX = 0;
                        }
                        _positionChipIconImgY = 0; _chipCount = 0;
                    }

                    //backButton
                    if ((Singleton.Instance.CurrentMouse.X >= 925 && Singleton.Instance.CurrentMouse.X <= 925 + 98) &&
                            (Singleton.Instance.CurrentMouse.Y >= 700 && Singleton.Instance.CurrentMouse.Y <= 700 + 34))
                    {
                        backButtonColor = new Color(247, 159, 47);
                        if (isClicked)
                        {
                            isSelectedFolder = false;
                            backButtonColor = Color.WhiteSmoke;
                            Singleton.Instance.CurrentMenuState = Singleton.MenuState.MainMenu;
                        }
                    }
                    else
                    {
                        backButtonColor = Color.WhiteSmoke;
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
                        case Singleton.MenuState.EditFolderChip:
                            //drawTextBackButton
                            spriteBatch.DrawString(Singleton.Instance._font, "Back", new Vector2(940, 700),
                                backButtonColor, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);

                            //drawChipFrameLeft
                            if (drawFrameName != "")
                            {
                                spriteBatch.Draw(_texture[2], new Vector2(17, 149), rectChipFrameImg[drawFrameName],
                                Color.White, 0f, Vector2.Zero, 2.5f, SpriteEffects.None, 0f);
                                spriteBatch.Draw(_texture[5], new Vector2(42, 162), rectChipImg[drawChipName],
                               Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                                spriteBatch.DrawString(Singleton.Instance._font, drawChipName, new Vector2(34, 332),
                                Color.WhiteSmoke, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                            }
                            else
                            {
                                spriteBatch.Draw(_texture[2], new Vector2(17, 149), rectChipFrameImg["Black"],
                                Color.White, 0f, Vector2.Zero, 2.5f, SpriteEffects.None, 0f);
                            }
                            //drawInformationLeft
                            spriteBatch.Draw(_texture[2], new Vector2(17, 476), rectChipFrameImg["Information"],
                                Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);

                            //drawChipFrameRight
                            if (drawFrameNameBag != "")
                            {
                                spriteBatch.Draw(_texture[2], new Vector2(943, 149), rectChipFrameImg[drawFrameNameBag],
                                Color.White, 0f, Vector2.Zero, 2.5f, SpriteEffects.None, 0f);
                                if (drawChipNameBag == "JaneRiver" ||
                                    drawChipNameBag == "CherprangRiver")
                                {
                                    spriteBatch.Draw(_texture[6], new Vector2(968, 162), rectChipRiverImg[drawChipNameBag],
                                Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                                }
                                else
                                {
                                    spriteBatch.Draw(_texture[5], new Vector2(968, 162), rectChipImg[drawChipNameBag],
                                Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                                }
                                
                                spriteBatch.DrawString(Singleton.Instance._font, drawChipNameBag, new Vector2(960, 332),
                                Color.WhiteSmoke, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                            }
                            else
                            {
                                spriteBatch.Draw(_texture[2], new Vector2(943, 149), rectChipFrameImg["Black"],
                                Color.White, 0f, Vector2.Zero, 2.5f, SpriteEffects.None, 0f);
                            }
                            //drawInformationRight
                            spriteBatch.Draw(_texture[2], new Vector2(943, 476), rectChipFrameImg["Information"],
                                Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);

                            //drawChipScreen
                            spriteBatch.Draw(_texture[1], new Vector2(272, 149), rectChipFrameImg["ChipScreen"],
                                Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                            //drawTextEdit
                            spriteBatch.DrawString(Singleton.Instance._font, "Edit", new Vector2(325, 165),
                                Color.WhiteSmoke, 0f, Vector2.Zero, 1.2f, SpriteEffects.None, 0f);
                            //drawTextFolder
                            spriteBatch.DrawString(Singleton.Instance._font, "Folder", new Vector2(350, 225),
                                Color.WhiteSmoke, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                            //drawTextBag
                            spriteBatch.DrawString(Singleton.Instance._font, "Bag", new Vector2(725, 225),
                                Color.WhiteSmoke, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);

                            //drawChipIconFolder
                            for (int i = 1;i<= Singleton.Instance.folderList.Count/5; i++)
                            {
                                for (int j = 1; j <= Singleton.Instance.folderList.Count / 6; j++)
                                {
                                    if (rectChipIconEXE4Img.ContainsKey(Singleton.Instance.folderList[_chipCount]))
                                    {
                                        spriteBatch.Draw(_texture[4], new Vector2(302 + _positionChipIconImgX, 302 + _positionChipIconImgY),
                                        rectChipIconEXE4Img[Singleton.Instance.folderList[_chipCount]],
                                        Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                                    }
                                    else
                                    {
                                        spriteBatch.Draw(_texture[3], new Vector2(302 + _positionChipIconImgX, 302 + _positionChipIconImgY),
                                        rectChipIconEXE6Img[Singleton.Instance.folderList[_chipCount]],
                                        Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                                    }
                                    _positionChipIconImgX += 50;
                                    _chipCount += 1;
                                }
                                _positionChipIconImgY += 50; _positionChipIconImgX = 0;
                            }
                            _positionChipIconImgY = 0; _chipCount = 0;
                            
                            //drawChipIconBag
                            if (Singleton.Instance.allChipDict.Count != 0)
                            {
                                numAllChip = Singleton.Instance.allChipDict.Count;
                                rowAllChip = numAllChip / 5;
                                if(numAllChip % 5 == 0)
                                {
                                    rowAllChip -= 1;
                                }
                                for (int i = 0; i <= rowAllChip; i++)
                                {
                                    if(numAllChip < 5 && numAllChip % 5 != 0)
                                    {
                                        columnAllChip = numAllChip % 5;
                                    }
                                    else if(numAllChip >= 5)
                                    {
                                        columnAllChip = 5;
                                        numAllChip -= 5;
                                    }
                                    for (int j = 1; j <= columnAllChip; j++)
                                    {
                                        if (rectChipIconEXE4Img.ContainsKey(allChipKeyList[_chipCount]))
                                        {
                                            spriteBatch.Draw(_texture[4], new Vector2(628 + _positionChipIconImgX, 302 + _positionChipIconImgY),
                                            rectChipIconEXE4Img[allChipKeyList[_chipCount]],
                                            Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                                        }
                                        else
                                        {
                                            spriteBatch.Draw(_texture[3], new Vector2(628 + _positionChipIconImgX, 302 + _positionChipIconImgY),
                                            rectChipIconEXE6Img[allChipKeyList[_chipCount]],
                                            Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                                        }

                                        if (Singleton.Instance.allChipDict.ContainsKey(drawChipNameBag) && drawChipNameBag != "")
                                        {
                                            // error
                                            //drawTextCount
                                            spriteBatch.DrawString(Singleton.Instance._font, string.Format("x {0}",
                                                Singleton.Instance.allChipDict[drawChipNameBag]),
                                                new Vector2(1090, 382), Color.WhiteSmoke, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                                        }
                                        _positionChipIconImgX += 50;
                                        _chipCount += 1;
                                    }
                                    _positionChipIconImgY += 50; _positionChipIconImgX = 0;
                                }
                                _positionChipIconImgY = 0; _chipCount = 0;
                            }
                            
                            //drawFadeBlack
                            spriteBatch.Draw(_texture[0], new Vector2(0, 0), null, fade, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                            break;
                    }
                    break;
            }
            base.Draw(spriteBatch);
        }
    }
}
