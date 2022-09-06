namespace Infoscreen.Data;

public class SinglePage
{
    public bool IsImage { get; set; }
    public uint Position { get; set; }
    public string FilePath { get; set; } = string.Empty;
    public TimeSpan Duration { get; set; } = new TimeSpan(0, 0, 15);
    public DateTime StartDate { get; set; } = DateTime.Now.Date;
    public DateTime EndDate { get; set; } = DateTime.Now.AddDays(10).Date;


    public string InternFileName => IsImage ? FilePath.Substring(FilePath.LastIndexOf('\\') + 1) : FilePath;
    public string InternFilePath => IsImage ? $"\\img\\{FilePath.Substring(FilePath.LastIndexOf('\\'))}" : FilePath;
    public string InternPosition => $"{Position};{InternFilePath}";
}
