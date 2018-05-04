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
    class CannonSprite : Chip
    {
        private float _cannonCoolDown = 0;

        public CannonSprite(Dictionary<string, Animation> animations)
            : base(animations)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.GamePlaying:
                    if (Singleton.Instance.useChipName == "Cannon"
                    || Singleton.Instance.useChipName == "HiCannon"
                    || Singleton.Instance.useChipName == "MegaCannon"
                    || Singleton.Instance.useChipName == "DarkCannon")
                    {
                        _cannonCoolDown += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        _animationManager.Play(_animations[Singleton.Instance.useChipName]);
                        if (_cannonCoolDown > Singleton.Instance.currentChipCoolDown)
                        {
                            _cannonCoolDown = 0;
                            Singleton.Instance.useChipName = "";
                        }
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
                    if (Singleton.Instance.useChipName == "Cannon"
                        || Singleton.Instance.useChipName == "HiCannon"
                        || Singleton.Instance.useChipName == "MegaCannon"
                        || Singleton.Instance.useChipName == "DarkCannon")
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
                            _animationManager.Draw(spriteBatch, new Vector2((TILESIZEX * Singleton.Instance.currentPlayerPoint.Y * 2) + (screenStageX + 95), (TILESIZEY * Singleton.Instance.currentPlayerPoint.X * 2) + (screenStageY - 130)), scale);
                        }
                    }
                    break;
            }
            base.Draw(spriteBatch);
        }
    }
}
