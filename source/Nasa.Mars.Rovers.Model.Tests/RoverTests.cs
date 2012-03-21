using System.Diagnostics;
using Nasa.Mars.Rovers.Model;
using NUnit.Framework;

namespace Nasa.Mars.Rovers.Model.Tests
{
    [TestFixture]
    public class RoverTests
    {
        [Test]
        public void should_navigate_correcty_on_Left_command_when_rover_at_1_1_heading_East()
        {
            var rover = new Rover(1, 1, Direction.East);
            rover.Navigate(Command.Left);
            Assert.AreEqual(1, rover.Easting);
            Assert.AreEqual(1, rover.Northing);
            Assert.AreEqual(Direction.North, rover.Heading);
        }

        [Test]
        public void should_navigate_correcty_on_Left_command_when_rover_at_1_1_heading_North()
        {
            var rover = new Rover(1, 1, Direction.North);
            rover.Navigate(Command.Left);
            Assert.AreEqual(1, rover.Easting);
            Assert.AreEqual(1, rover.Northing);
            Assert.AreEqual(Direction.West, rover.Heading);
        }

        [Test]
        public void should_navigate_correcty_on_Left_command_when_rover_at_1_1_heading_West()
        {
            var rover = new Rover(1, 1, Direction.West);
            rover.Navigate(Command.Left);
            Assert.AreEqual(1, rover.Easting);
            Assert.AreEqual(1, rover.Northing);
            Assert.AreEqual(Direction.South, rover.Heading);
        }

        [Test]
        public void should_navigate_correcty_on_Left_command_when_rover_at_1_1_heading_South()
        {
            var rover = new Rover(1, 1, Direction.South);
            rover.Navigate(Command.Left);
            Assert.AreEqual(1, rover.Easting);
            Assert.AreEqual(1, rover.Northing);
            Assert.AreEqual(Direction.East, rover.Heading);
        }

        [Test]
        public void should_navigate_correcty_on_Right_command_when_rover_at_1_1_heading_East()
        {
            var rover = new Rover(1, 1, Direction.East);
            rover.Navigate(Command.Right);
            Assert.AreEqual(1, rover.Easting);
            Assert.AreEqual(1, rover.Northing);
            Assert.AreEqual(Direction.South, rover.Heading);
        }

        [Test]
        public void should_navigate_correcty_on_Right_command_when_rover_at_1_1_heading_North()
        {
            var rover = new Rover(1, 1, Direction.North);
            rover.Navigate(Command.Right);
            Assert.AreEqual(1, rover.Easting);
            Assert.AreEqual(1, rover.Northing);
            Assert.AreEqual(Direction.East, rover.Heading);
        }

        [Test]
        public void should_navigate_correcty_on_Right_command_when_rover_at_1_1_heading_West()
        {
            var rover = new Rover(1, 1, Direction.West);
            rover.Navigate(Command.Right);
            Assert.AreEqual(1, rover.Easting);
            Assert.AreEqual(1, rover.Northing);
            Assert.AreEqual(Direction.North, rover.Heading);
        }

        [Test]
        public void should_navigate_correcty_on_Right_command_when_rover_at_1_1_heading_South()
        {
            var rover = new Rover(1, 1, Direction.South);
            rover.Navigate(Command.Right);
            Assert.AreEqual(1, rover.Easting);
            Assert.AreEqual(1, rover.Northing);
            Assert.AreEqual(Direction.West, rover.Heading);
        }

        [Test]
        public void should_navigate_correcty_on_Move_command_when_rover_at_1_1_heading_East()
        {
            var rover = new Rover(1, 1, Direction.East);
            rover.Navigate(Command.Move);
            Assert.AreEqual(2, rover.Easting);
            Assert.AreEqual(1, rover.Northing);
            Assert.AreEqual(Direction.East, rover.Heading);
        }

        [Test]
        public void should_navigate_correcty_on_Move_command_when_rover_at_1_1_heading_North()
        {
            var rover = new Rover(1, 1, Direction.North);
            rover.Navigate(Command.Move);
            Assert.AreEqual(1, rover.Easting);
            Assert.AreEqual(2, rover.Northing);
            Assert.AreEqual(Direction.North, rover.Heading);
        }

        [Test]
        public void should_navigate_correcty_on_Move_command_when_rover_at_1_1_heading_West()
        {
            var rover = new Rover(1, 1, Direction.West);
            rover.Navigate(Command.Move);
            Assert.AreEqual(0, rover.Easting);
            Assert.AreEqual(1, rover.Northing);
            Assert.AreEqual(Direction.West, rover.Heading);
        }

        [Test]
        public void should_navigate_correcty_on_Move_command_when_rover_at_1_1_heading_South()
        {
            var rover = new Rover(1, 1, Direction.South);
            rover.Navigate(Command.Move);
            Assert.AreEqual(1, rover.Easting);
            Assert.AreEqual(0, rover.Northing);
            Assert.AreEqual(Direction.South, rover.Heading);
        }

        [Test]
        public void should_calculate_turn_angle_in_degrees_for_left_turn()
        {
            Assert.AreEqual(90, Rover.calculateTurn(Command.Left));
        }

        [Test]
        public void should_calculate_turn_angle_in_degrees_for_right_turn()
        {
            Assert.AreEqual(-90, Rover.calculateTurn(Command.Right));
        }

        [Test]
        public void should_calculate_turn_angle_in_degrees_for_move_ahead()
        {
            Assert.AreEqual(0, Rover.calculateTurn(Command.Move));
        }

        [Test]
        public void should_calculate_turn_angle_in_degrees_for_erroneous_command()
        {
            Assert.AreEqual(0, Rover.calculateTurn(Command.Error));
        }
    }
}
