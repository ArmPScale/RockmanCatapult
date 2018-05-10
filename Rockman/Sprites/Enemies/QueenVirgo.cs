using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Rockman.Models;

namespace Rockman.Sprites
{
    class QueenVirgo : Enemy
    {
        private float _timer, _atkTime, _castingTime;
        public Point currentTile;
        public static Random random = new Random();
        int HP, panelX, panelY, chooseAtk;
        bool isProtected = false;

        public QueenVirgo(Texture2D[] _texture)
            : base(_texture)
        {
        }

        public QueenVirgo(Dictionary<string, Animation> animations)
            : base(animations)
        {
        }
        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.GamePlaying:
                    _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    _atkTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    _castingTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (Singleton.Instance.spriteMove[currentTile.X, currentTile.Y] == 5)
                    {
                        //aquaShield
                        if (isProtected && HP > Singleton.Instance.spriteHP[currentTile.X, currentTile.Y])
                        {
                            Singleton.Instance.spriteHP[currentTile.X, currentTile.Y] = HP;
                            SoundEffects["AquaSheild"].Volume = Singleton.Instance.MasterSFXVolume;
                            SoundEffects["AquaSheild"].Play();
                        }
                        //checkHP
                        if (Singleton.Instance.spriteHP[currentTile.X, currentTile.Y] <= 0)
                        {
                            Singleton.Instance.bossAttack = new int[3, 10]
                            {
                                { 0,0,0,0,0,0,0,0,0,0},
                                { 0,0,0,0,0,0,0,0,0,0},
                                { 0,0,0,0,0,0,0,0,0,0},
                            };
                            SoundEffects["Defeated"].Volume = Singleton.Instance.MasterSFXVolume;
                            SoundEffects["Defeated"].Play();
                            Singleton.Instance.spriteMove[currentTile.X, currentTile.Y] = 0;
                        }
                        //queenVirgoAtk
                        if (_atkTime > 3.0f)
                        {
                            if (_atkTime < 3.1f)
                            {
                                if (currentTile.X != Singleton.Instance.currentPlayerPoint.X &&
                                    Singleton.Instance.spriteMove[Singleton.Instance.currentPlayerPoint.X, currentTile.Y] == 0)
                                {
                                    Singleton.Instance.spriteMove[Singleton.Instance.currentPlayerPoint.X, currentTile.Y] = Singleton.Instance.spriteMove[currentTile.X, currentTile.Y];
                                    Singleton.Instance.spriteHP[Singleton.Instance.currentPlayerPoint.X, currentTile.Y] = Singleton.Instance.spriteHP[currentTile.X, currentTile.Y];
                                    Singleton.Instance.spriteMove[currentTile.X, currentTile.Y] = 0;
                                    Singleton.Instance.spriteHP[currentTile.X, currentTile.Y] = 0;
                                }
                                _animationManager.Play(_animations["StartCasting"]);
                                HP = Singleton.Instance.spriteHP[currentTile.X, currentTile.Y];
                                //randomChooseAtk
                                chooseAtk = random.Next(3, 4);
                            }
                            else if (_atkTime < 5.5f)
                            {
                                _animationManager.Play(_animations["Casting"]);
                                if(_atkTime < 3.2f)
                                {
                                    SoundEffects["Casting"].Volume = Singleton.Instance.MasterSFXVolume;
                                    SoundEffects["Casting"].Play();
                                    if (chooseAtk == 2 && _castingTime > 0.2f)
                                    {
                                        panelX = Singleton.Instance.currentPlayerPoint.X;
                                        panelY = Singleton.Instance.currentPlayerPoint.Y;
                                        Singleton.Instance.panelYellow[panelX, panelY] = 2;
                                        for(int i = 0; i < 3; i++)
                                        {
                                            panelX = random.Next(0, 3);
                                            panelY = random.Next(0, 5);
                                            Singleton.Instance.panelYellow[panelX, panelY] = 2;
                                        }
                                        _castingTime = 0f;
                                    }
                                    else if (chooseAtk == 3 && _castingTime > 0.2f)
                                    {
                                        panelX = 1;
                                        panelY = random.Next(1, 4);
                                        Singleton.Instance.panelYellow[panelX, panelY] = 3;
                                        Singleton.Instance.panelYellow[panelX, panelY - 1] = 3;
                                        Singleton.Instance.panelYellow[panelX, panelY + 1] = 3;
                                        Singleton.Instance.panelYellow[panelX - 1, panelY - 1] = 3;
                                        Singleton.Instance.panelYellow[panelX - 1, panelY] = 3;
                                        Singleton.Instance.panelYellow[panelX - 1, panelY + 1] = 3;
                                        Singleton.Instance.panelYellow[panelX + 1, panelY - 1] = 3;
                                        Singleton.Instance.panelYellow[panelX + 1, panelY] = 3;
                                        Singleton.Instance.panelYellow[panelX + 1, panelY + 1] = 3;
                                        _castingTime = 0f;
                                    }
                                }
                                if (chooseAtk == 1 && _atkTime > 3.5f && _atkTime < 3.5f + 0.1f)
                                {
                                    //aquaWave
                                    Singleton.Instance.panelYellow[currentTile.X, currentTile.Y - 1] = 1;
                                    Singleton.Instance.bossAttack[currentTile.X, currentTile.Y - 1] = 1;
                                }
                                else if (chooseAtk == 2 && _atkTime > 4f && _atkTime < 4f + 0.9f)
                                {
                                    //rainy
                                    for (int i = 0; i < 3; i++)
                                    {
                                        for (int j = 0; j < 10; j++)
                                        {
                                            if (Singleton.Instance.panelYellow[i, j] == 2) Singleton.Instance.bossAttack[i, j] = 2;
                                        }
                                    }
                                }
                                else if (chooseAtk == 3 && _atkTime > 4f && _atkTime < 4f + 1.8f)
                                {
                                    //magicianFreeze
                                    Singleton.Instance.bossAttack[panelX, panelY] = 3;
                                }
                                //aquaShieldActived
                                isProtected = true;
                            }
                            else if (_atkTime < 5.7f)
                            {
                                _animationManager.Play(_animations["FinishCasting"]);
                                isProtected = false;
                            }
                            else if (_atkTime < 5.9f)
                            {
                                _animationManager.Play(_animations["Alive"]);
                            }
                            else if (_atkTime > 6f)
                            {
                                _atkTime = 0f;
                            }
                        }
                        //movement
                        else if (_timer > 0.8f)
                        {
                            _animationManager.Play(_animations["Alive"]);
                            int xPos = random.Next(0, 3);
                            int yPos = random.Next(5, 10);
                            if ((xPos != currentTile.X || yPos != currentTile.Y) &&
                                (Singleton.Instance.spriteMove[xPos, yPos] == 0 && 
                                Singleton.Instance.panelBoundary[xPos, yPos] == 1 && 
                                Singleton.Instance.panelStage[xPos, yPos] <= 1))
                            {
                                Singleton.Instance.spriteMove[xPos, yPos] = Singleton.Instance.spriteMove[currentTile.X, currentTile.Y];
                                Singleton.Instance.spriteHP[xPos, yPos] = Singleton.Instance.spriteHP[currentTile.X, currentTile.Y];
                                Singleton.Instance.spriteMove[currentTile.X, currentTile.Y] = 0;
                                Singleton.Instance.spriteHP[currentTile.X, currentTile.Y] = 0;
                            }
                            _timer = 0f;
                        }
                    }
                    _animationManager.Update(gameTime);
                    break;
                case Singleton.GameState.GameUseChip:
                    //aquaShield
                    if (isProtected && HP > Singleton.Instance.spriteHP[currentTile.X, currentTile.Y])
                    {
                        Singleton.Instance.spriteHP[currentTile.X, currentTile.Y] = HP;
                        SoundEffects["AquaSheild"].Volume = Singleton.Instance.MasterSFXVolume;
                        SoundEffects["AquaSheild"].Play();
                    }
                    break;
            }
            base.Update(gameTime, sprites);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (Singleton.Instance.CurrentScreenState)
            {
                case Singleton.ScreenState.StoryMode:
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 10; j++)
                        {
                            //drawWizard
                            if (Singleton.Instance.spriteMove[i, j] == 5)
                            {
                                currentTile = new Point(i, j);
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
                                        new Vector2((TILESIZEX * j * 2) + (screenStageX - 90),
                                            (TILESIZEY * i * 2) + (screenStageY - 210)),
                                        1.5f);
                                }
                                //drawHP
                                if (Singleton.Instance.spriteHP[i, j] >= 0)
                                {
                                    spriteBatch.DrawString(Singleton.Instance._font, string.Format("{0}", (Singleton.Instance.spriteHP[i, j])), new Vector2((TILESIZEX * currentTile.Y * 2) + (screenStageX + TILESIZEY), (TILESIZEY * currentTile.X * 2) + (screenStageY + TILESIZEX - 10)), Color.White, 0f, Vector2.Zero, 1.3f, SpriteEffects.None, 0f);
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
