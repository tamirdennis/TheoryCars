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
    class Character : AnimatedObj
    {
        #region data
        private Baseinput keys;
        private float speed = 20;
        private V2 dir;

        private float demoRot = 0;
        //private Boolean flip = false;
        #endregion

        #region ctor
        public Character(Baseinput keys,string name,States state, V2 pos, Rec? rec, C color,
                                F rot, V2 org, V2 scale, SE se, F layer)
        :base(name,state,pos,rec,  color,
                                 rot,  org,  scale,  se,  layer)
        {
            this.keys = keys;
            if (SE.FlipHorizontally == se)
                flip = true;

            dir = V2.UnitX;
        } 
        #endregion
      

        #region Update
        public override void Update()
        {

            demoRot += 0.1f;
            Matrix mat = Matrix.CreateRotationZ(Rot);
            dir = V2.Transform(V2.UnitX, mat);

            if (keys.leftPressed())
            {
                Rot -= 0.05f;
                frontPos = dir * bigR / 2;
                backPos = dir * -bigR / 3;
                inFrontOfMe = dir * bigR / 1.2f;
            }
            if (keys.rightPressed())
            {
                Rot += 0.05f;
                frontPos = dir * bigR / 2;
                backPos = dir * -bigR / 3;
                inFrontOfMe = dir * bigR / 1.2f;
            }

            if (keys.upPressed())
            {
                Pos += dir * speed;
            }

            if (keys.downPressed())
            {
                Pos -= dir * speed;
            }
            base.Update();
            
        }

  
        
        #endregion

        public override void Draw()
        { 
            base.Draw();
            // Tools.draw_vector(Pos, demoRot , this.bigR);
            Tools.draw_vector(Pos + backPos, demoRot, this.bigR / 3);
            Tools.draw_vector(Pos + frontPos, demoRot, this.bigR / 3);
           // Tools.draw_vector(Pos + inFrontOfMe, demoRot, this.bigR / 3);
        }
    }
}
