﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Rockman.Models;

namespace Rockman.Sprites
{
    class MettonSprite : Sprite
    {
        private float _timer, _atkTime;
        public int HP, Attack;
        public Point currentTile;
        public static Random random = new Random();

        float delay = 50f, drawAtkTime;
        int atkFrames = 0;

        Rectangle destRectMetAtk, sourceRectMetAtk;

        public MettonSprite(Texture2D[] _texture)
            : base(_texture)
        {
        }

        //public MettonSprite(Dictionary<string, Animation> animations)
        //    : base(animations)
        //{
        //}
        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            //Singleton.Instance.spriteHP[currentTile.X, currentTile.Y] = HP;
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            _atkTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            drawAtkTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            //_animationManager.Play(_animations["MettonAtk"]);
            if (Singleton.Instance.spriteMove[currentTile.X, currentTile.Y] == 2)
            {
                //checkHP
                if (Singleton.Instance.spriteHP[currentTile.X, currentTile.Y] <= 0)
                {
                    Singleton.Instance.soundEffects[5].Play();
                    Singleton.Instance.spriteMove[currentTile.X, currentTile.Y] = 0;
                }
                //defaultSprite
                if(atkFrames == 0)
                {
                    sourceRectMetAtk = new Rectangle(0, 0, 50, 60);
                }
                //mettonAtk
                if (_atkTime > 3.0)
                {
                    if (drawAtkTime >= delay)
                    {
                        if (atkFrames >= 14)
                        {
                            atkFrames = 0;
                            _atkTime = 0;
                            _timer = 0;
                        }
                        else
                        {
                            if (atkFrames == 10 && Singleton.Instance.panelStage[currentTile.X, currentTile.Y - 1] < 2)
                            {
                                Singleton.Instance.virusAttack[currentTile.X, currentTile.Y - 1] = 2;
                            }
                            atkFrames++;
                        }
                        sourceRectMetAtk = new Rectangle((50 * atkFrames), 0, 50, 60);
                        drawAtkTime = 0;
                    }
                }
                //movement
                else if (_timer > 1.2)
                {
                    int xPos = random.Next(0, 3);
                    int yPos = random.Next(6, 10);
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
            base.Update(gameTime, sprites);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //rectAtk
            destRectMetAtk = new Rectangle((TILESIZE * currentTile.Y * 2) + (screenStageX - 12), (24 * currentTile.X * 2) + (screenStageY - 85), 50 * (int)scale, 60 * (int)scale);
            //Position = new Vector2((TILESIZE * currentTile.Y * 2) + (screenStageX - 12), (24 * currentTile.X * 2) + (screenStageY - 85));
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    //drawMetton
                    if (Singleton.Instance.spriteMove[i, j] == 2)
                    {
                        currentTile = new Point(i, j);
                        //if (_animationManager == null)
                        //{
                        //    spriteBatch.Draw(_texture[0], Position, Viewport, Color.White);
                        //}
                        //else {
                        //    _animationManager.Draw(spriteBatch, Position);
                        //}
                        spriteBatch.Draw(_texture[0], destRectMetAtk, sourceRectMetAtk, Color.White);
                        //drawHP
                        spriteBatch.DrawString(Singleton.Instance._font, string.Format("{0}", (Singleton.Instance.spriteHP[i, j])), new Vector2((TILESIZE * j * scale) + (screenStageX + 16 + 10), (24 * i * scale) + (screenStageY - 5 + 45)), Color.White);
                    }
                }
            }
            base.Draw(spriteBatch);
        }
    }
}
