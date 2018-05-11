using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rockman.Models;
using Rockman.Sprites.Chips;

namespace Rockman.Sprites.Screens
{
    class CustomBomb : Screen
    {
        private Rectangle rectBar = new Rectangle(424, 31, 0, 18);
        private Rectangle rectPushButton = new Rectangle(370, 80, 20 * 3, 27 * 3);
        private Rectangle ViewportButton = new Rectangle(0, 0, 20, 27);
        private string descripBomb = "";
        bool isNotPlayed = true, isThrown = false;

        public CustomBomb(Texture2D[] texture) :
            base(texture)
        {
            _texture = texture;
        }

        public CustomBomb(Dictionary<string, Animation> animations) :
            base(animations)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.GamePlaying:
                    rectBar.Width = 0;
                    Viewport = new Rectangle(15, 21, 5, 5);
                    isNotPlayed = true;
                    isThrown = false;
                    break;
                case Singleton.GameState.GameUseChip:
                    if (Singleton.Instance.isCustomBomb && 
                        Singleton.Instance.choosePlayerAnimate == "BombPrepare")
                    {
                        if(Chip.Velocity.X >= 2316)
                        {
                            if (isNotPlayed)
                            {
                                SoundEffects["FullCustom"].Volume = Singleton.Instance.MasterSFXVolume;
                                SoundEffects["FullCustom"].Play();
                                isNotPlayed = false;
                            }
                        }
                        else
                        {
                            rectBar.Width += 2;
                        }
                        isThrown = true;
                    }
                    break;
            }
            base.Update(gameTime, sprites);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.GameUseChip:
                    if (_animationManager == null)
                    {
                        if (Singleton.Instance.isCustomBomb)
                        {
                            if (Singleton.Instance.choosePlayerAnimate == "BombPrepare")
                            {
                                //drawBar
                                if (rectBar.Width > 50 && rectBar.Width <= 200)
                                {
                                    Viewport = new Rectangle(30, 21, 5, 5);
                                }
                                else if (rectBar.Width > 200 && rectBar.Width <= 300)
                                {
                                    Viewport = new Rectangle(45, 21, 5, 5);
                                }
                                else if (rectBar.Width > 300)
                                {
                                    Viewport = new Rectangle(60, 21, 5, 5);
                                }
                                //drawButton
                                ViewportButton = new Rectangle(54, 0, 20, 27);
                                descripBomb = "Charging";
                            }
                            else if (Singleton.Instance.choosePlayerAnimate == "Bomb")
                            {
                                //drawButton
                                ViewportButton = new Rectangle(0, 0, 20, 27);
                                descripBomb = "Throwing Bomb";
                            }
                            else if (!isThrown)
                            {
                                //drawButton
                                ViewportButton = new Rectangle(0, 0, 20, 27);
                                descripBomb = "Hold K Button to Charge";
                            }
                            //drawBar
                            spriteBatch.Draw(_texture[5], rectBar, Viewport, Color.White);
                            //drawButton
                            spriteBatch.Draw(_texture[6], rectPushButton, ViewportButton, Color.White);
                            //drawDescripBomb
                            spriteBatch.DrawString(Singleton.Instance._font, descripBomb,
                                   new Vector2(450, 125), Color.WhiteSmoke, 0f, Vector2.Zero, 1.2f, SpriteEffects.None, 0f);
                        }
                    }
                    else
                    {
                        if(Singleton.Instance.isCustomBomb)
                            _animationManager.Draw(spriteBatch, Position, scale);
                    }
                    break;
            }
            base.Draw(spriteBatch);
        }
    }
}
