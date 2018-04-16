using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rockman.Models;

namespace Rockman.Sprites.Chips
{
    class Recovery : Chip
    {
        Dictionary<string, Rectangle> rectChipRecovImg = new Dictionary<string, Rectangle>()
        {
            {"Recovery10",  new Rectangle(0, 145, 56, 47) },
            {"Recovery30",  new Rectangle(56, 145, 56, 47) },
            {"Recovery50",  new Rectangle(56*2, 145, 56, 47) },
            {"Recovery80",  new Rectangle(56*3, 145, 56, 47) },
            {"Recovery120",  new Rectangle(56*4, 145, 56, 47) },
            {"Recovery150",  new Rectangle(56*5, 145, 56, 47) },
            {"Recovery200",  new Rectangle(56*6, 145, 56, 47) },
            {"Recovery300",  new Rectangle(56*7, 145, 56, 47) },
        };

        public Recovery(Texture2D[] texture) 
            : base(texture)
        {
            _texture = texture;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.GameCustomScreen:
            
                    break;
            }
            base.Update(gameTime, sprites);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.GameCustomScreen:
                    if (rectChipRecovImg.ContainsKey(Singleton.Instance.chipCustomSelect[Singleton.Instance.currentChipSelect.X]))
                    {
                        spriteBatch.Draw(_texture[0], new Vector2(16 * 3, 24 * 3 - 2),
                            rectChipRecovImg[Singleton.Instance.chipCustomSelect[Singleton.Instance.currentChipSelect.X]],
                            Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                    }
                    break;
            }
            base.Draw(spriteBatch);
        }
    }
}
