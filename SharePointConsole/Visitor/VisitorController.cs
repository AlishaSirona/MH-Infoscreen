using Microsoft.SharePoint.Client;
using Newtonsoft.Json;
using SharePointConsole.Classes;
using SharePointConsole.Unfall;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharePointConsole.Visitor
{
    internal class VisitorController
    {
        public static void GenVisitorJson()
        {
            List<VisitorStatistic> visitorData = new List<VisitorStatistic>();

            using (ClientContext context = new ClientContext("http://intranet/"))
            {
                List list = context.Web.Lists.GetByTitle("Info-Besucher");

                CamlQuery camlQuery = CamlQuery.CreateAllItemsQuery();

                ListItemCollection listItems = list.GetItems(camlQuery);

                context.Load(listItems);
                context.ExecuteQuery();

                foreach (var item in listItems)
                {
                    var _date = (DateTime)item["Datum"];

                    if (_date >= DateTime.Now)
                    {
                        var _title = item["Anrede"] == null ? string.Empty : item["Anrede"].ToString();
                        var _lastname = item["Nachname"] == null ? string.Empty : item["Nachname"].ToString();
                        var _firstname = item["Name"] == null ? string.Empty : item["Name"].ToString();
                        var _contact = item["Ansprechperson"] == null ? string.Empty : item["Ansprechperson"].ToString();
                        var _company = item["Firmenname"] == null ? string.Empty : item["Firmenname"].ToString();

                        visitorData.Add(new VisitorStatistic()
                        {
                            Title = _title,
                            Lastname = _lastname,
                            Firstname = _firstname,
                            Contact = _contact,
                            Company = _company,
                            Date = _date,
                        });
                    }
                }
            }

            SerializData(visitorData);
        }

        static void SerializData(List<VisitorStatistic> visitorStatistic)
        {
            string output = JsonConvert.SerializeObject(visitorStatistic, Formatting.Indented);

            string folderPath = "json";

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            System.IO.File.WriteAllText(Path.Combine(folderPath, "visitor.json"), output);
        }
    }
}
