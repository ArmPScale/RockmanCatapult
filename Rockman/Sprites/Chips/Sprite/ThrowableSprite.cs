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
                Singleton.Instance.choosePlayerAnimate = "Bomb";
                Singleton.Instance.useThrowableChip = false;
                drawThrowableObject = true;
            }
            if (drawThrowableObject)
            {
                _throwableCoolDown += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (_throwableCoolDown > 0.1f)
                {
                    //projectile
                    Acceleration.Y += GRAVITY;
                    Velocity += Acceleration * gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;
                    Position += Velocity * gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;
                    if (_throwableCoolDown > 0.5f)
                    {
                        Singleton.Instance.choosePlayerAnimate = "Alive";
                    }
                    if (_throwableCoolDown > 2f)
                    {
                        Position = new Vector2(0, 0);
                        Velocity = new Vector2(1200, -2000);
                        Acceleration = new Vector2(100, 100);
                        _throwableCoolDown = 0f;
                        drawThrowableObject = false;
                        Singleton.Instance.useChipNearlySuccess = true;
                    }
                }
                
                //checkDamgeRange
                if (Position.Y >= (TILESIZEY * Singleton.Instance.currentPlayerPoint.X * 2) + (screenStageY - 90) &&
                    Position.Y <= (TILESIZEY * Singleton.Instance.currentPlayerPoint.X * 2) + (screenStageY - 90) + 100)
                {
                    Singleton.Instance.spriteHP[Singleton.Instance.currentPlayerPoint.X, Singleton.Instance.currentPlayerPoint.Y + 5] -= Singleton.Instance.playerChipAtk;
                    Console.WriteLine(Position);
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
                            if ((TILESIZEY * Singleton.Instance.currentPlayerPoint.X * 2) +(screenStageY - 90) + Position.Y
                                < Singleton.HEIGHT - 250)
                            {
                                _animationManager.Draw(spriteBatch,
                                new Vector2((TILESIZEX * Singleton.Instance.currentPlayerPoint.Y * 2) + (screenStageX + 95) + Position.X,
                                    (TILESIZEY * Singleton.Instance.currentPlayerPoint.X * 2) + (screenStageY - 90) + Position.Y),
                                scale);
                            }
                                
                        }
                    }
                    break;
            }
            base.Draw(spriteBatch);
        }
    }
}
