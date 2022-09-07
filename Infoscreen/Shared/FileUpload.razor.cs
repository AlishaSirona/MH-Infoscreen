using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.JSInterop;
using Infoscreen;
using Infoscreen.Shared;
using MudBlazor;
using Infoscreen.Pages.ProductionPages;
using Infoscreen.Pages.ProductionPages.ETC;
using Infoscreen.Pages.ProductionPages.STW;
using Infoscreen.Pages.ProductionPages.UA;
using Infoscreen.Pages.ProductionPages.VS;
using Infoscreen.Pages.ProductionPages.WW;
using Infoscreen.Data;
using Radzen;
using Radzen.Blazor;
using DbInfoscreenLibrary;
using Microsoft.EntityFrameworkCore;

namespace Infoscreen.Shared;

public partial class FileUpload
{
    bool isSuccess = true;

    FileData fileData = new();


    void OnSubmit()
    {
        if (fileData.BrowserFile == null)
        {
            ShowNotification(new NotificationMessage() { Duration = 4000, Severity = NotificationSeverity.Error, Summary = "Keine Datei zum Hochladen!" });
            return;
        }

        if (!fileData.BrowserFile.ContentType.ToLower().Contains("image/"))
        {
            ShowNotification(new NotificationMessage() { Duration = 4000, Severity = NotificationSeverity.Error, Summary = "Datei muss ein Bild sein!" });
            return;
        }

        Random rnd = new();

        string fileName = fileData.BrowserFile.Name
            .Remove(fileData.BrowserFile.Name.LastIndexOf('.'))
            .Replace(" ", "")
            .Replace('_', '-')
            .Replace(".", "")
            .ToLower();

        string fileType = fileData.BrowserFile.ContentType
            .Substring(fileData.BrowserFile.ContentType.LastIndexOf('/') + 1)
            .ToLower();

        string secureFileName = $"{fileName}_{rnd.Next(10000000, 99999999)}.{fileType}";

        Console.WriteLine(secureFileName);

        //Dispose();
    }



    void ShowNotification(NotificationMessage message)
    {
        NotificationService.Notify(message);
    }

    public void Dispose()
    {
        DialogService.Close(isSuccess);
    }
}

internal class FileData
{
    public IBrowserFile? BrowserFile { get; set; }
    public uint Order { get; set; } = 200;
    public uint Duration { get; set; } = 20;
    public DateTime StartDate { get; set; } = DateTime.Now.Date;
    public DateTime? EndDate { get; set; }
}