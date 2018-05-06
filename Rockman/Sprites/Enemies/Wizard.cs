using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Rockman.Models;

namespace Rockman.Sprites
{
    class Wizard : Enemy
    {
        private float _timer, _atkTime, _castingTime;
        public Point currentTile;
        public static Random random = new Random();
        int panelX, panelY;

        public Wizard(Texture2D[] _texture)
            : base(_texture)
        {
        }

        public Wizard(Dictionary<string, Animation> animations)
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
                    if (Singleton.Instance.spriteMove[currentTile.X, currentTile.Y] == 4)
                    {
                        //checkHP
                        if (Singleton.Instance.spriteHP[currentTile.X, currentTile.Y] <= 0)
                        {
                            Singleton.Instance.virusAttack[panelX, panelY] = 0;
                            SoundEffects["Explosion"].Volume = Singleton.Instance.MasterSFXVolume;
                            SoundEffects["Explosion"].Play();
                            Singleton.Instance.spriteMove[currentTile.X, currentTile.Y] = 0;
                        }
                        //wizardAtk
                        if (_atkTime > 6.0f)
                        {
                            _animationManager.Play(_animations["Casting"]);
                            if (_atkTime < 6.2f)
                            {
                                SoundEffects["Casting"].Volume = Singleton.Instance.MasterSFXVolume;
                                SoundEffects["Casting"].Play();
                                if (_castingTime > 0.2f)
                                {
                                    panelX = random.Next(0, 3);
                                    panelY = random.Next(0, 5);
                                    Singleton.Instance.virusAttack[panelX, panelY] = 3;
                                    _castingTime = 0f;
                                }
                                
                            }
                            else if (_atkTime > 7.5f)
                            {
                                SoundEffects["Paneltolce"].Volume = Singleton.Instance.MasterSFXVolume;
                                SoundEffects["Paneltolce"].Play();

                                Singleton.Instance.virusAttack[panelX, panelY] = 0;
                                _atkTime = 0f;
                            }
                        }
                        //movement
                        else if (_timer > 1.0f)
                        {
                            _animationManager.Play(_animations["Alive"]);
                            int xPos = random.Next(0, 3);
                            int yPos = random.Next(5, 10);
                            if ((xPos != currentTile.X || yPos != currentTile.Y) &&
                                (Singleton.Instance.spriteMove[xPos, yPos] == 0 && Singleton.Instance.panelBoundary[xPos, yPos] == 1
                                && Singleton.Instance.panelStage[xPos, yPos] <= 1))
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
                            if (Singleton.Instance.spriteMove[i, j] == 4)
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
                                        new Vector2((TILESIZEX * j * 2) + (screenStageX + 10),
                                            (TILESIZEY * i * 2) + (screenStageY - 140)),
                                        scale);
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
