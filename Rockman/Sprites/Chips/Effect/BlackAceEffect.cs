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
    class BlackAceEffect : Chip
    {
        private float _timerChange = 0f;
        public bool drawThrowableObject = false;

        public BlackAceEffect(Dictionary<string, Animation> animations)
            : base(animations)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.GameUseChip:
                    if (Singleton.Instance.useChipSlotIn.Count != 0 &&
                        Singleton.Instance.useChipSlotIn.Peek() == "BlackAce")
                    {
                        _timerChange += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (_timerChange < 0.07f)
                        {
                            _animationManager.Play(_animations["Changing"]);
                        }
                        else if (_timerChange > 0.07f)
                        {
                            _animationManager.Play(_animations["Changed"]);
                            if (_timerChange > 0.1f)
                            {
                                _timerChange = 0f;
                            }
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
                    if (_animationManager == null)
                    {
                        spriteBatch.Draw(_texture[0],
                                        Position,
                                        Viewport,
                                        Color.White);
                    }
                    else
                    {
                        if (Singleton.Instance.useChipSlotIn.Count != 0 &&
                            Singleton.Instance.useChipSlotIn.Peek() == "BlackAce")
                        {
                            _animationManager.Draw(spriteBatch,
                            new Vector2((TILESIZEX * Singleton.Instance.currentPlayerPoint.Y * 2) + (screenStageX - 50),
                                (TILESIZEY * Singleton.Instance.currentPlayerPoint.X * 2) + (screenStageY - 170)),
                            scale);
                        }
                    }
                    break;
            }
            base.Draw(spriteBatch);
        }
    }
}
