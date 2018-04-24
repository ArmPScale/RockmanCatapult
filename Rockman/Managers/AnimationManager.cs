﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rockman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rockman.Managers
{
    public class AnimationManager
    {
        private Animation _animation;
        private float _timer;
        public float TimePlayed;

        public AnimationManager(Animation animation)
        {
            _animation = animation;
        }

        public void Play(Animation animation)
        {
            if (_animation.Equals(animation))
                return;

            _animation = animation;
            _animation.CurrentFrame = 0;
            _timer = 0;
            TimePlayed = 0;
        }

        public void Stop()
        {
            _timer = 0f;

            _animation.CurrentFrame = 0;
        }

        public void Update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;
            TimePlayed += (float)gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;

            if (_timer > _animation.FrameSpeed)
            {
                _timer = 0f;
                _animation.CurrentFrame++;

                if (_animation.CurrentFrame >= _animation.FrameCount)
                {
                    _animation.CurrentFrame = 0;
                }

            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, float scale)
        {
            spriteBatch.Draw(_animation.Texture,
                            position,
                            new Rectangle(_animation.AnimationRectangle.X + _animation.CurrentFrame * _animation.FrameWidth,
                                          _animation.AnimationRectangle.Y,
                                          _animation.FrameWidth,
                                          _animation.FrameHeight),
                            Color.White,0f,Vector2.Zero,scale,SpriteEffects.None,0f);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, float scale, SpriteEffects spriteEffects)
        {
            spriteBatch.Draw(_animation.Texture,
                            position,
                            new Rectangle(_animation.AnimationRectangle.X + _animation.CurrentFrame * _animation.FrameWidth,
                                          _animation.AnimationRectangle.Y,
                                          _animation.FrameWidth,
                                          _animation.FrameHeight),
                            Color.White, 0f, Vector2.Zero, scale, spriteEffects, 0f);
        }
    }
}
