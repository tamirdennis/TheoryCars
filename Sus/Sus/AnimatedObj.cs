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
    class AnimatedObj : Drawing
    {
        #region Data
        protected Page page;
        protected int place = 0;
        int slow = 0;
        States state;
        Dictionary<States, Page> Acts = new Dictionary<States, Page>();
        protected Boolean flip;

        
        #endregion

        #region ctor
        public AnimatedObj(string name, States state, V2 pos, Rec? rec, C color,
                                F rot, V2 org, V2 scale, SE se, F layer)
        :base(null, pos, rec, color, rot, org, scale, se, layer)
        {

            #region Insert Acts
            foreach (States st in Enum.GetValues(typeof(States)))
            {
                try
                {
                    this.Acts.Add(st, new Page(name, st.ToString()));
                }
                catch (Exception)
                {
                    //אוקיי סבבה בסדר?
                }
            } 
            #endregion
            this.state = state;
            page = this.Acts[state];//מקבל את המ
            Game1.CallUpdate += new SigUpdate(Update);
            
        }
        #endregion
         
        #region Update
        public virtual void Update()
        {

        }
        #endregion

        #region Draw
        public override void Draw()
        {
            if (place >= page.recs.Count) place = 0;
            base.tex = page.tex;
            if (!flip)
                base.org = page.orgs[place];
            else
                base.org = page.flipOrgs[place];
            base.rec = page.recs[place];
            if (slow == 0)
            {
                place++;
                slow++;
            }
            else
            {
                slow++;
                if (slow == 10) slow = 0;
            }
            
            base.Draw();
            if (place == page.recs.Count) place = 0;
        } 
        
        #endregion
        public void change_state(States state)
        {
            this.state = state;
            page = this.Acts[state];
            
        }
    }
}
