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
        private float _chargeTime, _busterCoolDown, _chipCoolDown, _bugCoolDown;
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
                    if (Singleton.Instance.playerMove[currentTile.X, currentTile.Y] == 1)
                    {
                        //statusBug
                        if (Singleton.Instance.statusBugHP)
                        {
                            _bugCoolDown += (float)gameTime.ElapsedGameTime.TotalSeconds;
                            if (_bugCoolDown > 0.7f)
                            {
                                _bugCoolDown = 0;
                                if(HP > 1) Singleton.Instance.HeroHP -= 1;
                            }
                        }
                        switch (Singleton.Instance.CurrentPlayerState)
                        {
                            case Singleton.PlayerState.Playing:
                                _animationManager.Play(_animations["Alive"]);
                                //Damaged
                                if (Singleton.Instance.isDamaged)
                                {
                                    //holyPanelPlayer
                                    if (Singleton.Instance.panelElement[Singleton.Instance.currentPlayerPoint.X, Singleton.Instance.currentPlayerPoint.Y] == 1)
                                    {
                                        Singleton.Instance.enemyAtk /= 2;
                                    }
                                    //barrier
                                    if (Singleton.Instance.HeroBarrier > 0)
                                    {
                                        SoundEffects["Barrier"].Volume = Singleton.Instance.MasterSFXVolume;
                                        SoundEffects["Barrier"].Play();
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
                                    Singleton.Instance.enemyAtk = 0;
                                    Singleton.Instance.isDamaged = false;
                                }
                                //checkHP
                                if (HP <= 0)
                                {
                                    SoundEffects["Deleted"].Volume = Singleton.Instance.MasterSFXVolume;
                                    SoundEffects["Deleted"].Play();
                                    HP = 0;
                                    Singleton.Instance.CurrentPlayerState = Singleton.PlayerState.Dead;
                                }
                                //movementHero
                                if ((currentTile.X > 0 && Singleton.Instance.panelBoundary[currentTile.X - 1, currentTile.Y] == 0 &&
                                Singleton.Instance.panelStage[currentTile.X - 1, currentTile.Y] <= 1) &&
                                (Singleton.Instance.CurrentKey.IsKeyDown(W) && Singleton.Instance.PreviousKey.IsKeyUp(W)))
                                {
                                    Singleton.Instance.playerMove[currentTile.X - 1, currentTile.Y] = Singleton.Instance.playerMove[currentTile.X, currentTile.Y];
                                    Singleton.Instance.playerMove[currentTile.X, currentTile.Y] = 0;
                                }
                                else if ((currentTile.X < 2 && Singleton.Instance.panelBoundary[currentTile.X + 1, currentTile.Y] == 0 &&
                                    Singleton.Instance.panelStage[currentTile.X + 1, currentTile.Y] <= 1) &&
                                    (Singleton.Instance.CurrentKey.IsKeyDown(S) && Singleton.Instance.PreviousKey.IsKeyUp(S)))
                                {
                                    Singleton.Instance.playerMove[currentTile.X + 1, currentTile.Y] = Singleton.Instance.playerMove[currentTile.X, currentTile.Y];
                                    Singleton.Instance.playerMove[currentTile.X, currentTile.Y] = 0;
                                }
                                else if ((currentTile.Y > 0 && Singleton.Instance.panelBoundary[currentTile.X, currentTile.Y - 1] == 0 &&
                                    Singleton.Instance.panelStage[currentTile.X, currentTile.Y - 1] <= 1) &&
                                    (Singleton.Instance.CurrentKey.IsKeyDown(A) && Singleton.Instance.PreviousKey.IsKeyUp(A)))
                                {
                                    Singleton.Instance.playerMove[currentTile.X, currentTile.Y - 1] = Singleton.Instance.playerMove[currentTile.X, currentTile.Y];
                                    Singleton.Instance.playerMove[currentTile.X, currentTile.Y] = 0;
                                }
                                else if ((currentTile.Y < 10 && Singleton.Instance.panelBoundary[currentTile.X, currentTile.Y + 1] == 0 &&
                                    Singleton.Instance.panelStage[currentTile.X, currentTile.Y + 1] <= 1) &&
                                    (Singleton.Instance.CurrentKey.IsKeyDown(D) && Singleton.Instance.PreviousKey.IsKeyUp(D)))
                                {
                                    Singleton.Instance.playerMove[currentTile.X, currentTile.Y + 1] = Singleton.Instance.playerMove[currentTile.X, currentTile.Y];
                                    Singleton.Instance.playerMove[currentTile.X, currentTile.Y] = 0;
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
                                else if (Singleton.Instance.isCustomBarFull &&
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
                            case Singleton.PlayerState.UseChipNormal:
                                //animateUseChipNormal
                                _chipCoolDown += (float)gameTime.ElapsedGameTime.TotalSeconds;
                                _animationManager.Play(_animations[Singleton.Instance.choosePlayerAnimate]);
                                if (_chipCoolDown > Singleton.Instance.currentChipAtkTime
                                    //&& Singleton.Instance.currentChipAtkTime != 0
                                    && Singleton.Instance.currentVirusGotDmgIndex != -1)
                                {
                                    //holyPanelVirus
                                    if (Singleton.Instance.panelElement[Singleton.Instance.currentPlayerPoint.X, Singleton.Instance.currentVirusGotDmgIndex] == 1)
                                    {
                                        Singleton.Instance.playerChipAtk /= 2;
                                    }
                                    Singleton.Instance.spriteHP[Singleton.Instance.currentPlayerPoint.X, Singleton.Instance.currentVirusGotDmgIndex] -= Singleton.Instance.playerChipAtk;
                                    Singleton.Instance.currentVirusGotDmgIndex = -1;
                                    Singleton.Instance.playerChipAtk = 0;
                                    Singleton.Instance.currentChipAtkTime = 0f;
                                }
                                else if (_chipCoolDown > Singleton.Instance.currentChipCoolDown)
                                {
                                    _chipCoolDown = 0;
                                    Singleton.Instance.choosePlayerAnimate = "Alive";
                                    Singleton.Instance.currentChipCoolDown = 0f;
                                    Singleton.Instance.CurrentPlayerState = Singleton.PlayerState.Playing;
                                }
                                break;
                            case Singleton.PlayerState.Dead:
                                _animationManager.Play(_animations["Dead"]);
                                //Singleton.Instance.playerMove[currentTile.X, currentTile.Y] = 0;
                                Singleton.Instance.CurrentGameState = Singleton.GameState.GameOver;
                                break;
                        }
                    }
                    _animationManager.Update(gameTime);
                    break;
                case Singleton.GameState.GameUseChip:
                    _animationManager.Play(_animations[Singleton.Instance.choosePlayerAnimate]);
                    _animationManager.Update(gameTime);
                    break;
                case Singleton.GameState.GameClear:
                    _chargeTime = 0;
                    _animationManager.Play(_animations["Alive"]);
                    _animationManager.Update(gameTime);
                    break;
            }
            base.Update(gameTime, sprites);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (Singleton.Instance.CurrentScreenState)
            {
                case Singleton.ScreenState.StoryMode:
                    //rectCharge
                    destRectCharge = new Rectangle((TILESIZEX * currentTile.Y * 2) + (screenStageX - 40), (TILESIZEY * currentTile.X * 2) + (screenStageY - 100), 67 * (int)scale, 67 * (int)scale);
                    //drawHeroHP
                    if (Singleton.Instance.maxHeroHP / 4 >= HP)
                    {
                        SoundEffects["LowHP"].Volume = Singleton.Instance.MasterSFXVolume;
                        SoundEffects["LowHP"].Play();
                        if (Singleton.Instance.chooseEmotionPlayer != "DarkEmotion") Singleton.Instance.chooseEmotionPlayer = "HurtEmotion";
                        if (Singleton.Instance.CurrentGameState == Singleton.GameState.GameCustomScreen)
                        {
                            spriteBatch.DrawString(Singleton.Instance._font, string.Format("HP {0}", HP), new Vector2(375, 10), Color.OrangeRed, 0f, Vector2.Zero, 1.6f, SpriteEffects.None, 0f);
                        }
                        else if (Singleton.Instance.CurrentGameState == Singleton.GameState.GamePlaying)
                        {
                            spriteBatch.DrawString(Singleton.Instance._font, string.Format("HP {0}", HP), new Vector2(10, 10), Color.OrangeRed, 0f, Vector2.Zero, 1.6f, SpriteEffects.None, 0f);
                        }
                    }
                    else
                    {
                        if (Singleton.Instance.chooseEmotionPlayer != "DarkEmotion") Singleton.Instance.chooseEmotionPlayer = "NormalEmotion";
                        if (Singleton.Instance.CurrentGameState == Singleton.GameState.GameCustomScreen)
                        {
                            spriteBatch.DrawString(Singleton.Instance._font, string.Format("HP {0}", HP), new Vector2(375, 10), Color.White, 0f, Vector2.Zero, 1.6f, SpriteEffects.None, 0f);
                        }
                        else if (Singleton.Instance.CurrentGameState == Singleton.GameState.GamePlaying)
                        {
                            spriteBatch.DrawString(Singleton.Instance._font, string.Format("HP {0}", HP), new Vector2(10, 10), Color.White, 0f, Vector2.Zero, 1.6f, SpriteEffects.None, 0f);
                        }
                    }
                    //drawHeroSprite
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 10; j++)
                        {
                            if (Singleton.Instance.playerMove[i, j] == 1)
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
                    break;
            }
            
            base.Draw(spriteBatch);
        }
    }
}
