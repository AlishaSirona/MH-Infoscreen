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

namespace Infoscreen.Pages;

public partial class Overview
{
    private List<IBrowserFile> loadedFiles = new();
    private bool isLoading;

    private async Task LoadFiles(InputFileChangeEventArgs e)
    {
        isLoading = true;
        loadedFiles.Clear();

        foreach (var file in e.GetMultipleFiles(1))
        {
            try
            {
                loadedFiles.Add(file);

                var path = Path.Combine(Environment.WebRootPath, "img", file.Name);
                Console.WriteLine($"Path: {path}");

                await using FileStream fs = new(path, FileMode.Create);
                await file.OpenReadStream().CopyToAsync(fs);

                ScreenData.Pages.Add(new SinglePage() { FilePath = path, IsImage = true, Position = 0 });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error\nMessage: {ex.Message}");
            }
        }

        isLoading = false;


    }
}