using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rockman.Models;


namespace Rockman.Sprites.Screens
{
    class CustomScreen : Screen
    {
        public CustomScreen(Dictionary<string, Animation> animations) :
            base(animations)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.GameCustomScreen:
                    switch (CurrentCustomState)
                    {
                        case CustomState.Open:
                            SoundEffects["Custom"].Volume = Singleton.Instance.MasterSFXVolume;
                            SoundEffects["Custom"].Play();
                            //insertChipFolderToCustom
                            for (int i = 0; i < 5; i++)
                            {
                                if (Singleton.Instance.chipCustomSelect[i] == "" && Singleton.Instance.nextChipFolder.Count != 0)
                                {
                                    Singleton.Instance.chipCustomSelect[i] = Singleton.Instance.nextChipFolder.Dequeue();
                                }
                            }
                            setState(CustomState.Wait);
                            break;
                        case CustomState.Close:
                            if (Singleton.Instance.newTurnCustom)
                            {
                                Singleton.Instance.newTurnCustom = false;
                                setState(CustomState.Open);
                            }
                            break;
                    }
                    _animationManager.Update(gameTime);
                    break;
            }
            base.Update(gameTime, sprites);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.GameCustomScreen:
                        if (_animationManager == null)
                        {
                            spriteBatch.Draw(_texture[0],
                                            Position,
                                            Viewport,
                                            Color.White);
                        }
                        else
                        {
                            _animationManager.Draw(spriteBatch, Position, scale);
                        }
                    break;
            }
            base.Draw(spriteBatch);
        }
    }
}
