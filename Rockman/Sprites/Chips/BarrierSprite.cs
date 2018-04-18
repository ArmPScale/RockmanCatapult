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
    class BarrierSprite : Chip
    {
        public BarrierSprite(Dictionary<string, Animation> animations)
            : base(animations)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.GamePlaying:

                    _animationManager.Update(gameTime);
                    break;
                case Singleton.GameState.GameUseChip:
                    if (Singleton.Instance.HeroBarrier > 0)
                    {
                        if (Singleton.Instance.useChipName == "Barrier10") _animationManager.Play(_animations["Barrier10"]);
                        else if (Singleton.Instance.useChipName == "Barrier100") _animationManager.Play(_animations["Barrier100"]);
                        else if (Singleton.Instance.useChipName == "Barrier200") _animationManager.Play(_animations["Barrier200"]);
                    }
                    else if (Singleton.Instance.HeroAura > 0)
                    {
                        if (Singleton.Instance.useChipName == "DreamAura") _animationManager.Play(_animations["DreamAura"]);
                    }
                    _animationManager.Update(gameTime);
                    break;
            }
            base.Update(gameTime, sprites);
        }

        public override void Draw(SpriteBatch spriteBatch)
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
                if (Singleton.Instance.HeroBarrier > 0)
                {
                    _animationManager.Draw(spriteBatch, new Vector2((TILESIZEX * Singleton.Instance.currentPlayerPoint.Y * 2) + (screenStageX - 30), (TILESIZEY * Singleton.Instance.currentPlayerPoint.X * 2) + (screenStageY - 110)), scale);
                }
                else if (Singleton.Instance.HeroAura > 0)
                {
                    _animationManager.Draw(spriteBatch, new Vector2((TILESIZEX * Singleton.Instance.currentPlayerPoint.Y * 2) + (screenStageX - 30), (TILESIZEY * Singleton.Instance.currentPlayerPoint.X * 2) + (screenStageY - 125)), scale);
                }
            }
            base.Draw(spriteBatch);
        }
    }
}
