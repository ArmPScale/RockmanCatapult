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
    class BEGalaxySprite : Chip
    {
        private float _blackHoleCoolDown = 0;
        private Point currentPoint;
        private int currentPlayerNumber = 0;
        public BEGalaxySprite(Dictionary<string, Animation> animations)
            : base(animations)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.GameUseChip:
                    if (Singleton.Instance.useChipName == "BlackEndGalaxy")
                    {
                        _blackHoleCoolDown += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (_blackHoleCoolDown < 0.1f)
                        {
                            currentPoint = Singleton.Instance.currentPlayerPoint;
                            currentPlayerNumber = Singleton.Instance.playerMove[Singleton.Instance.currentPlayerPoint.X,Singleton.Instance.currentPlayerPoint.Y];
                            Singleton.Instance.choosePlayerAnimate = "Buster";
                            _animationManager.Play(_animations["Matter"]);
                            SoundEffects["Matter"].Volume = Singleton.Instance.MasterSFXVolume;
                            SoundEffects["Matter"].Play();
                        }
                        else if (_blackHoleCoolDown > 1.3f && _blackHoleCoolDown < 1.4f)
                        {
                            Singleton.Instance.blackScreen = true;
                            Singleton.Instance.choosePlayerAnimate = "Alive";
                        }
                        else if (_blackHoleCoolDown > 1.4f && _blackHoleCoolDown < 1.5f)
                        {
                            Singleton.Instance.blackScreen = false;
                            _animationManager.Play(_animations["BlackHole"]);
                            SoundEffects["BlackHole"].Volume = Singleton.Instance.MasterSFXVolume;
                            SoundEffects["BlackHole"].Play();
                        }
                        else if (_blackHoleCoolDown > 1.8f && _blackHoleCoolDown < 1.9f)
                        {
                            SoundEffects["BlackEnd!"].Volume = Singleton.Instance.MasterSFXVolume;
                            SoundEffects["BlackEnd!"].Play();
                        }
                        else if (_blackHoleCoolDown > 3f && _blackHoleCoolDown < 3.3f)
                        {
                            SoundEffects["Galaxy!"].Volume = Singleton.Instance.MasterSFXVolume;
                            SoundEffects["Galaxy!"].Play();
                            Singleton.Instance.playerMove[Singleton.Instance.currentPlayerPoint.X, Singleton.Instance.currentPlayerPoint.Y] = 0;
                            Singleton.Instance.playerMove[Singleton.Instance.currentPlayerPoint.X, 9] = currentPlayerNumber;
                            Singleton.Instance.whiteScreen = true;
                            Singleton.Instance.choosePlayerAnimate = "Sword";
                            SoundEffects["BlackAceSword"].Volume = Singleton.Instance.MasterSFXVolume;
                            SoundEffects["BlackAceSword"].Play();
                        }
                        else if (_blackHoleCoolDown > 3.3f && _blackHoleCoolDown < 3.4f)
                        {
                            Singleton.Instance.whiteScreen = false;
                            Singleton.Instance.choosePlayerAnimate = "SwordLast";
                        }
                        else if (_blackHoleCoolDown > 4f && _blackHoleCoolDown < 4.1f)
                        {
                            Singleton.Instance.choosePlayerAnimate = "Alive";
                            SoundEffects["Shining"].Volume = Singleton.Instance.MasterSFXVolume;
                            SoundEffects["Shining"].Play();
                        }
                        else if (_blackHoleCoolDown > 5.0f && _blackHoleCoolDown < 5.1f)
                        {
                            SoundEffects["Dimension"].Volume = Singleton.Instance.MasterSFXVolume;
                            SoundEffects["Dimension"].Play();
                        }
                        else if (_blackHoleCoolDown > 6.0f && _blackHoleCoolDown < 6.1f)
                        {
                            SoundEffects["Explode"].Volume = Singleton.Instance.MasterSFXVolume;
                            SoundEffects["Explode"].Play();
                        }
                        else if (_blackHoleCoolDown > 7.0f)
                        {
                            Singleton.Instance.playerMove[Singleton.Instance.currentPlayerPoint.X, 9] = 0;
                            Singleton.Instance.playerMove[currentPoint.X, currentPoint.Y] = currentPlayerNumber;
                            _blackHoleCoolDown = 0;
                            Rotation = 0f;
                            Singleton.Instance.useChipName = "";
                            Singleton.Instance.useChipNearlySuccess = true;
                        }
                    }
                    _animationManager.Update(gameTime);
                    break;
            }
            base.Update(gameTime, sprites);
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.GameUseChip:
                    if (Singleton.Instance.useChipName == "BlackEndGalaxy")
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
                            if (Singleton.Instance.choosePlayerAnimate == "Buster")
                            {
                                _animationManager.Draw(spriteBatch,
                               new Vector2((TILESIZEX * Singleton.Instance.currentPlayerPoint.Y * 2) + (screenStageX + 115),
                               (TILESIZEY * Singleton.Instance.currentPlayerPoint.X * 2) + (screenStageY - 75)), 1f);
                            }
                            else if (_blackHoleCoolDown > 1.4f)
                            {
                                Rotation += 0.001f;
                                _animationManager.Draw(spriteBatch,
                               new Vector2((TILESIZEX * 6 * 2) + (screenStageX + 180),
                               (TILESIZEY * 3 * 2) + (screenStageY - 150)), scale, Rotation,
                               new Vector2(126 / 2f, 123 / 2f));
                            }
                        }
                    }
                    break;
            }
            base.Draw(spriteBatch);
        }
    }
}
