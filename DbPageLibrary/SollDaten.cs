using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbInfoscreenLibrary;

public class SollDaten
{
    [Key]
    public uint Id { get; set; }
    public string Abteilung { get; set; } = string.Empty;
    public DateTime TimeStamp { get; set; }
    public double Value { get; set; }

}
