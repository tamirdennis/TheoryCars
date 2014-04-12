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
    class Camera
    {
        public Matrix Mat { get; private set; }
        V2 pos;
        IFocus focus;
        F zoom;
        Viewport vp;

        //cthor
        public Camera(IFocus focus , Viewport vp , F zoom)
        {
            this.focus = focus;
            this.zoom = zoom;
            this.vp = vp;
            this.pos = V2.Zero;
        }

        public void update()
        {
            Mat = Matrix.CreateTranslation(-pos.X, -pos.Y, 0) *
                       Matrix.CreateRotationZ(0) * 
                       Matrix.CreateScale(zoom , zoom , 1) * 
                       Matrix.CreateTranslation(vp.Width / 2 , vp.Height / 2 , 0);
            this.pos = V2.Lerp(this.pos, focus.Pos, 0.97f);
        }
    }
}
