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
using SharePointLibrary;

namespace Infoscreen.Pages;

public partial class Index
{
    TimeSpan cycleTimer = new TimeSpan(0, 0, 15);
    MudCarousel<SinglePage>? mudCarousel;

    List<SinglePage>? mudItems;


    protected override void OnInitialized()
    {
        mudItems = ScreenData.Pages;

        //SharePointSTW.GetSPLists();
    }

    void CarouselChanged(int index)
    {
        //if (index < ImgData.FrontList.Count)
        //{
        //    cycleTimer = ImgData.FrontList.Where(item => item.Index == index).First().Duration;
        //    Console.WriteLine($"Item Front: {ImgData.FrontList.Where(item => item.Index == index).First().InternFilePath}");
        //}
        //else if (index >= ImgData.FrontList.Count + cntFixedPages)
        //{
        //    cycleTimer = ImgData.BackList.Where(item => item.Index == index - ImgData.FrontList.Count - cntFixedPages).First().Duration;
        //    Console.WriteLine($"Item Back: {ImgData.BackList.Where(item => item.Index == index - ImgData.FrontList.Count - cntFixedPages).First().InternFilePath}");
        //}
        //else
        //{
        //    cycleTimer = new TimeSpan(0, 0, 1);
        //    Console.WriteLine($"Item Site");
        //}
    }
}