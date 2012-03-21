using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nasa.Mars.Rovers.Model;

namespace Nasa.Mars.Rovers.Control.Helpers
{
    public static class CommandMapper
    {
        public static IEnumerable<Command> Map(string commands)
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
