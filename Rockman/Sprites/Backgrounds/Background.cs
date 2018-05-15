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
    class Background : Sprite
    {
        public Color fade;
        public int alpha;
        public Dictionary<string, Rectangle> rectChipImg = new Dictionary<string, Rectangle>()
        {
            {"Cannon",  new Rectangle(0, 0, 56, 47) },
            {"HiCannon",  new Rectangle(56, 0, 56, 47) },
            {"MegaCannon",  new Rectangle(112, 0, 56, 47) },
            {"AirShot",  new Rectangle(168, 0, 56, 47) },
            {"SpreadGun1",  new Rectangle(392, 0, 56, 47) },
            {"SpreadGun2",  new Rectangle(448, 0, 56, 47) },
            {"SpreadGun3",  new Rectangle(504, 0, 56, 47) },
            {"MiniBomb",  new Rectangle(0, 48, 56, 47) },
            {"BigBomb",  new Rectangle(0 + (56*1), 48, 56, 47) },
            {"EnergyBomb",  new Rectangle(0 + (56*2), 48, 56, 47) },
            {"MegaEnergyBomb",  new Rectangle(0 + (56*3), 48, 56, 47) },
            {"BugBomb",  new Rectangle(0 + (56*4), 48, 56, 47) },
            {"SearchBomb1",  new Rectangle(0 + (56*5), 48, 56, 47) },
            {"SearchBomb2",  new Rectangle(0 + (56*6), 48, 56, 47) },
            {"SearchBomb3",  new Rectangle(0 + (56*7), 48, 56, 47) },
            {"CannonBall",  new Rectangle(0 + (56*8), 48, 56, 47) },
            {"BlackBomb",  new Rectangle(0 + (56*9), 48, 56, 47) },
            {"Recovery10",  new Rectangle(0, 145, 56, 47) },
            {"Recovery30",  new Rectangle(56, 145, 56, 47) },
            {"Recovery50",  new Rectangle(56*2, 145, 56, 47) },
            {"Recovery80",  new Rectangle(56*3, 145, 56, 47) },
            {"Recovery120",  new Rectangle(56*4, 145, 56, 47) },
            {"Recovery150",  new Rectangle(56*5, 145, 56, 47) },
            {"Recovery200",  new Rectangle(56*6, 145, 56, 47) },
            {"Recovery300",  new Rectangle(56*7, 145, 56, 47) },
            {"Barrier",  new Rectangle(224, 192, 56, 47) },
            {"Barrier100",  new Rectangle(224 + (56*1), 192, 56, 47) },
            {"Barrier200",  new Rectangle(224 + (56*2), 192, 56, 47) },
            {"PanelReturn",  new Rectangle(448, 144, 56, 47) },
            {"HolyPanel",  new Rectangle(448 + (56*1), 144, 56, 47) },
            {"Sanctuary",  new Rectangle(280, 288, 56, 47) },
            {"CrackOut",  new Rectangle(112, 240, 56, 47) },
            {"DoubleCrack",  new Rectangle(168, 240, 56, 47) },
            {"TripleCrack",  new Rectangle(224, 240, 56, 47) },
            {"DreamAura",  new Rectangle(56, 288, 56, 47) },
            {"DarkCannon",  new Rectangle(0, 336, 56, 47) },
            {"DarkBomb",  new Rectangle(56, 336, 56, 47) },
            {"DarkSpread",  new Rectangle(112, 336, 56, 47) },
            {"DarkRecovery",  new Rectangle(168, 336, 56, 47) },
            {"DarkStage",  new Rectangle(280, 336, 56, 47) },
            {"FinalGun",  new Rectangle(0, 384, 56, 47) },
        };
        public Dictionary<string, Rectangle> rectChipRiverImg = new Dictionary<string, Rectangle>()
        {
            {"CherprangRiver",  new Rectangle(0, 0, 168, 144) },
            {"JaneRiver",  new Rectangle(168, 0, 168, 144) },
        };
        public Dictionary<string, Rectangle> rectChipIconEXE6Img = new Dictionary<string, Rectangle>()
        {
            {"Cannon",  new Rectangle(22, 2, 14, 14) },
            {"HiCannon",  new Rectangle(22+(19*1), 2, 14, 14) },
            {"MegaCannon",  new Rectangle(22+(19*2), 2, 14, 14) },
            {"AirShot",  new Rectangle(79, 2, 14, 14) },
            {"SpreadGun1",  new Rectangle(174, 2, 14, 14) },
            {"SpreadGun2",  new Rectangle(174+(19*1), 2, 14, 14) },
            {"SpreadGun3",  new Rectangle(174+(19*2), 2, 14, 14) },
            {"MiniBomb",  new Rectangle(117, 56, 14, 14) },
            {"BigBomb",  new Rectangle(193, 218, 14, 14) },
            {"EnergyBomb",  new Rectangle(117+(19*1), 56, 14, 14) },
            {"MegaEnergyBomb",  new Rectangle(117+(19*2), 56, 14, 14) },
            {"BlackBomb",  new Rectangle(117+(19*6), 56, 14, 14) },
            {"BugBomb",  new Rectangle(60, 74, 14, 14) },
            {"Recovery10",  new Rectangle(193, 164, 14, 14) },
            {"Recovery30",  new Rectangle(212, 164, 14, 14) },
            {"Recovery50",  new Rectangle(231, 164, 14, 14) },
            {"Recovery80",  new Rectangle(250, 164, 14, 14) },
            {"Recovery120",  new Rectangle(192+(19*4), 164, 14, 14) },
            {"Recovery150",  new Rectangle(192+(19*5), 164, 14, 14) },
            {"Recovery200",  new Rectangle(3+(19*0), 164+18, 14, 14) },
            {"Recovery300",  new Rectangle(3+(19*1), 164+18, 14, 14) },
            {"PanelReturn",  new Rectangle(117, 182, 14, 14) },
            {"HolyPanel",  new Rectangle(117+(19*2), 182, 14, 14) },
            {"Sanctuary",  new Rectangle(117+(19*3), 182, 14, 14) },
            {"Barrier",  new Rectangle(41, 200, 14, 14) },
            {"Barrier100",  new Rectangle(41+(19*1), 200, 14, 14) },
            {"Barrier200",  new Rectangle(41+(19*2), 200, 14, 14) },
            {"DreamAura",  new Rectangle(41+(19*4), 200, 14, 14) },
            {"CherprangRiver",  new Rectangle(193, 254, 14, 14) },
            {"JaneRiver",  new Rectangle(193, 254, 14, 14) },
            {"BlackAce",  new Rectangle(136, 254, 14, 14) },
        };
        public Dictionary<string, Rectangle> rectChipIconEXE4Img = new Dictionary<string, Rectangle>()
        {
            {"CannonBall",  new Rectangle(256, 17, 14, 14) },
            {"SearchBomb1",  new Rectangle(144, 97, 14, 14) },
            {"SearchBomb2",  new Rectangle(144+(16*1), 97, 14, 14) },
            {"SearchBomb3",  new Rectangle(144+(16*2), 97, 14, 14) },
            {"CrackOut",  new Rectangle(240, 49, 14, 14) },
            {"DoubleCrack",  new Rectangle(240+(16*1), 49, 14, 14) },
            {"TripleCrack",  new Rectangle(240+(16*2), 49, 14, 14) },
            {"DarkCannon",  new Rectangle(128, 113, 14, 14) },
            {"DarkSpread",  new Rectangle(160, 113, 14, 14) },
            {"DarkBomb",  new Rectangle(176, 113, 14, 14) },
            {"DarkRecovery",  new Rectangle(224, 113, 14, 14) },
            {"DarkStage",  new Rectangle(240, 113, 14, 14) },
            {"FinalGun",  new Rectangle(240, 113, 14, 14) },

        };
        public Dictionary<string, string> chipClassDict = new Dictionary<string, string>()
        {
            {"Cannon",  "Standard" },
            {"HiCannon",  "Standard" },
            {"MegaCannon",  "Standard" },
            {"AirShot",  "Standard" },
            {"SpreadGun1",  "Standard" },
            {"SpreadGun2",  "Standard" },
            {"SpreadGun3",  "Standard" },
            {"MiniBomb",  "Standard" },
            {"BigBomb",  "Standard" },
            {"EnergyBomb",  "Standard" },
            {"MegaEnergyBomb",  "Standard" },
            {"BugBomb",  "Standard" },
            {"SearchBomb1",  "Standard" },
            {"SearchBomb2",  "Standard" },
            {"SearchBomb3",  "Standard" },
            {"CannonBall",  "Standard" },
            {"BlackBomb",  "Standard" },
            {"Recovery10",  "Standard" },
            {"Recovery30",  "Standard" },
            {"Recovery50",  "Standard" },
            {"Recovery80",  "Standard" },
            {"Recovery120",  "Standard" },
            {"Recovery150",  "Standard" },
            {"Recovery200",  "Standard" },
            {"Recovery300",  "Standard" },
            {"Barrier",  "Standard" },
            {"Barrier100",  "Standard" },
            {"Barrier200",  "Standard" },
            {"PanelReturn",  "Standard" },
            {"HolyPanel",  "Standard" },
            {"CrackOut",  "Standard" },
            {"DoubleCrack",  "Standard" },
            {"TripleCrack",  "Standard" },
            {"DreamAura",  "Mega" },
            {"Sanctuary",  "Mega" },
            {"DarkCannon",  "Dark" },
            {"DarkBomb",  "Dark" },
            {"DarkSpread",  "Dark" },
            {"DarkRecovery",  "Dark" },
            {"DarkStage",  "Dark" },
            {"FinalGun",  "Giga" },
            {"JaneRiver",  "Giga" },
            {"CherprangRiver",  "Giga" },
            {"BlackAce", "Black" }
        };
        public Dictionary<string, string> allChipTypeDict = new Dictionary<string, string>()
        {
            {"Cannon",  "Normal" },
            {"HiCannon",  "Normal" },
            {"MegaCannon",  "Normal" },
            {"AirShot",  "Wind" },
            {"SpreadGun1",  "Normal" },
            {"SpreadGun2",  "Normal" },
            {"SpreadGun3",  "Normal" },
            {"MiniBomb",  "Normal" },
            {"BigBomb",  "Normal" },
            {"EnergyBomb",  "Normal" },
            {"MegaEnergyBomb",  "Normal" },
            {"BugBomb",  "Normal" },
            {"SearchBomb1",  "Normal" },
            {"SearchBomb2",  "Normal" },
            {"SearchBomb3",  "Normal" },
            {"CannonBall",  "Break" },
            {"BlackBomb",  "Fire" },
            {"Recovery10",  "Normal" },
            {"Recovery30",  "Normal" },
            {"Recovery50",  "Normal" },
            {"Recovery80",  "Normal" },
            {"Recovery120",  "Normal" },
            {"Recovery150",  "Normal" },
            {"Recovery200",  "Normal" },
            {"Recovery300",  "Normal" },
            {"Barrier",  "Normal" },
            {"Barrier100",  "Normal" },
            {"Barrier200",  "Normal" },
            {"PanelReturn",  "Normal" },
            {"HolyPanel",  "Normal" },
            {"CrackOut",  "Normal" },
            {"DoubleCrack",  "Normal" },
            {"TripleCrack",  "Normal" },
            {"DreamAura",  "Normal" },
            {"Sanctuary",  "Normal" },
            {"DarkCannon",  "Normal" },
            {"DarkBomb",  "Normal" },
            {"DarkSpread",  "Normal" },
            {"DarkRecovery",  "Normal" },
            {"DarkStage",  "Normal" },
            {"FinalGun",  "Normal" },
            {"JaneRiver",  "Aqua" },
            {"CherprangRiver",  "Aqua" },
            {"BlackAce", "Normal" }
        };
        public Dictionary<string, int> allChipAtkDict = new Dictionary<string, int>()
        {
            {"Cannon",  40 },
            {"HiCannon",  100 },
            {"MegaCannon",  180 },
            {"AirShot",  20 },
            {"SpreadGun1",  30 },
            {"SpreadGun2",  60 },
            {"SpreadGun3",  90 },
            {"MiniBomb",  50 },
            {"BigBomb",  140 },
            {"EnergyBomb",  40 },
            {"MegaEnergyBomb",  60 },
            {"BlackBomb",  250 },
            {"CannonBall",  140 },
            {"SearchBomb1",  80 },
            {"SearchBomb2",  110 },
            {"SearchBomb3",  140 },
            {"DarkCannon",  Singleton.Instance.maxHeroHP - Singleton.Instance.HeroHP},
            {"DarkSpread",  400 },
            {"DarkBomb",  200 },
            {"FinalGun",  50 },
            {"CherprangRiver",  400 },
            {"JaneRiver",  400 },
        };
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
            {"PanelReturn",  "Fix all panels" },
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
            {"BlackAce",  "Change to \n\nBlackAce" },
        };
        public Background() : base()
        {
        }

        public Background(Texture2D[] texture) : base(texture)
        {
        }

        public Background(Dictionary<string, Animation> animations) : base(animations)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {

            base.Update(gameTime, sprites);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void Reset()
        {
            base.Reset();
        }
    }
}
