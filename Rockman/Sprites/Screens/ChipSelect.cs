using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rockman.Models;


namespace Rockman.Sprites.Screens
{
    class ChipSelect : Screen
    {
        public Point currentTile;
        public Keys W, S, A, D, J, K;
        public int chipLength = Singleton.Instance.chipSelect.Length;

        public ChipSelect(Dictionary<string, Animation> animations) : 
            base(animations)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.GameCustomScreen:

                    switch (CurrentCustomState)
                    {
                        case CustomState.Wait:
                            if (Singleton.Instance.chipSelect[currentTile.X] == 1)
                            {
                                if (Singleton.Instance.CurrentKey.IsKeyDown(A) && Singleton.Instance.PreviousKey.IsKeyUp(A))
                                {
                                    SoundEffects["ChipSelect"].Volume = Singleton.Instance.MasterSFXVolume;
                                    SoundEffects["ChipSelect"].Play();
                                    Singleton.Instance.chipSelect[currentTile.X - 1 < 0 ? currentTile.X + 5 : currentTile.X - 1] = Singleton.Instance.chipSelect[currentTile.X];
                                    Singleton.Instance.chipSelect[currentTile.X] = 0;
                                }
                                else if (Singleton.Instance.CurrentKey.IsKeyDown(D) && Singleton.Instance.PreviousKey.IsKeyUp(D))
                                {
                                    SoundEffects["ChipSelect"].Volume = Singleton.Instance.MasterSFXVolume;
                                    SoundEffects["ChipSelect"].Play();
                                    Singleton.Instance.chipSelect[(currentTile.X + 1) % chipLength] = Singleton.Instance.chipSelect[currentTile.X];
                                    Singleton.Instance.chipSelect[currentTile.X] = 0;
                                }
                                else if (Singleton.Instance.CurrentKey.IsKeyDown(J) && Singleton.Instance.PreviousKey.IsKeyUp(J))
                                {
                                    if (Singleton.Instance.chipSlotIn.Count > 0)
                                    {
                                        SoundEffects["ChipCancel"].Volume = Singleton.Instance.MasterSFXVolume;
                                        SoundEffects["ChipCancel"].Play();
                                        Singleton.Instance.chipStackImg[Singleton.Instance.chipSlotIn.Count - 1] = "";
                                        Singleton.Instance.chipCustomSelect[Singleton.Instance.indexChipSlotIn.Pop()] = Singleton.Instance.chipSlotIn.Pop();
                                    }
                                }
                                else if (Singleton.Instance.CurrentKey.IsKeyDown(K) && Singleton.Instance.PreviousKey.IsKeyUp(K))
                                {
                                    if (currentTile.X == 5)
                                    {
                                        SoundEffects["ChipConfirm"].Volume = Singleton.Instance.MasterSFXVolume;
                                        SoundEffects["ChipConfirm"].Play();
                                        Singleton.Instance.chipSelect[currentTile.X] = 0;
                                        Singleton.Instance.chipSelect[0] = 1;
                                        Singleton.Instance.chipStackImg = new string[6]
                                        {
                                            "","","","","",""
                                        };
                                        if(Singleton.Instance.chipSlotIn.Count != 0)
                                        {
                                            Singleton.Instance.useChipSlotIn.Clear();
                                        }
                                        while(Singleton.Instance.chipSlotIn.Count != 0)
                                        {
                                            Singleton.Instance.useChipSlotIn.Push(Singleton.Instance.chipSlotIn.Pop());
                                        }
                                        Singleton.Instance.indexChipSlotIn.Clear();
                                        Singleton.Instance.selectChipSuccess = true;
                                        setState(CustomState.Close);
                                    }
                                    else
                                    {
                                        if(Singleton.Instance.chipCustomSelect[currentTile.X] != "")
                                        {
                                            SoundEffects["ChipChoose"].Volume = Singleton.Instance.MasterSFXVolume;
                                            SoundEffects["ChipChoose"].Play();
                                            //selectChip
                                            Singleton.Instance.chipSlotIn.Push(Singleton.Instance.chipCustomSelect[currentTile.X]);
                                            Singleton.Instance.indexChipSlotIn.Push(currentTile.X);
                                            Singleton.Instance.chipCustomSelect[currentTile.X] = "";
                                            Singleton.Instance.chipStackImg[Singleton.Instance.chipSlotIn.Count - 1] = Singleton.Instance.chipSlotIn.Peek();
                                        }
                                    }
                                }
                            }
                            break;
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
                    for (int i = 0; i < 6; i++)
                    {
                        if (_animationManager == null)
                        {
                            spriteBatch.Draw(_texture[0],
                                            Position,
                                            Viewport,
                                            Color.White);
                        }
                        else
                        {
                            if (Singleton.Instance.chipSelect[i] == 1)
                            {
                                currentTile = new Point(i);
                                Singleton.Instance.currentChipSelect = new Point(i);
                                if (currentTile.X == 5) _animationManager.Draw(spriteBatch, new Vector2(280, 110 * 3), scale);
                                else _animationManager.Draw(spriteBatch, new Vector2(15+ (48 * currentTile.X), 100 * 3), scale);
                            }
                        }
                    }
                    break;
            }
            base.Draw(spriteBatch);
        }
    }
}
