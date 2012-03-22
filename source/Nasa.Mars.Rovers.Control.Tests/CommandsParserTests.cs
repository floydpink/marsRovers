using NUnit.Framework;
using Nasa.Mars.Rovers.Model;
using Nasa.Mars.Rovers.Control.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace Nasa.Mars.Rovers.Control.Tests
{
    [TestFixture]
    class CommandsParserTests
    {
        [Test]
        public void should_correctly_map_series_of_navigation_instructions_in_string_format_to_command_enum()
        {
            string commands = "LRMMRL";
            IEnumerable<Command> expectedCommands = new List<Command> { Command.Left, Command.Right, Command.Move, Command.Move, Command.Right, Command.Left };
            Assert.IsNotNull(CommandsParser.Map(commands));
            CollectionAssert.IsNotEmpty(CommandsParser.Map(commands));
            CollectionAssert.AllItemsAreInstancesOfType(CommandsParser.Map(commands), typeof(Command));
            CollectionAssert.DoesNotContain(CommandsParser.Map(commands), Command.Error);
            Assert.AreEqual(commands.Length, CommandsParser.Map(commands).Count());
            Assert.AreEqual(expectedCommands, CommandsParser.Map(commands));
        }

        [Test]
        public void should_throw_exception_when_invalid_navigation_instruction_is_input()
        {
            string commands = "LRMABC";
            Assert.Throws(typeof(System.InvalidOperationException), 
                () => CommandsParser.Map(commands), 
                "Invalid character found in commands. Valid values are 'L', 'R' or 'M'.");
        }
    }
}
