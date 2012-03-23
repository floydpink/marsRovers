using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Nasa.Mars.Rovers.Control.Parsers;
using Nasa.Mars.Rovers.Model;
using Nasa.Mars.Rovers.Model.Interfaces;

namespace Nasa.Mars.Rovers.Control
{
    static class Program
    {
        static void Main()
        {
            try
            {
                Console.Title = AppConstants.ProgramTitle;
                WriteLineToConsole(AppConstants.ProgramTitle);
                WriteLineToConsole(AppConstants.ProgramTitleUnderline);

                var fileOrStdIn = string.Empty;
                while (UserInputNotValid(fileOrStdIn))
                {
                    WriteLineToConsole(AppConstants.InputDataQuestion, true);
                    fileOrStdIn = Console.ReadLine().Trim();
                }
                if (fileOrStdIn.ToUpper() != "E")
                {
                    var inputData = CaptureTestData(fileOrStdIn);

                    if (inputData.Count != 0)
                    {
                        WriteLineToConsole(AppConstants.InputCapturedPrefix, true);
                        Console.WriteLine(string.Empty);
                        foreach (var data in inputData)
                        {
                            WriteLineToConsole(data);
                        }
                        var outputLines = ProcessInputAndBuildOutput(inputData);
                        PrintOutputData(outputLines);
                    }
                    else
                    {
                        WriteLineToConsole(AppConstants.GoodBye, true);
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLineToConsole(AppConstants.ErrorPrefix, true);
                WriteLineToConsole(ex.Message);
            }
            finally
            {
                WriteLineToConsole(AppConstants.ExitMessageOverline, true);
                WriteLineToConsole(AppConstants.ExitMessage);
                Console.ReadLine();
            }
        }

        internal static bool UserInputNotValid(string fileOrStdIn)
        {
            return fileOrStdIn.ToUpper() != "M" && fileOrStdIn.ToUpper() != "F" && fileOrStdIn.ToUpper() != "E";
        }

        private static List<string> CaptureTestData(string fileOrStdIn)
        {
            var inputData = new List<string>();
            switch (fileOrStdIn.ToUpper())
            {
                case "M":
                    inputData = GetInputFromStdIn();
                    break;
                case "F":
                    inputData = GetInputFromFile();
                    break;
            }
            return inputData;
        }

        private static void PrintTestDataFormat()
        {
            WriteLineToConsole(AppConstants.TestDataInputFormat, true);
        }

        private static List<string> GetInputFromFile()
        {
            var fileInputData = new List<string>();
            WriteLineToConsole(AppConstants.InputDataFilePathQuestion, true);
            PrintTestDataFormat();

            var validInputFile = false;
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
                    WriteLineToConsole(string.Format(AppConstants.InputDataFilePathQuestionRepeatFormat, inputFilePath), true);
                }
            }

            return fileInputData;
        }

        private static List<string> GetInputFromStdIn()
        {
            var stdInData = new List<string>();
            WriteLineToConsole(AppConstants.ManualTestDataInputQuestion, true);
            PrintTestDataFormat();
            WriteLineToConsole(AppConstants.EmptyLineInFormatForManualInput);
            Console.WriteLine(string.Empty);

            var endOfTestData = false;
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

        private static IEnumerable<string> ProcessInputAndBuildOutput(List<string> inputData)
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
                for (var i = 0; i < 2; i++)
                {
                    roverData.Add(inputData[0]);
                    inputData.RemoveAt(0);
                }
                roversAndInstructions.Add(RoverParser.Parse(roverData[0]), CommandsParser.Parse(roverData[1]));
            }

            var roversAfterNavigation = ExecuteNavigationCommands(roversAndInstructions);

            return GetOutputLines(plateau, roversAfterNavigation);
        }

        internal static List<IRover> ExecuteNavigationCommands(Dictionary<IRover,
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

        internal static IEnumerable<string> GetOutputLines(IPlateau plateau, IEnumerable<IRover> rovers)
        {
            var output = new List<string>();
            foreach (var rover in rovers)
            {
                output.Add(string.Format("{0} {1} {2}{3}",
                    rover.Easting.ToString(CultureInfo.InvariantCulture),
                    rover.Northing.ToString(CultureInfo.InvariantCulture),
                    rover.Heading.ToString().Substring(0, 1),
                    plateau.IsRoverWithinLimits(rover) ? string.Empty : AppConstants.BeyondLimits));
            }
            return output;
        }

        private static void PrintOutputData(IEnumerable<string> outputLines)
        {
            var fileOrStdOut = string.Empty;
            WriteLineToConsole(AppConstants.DoneProcessing, true);
            while (fileOrStdOut != "C" && fileOrStdOut != "F")
            {
                WriteLineToConsole(AppConstants.OutputDataQuestion, true);
                fileOrStdOut = Console.ReadLine().Trim().ToUpper();
            }
            if (fileOrStdOut == "C")
            {
                DisplayOnStdOut(outputLines);
            }
            else
            {
                var outputfilePath = string.Empty;
                WriteLineToConsole(AppConstants.OutputFilePathQuestion, true);
                while (string.IsNullOrEmpty(outputfilePath))
                {
                    outputfilePath = Console.ReadLine().Trim();
                }
                try
                {
                    File.WriteAllLines(outputfilePath, outputLines.ToArray());
                    WriteLineToConsole(string.Format(AppConstants.OutputWrittenConfirmationFormat,
                        Path.GetFullPath(outputfilePath)), true);

                }
                catch (Exception ex)
                {
                    WriteLineToConsole(AppConstants.OutputWriteErrorPrefix + ex.Message);
                    DisplayOnStdOut(outputLines);
                }
            }
        }

        private static void DisplayOnStdOut(IEnumerable<string> outputLines)
        {
            WriteLineToConsole(AppConstants.OutputDisplayPrefix, true);
            foreach (var line in outputLines)
            {
                WriteLineToConsole(line);
            }
        }

        private static void WriteLineToConsole(string message, bool afterAnEmptyLine = false)
        {
            if (afterAnEmptyLine)
            {
                Console.WriteLine(string.Empty);
            }
            Console.WriteLine(message);
        }
    }
}
