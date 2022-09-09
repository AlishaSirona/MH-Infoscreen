using System.Runtime.InteropServices;

namespace Infoscreen.Data;

public class SinglePage
{
    public bool IsImage { get; set; }
    public uint Order { get; set; }
    public string FilePath { get; set; } = string.Empty;
    public TimeSpan Duration { get; set; } = new TimeSpan(0, 0, 15);
    public DateTime StartDate { get; set; } = DateTime.Now.Date;
    public DateTime EndDate { get; set; } = DateTime.Now.AddDays(10).Date;

    private string _delimiter => RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "\\" : "/";

    public string InternFileName => IsImage ? FilePath.Substring(FilePath.LastIndexOf(_delimiter) + 1) : FilePath;
    public string InternFilePath => IsImage ? $"{_delimiter}img{_delimiter}{FilePath.Substring(FilePath.LastIndexOf(_delimiter))}" : FilePath;
    public string InternPosition => $"{Order};{InternFilePath}";
}
