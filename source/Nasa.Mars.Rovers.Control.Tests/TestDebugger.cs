using System;
using System.Reflection;

namespace Nasa.Mars.Rovers.Control.Tests
{
    class TestDebugger
    {
        static void Main(string[] args)
        {
            try
            {
                /*
                 * This test project could have been a class library rather than a console app, but for the debugging
                 * needs from within 2010 Visual Studio C# Express edition...
                 * It would need most of these additional files in Results/bin to be run as the startup project:
                 *                  * 
                 * nunit-console-runner.dll
                 * nunit-console.exe
                 * nunit-gui-runner.dll
                 * nunit.core.dll
                 * nunit.core.interfaces.dll
                 * nunit.uiexception.dll
                 * nunit.uikit.dll
                 * nunit.util.dll
                 *                  * 
                 */
                String NUnitPath = "nunit-console.exe";
                AssemblyName asmName = AssemblyName.GetAssemblyName(NUnitPath);
                AppDomain.CurrentDomain.ExecuteAssemblyByName(asmName, new[] { Assembly.GetExecutingAssembly().Location, 
                    "/framework:4.0" });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                Console.WriteLine("Press return to exit...");
                Console.ReadLine();
            }
        }
    }
}
