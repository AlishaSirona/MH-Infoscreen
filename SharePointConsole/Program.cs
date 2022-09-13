using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharePointConsole.Unfall;

namespace SharePointConsole
{
    internal class Program
    {
        static void Main()
        {
            AccidentController.GenAccidentJson();

            Console.WriteLine("End");
            Console.ReadLine();
        }
    }
}
