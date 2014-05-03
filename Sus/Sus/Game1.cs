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
        Camera cam;
     //   int slow = 0;
        List<Car> bots=new List <Car>();
        public static event SigUpdate CallUpdate;
        public static event SigDraw CallDraw;
        List<Car> cars = new List<Car>();
        List<changeDirection> ChangeDirectrionSigns = new List<changeDirection>();
        Drawing terrian;

        public static string title = "";
        #endregion
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here 
            this.TargetElapsedTime = TimeSpan.FromSeconds(1.0f / 200.0f);  //changing the frequency of the update
            base.Initialize();
        }
        /*
        class CarFactory
        {
        
        public    Car Build()
            {
                Car c = new Car(keys,);
                return c;
            }
            GamerKeys keys;
        public CarFactory SetGamerKeys(GamerKeys keys)
            {

                return this;
            }
        }
        */
        void AddCar(Car car)
        {
            cars.Add(car);
            CarCollisionListener car1CollListener = new CarCollisionListener(car);
            carInRoadSignAreaListener roadSignsListenerforCar1 = new carInRoadSignAreaListener(car);
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Tools.init(Content,GraphicsDevice);

            CreateMap();
            CreateCars();
            CreateSigns();
            CreateCamera();

        }

        private void CreateCamera()
        {
            for (int i = 0; i < 10; i++)
            {
                cam = new Camera(cars[0], GraphicsDevice.Viewport, 0.5f);
            }
        }

        private void CreateSigns()
        {

            ChangeDirectrionSigns.Add(new changeDirection(new Point(127, 191), new Point(620, 194)));
            ChangeDirectrionSigns.Add(new changeDirection(new Point(620, 194), new Point(622, 604)));
            ChangeDirectrionSigns.Add(new changeDirection(new Point(622, 604), new Point(124, 606)));
            ChangeDirectrionSigns.Add(new changeDirection(new Point(124, 606), new Point(127, 191)));

        }

        private void CreateCars()
        {
            AddCar( new Car(new GamerKeys(Keys.Up, Keys.Down, Keys.Left, Keys.Right, Keys.Space), "havazelet", States.Drive,
                                        new V2(127, 191),
                                        C.White,
                                        0,
                                        new V2(21, 55),
                                        new V2(0.1f),
                                        SE.None,
                                        0, "car1"));
            AddCar( new Car(new GamerKeys(Keys.W, Keys.S, Keys.A, Keys.D, Keys.Space), "havazelet", States.Drive,
                            new V2(323, 608),
                            C.Red,
                            0,
                            new V2(21, 55),
                            new V2(0.1f),
                            SE.None,
                            0, "car2"));
            AddCar( new Car(new GamerKeys(Keys.U, Keys.J, Keys.H, Keys.K, Keys.Space), "havazelet", States.Drive,
                            new V2(323, 608),
                            C.Blue,
                            0,
                            new V2(21, 55),
                            new V2(0.1f),
                            SE.None,
                            0, "car3"));
        }

        private void CreateMap()
        {
            terrian = new Drawing(Tools.cm.Load<T2>("terrian/track2"),
                                                new V2(0, 0),
                                                null,
                                                C.White,
                                                0,
                                                V2.Zero,
                                                new V2(2.15f, 2.26f),
                                                SE.None,
                                                0);
        }
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        protected override void Update(GameTime gameTime)
        {
            title = "";
            if (CallUpdate!=null)
            {
                CallUpdate();
            }
            cam.update();
            Window.Title = title;
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

