using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Rockman.Models;

namespace Rockman.Sprites
{
    class CrimsonDragon : Enemy
    {
        private float _breatheTime, _atkTime, _reHeadCoolDown, _objectCoolDown;
        public Point currentTile;
        public Random random = new Random();
        int HP, panelX, panelY, chooseAtk, randomWave;
        bool isProtected = false;

        public CrimsonDragon(Texture2D[] _texture)
            : base(_texture)
        {
        }

        public CrimsonDragon(Dictionary<string, Animation> animations)
            : base(animations)
        {
        }
        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.GameEnemyAppear:
                    _animationManager.Play(_animations["Alive"]);
                    _breatheTime = 0; _atkTime = 0; _reHeadCoolDown = 0; _objectCoolDown = 0;
                    HP = Singleton.Instance.spriteHP[currentTile.X, currentTile.Y];
                    IsActive = true;
                    break;
                case Singleton.GameState.GamePlaying:
                    if (Singleton.Instance.spriteMove[currentTile.X, currentTile.Y] == 10)
                    {
                        _atkTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        _breatheTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        _objectCoolDown += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        //dragonHeadShield
                        if (Singleton.Instance.spriteHP[currentTile.X, currentTile.Y - 2] > 0 ||
                            Singleton.Instance.spriteMove[currentTile.X, currentTile.Y - 2] == 11)
                        {
                           Singleton.Instance.spriteHP[currentTile.X, currentTile.Y] = HP;
                        }
                        //checkDragonHeadHP
                        if (Singleton.Instance.spriteHP[currentTile.X, currentTile.Y - 2] <= 0 &&
                            Singleton.Instance.spriteHP[currentTile.X, currentTile.Y] > 0)
                        {
                            _reHeadCoolDown += (float)gameTime.ElapsedGameTime.TotalSeconds;
                            if (Singleton.Instance.spriteMove[currentTile.X, currentTile.Y - 2] != 0)
                            {
                                SoundEffects["HeadDisappear"].Volume = Singleton.Instance.MasterSFXVolume;
                                SoundEffects["HeadDisappear"].Play();
                                _animationManager.Play(_animations["WithoutHead"]);
                                Singleton.Instance.spriteMove[currentTile.X, currentTile.Y - 2] = 0;
                            }
                            HP = Singleton.Instance.spriteHP[currentTile.X, currentTile.Y];
                            if (_reHeadCoolDown > 9.7f && _reHeadCoolDown < 10f)
                            {
                                SoundEffects["HeadReturn"].Volume = Singleton.Instance.MasterSFXVolume;
                                SoundEffects["HeadReturn"].Play();
                                _animationManager.Play(_animations["ReHead"]);
                            }
                            else if (_reHeadCoolDown > 10f)
                            {
                                _reHeadCoolDown = 0f;
                                Singleton.Instance.spriteMove[currentTile.X, currentTile.Y - 2] = 11;
                                Singleton.Instance.spriteHP[currentTile.X, currentTile.Y - 2] = 100;
                                _animationManager.Play(_animations["Alive"]);
                            }
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
                            Singleton.Instance.virusAttack = new int[3, 10]
                            {
                                { 0,0,0,0,0,0,0,0,0,0},
                                { 0,0,0,0,0,0,0,0,0,0},
                                { 0,0,0,0,0,0,0,0,0,0},
                            };
                            SoundEffects["Defeated"].Volume = Singleton.Instance.MasterSFXVolume;
                            SoundEffects["Defeated"].Play();
                            Singleton.Instance.spriteMove[currentTile.X, currentTile.Y] = 0;
                        }
                        //rocketAndJunk
                        if (_objectCoolDown > 3f)
                        {
                            if (_objectCoolDown > 3.01f && _objectCoolDown < 3.03f)
                            {
                                Singleton.Instance.panelYellow = new int[3, 10]
                                {
                                    { 0,0,0,0,20,0,0,0,0,0},
                                    { 0,0,0,0,0,0,0,0,0,0},
                                    { 0,0,0,0,0,0,0,0,0,0},
                                };
                            }
                            else if (_objectCoolDown > 3.04f && _objectCoolDown < 3.1f)
                            {
                                //rocketAndJunkStart
                                for (int i = 0; i < 3; i++)
                                {
                                    for (int j = 0; j < 10; j++)
                                    {
                                        if (Singleton.Instance.panelYellow[i, j] == 20)
                                        {
                                            Singleton.Instance.panelYellow[i, j] = 0;
                                            Singleton.Instance.virusAttack[i, j] = 20;
                                            Singleton.Instance.spriteMove[i, j] = 20;
                                            Singleton.Instance.spriteHP[i, j] = 40;
                                        }
                                    }
                                }
                            }
                            if (_objectCoolDown > 3.04 + 3f)
                            {
                                _objectCoolDown = 0f;
                            }
                            //returnRocketAndJunk
                            //else if (_objectCoolDown > 3.04f + 2.0f)
                            //{
                            //    for (int i = 0; i < 3; i++)
                            //    {
                            //        for (int j = 0; j < 10; j++)
                            //        {
                            //            if (Singleton.Instance.virusAttack[i, j] == 20)
                            //            {
                            //                Singleton.Instance.virusAttack[i, j] = 0;
                            //                Singleton.Instance.spriteMove[i, j] = 0;
                            //            }
                            //        }
                            //    }

                            //}
                        }
                        ////breatheAtk
                        //if (HP <= 800 && _breatheTime > 18f)
                        //{
                        //    //checkHead
                        //    if (Singleton.Instance.spriteHP[currentTile.X, currentTile.Y - 2] > 0)
                        //    {
                        //        _animationManager.Play(_animations["HeadAttack"]);
                        //    }
                        //    if (_breatheTime > 18.01f && _breatheTime < 18.03f)
                        //    {
                        //        SoundEffects["DragonRoar"].Volume = Singleton.Instance.MasterSFXVolume;
                        //        SoundEffects["DragonRoar"].Play();
                        //        Singleton.Instance.panelYellow = new int[3, 10]
                        //        {
                        //            { 0,7,7,7,0,0,0,0,0,0},
                        //            { 7,7,7,7,7,0,0,0,0,0},
                        //            { 0,7,7,7,0,0,0,0,0,0},
                        //        };
                        //    }
                        //    else if (_breatheTime > 18.8f && _breatheTime < 18.9f)
                        //    {
                        //        //dragonBreathe
                        //        for (int i = 0; i < 3; i++)
                        //        {
                        //            for (int j = 0; j < 10; j++)
                        //            {
                        //                if (Singleton.Instance.panelYellow[i, j] == 7)
                        //                {
                        //                    Singleton.Instance.panelYellow[i, j] = 0;
                        //                    Singleton.Instance.bossAttack[i, j] = 7;
                        //                }
                        //            }
                        //        }
                        //    }
                        //    //returnBreathe
                        //    if (_breatheTime > 18.8f + 1.0f)
                        //    {
                        //        for (int i = 0; i < 3; i++)
                        //        {
                        //            for (int j = 0; j < 10; j++)
                        //            {
                        //                if (Singleton.Instance.bossAttack[i, j] == 7) Singleton.Instance.bossAttack[i, j] = 0;
                        //            }
                        //        }
                        //    }
                        //    //atkSuccess
                        //    if (_breatheTime > 20f)
                        //    {
                        //        if (Singleton.Instance.spriteHP[currentTile.X, currentTile.Y - 2] > 0)
                        //        {
                        //            _animationManager.Play(_animations["Alive"]);
                        //        }
                        //        _breatheTime = 0f; _atkTime = 0f;
                        //    }
                        //}
                        ////checkAtk
                        //else if (_atkTime > 4f)
                        //{
                        //    if (_atkTime < 4.02f)
                        //    {
                        //        chooseAtk = random.Next(5, 7);
                        //    }
                        //    //checkHead
                        //    if (Singleton.Instance.spriteHP[currentTile.X, currentTile.Y - 2] > 0)
                        //    {
                        //        _animationManager.Play(_animations["HeadAttack"]);
                        //    }
                        //    //crimsonPrepareAtk
                        //    if (_atkTime < 4.1f)
                        //    {
                        //        SoundEffects["DragonRoar"].Volume = Singleton.Instance.MasterSFXVolume;
                        //        SoundEffects["DragonRoar"].Play();
                        //        if (chooseAtk == 5 && _atkTime > 4.0f && _atkTime < 4.02f)
                        //        {
                        //            Singleton.Instance.panelYellow[Singleton.Instance.currentPlayerPoint.X, Singleton.Instance.currentPlayerPoint.Y] = 5;
                        //            for (int i = 0; i < 6; i++)
                        //            {
                        //                panelX = random.Next(0, 3);
                        //                panelY = random.Next(0, 5);
                        //                Singleton.Instance.panelYellow[panelX, panelY] = 5;
                        //            }
                        //        }
                        //        else if (chooseAtk == 6 && _atkTime > 4.0f && _atkTime < 4.02f)
                        //        {
                        //            randomWave = random.Next(1, 3);
                        //            if (randomWave == 1)
                        //            {
                        //                Singleton.Instance.panelYellow = new int[3, 10]
                        //                {
                        //                    { 6,6,6,6,6,0,0,0,0,0},
                        //                    { 6,6,6,6,6,0,0,0,0,0},
                        //                    { 0,0,0,0,0,0,0,0,0,0},
                        //                };
                        //            }
                        //            else
                        //            {
                        //                Singleton.Instance.panelYellow = new int[3, 10]
                        //                {
                        //                    { 0,0,0,0,0,0,0,0,0,0},
                        //                    { 6,6,6,6,6,0,0,0,0,0},
                        //                    { 6,6,6,6,6,0,0,0,0,0},
                        //                };
                        //            }
                        //        }
                        //    }
                        //    //crimsonAtk
                        //    if (chooseAtk == 5 && _atkTime > 4.5f && _atkTime < 4.6f)
                        //    {
                        //        //headJet
                        //        for (int i = 0; i < 3; i++)
                        //        {
                        //            for (int j = 0; j < 10; j++)
                        //            {
                        //                if (Singleton.Instance.panelYellow[i, j] == 5)
                        //                {
                        //                    Singleton.Instance.panelYellow[i, j] = 0;
                        //                    Singleton.Instance.bossAttack[i, j] = 5;
                        //                }
                        //            }
                        //        }
                        //    }
                        //    else if (chooseAtk == 6 && _atkTime > 4.5f && _atkTime < 4.6f)
                        //    {
                        //        //fireWave
                        //        for (int i = 0; i < 3; i++)
                        //        {
                        //            for (int j = 0; j < 10; j++)
                        //            {
                        //                if (Singleton.Instance.panelYellow[i, j] == 6)
                        //                {
                        //                    Singleton.Instance.panelYellow[i, j] = 0;
                        //                    Singleton.Instance.bossAttack[i, j] = 6;
                        //                }
                        //            }
                        //        }
                        //    }
                        //    //removeAtkPanel
                        //    if (chooseAtk == 5 && _atkTime > 4.5f + 0.68 * 2f)
                        //    {
                        //        for (int i = 0; i < 3; i++)
                        //        {
                        //            for (int j = 0; j < 10; j++)
                        //            {
                        //                if (Singleton.Instance.bossAttack[i, j] == 5) Singleton.Instance.bossAttack[i, j] = 0;
                        //            }
                        //        }
                        //    }
                        //    else if (chooseAtk == 6 && _atkTime > 4.5f + 1.0f)
                        //    {
                        //        for (int i = 0; i < 3; i++)
                        //        {
                        //            for (int j = 0; j < 10; j++)
                        //            {
                        //                if (Singleton.Instance.bossAttack[i, j] == 6) Singleton.Instance.bossAttack[i, j] = 0;
                        //            }
                        //        }
                        //    }
                        //    //atkSuccess
                        //    if (_atkTime > 6)
                        //    {
                        //        if (Singleton.Instance.spriteHP[currentTile.X, currentTile.Y - 2] > 0)
                        //        {
                        //            _animationManager.Play(_animations["Alive"]);
                        //        }
                        //        _atkTime = 0f;
                        //    }
                        //}
                    }
                    _animationManager.Update(gameTime);
                    break;
                case Singleton.GameState.GameUseChip:
                    //dragonHeadShield
                    if (Singleton.Instance.spriteHP[currentTile.X, currentTile.Y - 2] > 0 ||
                            Singleton.Instance.spriteMove[currentTile.X, currentTile.Y - 2] == 11)
                    {
                        Singleton.Instance.spriteHP[currentTile.X, currentTile.Y] = HP;
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
                            //drawCrimsonDragon
                            if (Singleton.Instance.spriteMove[i, j] == 10)
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
                                        new Vector2((TILESIZEX * j * 2) + (screenStageX - 350),
                                            (TILESIZEY * i * 2) + (screenStageY - 575)),
                                        2.5f);
                                }
                                //drawHP
                                if (Singleton.Instance.spriteHP[i, j] > 0)
                                {
                                    spriteBatch.DrawString(Singleton.Instance._font, string.Format("{0}", (Singleton.Instance.spriteHP[i, j])),
                                        new Vector2((TILESIZEX * currentTile.Y * 2) + (screenStageX + TILESIZEY), (TILESIZEY * currentTile.X * 2) + (screenStageY + TILESIZEX - 10)), Color.White, 0f, Vector2.Zero, 1.3f, SpriteEffects.None, 0f);
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
