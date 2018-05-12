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
    class AirShotSprite : Chip
    {
        private float _airShotCoolDown = 0;

        public AirShotSprite(Dictionary<string, Animation> animations)
            : base(animations)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.GamePlaying:
                    if (Singleton.Instance.useChipName == "AirShot")
                    {
                        _airShotCoolDown += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        _animationManager.Play(_animations["AirShot"]);
                        if (_airShotCoolDown > Singleton.Instance.currentChipCoolDown)
                        {
                            _airShotCoolDown = 0;
                            Singleton.Instance.useChipName = "";
                        }
                    }
                    _animationManager.Update(gameTime);
                    break;
                case Singleton.GameState.GameClear:
                    _airShotCoolDown += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (_airShotCoolDown > Singleton.Instance.currentChipCoolDown)
                    {
                        _airShotCoolDown = 0;
                        Singleton.Instance.useChipName = "";
                    }
                    _animationManager.Update(gameTime);
                    break;
                case Singleton.GameState.GameOver:
                    _airShotCoolDown = 0;
                    break;
            }
            base.Update(gameTime, sprites);
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            //switch (Singleton.Instance.CurrentGameState)
            //{
            //    case Singleton.GameState.GamePlaying:
                    if (_animationManager == null)
                    {
                        spriteBatch.Draw(_texture[0],
                                        Position,
                                        Viewport,
                                        Color.White);
                    }
                    else
                    {
                        if (Singleton.Instance.useChipName == "AirShot")
                        {
                            _animationManager.Draw(spriteBatch, new Vector2((TILESIZEX * Singleton.Instance.currentPlayerPoint.Y * 2) + (screenStageX + 95), (TILESIZEY * Singleton.Instance.currentPlayerPoint.X * 2) + (screenStageY - 100)), scale);
                        }
                    }
                    //break;
            //}
            base.Draw(spriteBatch);
        }
    }
}
