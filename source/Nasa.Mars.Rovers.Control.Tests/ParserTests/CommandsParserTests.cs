using System.Collections.Generic;
using System.Linq;
using Nasa.Mars.Rovers.Control.Parsers;
using Nasa.Mars.Rovers.Model;
using NUnit.Framework;

namespace Nasa.Mars.Rovers.Control.Tests.ParserTests
{
    [TestFixture]
    class CommandsParserTests
    {
        [Test]
        public void should_correctly_map_series_of_navigation_instructions_in_string_format_to_command_enum()
        {
            const string commands = "LRMMRL";
            IEnumerable<Command> expectedCommands = new List<Command> { Command.Left, Command.Right, Command.Move, Command.Move, Command.Right, Command.Left };
            Assert.IsNotNull(CommandsParser.Parse(commands));
            CollectionAssert.IsNotEmpty(CommandsParser.Parse(commands));
            CollectionAssert.AllItemsAreInstancesOfType(CommandsParser.Parse(commands), typeof(Command));
            CollectionAssert.DoesNotContain(CommandsParser.Parse(commands), Command.Error);
            Assert.AreEqual(commands.Length, CommandsParser.Parse(commands).Count());
            Assert.AreEqual(expectedCommands, CommandsParser.Parse(commands));
        }

        [Test]
        public void should_throw_exception_when_invalid_navigation_instruction_is_input()
        {
            const string commands = "LRMABC";
            Assert.Throws(typeof(System.InvalidOperationException), 
                () => CommandsParser.Parse(commands), 
                "Invalid character found in commands. Valid values are 'L', 'R' or 'M'.");
        }
    }
}
