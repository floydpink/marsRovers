using NUnit.Framework;

namespace Nasa.Mars.Rovers.Model.Tests
{
    [TestFixture]
    public class PlateauTests
    {
        [Test]
        public void should_validate_rover_as_within_plateau_limits_when_easting_and_northing_less_than_eastboundary_and_northboundary()
        {
            var plateau = new Plateau(5, 5);
            var rover = new Rover(3, 4, Direction.North);
            Assert.IsTrue(plateau.IsRoverWithinLimits(rover));
        }

        [Test]
        public void should_not_validate_rover_as_within_plateau_limits_when_easting_more_than_eastboundary()
        {
            var plateau = new Plateau(5, 5);
            var rover = new Rover(6, 4, Direction.East);
            Assert.False(plateau.IsRoverWithinLimits(rover));
        }

        [Test]
        public void should_not_validate_rover_as_within_plateau_limits_when_northing_more_than_northboundary()
        {
            var plateau = new Plateau(5, 5);
            var rover = new Rover(3, 7, Direction.West);
            Assert.False(plateau.IsRoverWithinLimits(rover));
        }

        [Test]
        public void should_not_validate_rover_as_within_plateau_limits_when_easting_less_than_0()
        {
            var plateau = new Plateau(5, 5);
            var rover = new Rover(-1, 4, Direction.North);
            Assert.False(plateau.IsRoverWithinLimits(rover));
        }

        [Test]
        public void should_not_validate_rover_as_within_plateau_limits_when_northing_less_than_0()
        {
            var plateau = new Plateau(5, 5);
            var rover = new Rover(3, -4, Direction.South);
            Assert.False(plateau.IsRoverWithinLimits(rover));
        }
    }
}
