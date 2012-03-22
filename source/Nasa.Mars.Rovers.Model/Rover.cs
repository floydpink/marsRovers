using System;
using Nasa.Mars.Rovers.Model.Extensions;
using Nasa.Mars.Rovers.Model.Interfaces;

namespace Nasa.Mars.Rovers.Model
{
    public class Rover : IRover
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

        public void Navigate(Command command)
        {
            if (command == Command.Move)
            {
                var heading = Convert.ToInt32(_heading).ToRadian();
                _easting += (int)Math.Cos(heading);
                _northing += (int)Math.Sin(heading);
            }
            else
            {
                _heading = Convert.ToInt32(_heading + calculateTurn(command)).ToDirection();
            }
        }

        internal static int calculateTurn(Command command)
        {
            int turnmount = AppConstants.TurnInDegrees;
            switch (command)
            {
                case Command.Left:
                    turnmount *= +1;
                    break;
                case Command.Right:
                    turnmount *= -1;
                    break;
                default:
                    turnmount *= 0;
                    break;
            }
            return turnmount;
        }
    }
}
