﻿using NUnit.Framework;
using Nasa.Mars.Rovers.Model;
using Nasa.Mars.Rovers.Console.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace Nasa.Mars.Rovers.Logic.Tests
{
    [TestFixture]
    class CommandMapperTests
    {
        [Test]
        public void should_correctly_map_series_of_navigation_instructions_in_string_format_to_command_enum()
        {
            string commands = "LRMMRL";
            IEnumerable<Command> expectedCommands = new List<Command> { Command.Left, Command.Right, Command.Move, Command.Move, Command.Right, Command.Left };
            Assert.IsNotNull(CommandMapper.Map(commands));
            CollectionAssert.IsNotEmpty(CommandMapper.Map(commands));
            CollectionAssert.AllItemsAreInstancesOfType(CommandMapper.Map(commands), typeof(Command));
            CollectionAssert.DoesNotContain(CommandMapper.Map(commands), Command.Error);
            Assert.AreEqual(commands.Length, CommandMapper.Map(commands).Count());
            Assert.AreEqual(expectedCommands, CommandMapper.Map(commands));
        }

        [Test]
        public void should_throw_exception_when_invalid_navigation_instruction_is_input()
        {
            string commands = "LRMABC";
            Assert.Throws(typeof(System.InvalidOperationException), 
                () => CommandMapper.Map(commands), 
                "Invalid character found in commands. Valid values are 'L', 'R' or 'M'.");
        }
    }
}
