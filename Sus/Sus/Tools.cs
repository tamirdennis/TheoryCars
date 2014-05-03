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
    static class Tools
    {
        public static CM cm;
        public static V2 mapScale=new V2(10,3);
        public static KeyboardState ks, pks;
        public static T2 pixel;
        public static void init(CM cm, GraphicsDevice gd)
        {
            Tools.cm = cm;
            Game1.CallUpdate+=new SigUpdate(Update);
            pixel = new T2(gd,1,1);
            pixel.SetData<Color>(new Color[] {Color.White});
        }
        public static void Update()
        {
            pks = ks;
           ks=Keyboard.GetState();
            
        }

        public static void draw_vector(V2 pos, float rot, float length)
        {
            Game1.spriteBatch.Draw(pixel,
                                   pos,
                                   null,
                                   Color.Red,
                                   rot,
                                   new V2(0.5f, 1f),
                                   new V2(5,length),
                                   SE.None,
                                   0);

        }

        public static Point Vector2ToPoint(Vector2 vector2)
        {
            Point pt = new Point(
                (int)(vector2.X + 0.5f), (int)(vector2.Y + 0.5f));

            return pt;
        }

        public static Color[,] textureToColorArray(Texture2D texture)
        {
            Color[] colors1D = new Color[texture.Width * texture.Height];
            texture.GetData<Color>(colors1D);

            Color[,] colors2D = new Color[texture.Width, texture.Height];
            for (int x = 0; x < texture.Width; x++)
            {

                for (int y = 0; y < texture.Height; y++)
                {
                    colors2D[x, y] = colors1D[x + y * texture.Width];
                }
            }
            return colors2D;
        }
        
        public static Color colorOfPosition(Color[,] colorsArray, V2 pos){
            Point ppos = Vector2ToPoint(pos);
            return colorsArray[ppos.X, ppos.Y];

        }

        //returns the curve with the same color of the pos
        public static List<V2> getVectorsOfCurve(Color[,] colorsArray, V2 pos)
        {
            List<V2> curve = new List<V2>();
            Color color = colorOfPosition(colorsArray, pos);
            V2 currPos = pos;
            curve.Add(currPos);
            V2 prevPos;
            V2 nextPos = pos;
            V2 nullVector = new V2(-1,-1);
            do
            {
                prevPos = currPos;
                currPos = nextPos;
                nextPos = getNextPositionOfSameColorInRadius(colorsArray, currPos, prevPos, 1);
                curve.Add(nextPos);

                
            } while (!nextPos.Equals(nullVector));
            return curve;

        }

        private static V2 getNextPositionOfSameColorInRadius(Color[,] colorsArray, V2 pos, V2 prevPos, int radius)
        {
            Point ppos = Vector2ToPoint(pos);
            Point pprevPos = Vector2ToPoint(prevPos);
            Color color = colorOfPosition(colorsArray, pos);
            for (int row = ppos.X - radius; row <= ppos.X + radius; row++)
            {
                for (int col = ppos.Y - radius; col <= ppos.Y + radius; col++)
                {
                    if (!(row == ppos.X && col == ppos.Y) && !(row == pprevPos.X && col == pprevPos.Y) && colorsArray[row, col].Equals(color))
                    {
                        return new V2(row, col);
                    }
                }
            }
            return new V2(-1,-1);
        }

        public static V2 getDirectionByTwoPoints(Point first, Point second)
        {
            V2 dir = (new V2(second.X, second.Y)) - (new V2(first.X, first.Y));
            dir.Normalize();
            return dir;
        }
    }
}



