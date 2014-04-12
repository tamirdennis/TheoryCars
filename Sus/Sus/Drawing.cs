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
    class Drawing : IFocus
    {
        #region Data
        protected T2 tex;
        protected V2 org;
        protected Rec? rec;
        public V2 Pos { get; set;}
        C color;
        public F Rot { get; set;}      
        V2 scale;
       protected SE se;
        F layer; 
        #endregion
        //Rec? - can be NULL
        #region ctor
        public Drawing(T2 tex, V2 pos, Rec? rec, C color,
                                F rot, V2 org, V2 scale, SE se, F layer)
        {
            this.tex = tex;
            this.Pos = pos;
            this.rec = rec;
            this.color = color;
            this.Rot = rot;
            this.org = org;
            this.scale = scale;
            this.se = se;
            this.layer = layer;
            Game1.CallDraw+=new SigDraw(Draw);
        } 
        #endregion

        #region Drawing
        public virtual void Draw()
        {
            Game1.spriteBatch.Draw(tex, Pos, rec, color, Rot,
                                                    org, scale, se, layer);
        }
        #endregion
    }
}
