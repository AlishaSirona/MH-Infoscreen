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

namespace Infoscreen.Pages;

public partial class Index
{
    TimeSpan cycleTimer = new TimeSpan(0, 0, 2);
    MudCarousel<object>? mudCarousel;

    void CarouselChanged(int index)
    {
        if (index < ImgData.FrontList.Count)
        {
            cycleTimer = new TimeSpan(0, 0, 3);
        }
        else if (index >= ImgData.FrontList.Count + 12)
        {
            cycleTimer = new TimeSpan(0, 0, 7);
        }
        else
        {
            cycleTimer = new TimeSpan(0, 0, 1);
        }

        Console.WriteLine($"Index: {index}");
        Console.WriteLine(mudCarousel!.Items.Count);
    }
}