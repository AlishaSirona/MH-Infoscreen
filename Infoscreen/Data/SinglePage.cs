﻿namespace Infoscreen.Data;

public class SinglePage
{
    public bool IsImage { get; set; }
    public uint Position { get; set; }
    public string FilePath { get; set; } = string.Empty;
    public TimeSpan Duration { get; set; } = new TimeSpan(0, 0, 15);


    public string InternFilePath => IsImage ? $"\\img\\{FilePath.Substring(FilePath.LastIndexOf('\\'))}" : FilePath;
    public string InternPosition => $"{Position};{InternFilePath}";
}
