using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharePointConsole.Classes
{
    internal class AccidentStatistic
    {
        public DateTime LastAccidentDate { get; set; }
        public string LastAccidentDepartment { get; set; } = string.Empty;

        public int AccidentFreeDays { get; set; }

        public int AccidentsLastYear { get; set; }
        public int AccidentsLastYearTillNow { get; set; }
        public int AccidentsThisYear { get; set; }

        public Dictionary<string, int[]> AccidentsPerDepartment { get; set; } = new Dictionary<string, int[]>();

        public Dictionary<int, int[]> AccidentsPerMonth { get; set; } = new Dictionary<int, int[]>();


        public AccidentStatistic()
        {
            for (int i = 1; i < 13; i++)
            {
                AccidentsPerMonth.Add(i, new int[] { 0, 0 });
            }
        }

        public void PrintDebug()
        {
            Console.WriteLine($"Last Date {LastAccidentDate}");
            Console.WriteLine($"Last Dep.: {LastAccidentDepartment}");
            Console.WriteLine($"Free Days {AccidentFreeDays}");
            Console.WriteLine($"Last Year {AccidentsLastYear}");
            Console.WriteLine($"Last Till Now {AccidentsLastYearTillNow}");
            Console.WriteLine($"This Year {AccidentsThisYear}");

            foreach ( var keyValuePair in AccidentsPerDepartment)
            {
                Console.WriteLine($"Key: {keyValuePair.Key} Value: {keyValuePair.Value[0]} {keyValuePair.Value[1]}");
            }

            foreach (var keyValuePair in AccidentsPerMonth)
            {
                Console.WriteLine($"Key: {keyValuePair.Key} Value: {keyValuePair.Value[0]} {keyValuePair.Value[1]}");
            }
        }
    }
}
