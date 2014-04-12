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
    
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        #region Data
        GD graphics;
        public static SB spriteBatch;
        Character bobik;
        Character newCar;
        Camera cam;
        int slow = 0;
        List<Character> bots=new List <Character>();
        public static event SigUpdate CallUpdate;
        public static event SigDraw CallDraw;
        Drawing terrian;

        ////adsadssadsadsadsad
        //Texture2D tex;
        //Color[,] colorsArray;
        //List<V2> curve;

        public static string tilte = "";
        #endregion
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
            base.Initialize();
        }

        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);
            Tools.init(Content,GraphicsDevice);
            terrian = new Drawing(Tools.cm.Load<T2>("terrian/track2"),
                                                new V2(0, 0),
                                                null,
                                                C.White,
                                                0,
                                                V2.Zero,
                                                new V2(2.15f,2.26f),
                                                SE.None,
                                                0);
            #region Creation of the boobik
            bobik = new Character(new GamerKeys(Keys.Up, Keys.Down, Keys.Left, Keys.Right, Keys.Space), "havazelet", States.drive,
                                     new V2(0.1538f, 0.165454f),
                                     new Rec(60, 0, 60, 60),
                                     C.White,
                                     0,
                                     new V2(21, 55),
                                     new V2(0.1f),
                                     SE.None,
                                     0);

           newCar = new Character(new GamerKeys(Keys.W, Keys.S, Keys.A, Keys.D, Keys.Space), "havazelet", States.drive,
                         new V2(323, 648),
                         new Rec(60, 0, 60, 60),
                         C.Red,
                         0,
                         new V2(21, 55),
                         new V2(0.1f),
                         SE.None,
                         0);
            #endregion
            for (int i = 0; i < 10; i++)
            {

                cam = new Camera(bobik, GraphicsDevice.Viewport, 0.5f);
                
            }


            //asdasdasdsaddsaasd
            //tex = Tools.cm.Load<T2>("terrian/track2");
            //colorsArray = Tools.textureToColorArray(tex);
            //curve = Tools.getVectorsOfCurve(colorsArray, new V2(8, 20));

        }
        
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        //DECLERATION OF THE WORLD 3: THIS TIME IT'S PERSONAL
        protected override void Update(GameTime gameTime)
        {
      //      tilte = Tools.colorOfPosition(colorsArray,new V2(9,23)).ToString() + " " + curve[2];
            if (CallUpdate!=null)
            {
                CallUpdate();
            }
            cam.update();
            Window.Title = tilte;
            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(C.Green);
            spriteBatch.Begin(SpriteSortMode.Deferred,
                                          BlendState.AlphaBlend,
                                          null,
                                          null,
                                          null,
                                          null,
                                          cam.Mat);
            if (Game1.CallDraw!=null)
            {
                CallDraw();
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

