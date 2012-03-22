namespace Nasa.Mars.Rovers.Model
{
    public static class AppConstants
    {
        public const int TurnInDegrees = 90;

        //string literals
        public const string RoverBeyondLimits = "Rover Beyond Plateau Boundaries.";

        public const string ProgramTitle = "The NASA Mars Rovers Program";
        public const string ProgramTitleUnderline = "----------------------------";
        public const string InputDataQuestion = "Enter 'm' to manually type in the test data and \r\n" + 
            "      'f' to enter the path to input file.\r\nOr enter 'e' to exit and hit return:";
        public const string GoodBye = "Nothing to do here. Good bye!";
        public const string ErrorPrefix = "An error has occured...";
        public const string InputCapturedPrefix = "The input captured is:";
        public const string ExitMessageOverline = "_______________________";
        public const string ExitMessage = "Press return to exit...";
        public const string TestDataInputFormat = "x y\t\t// x & y int - plateau's top-right corner co-ordinates\r\n" + 
            "a b 'h'\t\t// a & b int - rover #1's position & 'h' char - heading\r\n" + 
            "'c''c'...'c'\t// each 'c' char - sequential navigation commands to rover #1\r\n" + 
            "l m 'h'\t\t// rover #2 position and heading\r\n" + 
            "'c''c'...'c'\t// rover #2 navigation commands\r\n" + 
            "...\t\t// keep going for n rovers\r\n" + 
            "...\r\n" + 
            "...\r\n" + 
            "p q 'h'\t\t// rover #n position and heading\r\n" + 
            "'c''c'...'c'\t// rover #n navigation commands\r\n";

        public const string InputDataFilePathQuestion = "Type the path to the file with input data in the below format:";
        public const string InputDataFilePathQuestionRepeatFormat = "You entered '{0}', and no such file exists. Try again or type 'e' to exit:";
        public const string ManualTestDataInputQuestion = "Type in the test data in the below format:";
        public const string EmptyLineInFormatForManualInput = "\t\t// empty line marking the end of test data input";
        public const string RoversDataNeedsTwoLines = "...while processing the Rovers data.\r\n" + "Each rover needs two lines of data as detailed above.";
        public const string DoneProcessing = "Done processing the input!";
        public const string OutputDataQuestion = "Enter 'c' to view output on console or\r\n" + 
            "      'f' to write it into an output file.";
        public const string OutputFilePathQuestion = "Type the path to the output file:";
        public const string OutputWrittenConfirmationFormat = "Output has been written into the file at {0}";
        public const string OutputWriteErrorPrefix = "An error occured, while attmpting to write to the file specified\r\n";
        public const string OutputDisplayPrefix = "The output is:";

        public const string RoverParserErrorPrefix = "...while processing the Rovers data.\r\n";
        public const string RoverParserParseDirectionError = "...while parsing the rover heading character.\r\n" + 
            "The heading character has to be 'N','E','W' or 'S' for the four cardinal directions.";

        public const string PlateauParserError = "... while parsing the plateau coordinates.\r\n" + 
            "The expected format is 'x y', where x and y are integers, delimited by single space.";

        public const string CommandParserError = "Invalid character found in commands. Valid values are 'L', 'R' or 'M'.";
    }
}
