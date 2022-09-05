namespace Infoscreen.Data;

public class Images
{
    public int Position { get; set; }
    public string FilePath { get; set; } = string.Empty;
    public TimeSpan Duration { get; set; } = new TimeSpan(0, 0, 15);

    public string InternFilePath => FilePath.Substring(FilePath.IndexOf('\\'));
}
