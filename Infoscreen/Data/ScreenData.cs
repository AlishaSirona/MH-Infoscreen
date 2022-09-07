using Microsoft.EntityFrameworkCore;
using static MudBlazor.Colors;

namespace Infoscreen.Data;

public class ScreenData
{
    public List<SinglePage> Pages { get; set; }

    public ScreenData()
    {
        Pages = new List<SinglePage>()
        {
            new SinglePage() { FilePath = "Accidents",  Order = 500},
            new SinglePage() { FilePath = "STW1",       Order = 501},
            new SinglePage() { FilePath = "STW2",       Order = 502},
            new SinglePage() { FilePath = "STW3",       Order = 503},
            new SinglePage() { FilePath = "WW1",        Order = 504},
            new SinglePage() { FilePath = "WW2",        Order = 505},
            new SinglePage() { FilePath = "UA1",        Order = 506},
            new SinglePage() { FilePath = "UA2",        Order = 507},
            new SinglePage() { FilePath = "VS1",        Order = 508},
            new SinglePage() { FilePath = "Companies",  Order = 509},
            new SinglePage() { FilePath = "Visitors",   Order = 510},
            new SinglePage() { FilePath = "Weather",    Order = 511},
        };

        var files = Directory.GetFiles(@"wwwroot\img");

        Random rnd = new();

        using var context = new DbInfoscreenLibrary.DbInfoscreenContext();

        var dbData = context.Pages
            .Where(item => item.StartDate <= DateTime.Now && item.EndDate >= DateTime.Now)
            .AsNoTracking();

        foreach (var item in dbData)
        {
            if (files.Contains($"wwwroot\\img\\{item.FileName}"))
            {
                var data = new SinglePage()
                {
                    FilePath = $"wwwroot\\img\\{item.FileName}",
                    Duration = new TimeSpan(0, 0, (int)item.Duration),
                    Order = item.Position,
                    StartDate = item.StartDate,
                    EndDate = item.EndDate,
                    IsImage = true
                };

                Pages.Add(data);
            }
        }
    }
}
