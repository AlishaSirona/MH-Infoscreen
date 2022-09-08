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

}