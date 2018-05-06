using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Rockman;

namespace Rockman.Sprites
{
    class PanelSprite : Sprite
    {
        public Rectangle destRectPanel;
        private float _poisonCoolDown = 0f;
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
            switch (Singleton.Instance.CurrentScreenState)
            {
                case Singleton.ScreenState.StoryMode:
                    _poisonCoolDown += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 10; j++)
                        {
                            switch (Singleton.Instance.CurrentGameState)
                            {
                                case Singleton.GameState.GamePlaying:
                                    //panelElementCracked
                                    if (Singleton.Instance.panelStage[i, j] > 0)
                                    {
                                        Singleton.Instance.panelElement[i, j] = 0;
                                    }
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
                                    //panelElementAbility
                                    //3.magmaPanel
                                    if (Singleton.Instance.panelElement[i, j] == 3 &&
                                        (Singleton.Instance.spriteMove[i, j] > 0 || Singleton.Instance.playerMove[i, j] > 0))
                                    {
                                        if (Singleton.Instance.panelElement[Singleton.Instance.currentPlayerPoint.X, Singleton.Instance.currentPlayerPoint.Y] == 3)
                                        {
                                            Singleton.Instance.isDamaged = true;
                                            Singleton.Instance.enemyAtk += 20;
                                            Singleton.Instance.panelElement[Singleton.Instance.currentPlayerPoint.X, Singleton.Instance.currentPlayerPoint.Y] = 0;
                                        }
                                        else if (Singleton.Instance.spriteMove[i, j] > 0)
                                        {
                                            for (int magmaRow = 0; magmaRow < 3; magmaRow++)
                                            {
                                                for (int magmaColumn = 0; magmaColumn < 10; magmaColumn++)
                                                {
                                                    if (Singleton.Instance.spriteMove[magmaRow, magmaColumn] > 0)
                                                    {
                                                        Singleton.Instance.spriteHP[magmaRow, magmaColumn] -= 20;
                                                        Singleton.Instance.panelElement[magmaRow, magmaColumn] = 0;
                                                    }
                                                }
                                            }
                                        }

                                    }
                                    //4.poisonPanel
                                    else if (Singleton.Instance.panelElement[i, j] == 4 &&
                                        (Singleton.Instance.spriteMove[i, j] > 0 || Singleton.Instance.playerMove[i, j] > 0))
                                    {
                                        if (_poisonCoolDown > 0.3f && Singleton.Instance.HeroBarrier == 0 &&
                                            Singleton.Instance.panelElement[Singleton.Instance.currentPlayerPoint.X, Singleton.Instance.currentPlayerPoint.Y] == 4)
                                        {
                                            Singleton.Instance.isDamaged = true;
                                            Singleton.Instance.enemyAtk = 1;
                                            _poisonCoolDown = 0f;
                                        }
                                        else if (_poisonCoolDown > 0.3f)
                                        {
                                            for (int poisonRow = 0; poisonRow < 3; poisonRow++)
                                            {
                                                for (int poisonColumn = 0; poisonColumn < 10; poisonColumn++)
                                                {
                                                    Singleton.Instance.spriteHP[poisonRow, poisonColumn] -= 1;
                                                }
                                            }
                                            _poisonCoolDown = 0f;
                                        }
                                    }
                                    break;
                            }
                        }
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
                            //rectPanel
                            destRectPanel = new Rectangle((40 * j * (int)scale) + screenStageX, (24 * i * (int)scale) + screenStageY, 40 * (int)scale, 24 * (int)scale);
                            //defaultSprite
                            if (Singleton.Instance.panelBoundary[i, j] == 0 && i == 0)
                            {
                                switch (Singleton.Instance.panelStage[i, j])
                                {
                                    case 0:
                                        spriteBatch.Draw(_texture[0], destRectPanel, new Rectangle(0, 0, 40, 24), Color.White);
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

                            //panelElement
                            // -> 1.holyPanel
                            // -> 2.icePanel
                            // -> 3.magmaPanel
                            // -> 4.poisonPanel
                            if (Singleton.Instance.panelElement[i, j] != 0 && i == 0)
                            {
                                destRectPanel = new Rectangle((40 * j * (int)scale) + screenStageX + 9, (24 * i * (int)scale) + screenStageY + 8, 34 * (int)scale, 20 * (int)scale);
                                switch (Singleton.Instance.panelElement[i, j])
                                {
                                    case 1:
                                        spriteBatch.Draw(_texture[0], destRectPanel, new Rectangle(0, 162, 34, 20), Color.White);
                                        break;
                                    case 2:
                                        spriteBatch.Draw(_texture[0], destRectPanel, new Rectangle(0, 88, 34, 19), Color.White);
                                        break;
                                    case 3:
                                        spriteBatch.Draw(_texture[0], destRectPanel, new Rectangle(200, 240, 34, 19), Color.White);
                                        break;
                                    case 4:
                                        spriteBatch.Draw(_texture[0], destRectPanel, new Rectangle(200, 400, 34, 19), Color.White);
                                        break;
                                }
                            }
                            else if (Singleton.Instance.panelElement[i, j] != 0 && i == 1)
                            {
                                destRectPanel = new Rectangle((40 * j * (int)scale) + screenStageX + 9, (24 * i * (int)scale) + screenStageY + 8, 34 * (int)scale, 20 * (int)scale);
                                switch (Singleton.Instance.panelElement[i, j])
                                {
                                    case 1:
                                        spriteBatch.Draw(_texture[0], destRectPanel, new Rectangle(0, 186, 34, 20), Color.White);
                                        break;
                                    case 2:
                                        spriteBatch.Draw(_texture[0], destRectPanel, new Rectangle(0, 112, 34, 19), Color.White);
                                        break;
                                    case 3:
                                        spriteBatch.Draw(_texture[0], destRectPanel, new Rectangle(200, 264, 34, 19), Color.White);
                                        break;
                                    case 4:
                                        spriteBatch.Draw(_texture[0], destRectPanel, new Rectangle(200, 424, 34, 19), Color.White);
                                        break;
                                }
                            }
                            else if (Singleton.Instance.panelElement[i, j] != 0 && i == 2)
                            {
                                destRectPanel = new Rectangle((40 * j * (int)scale) + screenStageX + 9, (30 * i * (int)scale) + screenStageY - 29, 34 * (int)scale, 20 * (int)scale);
                                switch (Singleton.Instance.panelElement[i, j])
                                {
                                    case 1:
                                        spriteBatch.Draw(_texture[0], destRectPanel, new Rectangle(0, 210, 34, 20), Color.White);
                                        break;
                                    case 2:
                                        spriteBatch.Draw(_texture[0], destRectPanel, new Rectangle(0, 136, 34, 19), Color.White);
                                        break;
                                    case 3:
                                        spriteBatch.Draw(_texture[0], destRectPanel, new Rectangle(200, 288, 34, 19), Color.White);
                                        break;
                                    case 4:
                                        spriteBatch.Draw(_texture[0], destRectPanel, new Rectangle(200, 448, 34, 19), Color.White);
                                        break;
                                }
                            }

                            //virusAtkPanel
                            if (Singleton.Instance.virusAttack[i, j] > 0)
                            {
                                destRectPanel = new Rectangle((40 * j * (int)scale) + screenStageX, (24 * i * (int)scale) + screenStageY, 40 * (int)scale, 24 * (int)scale);
                                spriteBatch.Draw(_texture[0], destRectPanel, new Rectangle(320, 0, 40, 24), Color.White);
                            }
                        }
                    }
                    break;
            }
            base.Draw(spriteBatch);
        }
    }
}
