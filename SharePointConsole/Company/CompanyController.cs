using Microsoft.SharePoint.Client;
using Newtonsoft.Json;
using SharePointConsole.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharePointConsole.Company
{
    internal class CompanyController
    {
        public static void GenCompanyJson()
        {
            List<CompaniesStatistic> companyData = new List<CompaniesStatistic>();

            using (ClientContext context = new ClientContext("http://intranet/Abteilungen/Arbeitssicherheit/"))
            {
                List list = context.Web.Lists.GetByTitle("Fremdfirmen");

                CamlQuery camlQuery = CamlQuery.CreateAllItemsQuery();

                ListItemCollection listItems = list.GetItems(camlQuery);

                context.Load(listItems);
                context.ExecuteQuery();

                foreach (var item in listItems)
                {
                    var _beginDate = (DateTime)item["EventDate"];
                    var _endDate = (DateTime)item["EndDate"];

                    if (_beginDate >= DateTime.Now && _endDate >= DateTime.Now)
                    {
                        var _companyName = item["Title"] == null ? string.Empty : item["Title"].ToString();
                        var _employeeCount = item["Anzahl_x0020_der_x0020_Mitarbeit"] == null ? 0 : (int)(double)item["Anzahl_x0020_der_x0020_Mitarbeit"];
                        var _contact = item["Ansprechperson"] == null ? string.Empty : item["Ansprechperson"].ToString();
                        var _project = item["Projekt"] == null ? string.Empty : item["Projekt"].ToString();

                        companyData.Add(new CompaniesStatistic()
                        {
                            StartTime = _beginDate,
                            EndTime = _endDate,
                            Company = _companyName,
                            EmployeeCount = _employeeCount,
                            Contact = _contact,
                            Project = _project,
                        });
                    }
                }
            }

            SerializData(companyData);
        }

        static void SerializData(List<CompaniesStatistic> companyStatistic)
        {
            string output = JsonConvert.SerializeObject(companyStatistic, Formatting.Indented);

            string folderPath = "json";

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            System.IO.File.WriteAllText(Path.Combine(folderPath, "companies.json"), output);
        }

    }
}
