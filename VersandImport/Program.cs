using DbInfoscreenLibrary;
using Microsoft.VisualBasic.FileIO;
using Serilog;

namespace VersandImport;

internal class Program
{
    static async Task Main()
    {
        Log.Logger = new LoggerConfiguration()
                .Enrich.WithProperty("Program", "VersandImport")
#if DEBUG
                .WriteTo.Seq("http://localhost:5341", bufferBaseFilename: Path.Join("Logs", "log"))
#else
                .WriteTo.Seq("http://192.168.0.42:5341", bufferBaseFilename: Path.Join("Logs", "log"))
#endif
                .CreateLogger();

        Log.Information("Program start");

        try
        {
            var csvPath = @"\\esoftvk\Export\versand.csv";

            using (TextFieldParser csvParser = new TextFieldParser(csvPath))
            {
                csvParser.CommentTokens = new string[] { "#" };
                csvParser.SetDelimiters(new string[] { ";" });
                csvParser.HasFieldsEnclosedInQuotes = true;

                csvParser.ReadLine();

                while (!csvParser.EndOfData)
                {
                    var fields = csvParser.ReadFields();

                    if (fields != null)
                    {
                        var date = Convert.ToDateTime(fields[0]);
                        var menge = double.Parse(fields[1]);

                        await WriteData(date, menge);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Log.Error("Fehler beim Versand-Import {Message} {StackTrace} {InnerException}", ex.Message, ex.StackTrace, ex.InnerException);
        }
        finally
        {
            Log.Information("Program end");
            Log.CloseAndFlush();
        }
    }

    static async Task WriteData(DateTime date, double menge)
    {
        using var context = new DbInfoscreenContext();

        var data = context.VersandDaten
            .Where(item => item.Date == date)
            .FirstOrDefault();

        if (data != null)
        {
            data.Verladen = menge;
        }
        else
        {
            var input = new VersandDaten() { Date = date, Verladen = menge };
            context.Add(input);
        }

        await context.SaveChangesAsync();
    }
}