using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nasa.Mars.Rovers.Control.Parsers;
using Nasa.Mars.Rovers.Model;
using Nasa.Mars.Rovers.Model.Interfaces;

namespace Nasa.Mars.Rovers.Control
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string fileOrStdIn = string.Empty;
                while (userInputNotValid(fileOrStdIn))
                {
                    Console.WriteLine("Enter 'm' to mnualy type in the test data and ");
                    Console.WriteLine("      'f' to enter the path to input file.");
                    Console.WriteLine("Or enter 'e' to exit and hit return:");
                    fileOrStdIn = Console.ReadLine().Trim();
                }
                if (fileOrStdIn.ToUpper() != "E")
                {
                    Console.WriteLine(string.Empty);
                    List<string> inputData = captureTestData(fileOrStdIn);

                    if (inputData.Count != 0)
                    {
                        Console.WriteLine("The input captured is:");
                        Console.WriteLine(string.Empty);
                        foreach (var data in inputData)
                        {
                            Console.WriteLine(data);
                        }
                        processInputDataAndDisplayOutput(inputData);
                    }
                    else
                    {
                        Console.WriteLine("Nothing to do here. Good bye!");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Empty);
                Console.WriteLine("An error has occured...");
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine(string.Empty);
                Console.WriteLine("_______________________");
                Console.WriteLine("Press return to exit...");
                Console.ReadLine();
            }
        }

        internal static bool userInputNotValid(string fileOrStdIn)
        {
            return fileOrStdIn.ToUpper() != "M" && fileOrStdIn.ToUpper() != "F" && fileOrStdIn.ToUpper() != "E";
        }

        private static List<string> captureTestData(string fileOrStdIn)
        {
            List<string> inputData = new List<string>();
            Console.WriteLine(string.Empty);
            Console.WriteLine(string.Format("You entered {0}", fileOrStdIn));
            if (fileOrStdIn.ToUpper() == "M")
            {
                inputData = getInputFromStdIn();
            }
            else if (fileOrStdIn.ToUpper() == "F")
            {
                inputData = getInputFromFile();
            }
            return inputData;
        }

        private static void printTestDataFormat()
        {
            Console.WriteLine(string.Empty);
            Console.WriteLine("x y\t\t// x & y int - plateau's top-right corner co-ordinates");
            Console.WriteLine("a b 'h'\t\t// a & b int - rover #1's position & 'h' char - heading");
            Console.WriteLine("'c''c'...'c'\t// each 'c' char - sequential navigation commands to rover #1");
            Console.WriteLine("l m 'h'\t\t// rover #2 position and heading");
            Console.WriteLine("'c''c'...'c'\t// rover #2 navigation commands");
            Console.WriteLine("...\t\t// keep going for n rovers");
            Console.WriteLine("...");
            Console.WriteLine("...");
            Console.WriteLine("p q 'h'\t\t// rover #n position and heading");
            Console.WriteLine("'c''c'...'c'\t// rover #n navigation commands");
        }

        private static List<string> getInputFromFile()
        {
            List<string> fileInputData = new List<string>();
            Console.WriteLine("Type the path to the file with input data in the below format:");
            printTestDataFormat();

            bool validInputFile = false;
            while (!validInputFile)
            {
                var inputFilePath = Console.ReadLine().Trim();
                if (inputFilePath.ToUpper() == "E")
                {
                    break;
                }
                if (!string.IsNullOrEmpty(inputFilePath) && File.Exists(inputFilePath))
                {
                    validInputFile = true;
                    fileInputData.AddRange(File.ReadAllLines(inputFilePath));
                }
                else
                {
                    Console.WriteLine(string.Format("You entered '{0}', and no such file exists. Try again or type 'e' to exit:", inputFilePath));
                }
            }

            return fileInputData;
        }

        private static List<string> getInputFromStdIn()
        {
            List<string> stdInData = new List<string>();
            Console.WriteLine("Type in the test data in the below format:");
            printTestDataFormat();
            Console.WriteLine("\t\t// empty line marking the end of test data input");

            bool endOfTestData = false;
            while (!endOfTestData)
            {
                var inputLine = Console.ReadLine().Trim().ToUpper();
                if (string.IsNullOrEmpty(inputLine))
                {
                    endOfTestData = true;
                }
                else
                {
                    stdInData.Add(inputLine);
                }
            }

            return stdInData;
        }

        private static void processInputDataAndDisplayOutput(List<string> inputData)
        {
            //TODO: untested method - refactor into something like MVP if there is time
            var plateauCoordinatesLine = inputData[0];
            inputData.RemoveAt(0);
            var plateau = PlateauParser.Parse(plateauCoordinatesLine);

            if (inputData.Count % 2 == 1)
            {
                throw new InvalidDataException("...while processing the Rovers data.\r\n" +
                    "Each rover needs two lines of data as detailed above.");
            }

            var roversAndInstructions = new Dictionary<IRover, IEnumerable<Command>>();

            while (inputData.Count > 0)
            {
                var roverData = new List<string>();
                for (int i = 0; i < 2; i++)
                {
                    roverData.Add(inputData[0]);
                    inputData.RemoveAt(0);
                }
                roversAndInstructions.Add(RoverParser.Parse(roverData[0]), CommandsParser.Parse(roverData[1]));
            }

            var roversAfterNavigation = executeNavigationCommands(roversAndInstructions);

            var ouput = getOutputLines(plateau, roversAfterNavigation);
        }

        internal static List<IRover> executeNavigationCommands(Dictionary<IRover,
            IEnumerable<Command>> roversAndInstructions)
        {
            var rovers = new List<IRover>();
            foreach (var roverInstructionPair in roversAndInstructions)
            {
                var rover = roverInstructionPair.Key;
                foreach (var navigationCommand in roverInstructionPair.Value)
                {
                    rover.Navigate(navigationCommand);
                }
                rovers.Add(rover);
            }
            return rovers;
        }

        internal static IEnumerable<string> getOutputLines(IPlateau plateau, List<IRover> rovers)
        {
            var output = new List<string>();
            foreach (var rover in rovers)
            {
                if (plateau.IsRoverWithinLimits(rover))
                {
                    output.Add(string.Format("{0} {1} {2}", rover.Easting.ToString(), rover.Northing.ToString(),
                        rover.Heading.ToString().Substring(0, 1)));
                }
                else
                {
                    output.Add(AppConstants.RoverBeyondLimits);
                }
            }
            return output;
        }

    }
}
