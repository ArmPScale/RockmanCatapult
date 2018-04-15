using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Rockman;

namespace Rockman.Sprites
{
    class RockmanEXESprite : Sprite
    {
        const float CHARGING = 1.2f ,CHARGED = 3.2f;
        private float _chargeTime;
        public int HP, Attack;
        public Point currentTile, busterDamagedPosition;
        public Keys W, S, A, D, J, K;
        float delay = 50f, drawChargeTime;
        int chargeFrames = 0;

        Rectangle destRectCharge, sourceRectCharge;
        public bool busterAttacked;


        //Vector2 initialPosition = new Vector2(100,600);
        //Vector2 initialVelocity = new Vector2(10, 10); // Choose values that work for you
        //Vector2 acceleration = new Vector2(5, -9.8f);

        //float time = 0;
        //Vector2 position = Vector2.Zero; // Use this when drawing your sprite


        public RockmanEXESprite(Texture2D[] texture)
            : base(texture)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            //time += (float)gameTime.ElapsedGameTime.TotalSeconds;

            //position = initialPosition + initialVelocity * time
            //           + 0.5f * acceleration * time * time;

            HP = Singleton.Instance.HeroHP;
            _chargeTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            drawChargeTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (Singleton.Instance.spriteMove[currentTile.X, currentTile.Y] == 1)
            {
                //checkHP
                if (HP <= 0)
                {
                    Singleton.Instance.soundEffects[6].Play();
                    HP = 0;
                    Singleton.Instance.spriteMove[currentTile.X, currentTile.Y] = 0;
                    Singleton.Instance.CurrentGameState = Singleton.GameState.GameOver;
                }
                //autoCharge
                if (drawChargeTime >= delay)
                {
                    if (_chargeTime > CHARGED)
                    {
                        if (_chargeTime < CHARGED+0.02) Singleton.Instance.soundEffects[2].Play();
                        delay = 25f;
                        if (chargeFrames >= 12)
                        {
                            chargeFrames = 0;
                        }
                        else
                        {
                            chargeFrames++;
                        }
                        sourceRectCharge = new Rectangle((67 * chargeFrames), 68, 67, 67);
                    }
                    else if (_chargeTime > CHARGING)
                    {
                        if (_chargeTime < CHARGING + 0.02) Singleton.Instance.soundEffects[1].Play();
                        delay = 50f;
                        if (chargeFrames >= 6)
                        {
                            chargeFrames = 0;
                        }
                        else
                        {
                            chargeFrames++;
                        }
                        sourceRectCharge = new Rectangle((67 * chargeFrames), 0, 67, 67);
                    }
                    drawChargeTime = 0;
                }

                //movementHero
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
                    Singleton.Instance.soundEffects[0].CreateInstance().Play();
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
                            break;
                        }
                    }
                    _chargeTime = 0; Attack = 1; 
                    //Singleton.Instance.busterAttacked = false;
                }
                else if (Singleton.Instance.CurrentKey.IsKeyDown(K) && Singleton.Instance.PreviousKey.IsKeyUp(K))
                {
                    if (true)
                    {
                        Singleton.Instance.soundEffects[7].CreateInstance().Play();
                        Singleton.Instance.CurrentGameState = Singleton.GameState.GameWaitingChip;
                    }
                    //Singleton.Instance.soundEffects[3].CreateInstance().Play();
                    //for (int k = currentTile.Y; k < 10; k++)
                    //{
                    //    if (Singleton.Instance.spriteMove[currentTile.X, k] > 1)
                    //    {
                    //        Singleton.Instance.spriteHP[currentTile.X, k] -= 40;
                    //        break;
                    //    }
                    //}
                    Attack = 1; _chargeTime = 0;
                }
            }
            base.Update(gameTime, sprites);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            //rectCharge
            destRectCharge = new Rectangle((TILESIZEX * currentTile.Y * 2) + (screenStageX - 40), (TILESIZEY * currentTile.X * 2) + (screenStageY - 100), 67*(int)scale, 67*(int)scale);
            //drawHeroHP
            spriteBatch.DrawString(Singleton.Instance._font, string.Format("HP {0}", (Singleton.Instance.HeroHP)), new Vector2(10, 750), Color.White, 0f, Vector2.Zero, 1.6f, SpriteEffects.None, 0f);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (Singleton.Instance.spriteMove[i, j] == 1)
                    {
                        currentTile = new Point(i, j);
                        spriteBatch.Draw(_texture[0], new Vector2((TILESIZEX * j * 2) + (screenStageX + 6), (TILESIZEY * i * 2) + (screenStageY - 75)), null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
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
                        //spriteBatch.Draw(_texture[0], position, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);

                    }
                }
            }
            base.Draw(spriteBatch);
        }
    }
}
