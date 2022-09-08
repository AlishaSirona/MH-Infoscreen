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
using Microsoft.EntityFrameworkCore.Internal;
using Infoscreen.Pages;

namespace Infoscreen.Shared;

public partial class DataGridView
{
    [Parameter]
    public IList<DbInfoscreenLibrary.Pages>? ViewData { get; set; }

    [Parameter]
    public bool ShowDelete { get; set; } = false;

    [Parameter]
    public EventCallback OnChange { get; set; }


    RadzenDataGrid<DbInfoscreenLibrary.Pages>? radzenDataGrid;
    DbInfoscreenLibrary.Pages? dataToInsert;


    async Task OnUpdateRow(DbInfoscreenLibrary.Pages dataLine)
    {
        if (dataLine == dataToInsert)
            dataToInsert = null;

        using var context = ContextFactory.CreateDbContext();

        context.Pages!.Update(dataLine);

        await context.SaveChangesAsync();

        Console.WriteLine(dataLine.FileName);

        var screenData = ScreenData.Pages.Where(item => item.InternFileName == dataLine.FileName).First();
        screenData.Duration = new TimeSpan(0, 0, (int)dataLine.Duration);
        screenData.Order = dataLine.Order;
        screenData.StartDate = dataLine.StartDate;
        screenData.EndDate = dataLine.EndDate;

        await OnChange.InvokeAsync();
    }

    async Task DeleteRow(DbInfoscreenLibrary.Pages dataLine)
    {
        if (dataLine == dataToInsert)
            dataToInsert = null;

        using var context = ContextFactory.CreateDbContext();

        if (ViewData!.Contains(dataLine))
        {
            dataLine.EndDate = DateTime.Now.AddDays(-1).Date;
            context.Pages!.Update(dataLine);
            await context.SaveChangesAsync();

            var screenData = ScreenData.Pages.Where(item => item.InternFileName == dataLine.FileName).First();
            screenData.EndDate = DateTime.Now.AddDays(-1).Date;

            await OnChange.InvokeAsync();
        }
        else
        {
            radzenDataGrid!.CancelEditRow(dataLine);
        }
    }

    async Task SaveRow(DbInfoscreenLibrary.Pages dataLine)
    {
        if (dataLine == dataToInsert)
            dataToInsert = null;

        await radzenDataGrid!.UpdateRow(dataLine);
    }

    async Task EditRow(DbInfoscreenLibrary.Pages dataLine)
    {
        await radzenDataGrid!.EditRow(dataLine);
    }

    void CancelEdit(DbInfoscreenLibrary.Pages dataLine)
    {
        if (dataLine == dataToInsert)
            dataToInsert = null;
        radzenDataGrid!.CancelEditRow(dataLine);

        using var context = ContextFactory.CreateDbContext();

        var userEntry = context.Entry(dataLine);
        if (userEntry.State == EntityState.Modified)
        {
            userEntry.CurrentValues.SetValues(userEntry.OriginalValues);
            userEntry.State = EntityState.Unchanged;
        }
    }
}