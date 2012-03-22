using System;
using System.Collections.Generic;
using Nasa.Mars.Rovers.Model;

namespace Nasa.Mars.Rovers.Control.Parsers
{
    public static class CommandsParser
    {
        public static IEnumerable<Command> Parse(string commands)
        {
            var commandsList = new List<Command>();
            foreach (var commandCharacter in commands)
            {
                Command command = Command.Error;
                switch (commandCharacter)
                {
                    case 'L':
                        command = Command.Left;
                        break;
                    case 'R':
                        command = Command.Right;
                        break;
                    case 'M':
                        command = Command.Move;
                        break;
                    default:
                        throw new InvalidOperationException("Invalid character found in commands. Valid values are 'L', 'R' or 'M'.");
                }
                commandsList.Add(command);
            }
            return commandsList;
        }
    }
}
