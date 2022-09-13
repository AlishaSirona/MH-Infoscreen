using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharePointDataLibrary;

public class AccidentStatistic
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
            AccidentsPerMonth[i][0] = 0;
            AccidentsPerMonth[i][1] = 0;
        }
    }
}
