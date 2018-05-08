using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Rockman.Managers;
using Rockman.Models;

namespace Rockman.Sprites
{
    class Sprite : ICloneable
    {
        public const int TILESIZEX = 40*3/2, TILESIZEY = 24*3/2, screenStageX = 0, screenStageY = 500;
        protected float scale = 3f;

        #region PUBLIC_VARIABLES

        public Dictionary<string, SoundEffectInstance> SoundEffects;

        public Vector2 Position;

        public float Rotation;
        public Vector2 Scale;

        public string Name;

        public bool IsActive;

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, Viewport.Width, Viewport.Height);
            }
        }

        public Rectangle Viewport;
        #endregion

        #region PROTECTED_VARIABLES

        protected Dictionary<string, Animation> _animations;
        protected AnimationManager _animationManager;

        protected Texture2D[] _texture;
        #endregion

        public Sprite()
        {
            Position = Vector2.Zero;
            Scale = Vector2.One;
            Rotation = 0f;
            IsActive = true;
        }

        public Sprite(Texture2D[] texture)
        {
            _texture = texture;
            Position = Vector2.Zero;
            Scale = Vector2.One;
            Rotation = 0f;
            IsActive = true;
        }

        public Sprite(Dictionary<string, Animation> animations)
        {
            Position = Vector2.Zero;
            Scale = Vector2.One;
            Rotation = 0f;
            IsActive = true;
            _animations = animations;
            _animationManager = new AnimationManager(_animations.First().Value);
        }

        public virtual void Update(GameTime gameTime, List<Sprite> sprites)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }

        public virtual void Reset()
        {
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #region Collision
        public bool IsTouching(Sprite g)
        {
            return IsTouchingLeft(g) ||
                IsTouchingTop(g) ||
                IsTouchingRight(g) ||
                IsTouchingBottom(g);
        }

        public bool IsTouchingLeft(Sprite g)
        {
            return this.Rectangle.Right > g.Rectangle.Left &&
                    this.Rectangle.Left < g.Rectangle.Left &&
                    this.Rectangle.Bottom > g.Rectangle.Top &&
                    this.Rectangle.Top < g.Rectangle.Bottom;
        }

        public bool IsTouchingRight(Sprite g)
        {
            return this.Rectangle.Right > g.Rectangle.Right &&
                    this.Rectangle.Left < g.Rectangle.Right &&
                    this.Rectangle.Bottom > g.Rectangle.Top &&
                    this.Rectangle.Top < g.Rectangle.Bottom;
        }

        public bool IsTouchingTop(Sprite g)
        {
            return this.Rectangle.Right > g.Rectangle.Left &&
                    this.Rectangle.Left < g.Rectangle.Right &&
                    this.Rectangle.Bottom > g.Rectangle.Top &&
                    this.Rectangle.Top < g.Rectangle.Top;
        }

        public bool IsTouchingBottom(Sprite g)
        {
            return this.Rectangle.Right > g.Rectangle.Left &&
                    this.Rectangle.Left < g.Rectangle.Right &&
                    this.Rectangle.Bottom > g.Rectangle.Bottom &&
                    this.Rectangle.Top < g.Rectangle.Bottom;
        }
        #endregion
    }
}
