using static MudBlazor.Colors;

namespace Infoscreen.Data;

public class ScreenData
{
    public List<SinglePage> Pages { get; set; }

    public ScreenData()
    {
        Pages = new List<SinglePage>()
        {
            new SinglePage() { FilePath = "Accidents",  Position = 500},
            new SinglePage() { FilePath = "STW1",       Position = 501},
            new SinglePage() { FilePath = "STW2",       Position = 502},
            new SinglePage() { FilePath = "STW3",       Position = 503},
            new SinglePage() { FilePath = "WW1",        Position = 504},
            new SinglePage() { FilePath = "WW2",        Position = 505},
            new SinglePage() { FilePath = "UA1",        Position = 506},
            new SinglePage() { FilePath = "UA2",        Position = 507},
            new SinglePage() { FilePath = "VS1",        Position = 508},
            new SinglePage() { FilePath = "Companies",  Position = 509},
            new SinglePage() { FilePath = "Visitors",   Position = 510},
            new SinglePage() { FilePath = "Weather",    Position = 511},
        };

        var files = Directory.GetFiles(@"wwwroot\img");

        Random rnd = new();
        foreach (var item in files)
        {
            var data = new SinglePage()
            {
                FilePath = item,
                Position = (uint)rnd.Next(10, 1000),
                IsImage = true,
            };

            Pages.Add(data);
        }
    }
}
