using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sus
{
    class CarCollisionListener
    {
        private Car car;
        public CarCollisionListener(Car car)
        {
            this.car = car;
            car.Collision += (CollisionHappened);
        }
        private void CollisionHappened(Car car, EventArgs e)
        {
            if(!Game1.title.Contains(car.name2))
                Game1.title += car.name2;
            //write here what to do when car had a collision with other car.
        }
        public void Detach()
        {
            // Detach the event and delete the car
            car.Collision -= (CollisionHappened);
            Car.carsExisting.Remove(car);
            car = null;
        }
    }
}
