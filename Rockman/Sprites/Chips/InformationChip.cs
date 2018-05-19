using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Rockman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rockman.Sprites.Chips
{
    class InformationChip : Chip
    {
        public Dictionary<string, string> chipInformationDict = new Dictionary<string, string>()
        {
            {"Cannon",  "Cannon attack \n\nto 1 enemy" },
            {"HiCannon",  "Cannon attack \n\nto 1 enemy" },
            {"MegaCannon",  "Cannon attack \n\nto 1 enemy" },
            {"AirShot",  "Knock enemy \n\nback 1 square" },
            {"SpreadGun1",  "Spreads damage \n\nto adj panels" },
            {"SpreadGun2",  "Spreads damage \n\nto adj panels" },
            {"SpreadGun3",  "Spreads damage \n\nto adj panels" },
            {"MiniBomb",  "Throws a\n\n MiniBomb" },
            {"BigBomb",  "Throws a\n\n 9 panel bomb" },
            {"EnergyBomb",  "Throws a bomb \n\nattack 3 times" },
            {"MegaEnergyBomb",  "Throws a bomb \n\nattack 3 times" },
            {"BugBomb",  "Throws a BugBomb \n\nmake panels" },
            {"SearchBomb1",  "Throws a bomb \n\nat the enemy" },
            {"SearchBomb2",  "Throws a bomb \n\nat the enemy" },
            {"SearchBomb3",  "Throws a bomb \n\nat the enemy" },
            {"CannonBall",  "Throws a bomb \n\nbreaks panel" },
            {"BlackBomb",  "Throws a bomb" },
            {"Recovery10",  "Recovers 10HP" },
            {"Recovery30",  "Recovers 30HP" },
            {"Recovery50",  "Recovers 50HP" },
            {"Recovery80",  "Recovers 80HP" },
            {"Recovery120",  "Recovers 120HP" },
            {"Recovery150",  "Recovers 150HP" },
            {"Recovery200",  "Recovers 200HP" },
            {"Recovery300",  "Recovers 300HP" },
            {"Barrier",  "Nullifies 10 HP \n\nof damage" },
            {"Barrier100",  "Nullifies 100 HP \n\nof damage" },
            {"Barrier200",  "Nullifies 200 HP \n\nof damage" },
            {"PanelReturn",  "Fix your \n\nall panels" },
            {"HolyPanel",  "Creates a HolyPanel\n\n in own panel" },
            {"CrackOut",  "Destroys 1 panel\n\n in front" },
            {"DoubleCrack",  "Destroys 2 panel\n\n in front" },
            {"TripleCrack",  "Destroys 3 panel\n\n in front" },
            {"DreamAura",  "Repels all attack\n\n under 200" },
            {"Sanctuary",  "Change all own \n\npanel to holy" },
            {"DarkCannon",  "Cannon attack \n\nto 1 enemy\n\n Status HPBug" },
            {"DarkBomb",  "Throws a\n\n 9 panel bomb\n\n Status HPBug" },
            {"DarkSpread",  "Spreads damage \n\nto adj panels\n\n Status HPBug" },
            {"DarkRecovery",  "Recovers 1000HP\n\n Status HPBug" },
            {"DarkStage",  "Turns opponents \n\narea into Swamp\n\n Status HPBug" },
            {"FinalGun",  "Charges up\n\n then uses chip\n\n Shoots 12 times" },
            {"JaneRiver",  "Jane Charges up\n\n freeze far \n\n 3x3 panels" },
            {"CherprangRiver",  "Cherprang Charges up\n\n freeze far \n\n 3x3 panels" },
            {"BlackEndGalaxy",  "Attack all \n\nenemies with \n\nBlackAce Sword" },
            {"BlackAce",  "Change to \n\nBlackAce" },
        };
        private bool _isHidden = false;

        public InformationChip(Dictionary<string, Animation> animations)
            : base(animations)
        {
        }
        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            switch (Singleton.Instance.CurrentGameState)
            {
                case Singleton.GameState.GameCustomScreen:
                    if (!_isHidden && Singleton.Instance.CurrentKey.IsKeyDown(Keys.N) && Singleton.Instance.PreviousKey.IsKeyUp(Keys.N))
                    {
                        _animationManager.Play(_animations["Close"]);
                        _isHidden = true;
                    }
                    else if(_isHidden && Singleton.Instance.CurrentKey.IsKeyDown(Keys.N) && Singleton.Instance.PreviousKey.IsKeyUp(Keys.N))
                    {
                        _animationManager.Play(_animations["Open"]);
                        _isHidden = false;
                    }
                    _animationManager.Update(gameTime);
                    break;
                case Singleton.GameState.GamePlaying:
                    _isHidden = false;
                    break;
                case Singleton.GameState.GameClear:
                    _isHidden = false;
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
                        _animationManager.Draw(spriteBatch,Position, scale);
                        if (!_isHidden && chipInformationDict.ContainsKey(chipCustomImg[Singleton.Instance.currentChipSelect.X]))
                        {
                            //textInformation
                            spriteBatch.DrawString(Singleton.Instance._font, chipInformationDict[chipCustomImg[Singleton.Instance.currentChipSelect.X]],
                            new Vector2(Position.X + 13, Position.Y + 44), Color.WhiteSmoke, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                        }
                    }
                    break;
            }
            base.Draw(spriteBatch);
        }
    }
}
