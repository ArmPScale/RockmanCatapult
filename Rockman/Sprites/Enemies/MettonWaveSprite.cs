using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Rockman.Sprites
{
    class MettonWaveSprite : Enemy
    {
        Point currentTile;
        float delay = 100f, drawWaveTime;
        int waveFrames = 0, Attack = 10;
        bool isDamaged = true;

        Rectangle destRectWave, sourceRectWave;

        public MettonWaveSprite(Texture2D[] texture)
            : base(texture)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.GameCustomScreen:
                    sourceRectWave = new Rectangle((50 * waveFrames), 61, 50, 60);
                    break;
                case Singleton.GameState.GamePlaying:
                    drawWaveTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    if (Singleton.Instance.virusAttack[currentTile.X, currentTile.Y] == 2)
                    {
                        //atkTakePlayer
                        if (Singleton.Instance.playerMove[currentTile.X, currentTile.Y] > 0 && isDamaged)
                        {
                            Singleton.Instance.enemyAtk = Attack;
                            Singleton.Instance.isDamaged = isDamaged;
                            isDamaged = false;
                        }
                        if (drawWaveTime >= delay)
                        {
                            if (waveFrames >= 4)
                            {
                                waveFrames = -1;
                                if (currentTile.Y > 0 && Singleton.Instance.panelStage[currentTile.X, currentTile.Y - 1] <= 1)
                                {
                                    Singleton.Instance.virusAttack[currentTile.X, currentTile.Y - 1] = Singleton.Instance.virusAttack[currentTile.X, currentTile.Y];
                                }
                                //crackedWave
                                //if (Singleton.Instance.panelStage[currentTile.X, currentTile.Y] == 1 && Singleton.Instance.spriteMove[currentTile.X, currentTile.Y] != 1)
                                //{
                                //    Singleton.Instance.panelStage[currentTile.X, currentTile.Y] = 2;
                                //}
                                //else if (Singleton.Instance.panelStage[currentTile.X, currentTile.Y] == 0)
                                //{
                                //    Singleton.Instance.panelStage[currentTile.X, currentTile.Y] = 1;
                                //}

                                Singleton.Instance.virusAttack[currentTile.X, currentTile.Y] = 0;
                                isDamaged = true;
                            }
                            else
                            {
                                if (waveFrames == 0)
                                {
                                    SoundEffects["MettonWave"].Volume = Singleton.Instance.MasterSFXVolume;
                                    SoundEffects["MettonWave"].Play();
                                }
                                waveFrames++;
                            }
                            sourceRectWave = new Rectangle((50 * waveFrames), 61, 50, 60);
                            drawWaveTime = 0;
                        }
                    }
                    break;
                case Singleton.GameState.GameWaitingChip:
                    sourceRectWave = new Rectangle((50 * waveFrames), 61, 50, 60);
                    break;
                case Singleton.GameState.GameUseChip:
                    sourceRectWave = new Rectangle((50 * waveFrames), 61, 50, 60);
                    break;
            }
            base.Update(gameTime, sprites);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //rectAtk
            destRectWave = new Rectangle((TILESIZEX * currentTile.Y * 2) + (screenStageX - 12), (TILESIZEY * currentTile.X * 2) + (screenStageY - 125), 50 * (int)scale, 60 * (int)scale);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    //drawAtk
                    if (Singleton.Instance.virusAttack[i, j] == 2)
                    {
                        currentTile = new Point(i, j);
                        spriteBatch.Draw(_texture[0], destRectWave, sourceRectWave, Color.White);
                    }
                }
            }
            base.Draw(spriteBatch);
        }
    }
}
