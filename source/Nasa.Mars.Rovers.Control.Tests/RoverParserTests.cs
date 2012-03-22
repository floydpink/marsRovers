using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Nasa.Mars.Rovers.Model;
using Nasa.Mars.Rovers.Control.Parsers;

namespace Nasa.Mars.Rovers.Control.Tests
{
    [TestFixture]
    public class RoverParserTests
    {
        [Test]
        public void should_parse_direction_character_for_East()
        {
            Assert.AreEqual(Direction.East, RoverParser.parseDirection("E"));
        }
        
        [Test]
        public void should_parse_direction_character_for_North()
        {
            Assert.AreEqual(Direction.North, RoverParser.parseDirection("N"));
        }
        
        [Test]
        public void should_parse_direction_character_for_West()
        {
            Assert.AreEqual(Direction.West, RoverParser.parseDirection("W"));
        }

        [Test]
        public void should_parse_direction_character_for_South()
        {
            Assert.AreEqual(Direction.South, RoverParser.parseDirection("S"));
        }

        [Test, ExpectedException(typeof(FormatException),
            ExpectedMessage="...while parsing the rover heading character.\r\n" +
            "The heading character has to be 'N','E','W' or 'S' for the four cardinal directions.")]
        public void should_parse_direction_character_is_invalid()
        {
            RoverParser.parseDirection("s");
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
            var rover = RoverParser.Parse("A 3 E");
        }
    }
}
