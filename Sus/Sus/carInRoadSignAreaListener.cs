using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sus
{
    class carInRoadSignAreaListener
    {
        private Car car;
        public carInRoadSignAreaListener(Car car)
        {
            this.car = car;
            car.CarInRoadSignArea += new carInRoadSignAreaEventHandler(carInRoadSignArea);
        }
        private void carInRoadSignArea(Car car,roadSign roadsign, EventArgs e)
        {
            roadsign.whenCarIsInArea(car);
        }
        public void Detach()
        {
            // Detach the event and delete the car
            car.CarInRoadSignArea -= new carInRoadSignAreaEventHandler(carInRoadSignArea);
            car = null;
        }
    }
}
