using System;
using Nasa.Mars.Rovers.Model;
using Nasa.Mars.Rovers.Model.Interfaces;

namespace Nasa.Mars.Rovers.Control.Parsers
{
    public static class PlateauParser
    {
        public static IPlateau Parse(string plateauCoordinatesLine)
        {
            try 
            {
                var coordinates = plateauCoordinatesLine.Split(' ');
                var eastBoundary = Convert.ToInt32(coordinates[0]);
                var northBoundary = Convert.ToInt32(coordinates[1]);
                return new Plateau(eastBoundary, northBoundary);
            }
            catch (Exception ex)
            {
                throw new FormatException(AppConstants.PlateauParserError, ex);
            }
        }
    }
}
