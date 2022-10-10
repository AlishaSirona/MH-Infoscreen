using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbInfoscreenLibrary;

public class VersandDaten
{
    [Key]
    public uint Id { get; set; }
    public DateTime? Date { get; set; } = new DateTime(DateTime.Now.Year, 1, 1);
    public double Verladen { get; set; }
}
