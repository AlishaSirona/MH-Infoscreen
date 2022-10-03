using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharePointConsole.Unfall;
using SharePointConsole.Visitor;

namespace SharePointConsole
{
    internal class Program
    {
        static void Main()
        {
            AccidentController.GenAccidentJson();
            VisitorController.GenVisitorJson();

            Console.WriteLine("End");
            Console.ReadLine();
        }
    }
}
