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
using Microsoft.AspNetCore.Mvc.Razor;
using Infoscreen.Data;

namespace Infoscreen.Pages;

public partial class Index
{
    TimeSpan cycleTimer = new TimeSpan(0, 0, 15);
    MudCarousel<SinglePage>? mudCarousel;

    List<SinglePage>? mudItems;


    protected override void OnInitialized()
    {
        mudItems = ScreenData.Pages;

    }

    void CarouselChanged(int index)
    {
        int counter = 0;

        foreach (var item in ScreenData.Pages)
        {
            if (counter == index)
            {
                cycleTimer = item.Duration;
                break;
            }
            else
            {
                counter++;
            }
        }
    }
}