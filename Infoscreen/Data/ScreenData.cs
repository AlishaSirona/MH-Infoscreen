using Microsoft.EntityFrameworkCore;
using Serilog;
using static MudBlazor.Colors;

namespace Infoscreen.Data;

public class ScreenData
{
    public List<SinglePage> Pages { get; set; }

    public ScreenData()
    {
        Pages = new List<SinglePage>()
        {
            new SinglePage() { FilePath = "Accidents",  Order = 100},
            new SinglePage() { FilePath = "STW1",       Order = 101},
            new SinglePage() { FilePath = "STW2",       Order = 102},
            new SinglePage() { FilePath = "STW3",       Order = 103},
            new SinglePage() { FilePath = "WW1",        Order = 104},
            new SinglePage() { FilePath = "WW2",        Order = 105},
            new SinglePage() { FilePath = "UA1",        Order = 106},
            new SinglePage() { FilePath = "UA2",        Order = 107},
            new SinglePage() { FilePath = "VS1",        Order = 108},
            new SinglePage() { FilePath = "Companies",  Order = 109},
            new SinglePage() { FilePath = "Visitors",   Order = 110},
            new SinglePage() { FilePath = "Weather",    Order = 111},
        };

        try
        {
            var files = Directory.GetFiles(Path.Join("wwwroot", "img"));

            Random rnd = new();

            using var context = new DbInfoscreenLibrary.DbInfoscreenContext();

            var dbData = context.Pages
                .AsNoTracking();

            foreach (var item in dbData)
            {
                if (files.Contains(Path.Join("wwwroot", "img", item.FileName)))
                {
                    var data = new SinglePage()
                    {
                        FilePath = Path.Join("wwwroot", "img", item.FileName),
                        Duration = new TimeSpan(0, 0, (int)item.Duration),
                        Order = item.Order,
                        StartDate = item.StartDate,
                        EndDate = item.EndDate,
                        IsImage = true,
                    };

                    Pages.Add(data);
                }
            }
        }
        catch (Exception ex)
        {
            Log.Error("Fehler beim generieren der ScreenData {Message} {StackTrace} {InnerException}", ex.Message, ex.StackTrace, ex.InnerException);
        }
    }
}
