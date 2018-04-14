using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Rockman;

namespace Rockman.Sprites
{
    class PanelSprite : Sprite
    {
        Rectangle destRectPanel, sourceRectPanel;
        int[,] areaCracked = new int[3, 10]
        {
            { 0,0,0,0,0,0,0,0,0,0},
            { 0,0,0,0,0,0,0,0,0,0},
            { 0,0,0,0,0,0,0,0,0,0},
        };


        public PanelSprite(Texture2D[] texture)
            : base(texture)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {

            base.Update(gameTime, sprites);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    //areaCracked
                    if (Singleton.Instance.panelStage[i, j] == 1 && Singleton.Instance.spriteMove[i, j] > 0)
                    {
                        areaCracked[i, j] = 1;
                    }
                    else if (areaCracked[i, j] == 1)
                    {
                        Singleton.Instance.panelStage[i, j] = 2;
                        areaCracked[i, j] = 0;
                    }
                    //rectPanel
                    destRectPanel = new Rectangle((TILESIZE * j * (int)scale) + screenStageX, (24 * i * (int)scale) + screenStageY, 40 * (int)scale, 24 * (int)scale);
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
                        destRectPanel = new Rectangle((TILESIZE * j * (int)scale) + screenStageX, (24 * i * (int)scale) + screenStageY, 40 * (int)scale, 30 * (int)scale);
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
                        destRectPanel = new Rectangle((TILESIZE * j * (int)scale) + screenStageX, (24 * i * (int)scale) + screenStageY, 40 * (int)scale, 30 * (int)scale);
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
                        destRectPanel = new Rectangle((TILESIZE * j * (int)scale) + screenStageX, (24 * i * (int)scale) + screenStageY, 40 * (int)scale, 24 * (int)scale);
                        spriteBatch.Draw(_texture[0], destRectPanel, new Rectangle(320, 0, 40, 24), Color.White);
                    }
                }
            }
            base.Draw(spriteBatch);
        }
    }
}
