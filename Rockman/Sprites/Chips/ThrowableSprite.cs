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
    class ThrowableSprite : Chip
    {
        private float _throwableCoolDown = 0f;
        public bool drawThrowableObject = false;
        public int projectX = 0, projectY = 0;

        public ThrowableSprite(Dictionary<string, Animation> animations)
            : base(animations)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            if (Singleton.Instance.useThrowableChip)
            {
                SoundEffects["Throw"].Volume = Singleton.Instance.MasterSFXVolume;
                SoundEffects["Throw"].Play();
                _animationManager.Play(_animations[Singleton.Instance.useChipName]);
                Singleton.Instance.useThrowableChip = false;
                drawThrowableObject = true;
            }
            if (drawThrowableObject)
            {
                _throwableCoolDown += (float)gameTime.ElapsedGameTime.TotalSeconds;
                //if (_throwableCoolDown > 0.1f)
                //{
                    projectX += 2;
                    projectY -= 2;
                //}
                if (_throwableCoolDown > 3f)
                {
                    projectX = 0; projectY = 0;
                    _throwableCoolDown = 0f;
                    drawThrowableObject = false;
                    Singleton.Instance.useChipNearlySuccess = true;
                }
            }
            _animationManager.Update(gameTime);
            base.Update(gameTime, sprites);
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.GameUseChip:
                    if (drawThrowableObject)
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
                            _animationManager.Draw(spriteBatch, 
                                new Vector2((TILESIZEX * Singleton.Instance.currentPlayerPoint.Y * 2) + (screenStageX + 95) + projectX,
                                    (TILESIZEY * Singleton.Instance.currentPlayerPoint.X * 2) + (screenStageY - 90) + projectY), 
                                scale);
                        }
                    }
                    break;
            }
            base.Draw(spriteBatch);
        }
    }
}
