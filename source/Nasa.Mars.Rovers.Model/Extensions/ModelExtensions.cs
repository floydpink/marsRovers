using System;

namespace Nasa.Mars.Rovers.Model.Extensions
{
    public static class ModelExtensions
    {
        public static Direction ToDirection(this int degrees)
        {            
            return (Direction)(degrees >= 360 ? degrees - 360 : (degrees < 0 ? 360 + degrees : degrees));
        }

        public static double ToRadian(this int degrees)
        {
            return Math.PI * degrees / 180.0;
        }

        public static bool Between(this int value, int lowerBound, int upperBound)
        {
            return value >= lowerBound && value <= upperBound;
        }

    }
}
