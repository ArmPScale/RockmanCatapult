using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rockman.Models
{
    public class Animation
    {
        public Rectangle AnimationRectangle { get; private set; }
        public int CurrentFrame { get; set; }
        public int FrameCount { get; private set; }
        public int FrameHeight { get { return AnimationRectangle.Height; } }
        public float FrameSpeed { get; set; }
        public int FrameWidth { get { return AnimationRectangle.Width / FrameCount; } }
        public bool IsLooping { get; set; }
        public Texture2D Texture { get; private set; }

        public Animation (Texture2D texture, Rectangle rect, int frameCount)
        {
            Texture = texture;
            AnimationRectangle = rect;
            FrameCount = frameCount;
            IsLooping = false;
            FrameSpeed = 0.1f;
        }
    }
}
