using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;

namespace Sample_NET_API_Home_Sales.Application
{
    public class HomeSalesLoadingCSV : ILoad
    {
        private readonly string _homeSalesFilePath;
        private readonly CsvProvide _csvProvide;
        public HomeSalesLoadingCSV()
        {
            _csvProvide = new CsvProvide();
            _homeSalesFilePath = ConfigurationSettings.AppSettings["HomeSalesfilePath"];
        }

        public async Task<List<HomeSale>> LoadFromCsv()
        {
            var HomeSales = _csvProvide.OpenFile(_homeSalesFilePath).Skip(1);

            var queryResult =
               (from eLine in HomeSales

               let split = eLine.Split(',')
               select new HomeSale
               {
                    PropertyZip = split[0].Trim(),
                    SchoolCode = split[1].Trim(),
                    SchoolDesc = split[2].Trim(),
                    RecordDate = Convert.ToDateTime(split[3].Trim()),
                    SaleDate = Convert.ToDateTime(split[4].Trim()),
                    Price = Convert.ToDouble(split[5].Trim())
               }).ToList();

            return queryResult;
        }
    }
}
