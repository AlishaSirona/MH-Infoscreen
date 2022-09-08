using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbInfoscreenLibrary;

public class Pages
{
    public uint Id { get; set; }
    public uint Order { get; set; }
    public string FileName { get; set; } = string.Empty;
    public uint Duration { get; set; }
    public DateTime StartDate { get; set; } = DateTime.Now.Date;
    public DateTime EndDate { get; set; } = DateTime.Now.AddDays(10).Date;
}
