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
    class CustomWindow : Screen
    {
        public CustomWindow(Dictionary<string, Animation> animations) :
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
                        case CustomState.Wait:
                            if (Singleton.Instance.chipClass == "Mega")
                            {
                                _animationManager.Play(_animations["Mega"]);
                            }
                            else if (Singleton.Instance.chipClass == "Giga")
                            {
                                _animationManager.Play(_animations["Giga"]);
                            }
                            else if (Singleton.Instance.chipClass == "Dark")
                            {
                                _animationManager.Play(_animations["Dark"]);
                            }
                            else
                            {
                                _animationManager.Play(_animations["Standard"]);
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
            switch (Singleton.Instance.CurrentScreenState)
            {
                case Singleton.ScreenState.StoryMode:
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
                    break;
            }
            base.Draw(spriteBatch);
        }
    }
}
