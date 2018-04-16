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
    class ChipIcon : Chip
    {
        Dictionary<string, Rectangle> rectChipIconImg = new Dictionary<string, Rectangle>()
        {
            {"IconRecovery10",  new Rectangle(0, 145, 56, 47) },
            {"IconRecovery30",  new Rectangle(56, 145, 56, 47) },
            {"IconRecovery50",  new Rectangle(56*2, 145, 56, 47) },
            {"IconRecovery80",  new Rectangle(56*3, 145, 56, 47) },
            {"IconRecovery120",  new Rectangle(56*4, 145, 56, 47) },
            {"IconRecovery150",  new Rectangle(56*5, 145, 56, 47) },
            {"IconRecovery200",  new Rectangle(56*6, 145, 56, 47) },
            {"IconRecovery300",  new Rectangle(56*7, 145, 56, 47) },
        };

        public ChipIcon(Texture2D[] texture)
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
            for (int i = 0; i < 3; i++)
            {
                switch (Singleton.Instance.CurrentGameState)
                {
                    case Singleton.GameState.GameCustomScreen:
                        if (rectChipIconImg.ContainsKey(Singleton.Instance.chipCustomSelect[Singleton.Instance.currentChipSelect.X]))
                        {
                            spriteBatch.Draw(_texture[0], new Vector2(15 + (48 * i), 100 * 3),
                                rectChipIconImg[Singleton.Instance.chipCustomSelect[Singleton.Instance.currentChipSelect.X]],
                                Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                        }
                        break;
                }
            }
            base.Draw(spriteBatch);
        }
    }
}
