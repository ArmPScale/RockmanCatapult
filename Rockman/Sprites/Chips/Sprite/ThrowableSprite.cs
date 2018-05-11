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
        public bool drawThrowableObject = false, isDamaged = true;
        public int areaBombRangeY = 0;
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
                isDamaged = true;
                Singleton.Instance.choosePlayerAnimate = "Bomb";
                Singleton.Instance.currentChipCoolDown = 0.4f;
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
                        Velocity = new Vector2(0, 0);
                        Acceleration = new Vector2(100, 100);
                        _throwableCoolDown = 0f;
                        drawThrowableObject = false;
                        Singleton.Instance.useChipNearlySuccess = true;
                    }
                }
                //calculatePanel
                areaBombRangeY = (int)Position.X / (40 * 3) + Singleton.Instance.currentPlayerPoint.Y;

                //checkDamgeRange
                if (areaBombRangeY < 10 && Position.Y >= (TILESIZEY * Singleton.Instance.currentPlayerPoint.X * 2) + (screenStageY - 90) &&
                    Position.Y <= (TILESIZEY * Singleton.Instance.currentPlayerPoint.X * 2) + (screenStageY - 90) + 150)
                {
                    Singleton.Instance.currentChipAtkTime = 0.2f;
                    if (Singleton.Instance.useChipName == "BigBomb" ||
                        Singleton.Instance.useChipName == "DarkBomb" ||
                        Singleton.Instance.useChipName == "BugBomb")
                    {
                        if (Singleton.Instance.currentPlayerPoint.X - 1 >= 0)
                        {
                            if (Singleton.Instance.useChipName == "DarkBomb")
                            {
                                Singleton.Instance.chipEffect[Singleton.Instance.currentPlayerPoint.X - 1, areaBombRangeY - 1] = 4;
                                Singleton.Instance.chipEffect[Singleton.Instance.currentPlayerPoint.X - 1, areaBombRangeY] = 4;
                            }
                            else
                            {
                                Singleton.Instance.chipEffect[Singleton.Instance.currentPlayerPoint.X - 1, areaBombRangeY - 1] = 3;
                                Singleton.Instance.chipEffect[Singleton.Instance.currentPlayerPoint.X - 1, areaBombRangeY] = 3;
                            }
                            Singleton.Instance.spriteHP[Singleton.Instance.currentPlayerPoint.X - 1, areaBombRangeY - 1] -= Singleton.Instance.playerChipAtk;
                            Singleton.Instance.spriteHP[Singleton.Instance.currentPlayerPoint.X - 1, areaBombRangeY] -= Singleton.Instance.playerChipAtk;
                            if (areaBombRangeY + 1 < 10)
                            {
                                if (Singleton.Instance.useChipName == "DarkBomb")
                                {
                                    Singleton.Instance.chipEffect[Singleton.Instance.currentPlayerPoint.X - 1, areaBombRangeY + 1] = 4;
                                }
                                else
                                {
                                    Singleton.Instance.chipEffect[Singleton.Instance.currentPlayerPoint.X - 1, areaBombRangeY + 1] = 3;
                                }
                                Singleton.Instance.spriteHP[Singleton.Instance.currentPlayerPoint.X - 1, areaBombRangeY + 1] -= Singleton.Instance.playerChipAtk;
                            }
                        }
                        if (Singleton.Instance.currentPlayerPoint.X + 1 < 3)
                        {
                            if (Singleton.Instance.useChipName == "DarkBomb")
                            {
                                Singleton.Instance.chipEffect[Singleton.Instance.currentPlayerPoint.X + 1, areaBombRangeY - 1] = 4;
                                Singleton.Instance.chipEffect[Singleton.Instance.currentPlayerPoint.X + 1, areaBombRangeY] = 4;
                            }
                            else
                            {
                                Singleton.Instance.chipEffect[Singleton.Instance.currentPlayerPoint.X + 1, areaBombRangeY - 1] = 3;
                                Singleton.Instance.chipEffect[Singleton.Instance.currentPlayerPoint.X + 1, areaBombRangeY] = 3;
                            }
                            Singleton.Instance.spriteHP[Singleton.Instance.currentPlayerPoint.X + 1, areaBombRangeY - 1] -= Singleton.Instance.playerChipAtk;
                            Singleton.Instance.spriteHP[Singleton.Instance.currentPlayerPoint.X + 1, areaBombRangeY] -= Singleton.Instance.playerChipAtk;
                            if (areaBombRangeY + 1 < 10)
                            {
                                if (Singleton.Instance.useChipName == "DarkBomb")
                                {
                                    Singleton.Instance.chipEffect[Singleton.Instance.currentPlayerPoint.X + 1, areaBombRangeY + 1] = 4;
                                }
                                else
                                {
                                    Singleton.Instance.chipEffect[Singleton.Instance.currentPlayerPoint.X + 1, areaBombRangeY + 1] = 3;
                                }
                                Singleton.Instance.spriteHP[Singleton.Instance.currentPlayerPoint.X + 1, areaBombRangeY + 1] -= Singleton.Instance.playerChipAtk;
                            }
                        }
                        if (Singleton.Instance.useChipName == "BugBomb")
                        {
                            // todo bug bomb
                        }
                    }
                    else if(Singleton.Instance.useChipName == "EnergyBomb" ||
                        Singleton.Instance.useChipName == "MegaEnergyBomb")
                    {
                        SoundEffects["EnergyBomb"].Volume = Singleton.Instance.MasterSFXVolume;
                        SoundEffects["EnergyBomb"].Play();
                        Singleton.Instance.chipEffect[Singleton.Instance.currentPlayerPoint.X, areaBombRangeY] = 3;
                        Singleton.Instance.spriteHP[Singleton.Instance.currentPlayerPoint.X, areaBombRangeY] -= Singleton.Instance.playerChipAtk;
                        Singleton.Instance.spriteHP[Singleton.Instance.currentPlayerPoint.X, areaBombRangeY] -= Singleton.Instance.playerChipAtk;
                    }
                    else if (Singleton.Instance.useChipName == "SearchBomb1" || 
                        Singleton.Instance.useChipName == "SearchBomb2" ||
                        Singleton.Instance.useChipName == "SearchBomb3")
                    {
                        // throw at enemy
                        
                    }
                    else if (Singleton.Instance.useChipName == "CannonBall")
                    {
                        // areaCracked
                        SoundEffects["CannonBall"].Volume = Singleton.Instance.MasterSFXVolume;
                        SoundEffects["CannonBall"].Play();
                        if (Singleton.Instance.spriteMove[Singleton.Instance.currentPlayerPoint.X, areaBombRangeY] > 0)
                        {
                            Singleton.Instance.panelStage[Singleton.Instance.currentPlayerPoint.X, areaBombRangeY] = 1;
                        }
                        else
                        {
                            Singleton.Instance.areaCracked[Singleton.Instance.currentPlayerPoint.X, areaBombRangeY] = 1;
                            Singleton.Instance.panelStage[Singleton.Instance.currentPlayerPoint.X, areaBombRangeY] = 2;
                        }

                    }
                    else if (Singleton.Instance.useChipName == "BlackBomb")
                    {
                        Singleton.Instance.areaCracked[Singleton.Instance.currentPlayerPoint.X, areaBombRangeY] = 1;
                    }
                    //playerChipAtk
                    if (Singleton.Instance.useChipName == "DarkBomb")
                    {
                        Singleton.Instance.chipEffect[Singleton.Instance.currentPlayerPoint.X, areaBombRangeY] = 4;
                    }
                    else if(Singleton.Instance.useChipName != "CannonBall")
                    {
                        Singleton.Instance.chipEffect[Singleton.Instance.currentPlayerPoint.X, areaBombRangeY] = 3;
                    }
                    if (Singleton.Instance.useChipName != "CannonBall" && 
                        Singleton.Instance.useChipName != "EnergyBomb" &&
                        Singleton.Instance.useChipName != "MegaEnergyBomb")
                    {
                        SoundEffects["BombExplosion"].Volume = Singleton.Instance.MasterSFXVolume;
                        SoundEffects["BombExplosion"].Play();
                    }
                    Singleton.Instance.spriteHP[Singleton.Instance.currentPlayerPoint.X, areaBombRangeY] -= Singleton.Instance.playerChipAtk;
                    //checkBombAgain
                    if (Singleton.Instance.useChipName == "BigBomb" ||
                        Singleton.Instance.useChipName == "DarkBomb" ||
                        Singleton.Instance.useChipName == "BugBomb")
                    {
                        if (Singleton.Instance.useChipName == "DarkBomb")
                        {
                            Singleton.Instance.chipEffect[Singleton.Instance.currentPlayerPoint.X, areaBombRangeY - 1] = 4;
                        }
                        else
                        {
                            Singleton.Instance.chipEffect[Singleton.Instance.currentPlayerPoint.X, areaBombRangeY - 1] = 3;
                        }
                        Singleton.Instance.spriteHP[Singleton.Instance.currentPlayerPoint.X, areaBombRangeY - 1] -= Singleton.Instance.playerChipAtk;
                        if (areaBombRangeY + 1 < 10)
                        {
                            if (Singleton.Instance.useChipName == "DarkBomb")
                            {
                                Singleton.Instance.chipEffect[Singleton.Instance.currentPlayerPoint.X, areaBombRangeY + 1] = 4;
                            }
                            else
                            {
                                Singleton.Instance.chipEffect[Singleton.Instance.currentPlayerPoint.X, areaBombRangeY + 1] = 3;
                            }
                            Singleton.Instance.spriteHP[Singleton.Instance.currentPlayerPoint.X, areaBombRangeY + 1] -= Singleton.Instance.playerChipAtk;
                        }
                    }
                    //bombingMe
                    if (isDamaged && Singleton.Instance.currentPlayerPoint.Y == areaBombRangeY)
                    {
                        Singleton.Instance.isDamaged = isDamaged;
                        Singleton.Instance.enemyAtk = Singleton.Instance.playerChipAtk;
                        isDamaged = false;
                    }
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
