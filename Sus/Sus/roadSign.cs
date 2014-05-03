#region Using
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
#endregion

#region Shorcuts
using T2 = Microsoft.Xna.Framework.Graphics.Texture2D;
using V2 = Microsoft.Xna.Framework.Vector2;
using Rec = Microsoft.Xna.Framework.Rectangle;
using C = Microsoft.Xna.Framework.Color;
using SE = Microsoft.Xna.Framework.Graphics.SpriteEffects;
using F = System.Single;//F = float
using SB = Microsoft.Xna.Framework.Graphics.SpriteBatch;
using GD = Microsoft.Xna.Framework.GraphicsDeviceManager;
using CM = Microsoft.Xna.Framework.Content.ContentManager;
#endregion

namespace Sus
{
    abstract class roadSign : AnimatedObj
    {
        public static List<roadSign> roadSigns = new List<roadSign>();
        protected Point[] area;
        public roadSign(string name, States state, V2 pos, Rec? rec, C color,
                                F rot, V2 org, V2 scale, SE se, F layer, Point[] area)
        : base(name, state, pos, rec, color,
                                rot, org, scale, se, layer)
        {
            this.area = area;
            roadSigns.Add(this);
        }

        public Point[] getArea()
        {
            return area;
        }

        public abstract void whenCarIsInArea(Car car);
    }
}
