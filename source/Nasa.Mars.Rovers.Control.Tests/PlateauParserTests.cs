using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Nasa.Mars.Rovers.Control.Helpers;

namespace Nasa.Mars.Rovers.Control.Tests
{
    [TestFixture]
    public class PlateauParserTests
    {
        [Test]
        public void should_parse_valid_plateau_coordinates_input()
        {
            var plateau = PlateauParser.Parse("5 5");
            Assert.IsNotNull(plateau);
            Assert.AreEqual(5, plateau.EastBoundary);
            Assert.AreEqual(5, plateau.NorthBoundary);
        }
        [Test, ExpectedException(typeof(FormatException),
            ExpectedMessage = "... while parsing the plateau coordinates.\r\n" + 
            "The expected format is 'x y', where x and y are integers, delimited by single space.")]
        public void should_fail_when_input_is_invalid()
        {
            var plateau = PlateauParser.Parse("a 5");
        }
    }
}
