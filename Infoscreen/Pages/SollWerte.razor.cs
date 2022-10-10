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
using Serilog;
using static MudBlazor.Defaults;

namespace Infoscreen.Pages;

public partial class SollWerte
{
    int filterYear = DateTime.Now.Year;
    IEnumerable<string> filterAbt = new List<string>()
    {
        "WW",
        "UA",
        "STW",
        "VS"
    };
    string? selectedAbt;

    IList<SollDaten>? solls;
    RadzenDataGrid<SollDaten>? dataGrid;
    SollDaten? dataToInsert;

    protected override async Task OnInitializedAsync()
    {
        using var context = ContextFactory.CreateDbContext();

        solls = await context.SollDaten
            .Where(item => item.TimeStamp.Year == filterYear)
            .ToListAsync();
    }

    async Task ApplyFilter()
    {
        if (selectedAbt == null)
            return;

        using var context = ContextFactory.CreateDbContext();

        solls = await context.SollDaten
            .Where(item => item.TimeStamp.Year == filterYear && item.Abteilung == selectedAbt)
            .ToListAsync();
    }

    async Task OnUpdateRow(SollDaten input)
    {
        try
        {
            if (input == dataToInsert)
                dataToInsert = null;

            using var context = ContextFactory.CreateDbContext();

            context.SollDaten!.Update(input);

            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Log.Error("Update Error {Table} {Message} {StackTrace} {InnerException}", "Infoscreen-Sollwerte", ex.Message, ex.StackTrace, ex.InnerException);
        }
    }

    async Task OnCreateRow(SollDaten input)
    {
        try
        {
            using var context = ContextFactory.CreateDbContext();

            context.SollDaten!.Add(input);

            await context.SaveChangesAsync();

            solls!.Add(input);
            await dataGrid!.Reload();
        }
        catch (Exception ex)
        {
            Log.Error("Create Error {Table} {Message} {StackTrace} {InnerException}", "Infoscreen-Sollwerte", ex.Message, ex.StackTrace, ex.InnerException);
        }
    }

    async Task InsertRow()
    {
        dataToInsert = new();
        await dataGrid!.InsertRow(dataToInsert);
    }

    async Task SaveRow(SollDaten input)
    {
        if (input == dataToInsert)
            dataToInsert = null;

        await dataGrid!.UpdateRow(input);
    }

    async Task EditRow(SollDaten input)
    {
        await dataGrid!.EditRow(input);
    }

    void CancelEdit(SollDaten input)
    {
        if (input == dataToInsert)
            dataToInsert = null;

        dataGrid!.CancelEditRow(input);

        using var context = ContextFactory.CreateDbContext();

        var userEntry = context.Entry(input);
        if (userEntry.State == EntityState.Modified)
        {
            userEntry.CurrentValues.SetValues(userEntry.OriginalValues);
            userEntry.State = EntityState.Unchanged;
        }
    }

    async Task DeleteRow(SollDaten input)
    {
        try
        {
            if (input == dataToInsert)
                dataToInsert = null;

            using var context = ContextFactory.CreateDbContext();

            if (solls!.Contains(input))
            {
                context.SollDaten!.Remove(input);
                await context.SaveChangesAsync();

                solls!.Remove(input);

                await dataGrid!.Reload();
            }
            else
            {
                dataGrid!.CancelEditRow(input);
            }
        }
        catch (Exception ex)
        {
            Log.Error("Delete Error {Table} {Message} {StackTrace} {InnerException}", "Infoscreen-Sollwerte", ex.Message, ex.StackTrace, ex.InnerException);
        }
    }

}