using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nasa.Mars.Rovers.Model.Interfaces;
using Nasa.Mars.Rovers.Model;

namespace Nasa.Mars.Rovers.Control.Helpers
{
    public static class PlateauParser
    {
        public static IPlateau Parse(string plateauCoordinatesLine)
        {
            try 
            {
                var coordinates = plateauCoordinatesLine.Split(' ');
                int eastBoundary = Convert.ToInt32(coordinates[0]);
                int northBoundary = Convert.ToInt32(coordinates[1]);
                return new Plateau(eastBoundary, northBoundary);
            }
            catch (Exception ex)
            {
                string message = "... while parsing the plateau coordinates.\r\n"  + 
                "The expected format is 'x y', where x and y are integers, delimited by single space.";
                throw new FormatException(message, ex);
            }
        }
    }
}
