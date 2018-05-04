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
    class Buster : Chip
    {
        public Buster(Dictionary<string, Animation> animations) 
            : base(animations)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.GamePlaying:
                    switch (Singleton.Instance.CurrentPlayerState)
                    {
                        case Singleton.PlayerState.BusterShot:
                            _animationManager.Play(_animations["NormalBuster"]);
                            break;
                    }
                    break;
            }
            _animationManager.Update(gameTime);
            base.Update(gameTime, sprites);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.GamePlaying:
                    switch (Singleton.Instance.CurrentPlayerState)
                    {
                        case Singleton.PlayerState.BusterShot:
                            if (_animationManager == null)
                            {
                                spriteBatch.Draw(_texture[0],
                                                Position,
                                                Viewport,
                                                Color.White);
                            }
                            else
                            {
                                _animationManager.Draw(spriteBatch,
                                    new Vector2((TILESIZEX * Singleton.Instance.currentPlayerPoint.Y * 2) + (screenStageX + 95), (TILESIZEY * Singleton.Instance.currentPlayerPoint.X * 2) + (screenStageY - 50)), 
                                    scale);
                            }
                            break;
                    }
                    break;
            }
            base.Draw(spriteBatch);
        }
    }
}
