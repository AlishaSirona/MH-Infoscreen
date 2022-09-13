using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharePointConsole.Unfall
{
    internal class AccidentController
    {
        public static void GenAccidentJson()
        {
            List<AccidentData> accidentData = new List<AccidentData>();

            using (ClientContext context = new ClientContext("http://intranet/Abteilungen/Arbeitssicherheit"))
            {
                List list = context.Web.Lists.GetByTitle("Unfallfreie Tage");

                CamlQuery camlQuery = CamlQuery.CreateAllItemsQuery();

                ListItemCollection listItems = list.GetItems(camlQuery);

                context.Load(listItems);
                context.ExecuteQuery();

                foreach (var item in listItems)
                {
                    if (item["Abteilung"] != null && item["LU"] != null)
                    {
                        var _date = (DateTime)item["LU"];
                        var _department = item["Abteilung"].ToString();

                        accidentData.Add(new AccidentData()
                        {
                            AccidentDate = _date.AddHours(2),
                            Department = _department
                        });
                    }
                }
            }

            CalcData(accidentData.OrderBy(item => item.AccidentDate).Where(item => item.AccidentDate.Year >= DateTime.Now.AddYears(-1).Year).ToList());
        }

        static void CalcData(List<AccidentData> accidents)
        {
            foreach (var item in accidents)
            {
                Console.WriteLine($"{item.AccidentDate} {item.Department}");
            }
        }
    }
}
