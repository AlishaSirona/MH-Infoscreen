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
using Microsoft.Extensions.Logging;
using DbInfoscreenLibrary;
using Microsoft.EntityFrameworkCore;
using Radzen;

namespace Infoscreen.Pages;

public partial class Overview
{
    IList<DbInfoscreenLibrary.Pages>? dataViewLive;
    IList<DbInfoscreenLibrary.Pages>? dataViewUpcoming;
    IList<DbInfoscreenLibrary.Pages>? dataViewOver;

    protected override async Task OnInitializedAsync()
    {
        await LoadDataViews();
    }

    async Task LoadDataViews()
    {
        using var context = ContextFactory.CreateDbContext();

        var data = await context.Pages.AsNoTracking().ToListAsync();

        dataViewLive = data
            .Where(item => item.StartDate <= DateTime.Now && item.EndDate >= DateTime.Now)
            .OrderBy(item => item.Order)
            .ToList();

        dataViewUpcoming = data
            .Where(item => item.StartDate > DateTime.Now && item.EndDate > DateTime.Now)
            .OrderBy(item => item.Order)
            .ToList();

        dataViewOver = data
            .Where(item => item.EndDate < DateTime.Now)
            .OrderBy(item => item.Order)
            .ToList();
    }


    async Task OpenUploadFile()
    {
        bool? result = await DialogService.OpenAsync<FileUpload>("File-Upload",
            options: new Radzen.DialogOptions() { Resizable = true, CloseDialogOnOverlayClick = true});

        if (result == true)
            await LoadDataViews();
    }

    async Task OpenConfirmDialog()
    {
        var result = await DialogService.Confirm(
            "Dadurch werden alle alten Bilder gelöscht und können nicht mehr reaktiviert werden!",
            "Achtung!",
            new Radzen.ConfirmOptions() { OkButtonText = "Weiter", CancelButtonText = "Abbrechen" });

        if (result == true)
            await CleanupFiles();

        DialogService.Dispose();
    }

    async Task CleanupFiles()
    {
        using var context = ContextFactory.CreateDbContext();

        int fileCounter = 0;

        var oldDbData = await context.Pages
            .Where(item => item.EndDate < DateTime.Now)
            .ToListAsync();

        if (oldDbData != null)
        {
            context.Pages.RemoveRange(oldDbData);
            await context.SaveChangesAsync();
        }

        List<SinglePage>? oldPages = ScreenData.Pages.Where(item => item.EndDate < DateTime.Now).ToList();


        foreach (var item in oldPages)
        {
            if (!item.IsImage)
                continue;

            if (File.Exists(item.FilePath))
            {
                File.Delete(item.FilePath);
                ScreenData.Pages.Remove(item);
                fileCounter++;
            }
        }

        await LoadDataViews();

        string message;

        if (fileCounter == 0)
            message = "Keine Bilder zum Löschen vorhanden";
        else if (fileCounter == 1)
            message = "Ein Bild wurde gelöscht";
        else
            message = $"{fileCounter} Bilder wurden gelöscht";
        ShowNotification(new NotificationMessage { Severity = NotificationSeverity.Success, Duration = 4000, Summary = message });
    }

    void ShowNotification(NotificationMessage message)
    {
        NotificationService.Notify(message);
    }
}