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
            //if ((currentTile.X > 0 && Singleton.Instance.panelBoundary[currentTile.X - 1, currentTile.Y] == 0 &&
            //            Singleton.Instance.panelStage[currentTile.X - 1, currentTile.Y] <= 1) &&
            //            (Singleton.Instance.CurrentKey.IsKeyDown(W) && Singleton.Instance.PreviousKey.IsKeyUp(W)))
            //{
            //    Singleton.Instance.spriteMove[currentTile.X - 1, currentTile.Y] = Singleton.Instance.spriteMove[currentTile.X, currentTile.Y];
            //    Singleton.Instance.spriteMove[currentTile.X, currentTile.Y] = 0;
            //}
            //else if ((currentTile.X < 2 && Singleton.Instance.panelBoundary[currentTile.X + 1, currentTile.Y] == 0 &&
            //    Singleton.Instance.panelStage[currentTile.X + 1, currentTile.Y] <= 1) &&
            //    (Singleton.Instance.CurrentKey.IsKeyDown(S) && Singleton.Instance.PreviousKey.IsKeyUp(S)))
            //{
            //    Singleton.Instance.spriteMove[currentTile.X + 1, currentTile.Y] = Singleton.Instance.spriteMove[currentTile.X, currentTile.Y];
            //    Singleton.Instance.spriteMove[currentTile.X, currentTile.Y] = 0;
            //}


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
                                    Singleton.Instance.chipSelect[(chipLength - 1) - currentTile.X] = Singleton.Instance.chipSelect[currentTile.X];
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
                                    SoundEffects["ChipCancel"].Volume = Singleton.Instance.MasterSFXVolume;
                                    SoundEffects["ChipCancel"].Play();
                                }
                                else if (Singleton.Instance.CurrentKey.IsKeyDown(K) && Singleton.Instance.PreviousKey.IsKeyUp(K))
                                {
                                    if (currentTile.X == 5)
                                    {
                                        SoundEffects["ChipConfirm"].Volume = Singleton.Instance.MasterSFXVolume;
                                        SoundEffects["ChipConfirm"].Play();
                                        setState(CustomState.Close);
                                    }
                                    else
                                    {
                                        SoundEffects["ChipChoose"].Volume = Singleton.Instance.MasterSFXVolume;
                                        SoundEffects["ChipChoose"].Play();
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
                                if (currentTile.X == 5) _animationManager.Draw(spriteBatch, new Vector2(280, 110 * 3), scale);
                                else _animationManager.Draw(spriteBatch, new Vector2(15 * i, 100 * 3), scale);
                            }
                        }
                    }
                    break;
            }
            base.Draw(spriteBatch);
        }
    }
}
