namespace Infoscreen.Models;

public class LineChartValues
{
    public int MonthNumber { get; set; }
    public DateTime LastDay { get; set; }
    public double Value { get; set; }

    public double RoundenValue => Math.Round(Value, 2, MidpointRounding.AwayFromZero);
}
