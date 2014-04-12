using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheoryLogic
{
    public interface ICar
    {
        enum TurnDirection
        {
            Right,
            Left
        }
        void ChangeSpeed(double speedChanged);
        void Turn(TurnDirection direction);
        void ChangeLane(TurnDirection dirction);
    }
    public class Car
    {
        public 
    }
}
