using System;
using System.Diagnostics;
using System.Reflection;

namespace Nasa.Mars.Rovers.Model.Tests
{
    class TestDebugger
    {
        static void Main(string[] args)
        {
            try
            {
                String NUnitPath = "nunit-console.exe";
                AssemblyName asmName = AssemblyName.GetAssemblyName(NUnitPath);
                AppDomain.CurrentDomain.ExecuteAssemblyByName(asmName, new[] { Assembly.GetExecutingAssembly().Location, 
                    "/framework:4.0" });
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                Trace.WriteLine(ex.StackTrace);
            }
        }
    }
}
