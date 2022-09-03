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

namespace Infoscreen.Pages;

public partial class Index
{
    string[]? images;
    TimeSpan cycleTimer = new TimeSpan(0, 0, 2);

    protected override void OnInitialized()
    {
        images = Directory.GetFiles(@"wwwroot\img");

    }

}