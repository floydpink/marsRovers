using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nasa.Mars.Rovers.Model.Interfaces;

namespace Nasa.Mars.Rovers.Model
{
    class Rover : IRover
    {
        private int _easting;
        private int _northing;
        private Direction _heading;

        public Rover(int easting, int northing, Direction heading)
        {
            _easting = easting;
            _northing = northing;
            _heading = heading;
        }

        public int Easting
        {
            get { return _easting; }
        }

        public int Northing
        {
            get { return _northing; }
        }

        public Direction Heading
        {
            get { return _heading; }
        }

        public IPlateau Plateau
        {
            get { throw new NotImplementedException(); }
        }

        public void Navigate(Command command)
        {
            throw new NotImplementedException();
        }
    }
}
