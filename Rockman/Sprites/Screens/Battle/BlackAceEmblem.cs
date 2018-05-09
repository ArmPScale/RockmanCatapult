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
    class BlackAceEmblem : Screen
    {
        public BlackAceEmblem(Dictionary<string, Animation> animations)
            : base(animations)
        {
        }
        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.GameCustomScreen:
                    if (Singleton.Instance.chipCustomSelect[6] == "BlackAce")
                    {
                        _animationManager.Play(_animations["Available"]);
                    }
                    else
                    {
                        _animationManager.Play(_animations["Disable"]);
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
                        _animationManager.Draw(spriteBatch,Position, 1f);
                    }
                    break;
            }
            base.Draw(spriteBatch);
        }
    }
}
