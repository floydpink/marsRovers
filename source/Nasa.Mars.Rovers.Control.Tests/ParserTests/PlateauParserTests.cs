using System;
using Nasa.Mars.Rovers.Control.Parsers;
using NUnit.Framework;

namespace Nasa.Mars.Rovers.Control.Tests.ParserTests
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
        public void should_fail_when_input_is_invalid_with_only_one_integer()
        {
            PlateauParser.Parse("5");
        }

        [Test, ExpectedException(typeof(FormatException),
            ExpectedMessage = "... while parsing the plateau coordinates.\r\n" + 
            "The expected format is 'x y', where x and y are integers, delimited by single space.")]
        public void should_fail_when_input_is_invalid()
        {
            PlateauParser.Parse("a 5");
        }
    }
}
