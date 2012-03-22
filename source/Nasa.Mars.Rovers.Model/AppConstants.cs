namespace Nasa.Mars.Rovers.Model
{
    public static class AppConstants
    {
        public static readonly int TurnInDegrees = 90;

        //string literals
        public static readonly string RoverBeyondLimits = "Rover Beyond Plateau Boundaries.";

        public static readonly string ProgramTitle = "The NASA Mars Rovers Program";
        public static readonly string ProgramTitleUnderline = "----------------------------";
        public static readonly string InputDataQuestion = "Enter 'm' to manually type in the test data and \r\n" +
            "      'f' to enter the path to input file.\r\nOr enter 'e' to exit and hit return:";
        public static readonly string GoodBye = "Nothing to do here. Good bye!";
        public static readonly string ErrorPrefix = "An error has occured...";
        public static readonly string InputCapturedPrefix = "The input captured is:";
        public static readonly string ExitMessageOverline = "_______________________";
        public static readonly string ExitMessage = "Press return to exit...";
        public static readonly string TestDataInputFormat = 
            "x y\t\t// x & y int - plateau's top-right corner co-ordinates\r\n" +
            "a b 'h'\t\t// a & b int - rover #1's position & 'h' char - heading\r\n" +
            "'c''c'...'c'\t// each 'c' char - sequential navigation commands to rover #1\r\n" +
            "l m 'h'\t\t// rover #2 position and heading\r\n" +
            "'c''c'...'c'\t// rover #2 navigation commands\r\n" +
            "...\t\t// keep going for n rovers\r\n" +
            "...\r\n" +
            "...\r\n" +
            "p q 'h'\t\t// rover #n position and heading\r\n" +
            "'c''c'...'c'\t// rover #n navigation commands\r\n";
        public static readonly string InputDataFilePathQuestion = 
            "Type the path to the file with input data in the below format:";
        public static readonly string InputDataFilePathQuestionRepeatFormat = 
            "You entered '{0}', and no such file exists. Try again or type 'e' to exit:";
        public static readonly string ManualTestDataInputQuestion = 
            "Type in the test data in the below format:";
        public static readonly string EmptyLineInFormatForManualInput = 
            "\t\t// empty line marking the end of test data input";
        public static readonly string RoversDataNeedsTwoLines = "...while processing the Rovers data.\r\n" +
            "Each rover needs two lines of data as detailed above.";
        public static readonly string DoneProcessing = "Done processing the input!";
        public static readonly string OutputDataQuestion = "Enter 'c' to view output on console or\r\n" +
            "      'f' to write it into an output file.";
        public static readonly string OutputFilePathQuestion = "Type the path to the output file:";
        public static readonly string OutputWrittenConfirmationFormat = "Output has been written into the file at {0}";
        public static readonly string OutputWriteErrorPrefix = "An error occured, while attmpting to write to the file specified\r\n";
        public static readonly string OutputDisplayPrefix = "The output is:";
    }
}
