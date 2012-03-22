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
                Console.Title = AppConstants.ProgramTitle;
                writeLineToConsole(AppConstants.ProgramTitle);
                writeLineToConsole(AppConstants.ProgramTitleUnderline);

                string fileOrStdIn = string.Empty;
                while (userInputNotValid(fileOrStdIn))
                {
                    writeLineToConsole(AppConstants.InputDataQuestion, true);
                    fileOrStdIn = Console.ReadLine().Trim();
                }
                if (fileOrStdIn.ToUpper() != "E")
                {
                    List<string> inputData = captureTestData(fileOrStdIn);

                    if (inputData.Count != 0)
                    {
                        writeLineToConsole(AppConstants.InputCapturedPrefix, true);
                        Console.WriteLine(string.Empty);
                        foreach (var data in inputData)
                        {
                            writeLineToConsole(data);
                        }
                        var outputLines = processInputAndBuildOutput(inputData);
                        printOutputData(outputLines);
                    }
                    else
                    {
                        writeLineToConsole(AppConstants.GoodBye, true);
                    }
                }
            }
            catch (Exception ex)
            {
                writeLineToConsole(AppConstants.ErrorPrefix, true);
                writeLineToConsole(ex.Message);
            }
            finally
            {
                writeLineToConsole(AppConstants.ExitMessageOverline, true);
                writeLineToConsole(AppConstants.ExitMessage);
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
            writeLineToConsole(AppConstants.TestDataInputFormat, true);
        }

        private static List<string> getInputFromFile()
        {
            List<string> fileInputData = new List<string>();
            writeLineToConsole(AppConstants.InputDataFilePathQuestion, true);
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
                    writeLineToConsole(string.Format(AppConstants.InputDataFilePathQuestionRepeatFormat, inputFilePath), true);
                }
            }

            return fileInputData;
        }

        private static List<string> getInputFromStdIn()
        {
            List<string> stdInData = new List<string>();
            Console.WriteLine(AppConstants.ManualTestDataInputQuestion, true);
            printTestDataFormat();
            writeLineToConsole(AppConstants.EmptyLineInFormatForManualInput);
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
            writeLineToConsole(AppConstants.DoneProcessing, true);
            while (fileOrStdOut != "C" && fileOrStdOut != "F")
            {
                writeLineToConsole(AppConstants.OutputDataQuestion, true);
                fileOrStdOut = Console.ReadLine().Trim().ToUpper();
            }
            if (fileOrStdOut == "C")
            {
                displayOnStdOut(outputLines);
            }
            else
            {
                var outputfilePath = string.Empty;
                writeLineToConsole(AppConstants.OutputFilePathQuestion, true);
                while (string.IsNullOrEmpty(outputfilePath))
                {
                    outputfilePath = Console.ReadLine().Trim();
                }
                try
                {
                    File.WriteAllLines(outputfilePath, outputLines.ToArray());
                    writeLineToConsole(string.Format(AppConstants.OutputWrittenConfirmationFormat,
                        Path.GetFullPath(outputfilePath)), true);

                }
                catch (Exception ex)
                {
                    writeLineToConsole(AppConstants.OutputWriteErrorPrefix + ex.Message);
                    displayOnStdOut(outputLines);
                }
            }
        }

        private static void displayOnStdOut(IEnumerable<string> outputLines)
        {
            writeLineToConsole(AppConstants.OutputDisplayPrefix, true);
            foreach (var line in outputLines)
            {
                writeLineToConsole(line);
            }
        }

        private static void writeLineToConsole(string message, bool afterAnEmptyLine = false)
        {
            if (afterAnEmptyLine)
            {
                Console.WriteLine(string.Empty);
            }
            Console.WriteLine(message);
        }
    }
}
