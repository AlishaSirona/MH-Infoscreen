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

namespace Infoscreen.Pages;

public partial class Overview
{
    private List<IBrowserFile> loadedFiles = new();
    private bool isLoading;

    uint position;
    uint duration;
    DateTime startDate;
    DateTime endDate;


    private async Task LoadFiles(InputFileChangeEventArgs e)
    {
        isLoading = true;
        loadedFiles.Clear();

        foreach (var file in e.GetMultipleFiles(1))
        {
            try
            {
                if (file.ContentType.Remove(file.ContentType.LastIndexOf('/')).ToLower() == "image")
                {
                    loadedFiles.Add(file);

                    Random rnd = new Random();

                    string fileName = file.Name
                        .Remove(file.Name.LastIndexOf('.'))
                        .Replace('_', '-')
                        .Replace('.', '-')
                        .ToLower();

                    string fileType = file.ContentType
                        .Substring(file.ContentType.LastIndexOf('/') + 1)
                        .ToLower();

                    string secureFileName = $"{rnd.Next(10000000, 99999999)}_{fileName}.{fileType}";


                    var path = Path.Combine(Environment.WebRootPath, "img", secureFileName);

                    await using FileStream fs = new(path, FileMode.Create);
                    await file.OpenReadStream().CopyToAsync(fs);
                    await PostToDatabase(secureFileName);

                    ScreenData.Pages.Add(new SinglePage() { FilePath = path, IsImage = true, Position = 0 });
                }
                else
                {
                    Console.WriteLine("Kein Bild!!!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error\nMessage: {ex.Message}");
            }
        }

        isLoading = false;
    }

    async Task PostToDatabase(string fileName)
    {
        try
        {
            using var context = ContextFactory.CreateDbContext();

            var data = new DbInfoscreenLibrary.Pages()
            {
                FileName = fileName,
                Duration = duration,
                Position = position,
                StartDate = startDate,
                EndDate = endDate
            };

            context.Pages.Add(data);

            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {

        }
    }

    async Task OpenUploadFile()
    {
        await DialogService.OpenAsync<FileUpload>("File-Upload",
            options: new Radzen.DialogOptions() { Resizable = true, CloseDialogOnOverlayClick = true});
    }



}