namespace Infoscreen.Data;

public class Images
{
    public uint Index { get; set; }
    public uint Position { get; set; }
    public string FilePath { get; set; } = string.Empty;
    public TimeSpan Duration { get; set; } = new TimeSpan(0, 0, 15);

    public string InternFilePath => FilePath.Substring(FilePath.IndexOf('\\'));
}
