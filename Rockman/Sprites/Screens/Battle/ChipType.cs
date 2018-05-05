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
    class ChipType : Screen
    {
        public ChipType(Dictionary<string, Animation> animations) :
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
                            switch (Singleton.Instance.chipType)
                            {
                                case "Fire":
                                    _animationManager.Play(_animations["Fire"]);
                                    break;
                                case "Aqua":
                                    _animationManager.Play(_animations["Aqua"]);
                                    break;
                                case "Elec":
                                    _animationManager.Play(_animations["Elec"]);
                                    break;
                                case "Wood":
                                    _animationManager.Play(_animations["Wood"]);
                                    break;
                                case "Sword":
                                    _animationManager.Play(_animations["Sword"]);
                                    break;
                                case "Wind":
                                    _animationManager.Play(_animations["Wind"]);
                                    break;
                                case "Scope":
                                    _animationManager.Play(_animations["Scope"]);
                                    break;
                                case "Box":
                                    _animationManager.Play(_animations["Box"]);
                                    break;
                                case "Number":
                                    _animationManager.Play(_animations["Number"]);
                                    break;
                                case "Break":
                                    _animationManager.Play(_animations["Break"]);
                                    break;
                                default:
                                    _animationManager.Play(_animations["Normal"]);
                                    break;
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
