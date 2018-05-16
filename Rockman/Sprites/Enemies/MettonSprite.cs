using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Rockman.Models;

namespace Rockman.Sprites
{
    class MettonSprite : Enemy
    {
        private float _timer, _atkTime;
        public Point currentTile;
        public static Random random = new Random();

        public MettonSprite(Texture2D[] _texture)
            : base(_texture)
        {
        }

        public MettonSprite(Dictionary<string, Animation> animations)
            : base(animations)
        {
        }
        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.GameEnemyAppear:
                    _animationManager.Play(_animations["Alive"]);
                    _timer = 0; _atkTime = 0;
                    break;
                case Singleton.GameState.GamePlaying:
                    _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    _atkTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (Singleton.Instance.spriteMove[currentTile.X, currentTile.Y] == 2)
                    {
                        //checkHP
                        if (Singleton.Instance.spriteHP[currentTile.X, currentTile.Y] <= 0)
                        {
                            SoundEffects["Explosion"].Volume = Singleton.Instance.MasterSFXVolume;
                            SoundEffects["Explosion"].Play();
                            for (int i = 0; i < 3; i++)
                            {
                                for (int j = 0; j < 10; j++)
                                {
                                    if (Singleton.Instance.panelYellow[i, j] == 2)
                                    {
                                        Singleton.Instance.panelYellow[i, j] = 0;
                                        Singleton.Instance.virusAttack[i, j] = 0;
                                    }
                                }
                            }
                            Singleton.Instance.spriteMove[currentTile.X, currentTile.Y] = 0;
                        }
                        //mettonAtk
                        if (_atkTime > 3.0f)
                        {
                            _animationManager.Play(_animations["Attack"]);
                            if (_atkTime > 3.9f && _atkTime < 3.95f)
                            {
                                Singleton.Instance.panelYellow[currentTile.X, currentTile.Y - 1] = 2;
                                Singleton.Instance.virusAttack[currentTile.X, currentTile.Y - 1] = 2;
                            }
                            else if (_atkTime > 4.3f)
                            {
                                _timer = 0f; _atkTime = 0f;
                                _animationManager.Play(_animations["Alive"]);
                            }
                        }
                        //movement
                        else if (_timer > 1.2)
                        {
                            int xPos = random.Next(0, 3);
                            int yPos = random.Next(5, 10);
                            if ((xPos != currentTile.X || yPos != currentTile.Y) &&
                                (Singleton.Instance.spriteMove[xPos, yPos] == 0 && Singleton.Instance.panelBoundary[xPos, yPos] == 1
                                && Singleton.Instance.panelStage[xPos, yPos] <= 1))
                            {
                                //Singleton.Instance.spriteMove[xPos, currentTile.Y] = Singleton.Instance.spriteMove[currentTile.X, currentTile.Y];
                                //Singleton.Instance.spriteHP[xPos, currentTile.Y] = Singleton.Instance.spriteHP[currentTile.X, currentTile.Y];
                                //Singleton.Instance.spriteMove[currentTile.X, currentTile.Y] = 0;
                                //Singleton.Instance.spriteHP[currentTile.X, currentTile.Y] = 0;
                                Singleton.Instance.spriteMove[xPos, yPos] = Singleton.Instance.spriteMove[currentTile.X, currentTile.Y];
                                Singleton.Instance.spriteHP[xPos, yPos] = Singleton.Instance.spriteHP[currentTile.X, currentTile.Y];
                                Singleton.Instance.spriteMove[currentTile.X, currentTile.Y] = 0;
                                Singleton.Instance.spriteHP[currentTile.X, currentTile.Y] = 0;
                            }
                            _timer = 0;
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
                            //drawMeeton
                            if (Singleton.Instance.spriteMove[i, j] == 2)
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
                                        new Vector2((TILESIZEX * j * 2) + (screenStageX - 20),
                                            (TILESIZEY * i * 2) + (screenStageY - 130)),
                                        scale);
                                }
                                //drawHP
                                if (Singleton.Instance.spriteHP[i, j] > 0)
                                {
                                    spriteBatch.DrawString(Singleton.Instance._font, string.Format("{0}", (Singleton.Instance.spriteHP[i, j])), 
                                        new Vector2((TILESIZEX * currentTile.Y * 2) + (screenStageX + TILESIZEY), (TILESIZEY * currentTile.X * 2) + (screenStageY + TILESIZEX - 10)),
                                        Color.White, 0f, Vector2.Zero, 1.3f, SpriteEffects.None, 0f);
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
