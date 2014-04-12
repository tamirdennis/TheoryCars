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
    class BotKeys:Baseinput
    {
        bool left, right, up, down,space,prevLeft,prevRight,prevUp,prevDown,prevSpace;
        public BotKeys(bool left,bool right,bool up,bool down,bool space)
        {
            this.prevLeft = this.left;
            this.left = left;
            this.prevRight = this.right;
            this.right = right;
            this.prevUp = this.up;
            this.up = up;
            this.prevDown = this.down;
            this.down = down;
            this.prevSpace = this.space;
            this.space = space;
        }
        public override bool leftPressed()
        {
            return left;
        }

        public override bool rightPressed()
        {
            return right;
        }

        public override bool upPressed()
        {
            return up;
        }

        public override bool downPressed()
        {
            return down;
        }



        public override bool prevLeftPressed()
        {
            return prevLeft;
        }

        public override bool prevRightPressed()
        {
            return prevRight;
        }

        public override bool prevUpPressed()
        {
            return prevUp;
        }

        public override bool prevDownPressed()
        {
            return prevDown;
        }
    }
    class GamerKeys:Baseinput
    {
        Keys left, right, up, down, space, prevLeft, prevRight, prevUp, prevDown, prevSpace;
        public GamerKeys (Keys up,Keys down,Keys left,Keys right,Keys space)
	{
        this.prevLeft = this.left;
        this.left = left;
        this.prevRight = this.right;
        this.right = right;
        this.prevUp = this.up;
        this.up = up;
        this.prevDown = this.down;
        this.down = down;
        this.prevSpace = this.space;
        this.space = space;
	}

        public override bool leftPressed()
        {
            
          return Tools.ks.IsKeyDown(left);
              
        }

        public override bool rightPressed()
        {
                return Tools.ks.IsKeyDown(right);
        }

        public override bool upPressed()
        {
               return Tools.ks.IsKeyDown(up);
        }

        public override bool downPressed()
        {
                  return Tools.ks.IsKeyDown(down);
        }

        public override bool prevLeftPressed()
        {
            return Tools.pks.IsKeyDown(left);
        }

        public override bool prevRightPressed()
        {
            return Tools.pks.IsKeyDown(right);
        }

        public override bool prevUpPressed()
        {
            return Tools.pks.IsKeyDown(up);
        }

        public override bool prevDownPressed()
        {
            return Tools.pks.IsKeyDown(down);
        }
    }
   abstract class Baseinput
    {
      
        public abstract bool leftPressed();
        public abstract bool rightPressed();
        public abstract bool upPressed();
        public abstract bool downPressed();

        public abstract bool prevLeftPressed();
        public abstract bool prevRightPressed();
        public abstract bool prevUpPressed();
        public abstract bool prevDownPressed();


    }
}
