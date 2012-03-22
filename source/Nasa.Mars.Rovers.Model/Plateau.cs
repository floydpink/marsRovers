using Nasa.Mars.Rovers.Model.Extensions;
using Nasa.Mars.Rovers.Model.Interfaces;

namespace Nasa.Mars.Rovers.Model
{
    public class Plateau : IPlateau
    {
        private readonly int _eastBoundary;
        private readonly int _northBoundary;

        public Plateau(int eastBoundary, int northBoundary)
        {
            _eastBoundary = eastBoundary;
            _northBoundary = northBoundary;
        }

        public int EastBoundary
        {
            get { return _eastBoundary; }
        }

        public int NorthBoundary
        {
            get { return _northBoundary; }
        }


        public bool IsRoverWithinLimits(IRover rover)
        {
            return rover.Easting.Between(0, _eastBoundary) && rover.Northing.Between(0, _northBoundary);
        }
    }
}
