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
    class Chip : Sprite
    {
        #region PHYSICS_VARIABLES
        public float GRAVITY = 981;
        public float Restitution;
        public float Mass;

        public enum PhysicsType
        {
            STATICS,
            KINEMATICS,
            DYNAMICS
        }
        public PhysicsType EntityPhysicsType;

        public enum BoundingBoxType
        {
            NONE,
            AABB,
            CIRCLE
        }
        public BoundingBoxType EntityBoundingBoxType;

        public enum ImpluseType
        {
            NONE,
            SURFACE,
            NORMAL
        }
        public ImpluseType EntityImpluseType;

        public List<CollisionManifold> CollideeManifold;
        public struct CollisionManifold
        {
            public Sprite Collidee;
            public float Penetration;
            public Vector2 Normal;
        }
        #endregion

        public String[] chipCustomImg = new string[6];
        public String[] chipCustomImgCopy = new string[6];

        public Chip(Texture2D[] texture) 
            : base(texture)
        {
            chipCustomImg = Singleton.Instance.chipCustomSelect;
        }

        public Chip(Dictionary<string, Animation> animations) 
            : base(animations)
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
