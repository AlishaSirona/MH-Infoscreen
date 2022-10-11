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

namespace Infoscreen.Pages.ProductionPages.VS;

public partial class VS1
{
    string lastShipment = string.Empty;
    IEnumerable<VersandDaten>? lastTenDays;

    protected override async Task OnInitializedAsync()
    {
        lastShipment = GetLastShipment();
        await GetLastTenDays();
    }

    async Task GetLastTenDays()
    {
        using var context = ContextFactory.CreateDbContext();

        lastTenDays = await context.VersandDaten
            .Where(item => item.Date <= DateTime.Now.AddDays(-1).Date && item.Verladen != 0)
            .OrderByDescending(item => item.Date)
            .AsNoTracking()
            .Take(10)
            .ToListAsync();
    }

    string SetLastTenDayFormatString() => "#,##0.0";

    string GetLastShipment()
    {
        using var context = ContextFactory.CreateDbContext();

        int _subDays;

        switch (DateTime.Now.DayOfWeek)
        {
            case DayOfWeek.Monday:
                _subDays = -3;
                break;
            case DayOfWeek.Sunday:
                _subDays = -2;
                break;
            default:
                _subDays = -1;
                break;
        }

        var yesterdayEntry = context.VersandDaten
            .Where(item => item.Date == DateTime.Now.AddDays(_subDays).Date)
            .FirstOrDefault();

        if (yesterdayEntry == null || yesterdayEntry.Date == null)
            return String.Empty;

        DateTime date = (DateTime)yesterdayEntry.Date;
        string amount = yesterdayEntry.Verladen == 0 ? "Keine Verladung" : $"{yesterdayEntry.Verladen.ToString("#,##0.0")}t";

        return $"{date.ToString("dddd, dd.MM.yyyy")}: {amount}";
    }
}