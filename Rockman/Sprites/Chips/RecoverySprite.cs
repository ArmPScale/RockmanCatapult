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
    class RecoverySprite : Chip
    {
        private float _recoverCoolDown = 0;
        Dictionary<string, Rectangle> rectChipRecovImg = new Dictionary<string, Rectangle>()
        {
            {"Recovery10",  new Rectangle(0, 145, 56, 47) },
            {"Recovery30",  new Rectangle(56, 145, 56, 47) },
            {"Recovery50",  new Rectangle(56*2, 145, 56, 47) },
            {"Recovery80",  new Rectangle(56*3, 145, 56, 47) },
            {"Recovery120",  new Rectangle(56*4, 145, 56, 47) },
            {"Recovery150",  new Rectangle(56*5, 145, 56, 47) },
            {"Recovery200",  new Rectangle(56*6, 145, 56, 47) },
            {"Recovery300",  new Rectangle(56*7, 145, 56, 47) },
            {"DarkRecovery",  new Rectangle(168, 336, 56, 47) },
        };

        public RecoverySprite(Dictionary<string, Animation> animations)
            : base(animations)
        {
            _animations = animations;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.GamePlaying:
                    if (Singleton.Instance.isRecovered)
                    {
                        _recoverCoolDown += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        _animationManager.Play(_animations["Recovery"]);
                        if (_recoverCoolDown > 0.4f)
                        {
                            _recoverCoolDown = 0;
                            Singleton.Instance.isRecovered = false;
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
                    if (Singleton.Instance.isRecovered)
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

                            _animationManager.Draw(spriteBatch, new Vector2((TILESIZEX * Singleton.Instance.currentPlayerPoint.Y * 2) + (screenStageX + 10), (TILESIZEY * Singleton.Instance.currentPlayerPoint.X * 2) + (screenStageY - 150)), scale);

                        }
                    }
                    break;
            }
            base.Draw(spriteBatch);
        }
    }
}
