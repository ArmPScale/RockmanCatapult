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
    class SpreaderSprite : Chip
    {
        private float _spreaderCoolDown = 0;

        public SpreaderSprite(Dictionary<string, Animation> animations)
            : base(animations)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.GamePlaying:
                    if (Singleton.Instance.useChipName == "Spreader" 
                        || Singleton.Instance.useChipName == "DarkSpread")
                    {
                        _spreaderCoolDown += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        _animationManager.Play(_animations[Singleton.Instance.useChipName]);
                        if (_spreaderCoolDown > Singleton.Instance.currentChipCoolDown)
                        {
                            _spreaderCoolDown = 0;
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
                    if (Singleton.Instance.useChipName == "Spreader"
                        || Singleton.Instance.useChipName == "DarkSpread")
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
                            _animationManager.Draw(spriteBatch, new Vector2((TILESIZEX * Singleton.Instance.currentPlayerPoint.Y * 2) + (screenStageX + 95), (TILESIZEY * Singleton.Instance.currentPlayerPoint.X * 2) + (screenStageY - 90)), scale);
                        }
                    }
                    break;
            }
            base.Draw(spriteBatch);
        }
    }
}
