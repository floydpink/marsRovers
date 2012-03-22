using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
                Console.WriteLine(AppConstants.ProgramTitle);
                Console.WriteLine(AppConstants.ProgramTitleUnderline);

                string fileOrStdIn = string.Empty;
                while (userInputNotValid(fileOrStdIn))
                {
                    Console.WriteLine(string.Empty);
                    Console.WriteLine(AppConstants.InputDataQuestion);
                    fileOrStdIn = Console.ReadLine().Trim();
                }
                if (fileOrStdIn.ToUpper() != "E")
                {
                    Console.WriteLine(string.Empty);
                    List<string> inputData = captureTestData(fileOrStdIn);

                    if (inputData.Count != 0)
                    {
                        Console.WriteLine(string.Empty);
                        Console.WriteLine(AppConstants.InputCapturedPrefix);
                        Console.WriteLine(string.Empty);
                        foreach (var data in inputData)
                        {
                            Console.WriteLine(data);
                        }
                        var outputLines = processInputAndBuildOutput(inputData);
                        printOutputData(outputLines);
                    }
                    else
                    {
                        Console.WriteLine(string.Empty);
                        Console.WriteLine(AppConstants.GoodBye);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Empty);
                Console.WriteLine(AppConstants.ErrorPrefix);
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine(string.Empty);
                Console.WriteLine(AppConstants.ExitMessageOverline);
                Console.WriteLine(AppConstants.ExitMessage);
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
            Console.WriteLine(AppConstants.TestDataInputFormat);
        }

        private static List<string> getInputFromFile()
        {
            List<string> fileInputData = new List<string>();
            Console.WriteLine(AppConstants.InputDataFilePathQuestion);
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
                    Console.WriteLine(string.Format(AppConstants.InputDataFilePathQuestionRepeatFormat, inputFilePath));
                }
            }

            return fileInputData;
        }

        private static List<string> getInputFromStdIn()
        {
            List<string> stdInData = new List<string>();
            Console.WriteLine(string.Empty);
            Console.WriteLine(AppConstants.ManualTestDataInputQuestion);
            printTestDataFormat();
            Console.WriteLine(AppConstants.EmptyLineInFormatForManualInput);
            Console.WriteLine(string.Empty);

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

        private static IEnumerable<string> processInputAndBuildOutput(List<string> inputData)
        {
            var plateauCoordinatesLine = inputData[0];
            inputData.RemoveAt(0);
            var plateau = PlateauParser.Parse(plateauCoordinatesLine);

            if (inputData.Count % 2 == 1)
            {
                throw new InvalidDataException(AppConstants.RoversDataNeedsTwoLines);
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

            return getOutputLines(plateau, roversAfterNavigation);
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

        private static void printOutputData(IEnumerable<string> outputLines)
        {
            var fileOrStdOut = string.Empty;
            Console.WriteLine(string.Empty);
            Console.WriteLine(AppConstants.DoneProcessing);
            while (fileOrStdOut != "C" && fileOrStdOut != "F")
            {
                Console.WriteLine(string.Empty);
                Console.WriteLine(AppConstants.OutputDataQuestion);
                fileOrStdOut = Console.ReadLine().Trim().ToUpper(); 
            }
            if (fileOrStdOut == "C")
            {
                displayOnStdOut(outputLines);
            }
            else
            {
                var outputfilePath = string.Empty;
                Console.WriteLine(string.Empty);
                Console.WriteLine(AppConstants.OutputFilePathQuestion);
                while (string.IsNullOrEmpty(outputfilePath))
                {
                    outputfilePath = Console.ReadLine().Trim();
                }
                try
                {
                    File.WriteAllLines(outputfilePath, outputLines.ToArray());
                    Console.WriteLine(string.Empty);
                    Console.WriteLine(string.Format(AppConstants.OutputWrittenConfirmationFormat,
                        Path.GetFullPath(outputfilePath)));

                }
                catch (Exception ex)
                {
                    Console.WriteLine(AppConstants.OutputWriteErrorPrefix +
                        ex.Message);
                    displayOnStdOut(outputLines);
                }
            }
        }

        private static void displayOnStdOut(IEnumerable<string> outputLines)
        {
            Console.WriteLine(string.Empty);
            Console.WriteLine(AppConstants.OutputDisplayPrefix);
            foreach (var line in outputLines)
            {
                Console.WriteLine(line);
            }
        }

    }
}
