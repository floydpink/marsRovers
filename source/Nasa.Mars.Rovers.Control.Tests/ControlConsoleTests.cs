using System.Collections.Generic;
using System.Linq;
using Nasa.Mars.Rovers.Model;
using Nasa.Mars.Rovers.Model.Interfaces;
using NUnit.Framework;

namespace Nasa.Mars.Rovers.Control.Tests
{
    [TestFixture]
    public class ControlConsoleTests
    {
        [Test]
        public void should_validate_user_input_for_test_data_input_or_exit()
        {
            Assert.IsFalse(Program.UserInputNotValid("M"));
            Assert.IsFalse(Program.UserInputNotValid("m"));
            Assert.IsFalse(Program.UserInputNotValid("F"));
            Assert.IsFalse(Program.UserInputNotValid("f"));
            Assert.IsFalse(Program.UserInputNotValid("E"));
            Assert.IsFalse(Program.UserInputNotValid("e"));
            Assert.IsTrue(Program.UserInputNotValid(string.Empty));
            Assert.IsTrue(Program.UserInputNotValid("mf"));
            Assert.IsTrue(Program.UserInputNotValid(" m "));
        }

        [Test]
        public void should_execute_navigation_instructions_on_all_rovers()
        {
            var roversAndInstructions = new Dictionary<IRover, IEnumerable<Command>>
                                            {
                { 
                    new Rover(1, 2, Direction.North), 
                    new List<Command> { Command.Left,Command.Move,Command.Left,Command.Move,
                        Command.Left,Command.Move,Command.Left,Command.Move,Command.Move }
                },
                { 
                    new Rover(3, 3, Direction.East), 
                    new List<Command> { Command.Move,Command.Move,Command.Right,Command.Move,
                        Command.Move,Command.Right,Command.Move,Command.Right,Command.Right,Command.Move }
                }
            };
            var navigatedRovers = Program.ExecuteNavigationCommands(roversAndInstructions);
            Assert.AreEqual(1, navigatedRovers[0].Easting);
            Assert.AreEqual(3, navigatedRovers[0].Northing);
            Assert.AreEqual(Direction.North, navigatedRovers[0].Heading);
            Assert.AreEqual(5, navigatedRovers[1].Easting);
            Assert.AreEqual(1, navigatedRovers[1].Northing);
            Assert.AreEqual(Direction.East, navigatedRovers[1].Heading);
        }

        [Test]
        public void should_print_output_for_all_rovers()
        {
            var plateau = new Plateau(5, 5);
            var navigatedRovers = new List<IRover> {
                new Rover(1, 3, Direction.North),
                new Rover(5, 1, Direction.South),
                new Rover(2, 2, Direction.East),
                new Rover(1, 3, Direction.West),
                new Rover(7, 2, Direction.West)
            };
            var output = Program.GetOutputLines(plateau, navigatedRovers).ToList();
            Assert.AreEqual("1 3 N", output[0]);
            Assert.AreEqual("5 1 S", output[1]);
            Assert.AreEqual("2 2 E", output[2]);
            Assert.AreEqual("1 3 W", output[3]);
            Assert.AreEqual(AppConstants.RoverBeyondLimits, output[4]);
        }
    }
}
