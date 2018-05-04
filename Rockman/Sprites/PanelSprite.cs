using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Rockman;

namespace Rockman.Sprites
{
    class PanelSprite : Sprite
    {
        Rectangle destRectPanel;
        
        private float[,] timePanelReturn = new float[3, 10]
        {
            { 0,0,0,0,0,0,0,0,0,0},
            { 0,0,0,0,0,0,0,0,0,0},
            { 0,0,0,0,0,0,0,0,0,0},
        };
        private float timeToReturn = 10f;


        public PanelSprite(Texture2D[] texture)
            : base(texture)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    switch (Singleton.Instance.CurrentGameState)
                    {
                        case Singleton.GameState.GamePlaying:
                            //areaCracked
                            if (Singleton.Instance.panelStage[i, j] == 1 && 
                                (Singleton.Instance.spriteMove[i, j] > 0 || Singleton.Instance.playerMove[i, j] > 0))
                            {
                                Singleton.Instance.areaCracked[i, j] = 1;
                            }
                            else if (Singleton.Instance.areaCracked[i, j] == 1)
                            {
                                SoundEffects["PanelCrack"].Volume = Singleton.Instance.MasterSFXVolume;
                                SoundEffects["PanelCrack"].Play();
                                Singleton.Instance.panelStage[i, j] = 2;
                                Singleton.Instance.areaCracked[i, j] = 0;
                                //startCountTime
                                timePanelReturn[i, j] += (float)gameTime.ElapsedGameTime.TotalSeconds;
                            }
                            //panelReturn
                            if (timePanelReturn[i, j] > 0 && timePanelReturn[i, j] < timeToReturn)
                            {
                                timePanelReturn[i, j] += (float)gameTime.ElapsedGameTime.TotalSeconds;
                            }
                            if (timePanelReturn[i, j] >= timeToReturn)
                            {
                                Singleton.Instance.panelStage[i, j] = 0;
                                timePanelReturn[i, j] = 0;
                            }
                            break;
                    }
                }
            }
            base.Update(gameTime, sprites);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    //rectPanel
                    destRectPanel = new Rectangle((40 * j * (int)scale) + screenStageX, (24 * i * (int)scale) + screenStageY, 40 * (int)scale, 24 * (int)scale);
                    //defaultSprite
                    if (Singleton.Instance.panelBoundary[i, j] == 0 && i == 0)
                    {
                        switch (Singleton.Instance.panelStage[i, j])
                        {
                            case 0:
                                spriteBatch.Draw(_texture[0], destRectPanel, new Rectangle(0,0,40,24), Color.White);
                                break;
                            case 1:
                                spriteBatch.Draw(_texture[0], destRectPanel, new Rectangle(80, 0, 40, 24), Color.White);
                                break;
                            case 2:
                                spriteBatch.Draw(_texture[0], destRectPanel, new Rectangle(160, 0, 40, 24), Color.White);
                                break;
                            case 3:
                                spriteBatch.Draw(_texture[0], destRectPanel, new Rectangle(240, 0, 40, 24), Color.White);
                                break;
                        }
                    }
                    else if (Singleton.Instance.panelBoundary[i, j] == 0 && i == 1)
                    {
                        switch (Singleton.Instance.panelStage[i, j])
                        {
                            case 0:
                                spriteBatch.Draw(_texture[0], destRectPanel, new Rectangle(0, 24, 40, 24), Color.White);
                                break;
                            case 1:
                                spriteBatch.Draw(_texture[0], destRectPanel, new Rectangle(80, 24, 40, 24), Color.White);
                                break;
                            case 2:
                                spriteBatch.Draw(_texture[0], destRectPanel, new Rectangle(160, 24, 40, 24), Color.White);
                                break;
                            case 3:
                                spriteBatch.Draw(_texture[0], destRectPanel, new Rectangle(240, 24, 40, 24), Color.White);
                                break;
                        }
                    }
                    else if (Singleton.Instance.panelBoundary[i, j] == 0 && i == 2)
                    {
                        destRectPanel = new Rectangle((40 * j * (int)scale) + screenStageX, (24 * i * (int)scale) + screenStageY, 40 * (int)scale, 30 * (int)scale);
                        switch (Singleton.Instance.panelStage[i, j])
                        {
                            case 0:
                                spriteBatch.Draw(_texture[0], destRectPanel, new Rectangle(0, 48, 40, 30), Color.White);
                                break;
                            case 1:
                                spriteBatch.Draw(_texture[0], destRectPanel, new Rectangle(80, 48, 40, 30), Color.White);
                                break;
                            case 2:
                                spriteBatch.Draw(_texture[0], destRectPanel, new Rectangle(160, 48, 40, 30), Color.White);
                                break;
                            case 3:
                                spriteBatch.Draw(_texture[0], destRectPanel, new Rectangle(240, 48, 40, 30), Color.White);
                                break;
                        }
                    }
                    else if (Singleton.Instance.panelBoundary[i, j] == 1 && i == 0)
                    {
                        switch (Singleton.Instance.panelStage[i, j])
                        {
                            case 0:
                                spriteBatch.Draw(_texture[0], destRectPanel, new Rectangle(40, 0, 40, 24), Color.White);
                                break;
                            case 1:
                                spriteBatch.Draw(_texture[0], destRectPanel, new Rectangle(120, 0, 40, 24), Color.White);
                                break;
                            case 2:
                                spriteBatch.Draw(_texture[0], destRectPanel, new Rectangle(200, 0, 40, 24), Color.White);
                                break;
                            case 3:
                                spriteBatch.Draw(_texture[0], destRectPanel, new Rectangle(280, 0, 40, 24), Color.White);
                                break;
                        }
                    }
                    else if (Singleton.Instance.panelBoundary[i, j] == 1 && i == 1)
                    {
                        switch (Singleton.Instance.panelStage[i, j])
                        {
                            case 0:
                                spriteBatch.Draw(_texture[0], destRectPanel, new Rectangle(40, 24, 40, 24), Color.White);
                                break;
                            case 1:
                                spriteBatch.Draw(_texture[0], destRectPanel, new Rectangle(120, 24, 40, 24), Color.White);
                                break;
                            case 2:
                                spriteBatch.Draw(_texture[0], destRectPanel, new Rectangle(200, 24, 40, 24), Color.White);
                                break;
                            case 3:
                                spriteBatch.Draw(_texture[0], destRectPanel, new Rectangle(280, 24, 40, 24), Color.White);
                                break;
                        }
                    }
                    else if (Singleton.Instance.panelBoundary[i, j] == 1 && i == 2)
                    {
                        destRectPanel = new Rectangle((40 * j * (int)scale) + screenStageX, (24 * i * (int)scale) + screenStageY, 40 * (int)scale, 30 * (int)scale);
                        switch (Singleton.Instance.panelStage[i, j])
                        {
                            case 0:
                                spriteBatch.Draw(_texture[0], destRectPanel, new Rectangle(40, 48, 40, 30), Color.White);
                                break;
                            case 1:
                                spriteBatch.Draw(_texture[0], destRectPanel, new Rectangle(120, 48, 40, 30), Color.White);
                                break;
                            case 2:
                                spriteBatch.Draw(_texture[0], destRectPanel, new Rectangle(200, 48, 40, 30), Color.White);
                                break;
                            case 3:
                                spriteBatch.Draw(_texture[0], destRectPanel, new Rectangle(280, 48, 40, 30), Color.White);
                                break;
                        }
                    }
                    //virusAtkPanel
                    if (Singleton.Instance.virusAttack[i, j] == 2)
                    {
                        destRectPanel = new Rectangle((40 * j * (int)scale) + screenStageX, (24 * i * (int)scale) + screenStageY, 40 * (int)scale, 24 * (int)scale);
                        spriteBatch.Draw(_texture[0], destRectPanel, new Rectangle(320, 0, 40, 24), Color.White);
                    }
                }
            }
            base.Draw(spriteBatch);
        }
    }
}
