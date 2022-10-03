using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharePointConsole.Classes
{
    internal class CompaniesStatistic
    {
        public string Company { get; set; } = string.Empty;
        public int EmployeeCount { get; set; }
        public string Contact { get; set; } = string.Empty;
        public string Project { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
