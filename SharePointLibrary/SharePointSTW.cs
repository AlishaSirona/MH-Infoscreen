using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;

namespace SharePointLibrary;

public class SharePointSTW
{
    public static void GetSPLists()
    {
        ClientContext context = new ClientContext("http://intranet");

        Web web = context.Web;

        context.Load(web.Lists, lists => lists.Include(list => list.Title, list => list.Id));

        context.ExecuteQuery();

        foreach (var item in web.Lists)
        {
            Console.WriteLine(item.Title);
        }
        context.Dispose();
    }
}
