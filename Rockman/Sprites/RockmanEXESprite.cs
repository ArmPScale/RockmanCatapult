using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Rockman.Models;

namespace Rockman.Sprites
{
    class RockmanEXESprite : Sprite
    {
        const float CHARGING = 1.2f ,CHARGED = 3.2f;
        private float _chargeTime, _busterCoolDown;
        public int HP, Attack, Barrier;
        public Point currentTile, busterDamagedPosition;
        public Keys W, S, A, D, J, K, U;
        float delay = 50f, drawChargeTime;
        int chargeFrames = 0;

        Rectangle destRectCharge, sourceRectCharge;
        public bool busterAttacked;

        public RockmanEXESprite(Dictionary<string, Animation> animations) : base(animations)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            HP = Singleton.Instance.HeroHP;
            Attack = Singleton.Instance.HeroAttack;
            Barrier = Singleton.Instance.HeroBarrier;
            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.GameCustomScreen:
                    //_animationManager.Play(_animations["Alive"]);
                    break;
                case Singleton.GameState.GamePlaying:
                    _chargeTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    drawChargeTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    if (Singleton.Instance.spriteMove[currentTile.X, currentTile.Y] == 1)
                    {
                        switch (Singleton.Instance.CurrentPlayerState)
                        {
                            case Singleton.PlayerState.Playing:
                                _animationManager.Play(_animations["Alive"]);
                                if (Singleton.Instance.isDamaged)
                                {
                                    if (Singleton.Instance.HeroBarrier > 0)
                                    {
                                        Singleton.Instance.HeroBarrier -= Singleton.Instance.enemyAtk;
                                    }
                                    else if (Singleton.Instance.HeroAura > 0)
                                    {
                                        if (Singleton.Instance.enemyAtk >= Singleton.Instance.HeroAura) Singleton.Instance.HeroAura = 0;
                                    }
                                    else
                                    {
                                        SoundEffects["Damaged"].Volume = Singleton.Instance.MasterSFXVolume;
                                        SoundEffects["Damaged"].Play();
                                        Singleton.Instance.HeroHP -= Singleton.Instance.enemyAtk;
                                    }
                                    Singleton.Instance.isDamaged = false;
                                }
                                if (HP <= 0)
                                {
                                    SoundEffects["Deleted"].Volume = Singleton.Instance.MasterSFXVolume;
                                    SoundEffects["Deleted"].Play();
                                    HP = 0;
                                    Singleton.Instance.CurrentPlayerState = Singleton.PlayerState.Dead;
                                }
                                if ((currentTile.X > 0 && Singleton.Instance.panelBoundary[currentTile.X - 1, currentTile.Y] == 0 &&
                                Singleton.Instance.panelStage[currentTile.X - 1, currentTile.Y] <= 1) &&
                                (Singleton.Instance.CurrentKey.IsKeyDown(W) && Singleton.Instance.PreviousKey.IsKeyUp(W)))
                                {
                                    Singleton.Instance.spriteMove[currentTile.X - 1, currentTile.Y] = Singleton.Instance.spriteMove[currentTile.X, currentTile.Y];
                                    Singleton.Instance.spriteMove[currentTile.X, currentTile.Y] = 0;
                                }
                                else if ((currentTile.X < 2 && Singleton.Instance.panelBoundary[currentTile.X + 1, currentTile.Y] == 0 &&
                                    Singleton.Instance.panelStage[currentTile.X + 1, currentTile.Y] <= 1) &&
                                    (Singleton.Instance.CurrentKey.IsKeyDown(S) && Singleton.Instance.PreviousKey.IsKeyUp(S)))
                                {
                                    Singleton.Instance.spriteMove[currentTile.X + 1, currentTile.Y] = Singleton.Instance.spriteMove[currentTile.X, currentTile.Y];
                                    Singleton.Instance.spriteMove[currentTile.X, currentTile.Y] = 0;
                                }
                                else if ((currentTile.Y > 0 && Singleton.Instance.panelBoundary[currentTile.X, currentTile.Y - 1] == 0 &&
                                    Singleton.Instance.panelStage[currentTile.X, currentTile.Y - 1] <= 1) &&
                                    (Singleton.Instance.CurrentKey.IsKeyDown(A) && Singleton.Instance.PreviousKey.IsKeyUp(A)))
                                {
                                    Singleton.Instance.spriteMove[currentTile.X, currentTile.Y - 1] = Singleton.Instance.spriteMove[currentTile.X, currentTile.Y];
                                    Singleton.Instance.spriteMove[currentTile.X, currentTile.Y] = 0;
                                }
                                else if ((currentTile.Y < 10 && Singleton.Instance.panelBoundary[currentTile.X, currentTile.Y + 1] == 0 &&
                                    Singleton.Instance.panelStage[currentTile.X, currentTile.Y + 1] <= 1) &&
                                    (Singleton.Instance.CurrentKey.IsKeyDown(D) && Singleton.Instance.PreviousKey.IsKeyUp(D)))
                                {
                                    Singleton.Instance.spriteMove[currentTile.X, currentTile.Y + 1] = Singleton.Instance.spriteMove[currentTile.X, currentTile.Y];
                                    Singleton.Instance.spriteMove[currentTile.X, currentTile.Y] = 0;
                                }
                                else if (Singleton.Instance.CurrentKey.IsKeyDown(J) && Singleton.Instance.PreviousKey.IsKeyUp(J))
                                {
                                    _animationManager.Play(_animations["Buster"]);
                                    for (int k = currentTile.Y; k < 10; k++)
                                    {
                                        chargeFrames = -1;
                                        if (Singleton.Instance.spriteMove[currentTile.X, k] > 1)
                                        {
                                            if (_chargeTime > CHARGED)
                                            {
                                                Attack = Attack * 10;
                                            }
                                            Singleton.Instance.spriteHP[currentTile.X, k] -= Attack;
                                            busterDamagedPosition.X = currentTile.X;
                                            busterDamagedPosition.Y = k;
                                            busterAttacked = true;
                                            SoundEffects["BusterHit"].Volume = Singleton.Instance.MasterSFXVolume;
                                            SoundEffects["BusterHit"].Play();
                                            break;
                                        }
                                    }
                                    _chargeTime = 0;
                                    SoundEffects["Buster"].Volume = Singleton.Instance.MasterSFXVolume;
                                    SoundEffects["Buster"].Play();
                                    Singleton.Instance.CurrentPlayerState = Singleton.PlayerState.BusterShot;

                                    //Singleton.Instance.busterAttacked = false;
                                }
                                else if (Singleton.Instance.useChipSlotIn.Count != 0 && 
                                    Singleton.Instance.CurrentKey.IsKeyDown(K) && Singleton.Instance.PreviousKey.IsKeyUp(K))
                                {
                                    if (Singleton.Instance.useSceneChip)
                                    {
                                        SoundEffects["UseChip"].Volume = Singleton.Instance.MasterSFXVolume;
                                        SoundEffects["UseChip"].Play();
                                        Singleton.Instance.useChipName = Singleton.Instance.useChipSlotIn.Peek();
                                        Singleton.Instance.useSceneChip = false;
                                        Singleton.Instance.CurrentGameState = Singleton.GameState.GameWaitingChip;
                                    }
                                    else
                                    {
                                        Singleton.Instance.useNormalChip = true;
                                    }
                                    _chargeTime = 0;
                                }
                                else if (Singleton.Instance.isCustomBarFull == true &&
                                    Singleton.Instance.CurrentKey.IsKeyDown(U) && Singleton.Instance.PreviousKey.IsKeyUp(U))
                                {
                                    Singleton.Instance.newTurnCustom = true;
                                    Singleton.Instance.isCustomBarFull = false;
                                    Singleton.Instance.CurrentGameState = Singleton.GameState.GameCustomScreen;
                                }

                                //autoCharge
                                if (drawChargeTime >= delay)
                                {
                                    if (_chargeTime > CHARGED)
                                    {
                                        if (_chargeTime < CHARGED + 0.02)
                                        {
                                            SoundEffects["Charged"].Volume = Singleton.Instance.MasterSFXVolume;
                                            SoundEffects["Charged"].Play();
                                        }
                                        delay = 25f;
                                        if (chargeFrames >= 12) chargeFrames = 0;
                                        else chargeFrames++;
                                        sourceRectCharge = new Rectangle((67 * chargeFrames), 68, 67, 67);
                                    }
                                    else if (_chargeTime > CHARGING)
                                    {
                                        if (_chargeTime < CHARGING + 0.02)
                                        {
                                            SoundEffects["Charging"].Volume = Singleton.Instance.MasterSFXVolume;
                                            SoundEffects["Charging"].Play();
                                        }
                                        delay = 50f;
                                        if (chargeFrames >= 6) chargeFrames = 0;
                                        else chargeFrames++;
                                        sourceRectCharge = new Rectangle((67 * chargeFrames), 0, 67, 67);
                                    }
                                    drawChargeTime = 0;
                                }
                                break;
                            case Singleton.PlayerState.BusterShot:
                                _busterCoolDown += (float)gameTime.ElapsedGameTime.TotalSeconds;
                                if (_busterCoolDown > 0.3f)
                                {
                                    _busterCoolDown = 0;
                                    Singleton.Instance.CurrentPlayerState = Singleton.PlayerState.Playing;
                                }
                                break;
                            case Singleton.PlayerState.Dead:
                                _animationManager.Play(_animations["Dead"]);
                                //Singleton.Instance.spriteMove[currentTile.X, currentTile.Y] = 0;
                                Singleton.Instance.CurrentGameState = Singleton.GameState.GameOver;
                                break;
                        }
                    }
                    _animationManager.Update(gameTime);
                    break;
            }
            base.Update(gameTime, sprites);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            //rectCharge
            destRectCharge = new Rectangle((TILESIZEX * currentTile.Y * 2) + (screenStageX - 40), (TILESIZEY * currentTile.X * 2) + (screenStageY - 100), 67*(int)scale, 67*(int)scale);

            //drawHeroHP
            if(Singleton.Instance.maxHeroHP/4 >= Singleton.Instance.HeroHP)
            {
                SoundEffects["LowHP"].Volume = Singleton.Instance.MasterSFXVolume;
                SoundEffects["LowHP"].Play();
                spriteBatch.DrawString(Singleton.Instance._font, string.Format("HP {0}", (Singleton.Instance.HeroHP)), new Vector2(10, 750), Color.OrangeRed, 0f, Vector2.Zero, 1.6f, SpriteEffects.None, 0f);
            }
            else
            {
                spriteBatch.DrawString(Singleton.Instance._font, string.Format("HP {0}", (Singleton.Instance.HeroHP)), new Vector2(10, 750), Color.White, 0f, Vector2.Zero, 1.6f, SpriteEffects.None, 0f);
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (Singleton.Instance.spriteMove[i, j] == 1)
                    {
                        currentTile = new Point(i, j);
                        Singleton.Instance.currentPlayerPoint = new Point(i, j);
                        if (_animationManager == null)
                        {
                            spriteBatch.Draw(_texture[0],
                                            Position,
                                            Viewport,
                                            Color.White);
                        }
                        else
                        {
                            _animationManager.Draw(spriteBatch, new Vector2((TILESIZEX * j * 2) + (screenStageX + 5), (TILESIZEY * i * 2) + (screenStageY - 100)), scale);
                            //drawCharge
                            if (_chargeTime > 1.2)
                            {
                                spriteBatch.Draw(Singleton.Instance.effectsTexture[1], destRectCharge, sourceRectCharge, Color.White);
                            }
                            //drawEffectBuster
                            if (busterAttacked)
                            {
                                spriteBatch.Draw(Singleton.Instance.effectsTexture[0], new Rectangle((TILESIZEX * busterDamagedPosition.Y * 2) + (screenStageX + 6), (TILESIZEY * busterDamagedPosition.X * 2) + (screenStageY - 20), 32 * (int)scale, 35 * (int)scale), new Rectangle(114, 0, 32, 35), Color.White);
                            }
                        }
                    }
                }
            }
            base.Draw(spriteBatch);
        }
    }
}
