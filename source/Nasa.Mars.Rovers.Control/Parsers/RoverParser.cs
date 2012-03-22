using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nasa.Mars.Rovers.Model.Interfaces;
using Nasa.Mars.Rovers.Model;

namespace Nasa.Mars.Rovers.Control.Parsers
{
    public static class RoverParser
    {
        public static IRover Parse(string roverPositionAndHeadingLine)
        {
            try
            {
                var roverPositionAndHeading = roverPositionAndHeadingLine.Split(' ');
                int easting = Convert.ToInt32(roverPositionAndHeading[0]);
                int northing = Convert.ToInt32(roverPositionAndHeading[1]);
                Direction heading = parseDirection(roverPositionAndHeading[2]);

                return new Rover(easting, northing, heading);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("...while processing the Rovers data.\r\n" +
                    ex.Message, ex);
            }
        }

        internal static Direction parseDirection(string direction)
        {
            switch (direction.Trim())
            {
                case "E":
                    return Direction.East;
                case "N":
                    return Direction.North;
                case "W":
                    return Direction.West;
                case "S":
                    return Direction.South;
                default:
                    throw new FormatException("...while parsing the rover heading character.\r\n" +
                        "The heading character has to be 'N','E','W' or 'S' for the four cardinal directions.");
            };
        }
    }
}
