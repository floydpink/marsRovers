using System;
using Nasa.Mars.Rovers.Control.Parsers;
using Nasa.Mars.Rovers.Model;
using NUnit.Framework;

namespace Nasa.Mars.Rovers.Control.Tests.ParserTests
{
    [TestFixture]
    public class RoverParserTests
    {
        [Test]
        public void should_parse_direction_character_for_East()
        {
            Assert.AreEqual(Direction.East, RoverParser.ParseDirection("E"));
        }
        
        [Test]
        public void should_parse_direction_character_for_North()
        {
            Assert.AreEqual(Direction.North, RoverParser.ParseDirection("N"));
        }
        
        [Test]
        public void should_parse_direction_character_for_West()
        {
            Assert.AreEqual(Direction.West, RoverParser.ParseDirection("W"));
        }

        [Test]
        public void should_parse_direction_character_for_South()
        {
            Assert.AreEqual(Direction.South, RoverParser.ParseDirection("S"));
        }

        [Test, ExpectedException(typeof(FormatException),
            ExpectedMessage="...while parsing the rover heading character.\r\n" +
            "The heading character has to be 'N','E','W' or 'S' for the four cardinal directions.")]
        public void should_parse_direction_character_is_invalid()
        {
            RoverParser.ParseDirection("s");
        }

        [Test]
        public void should_parse_rover_position_coordinates_and_direction()
        {
            var rover = RoverParser.Parse("0 3 E");
            Assert.AreEqual(0, rover.Easting);
            Assert.AreEqual(3, rover.Northing);
            Assert.AreEqual(Direction.East, rover.Heading);
        }

        [Test, ExpectedException(typeof(InvalidOperationException))]
        public void should_fail_parsing_invalid_rover_position_coordinates_and_direction()
        {
            RoverParser.Parse("A 3 E");
        }
    }
}
