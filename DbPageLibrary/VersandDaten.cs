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
    public DateTime Date { get; set; }
    public double Verladen { get; set; }
}
