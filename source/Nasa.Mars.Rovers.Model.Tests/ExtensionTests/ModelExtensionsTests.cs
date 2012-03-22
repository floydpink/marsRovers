using System;
using Nasa.Mars.Rovers.Model.Extensions;
using NUnit.Framework;

namespace Nasa.Mars.Rovers.Model.Tests.ExtensionTests
{
    [TestFixture]
    public class ModelExtensionsTests
    {
        [Test]
        public void should_convert_0_degree_to_0_radian()
        {
            int degree = 0;
            double expectedRadian = 0.0;
            Assert.AreEqual(expectedRadian, degree.ToRadian());
        }
        
        [Test]
        public void should_convert_90_degrees_to_half_pi_radians()
        {
            int degree = 90;
            double expectedRadian = Math.PI / 2;
            Assert.AreEqual(expectedRadian, degree.ToRadian());
        }

        [Test]
        public void should_convert_180_degree_to_pi_radians()
        {
            int degree = 180;
            double expectedRadian = Math.PI;
            Assert.AreEqual(expectedRadian, degree.ToRadian());
        }

        [Test]
        public void should_convert_270_degree_to_one_and_half_pi_radians()
        {
            int degree = 270;
            double expectedRadian = 1.5 * Math.PI;
            Assert.AreEqual(expectedRadian, degree.ToRadian());
        }

        [Test]
        public void should_translate_angle_in_degrees_to_Direction_enum()
        {
            Assert.AreEqual(Direction.East, 360.ToDirection());
            Assert.AreEqual(Direction.North, 450.ToDirection());
            Assert.AreEqual(Direction.South, (-90).ToDirection());
        }

        [Test]
        public void should_validate_integer_is_between_lower_and_upper_bounds()
        {
            Assert.IsTrue(5.Between(0, 10));
            Assert.IsTrue(0.Between(0, 10));
            Assert.IsTrue(10.Between(0, 10));
            Assert.IsFalse(11.Between(0, 10));
            Assert.IsFalse((-1).Between(0, 10));
        }
    }
}
