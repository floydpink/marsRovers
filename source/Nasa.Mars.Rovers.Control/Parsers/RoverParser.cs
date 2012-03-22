using System;
using Nasa.Mars.Rovers.Model;
using Nasa.Mars.Rovers.Model.Interfaces;

namespace Nasa.Mars.Rovers.Control.Parsers
{
    public static class RoverParser
    {
        public static IRover Parse(string roverPositionAndHeadingLine)
        {
            try
            {
                var roverPositionAndHeading = roverPositionAndHeadingLine.Split(' ');
                var easting = Convert.ToInt32(roverPositionAndHeading[0]);
                var northing = Convert.ToInt32(roverPositionAndHeading[1]);
                var heading = ParseDirection(roverPositionAndHeading[2]);

                return new Rover(easting, northing, heading);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(AppConstants.RoverParserErrorPrefix +
                    ex.Message, ex);
            }
        }

        internal static Direction ParseDirection(string direction)
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
                    throw new FormatException(AppConstants.RoverParserParseDirectionError);
            }
        }
    }
}
