using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharePointConsole.Classes
{
    internal class VisitorStatistic
    {
        public string Title { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public string Firstname { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public string Contact { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }
}
