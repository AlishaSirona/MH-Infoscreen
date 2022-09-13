using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharePointConsole.Classes
{
    internal class StwStatistic
    {
        public double StundenleistungDay { get; set; }
        public double StundenleistungWeek { get; set; }
        public double StundenleistungMonth { get; set; }
        public double AusbringenDay { get; set; }
        public double AusbringenWeek { get; set; }
        public double AusbringenMonth { get; set; }
        public double PowerOnDay { get; set; }
        public double PowerOnWeek { get; set; }
        public double PowerOnMonth { get; set; }
        public double SchrottDay { get; set; }
        public double SchrottWeek { get; set; }
        public double SchrottMonth { get; set; }
        public double SauerstoffDay { get; set; }
        public double SauerstoffWeek { get; set; }
        public double SauerstoffMonth { get; set; }
        public double PowerOffDay { get; set; }
        public double PowerOffWeek { get; set; }
        public double PowerOffMonth { get; set; }

        public Dictionary<int, double?> ProductionYear { get; set; }


        public StwStatistic()
        {
            ProductionYear = new Dictionary<int, double?>();

            for (int i = 1; i < 13; i++)
            {
                ProductionYear.Add(i, null);
            }
        }


        public void PrintDebug()
        {
            Console.WriteLine($"Stundenleistung {StundenleistungDay} {StundenleistungMonth} {StundenleistungWeek}");
            Console.WriteLine($"Ausbringen {AusbringenDay} {AusbringenWeek} {AusbringenMonth}");
            Console.WriteLine($"Power On {PowerOnDay} {PowerOnWeek} {PowerOnMonth}");
            Console.WriteLine($"Schrott {SchrottDay} {SchrottWeek} {SchrottMonth}");
            Console.WriteLine($"Sauerstoff {SauerstoffDay} {SauerstoffWeek} {SauerstoffMonth}");
            Console.WriteLine($"Power Off {PowerOffDay} {PowerOffWeek} {PowerOffMonth}");
        }
    }
}
