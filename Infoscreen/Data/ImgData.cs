namespace Infoscreen.Data;

public class ImgData
{
    public List<Images> FrontList { get; set; }
    public List<Images> BackList { get; set; }

    public ImgData()
    {
        FrontList = new List<Images>();
        BackList = new List<Images>();

        int pos = 0;

        var files = Directory.GetFiles(@"wwwroot\img");

        foreach (var item in files)
        {
            var data = new Images()
            {
                FilePath = item,
                Position = pos,
            };
            FrontList.Add(data);
            BackList.Add(data);
            pos++;
        }
    }
}
