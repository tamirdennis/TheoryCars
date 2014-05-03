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
    class changeDirection : roadSign
    {
        private Point first;
        private Point second;
        private V2 nextPos;
        private V2 dir;
        public changeDirection(Point first, Point second)
            : base("changeDirection", States.Null, new V2(first.X, first.Y), null, Color.Black,
                                0, new V2(0,0) , new V2(0,0), SE.None, 0, null)
        {
            this.first = first;
            this.second = second;
            area = new Point[961];
            int i = 0;
            for (int row = first.X - 15; row <= first.X + 15; row++)
            {
                for (int col = first.Y - 15; col <= first.Y + 15; col++)
                {
                    area[i] = new Point(row, col);
                    i++;
                }
            }
            dir = (new V2(second.X, second.Y) - new V2(first.X, first.Y));
            dir.Normalize();
            nextPos = new V2(first.X, first.Y) + dir * 16;
        }

        public override void whenCarIsInArea(Car car)
        {
            car.setDirection(dir);
            car.setPosition(nextPos);
        }
    }
}
