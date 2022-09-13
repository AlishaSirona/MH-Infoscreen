using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharePointConsole.Classes;
using Newtonsoft.Json;
using System.IO;

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
            AccidentStatistic accidentStatistic = new AccidentStatistic();

            accidentStatistic.LastAccidentDate = accidents.Last().AccidentDate;
            accidentStatistic.LastAccidentDepartment = accidents.Last().Department;
            accidentStatistic.AccidentFreeDays = (int)(DateTime.Now - accidents.Last().AccidentDate).TotalDays;
            accidentStatistic.AccidentsLastYear = accidents.Where(item => item.AccidentDate.Year == DateTime.Now.AddYears(-1).Year).ToList().Count;
            accidentStatistic.AccidentsLastYearTillNow = accidents.Where(item => item.AccidentDate.Year == DateTime.Now.AddYears(-1).Year && item.AccidentDate < DateTime.Now.AddYears(-1)).ToList().Count;
            accidentStatistic.AccidentsThisYear = accidents.Where(item => item.AccidentDate.Year == DateTime.Now.Year).ToList().Count;

            foreach (var dep in accidents.Select(item => item.Department).Distinct())
            {
                if (!accidentStatistic.AccidentsPerDepartment.ContainsKey(dep))
                {
                    accidentStatistic.AccidentsPerDepartment.Add(dep, new int[] { 0, 0 });
                }

                accidentStatistic.AccidentsPerDepartment[dep][0] = accidents
                    .Where(item => item.AccidentDate.Year == DateTime.Now.AddYears(-1).Year && item.Department == dep)
                    .ToList().Count;

                accidentStatistic.AccidentsPerDepartment[dep][1] = accidents
                    .Where(item => item.AccidentDate.Year == DateTime.Now.Year && item.Department == dep)
                    .ToList().Count;
            }

            for (int i = 1; i < 13; i++)
            {
                accidentStatistic.AccidentsPerMonth[i][0] = accidents
                    .Where(item => item.AccidentDate.Year == DateTime.Now.AddYears(-1).Year && item.AccidentDate.Month == i)
                    .ToList().Count;

                accidentStatistic.AccidentsPerMonth[i][0] = accidents
                    .Where(item => item.AccidentDate.Year == DateTime.Now.Year && item.AccidentDate.Month == i)
                    .ToList().Count;
            }

            SerializData(accidentStatistic);
        }

        static void SerializData(AccidentStatistic accidentStatistic)
        {
            string output = JsonConvert.SerializeObject(accidentStatistic, Formatting.Indented);

            string folderPath = "json";

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            System.IO.File.WriteAllText(Path.Combine(folderPath, "accident.json"), output);
        }
    }
}
