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
    class Car : AnimatedObj
    {
        #region data
        public string name2;
        private Baseinput keys;
        private float speed = 0;
        private V2 dir;

        public event collisionEventHandler Collision;
        public event carInRoadSignAreaEventHandler CarInRoadSignArea;
        public static List<Car> carsExisting = new List<Car>();

        private float bigR;
        private V2 frontPos;
        private V2 backPos;
        private V2 inFrontOfMe;

        private float demoRot = 6f;
        #endregion

        #region ctor
        public Car(Baseinput keys, string name, States state, V2 pos, C color,
                                F rot, V2 org, V2 scale, SE se, F layer, string name2)
        :base(name, state, pos, new Rec(), color,
                                 rot, org, scale, se, layer)
        {
            this.name2 = name2;
            this.keys = keys;
            if (SE.FlipHorizontally == se)
                flip = true;

            dir = V2.UnitX;
            
            bigR = page.tex.Width * scale.X / 1.42f;
            inFrontOfMe = V2.UnitX * bigR / 1.2f;   //this vector is relative to the pos vector of the Car
            frontPos = V2.UnitX * (bigR / 2);       //this vector is relative to the pos vector of the Car
            backPos = V2.UnitX * (-bigR / 3);       //this vector is relative to the pos vector of the Car
            carsExisting.Add(this);
        } 
        #endregion

        public bool checkColl(Car x)
        {
            if (x.Equals(this))
                return false;

            return ((x.Pos - this.Pos).Length() < x.bigR + this.bigR) &&
                     (((x.Pos + x.frontPos - (this.Pos + frontPos)).Length() < x.bigR / 3 + this.bigR / 3) ||
                     ((x.Pos + x.backPos - (this.Pos + backPos)).Length() < x.bigR / 3 + this.bigR / 3) ||
                     ((x.Pos + x.frontPos - (this.Pos + backPos)).Length() < x.bigR / 3 + this.bigR / 3) ||
                     ((x.Pos + x.backPos - (this.Pos + frontPos)).Length() < x.bigR / 3 + this.bigR / 3));

        }

        public bool isOnSignArea(roadSign roadsign)
        {
            if (roadsign.getArea().Contains(Tools.Vector2ToPoint(this.Pos)))
                return true;
            return false;
        }

        private void onCollision(EventArgs e)
        {
            if (Collision != null)
                Collision(this, e);
        }

        private void onSignArea(roadSign roadsign, EventArgs e)
        {
            if (CarInRoadSignArea != null)
                CarInRoadSignArea(this, roadsign, e);
        }

        public void setDirection(V2 direction)
        {
            dir = direction;
            Rot = (float)Math.Atan2(dir.Y, dir.X);
            frontPos = dir * bigR / 2;
            backPos = dir * -bigR / 3;
        }

        public void setPosition(V2 position)
        {
            Pos = position;
        }

        #region Update
        public override void Update()
        {
            foreach (Car car in Car.carsExisting)
            {
                if (checkColl(car))
                    onCollision(EventArgs.Empty);
            }

            foreach (roadSign roadsign in roadSign.roadSigns)
            {
                if (isOnSignArea(roadsign))
                    onSignArea(roadsign, EventArgs.Empty);
            }

            demoRot += 0.1f;
            //Matrix mat = Matrix.CreateRotationZ(Rot);
            //dir = V2.Transform(V2.UnitX, mat);

            //if (keys.leftPressed())
            //{
            //    Rot -= 0.05f;
            //    frontPos = dir * bigR / 2;
            //    backPos = dir * -bigR / 3;
            //    inFrontOfMe = dir * bigR / 1.2f;
            //}
            //if (keys.rightPressed())
            //{
            //    Rot += 0.05f;
            //    frontPos = dir * bigR / 2;
            //    backPos = dir * -bigR / 3;
            //    inFrontOfMe = dir * bigR / 1.2f;
            //}

            //if (keys.upPressed())
            //{
            //    Pos += dir * speed;
            //}

            //if (keys.downPressed())
            //{
            //    Pos -= dir * speed;
            //}

            if (keys.upPressed())
                speed += 0.05f;
            if (keys.downPressed())
                speed -= 0.05f;

            ////##################################
            //Dictionary<Point, Point> carDirectionPoints  = new Dictionary<Point,Point>();
            //carDirectionPoints.Add(new Point(127, 191), new Point(620, 194));
            //carDirectionPoints.Add(new Point(620, 194), new Point(622, 604));
            //carDirectionPoints.Add(new Point(622, 604), new Point(124, 606));
            //carDirectionPoints.Add(new Point(124, 606), new Point(127, 191));
            //Point P = Tools.Vector2ToPoint(Pos);

            //for (int row = P.X - 10; row <= P.X + 10; row++)
            //{
            //    for (int col = P.Y - 10; col <= P.Y + 10; col++)
            //    {
            //        Point inAreaOfP = new Point(row, col);
            //        if (carDirectionPoints.ContainsKey(inAreaOfP))
            //        {
            //            Pos = new V2(row, col);
            //            dir = Tools.getDirectionByTwoPoints(inAreaOfP, carDirectionPoints[inAreaOfP]);
            //        }
            //    }
            //}
            
            
            ////################################

            //dir.Normalize();
            //Rot = (float)Math.Atan2(dir.Y, dir.X);
            //frontPos = dir * bigR / 2;
            //backPos = dir * -bigR / 3;
            Pos += dir * speed;
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
