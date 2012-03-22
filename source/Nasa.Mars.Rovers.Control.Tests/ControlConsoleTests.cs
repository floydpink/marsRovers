using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Nasa.Mars.Rovers.Control.Tests
{
    [TestFixture]
    public class ControlConsoleTests
    {
        [Test]
        public void should_validate_user_input_for_test_data_input_or_exit()
        {
            Assert.IsFalse(Program.userInputNotValid("M"));
            Assert.IsFalse(Program.userInputNotValid("m"));
            Assert.IsFalse(Program.userInputNotValid("F"));
            Assert.IsFalse(Program.userInputNotValid("f"));
            Assert.IsFalse(Program.userInputNotValid("E"));
            Assert.IsFalse(Program.userInputNotValid("e"));
            Assert.IsTrue(Program.userInputNotValid(string.Empty));
            Assert.IsTrue(Program.userInputNotValid("mf"));
            Assert.IsTrue(Program.userInputNotValid(" m "));
        }
    }
}
