using System.ComponentModel;

namespace Infoscreen.Data;

public class ImgData
{
    public List<Images> FrontList { get; set; }
    public List<Images> BackList { get; set; }

    private uint _index { get; set; } = 0;

    public ImgData()
    {
        FrontList = new List<Images>();
        BackList = new List<Images>();

        var files = Directory.GetFiles(@"wwwroot\img");

        foreach (var item in files)
        {
            var data = new Images()
            {
                FilePath = item,
                Index = _index,
            };
            FrontList.Add(data);
            BackList.Add(data);
            _index++;
        }
    }

    public void Refresh()
    {
        var files = Directory.GetFiles(@"wwwroot\img");

        foreach (var item in files)
        {
            if (!FrontList.Select(item => item.FilePath).Contains(item))
            {
                var data = new Images()
                {
                    FilePath = item,
                    Index = _index,
                };
                FrontList.Add(data);
                BackList.Add(data);
                _index++;
            }
        }
    }
}
