using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Sample_NET_API_Home_Sales.Application;

namespace Sample_NET_API_Home_Sales.Controllers
{
    public class HomeSalesController : ApiController
    {
        // GET: api/HomeSales

        private readonly HomeSalesLoadingCSV _homeSalesLodindFromCsv;

        public HomeSalesController()
        {
            _homeSalesLodindFromCsv = new HomeSalesLoadingCSV();
        }

        // GET: api/HomeSales/5
        [HttpGet]
        [Route("api/GetSchools/{year}/{month}")]
        public async Task<IHttpActionResult> GetSchools(int year, int month)
        {
            
            var listHomeSales = await _homeSalesLodindFromCsv.LoadFromCsv();


            var startdate = new DateTime(year, month, 01);
            var lastDayOfMonth = DateTime.DaysInMonth(year, month);
            var endDate = new DateTime(year, month, lastDayOfMonth);

            var distinctSchools = listHomeSales.Where(x => (x.SaleDate >= startdate) && (x.SaleDate < endDate)).
                                                GroupBy(x => x.SchoolCode).
                                                Select(x => x.FirstOrDefault());
            var topHundredschools = distinctSchools.
                OrderByDescending(x => x.Price).Take(100).Select(x => new { x.SchoolDesc, x.SchoolCode });

            if (topHundredschools.Any())
            {
                return Ok(topHundredschools);
            }

            return NotFound();
        }

        [HttpGet]
        [Route("api/GetAverageNoOfDaysToRecordTheProperty/{schoolName}/{year}/{month}")]
        public async Task<IHttpActionResult> GetAverageNoOfDaysToRecordTheProperty(string schoolName, int year, int month)
        {
            var listHomeSales = await _homeSalesLodindFromCsv.LoadFromCsv();

            var startdate = new DateTime(year, month, 01);
            var lastDayOfMonth = DateTime.DaysInMonth(year, month);
            var endDate = new DateTime(year, month, lastDayOfMonth);

            var distinctSchools = listHomeSales.Where(x => (x.SaleDate >= startdate) && (x.SaleDate < endDate) && ( x.SchoolDesc == schoolName));

            var listOfNoDays = new List<double>();

            var total = 0.0;

            foreach (var item in distinctSchools)
            {
                var daysToRegister = (item.RecordDate - item.SaleDate).TotalDays;
                total = total + daysToRegister;
            }

            var averageNoDaysToRecord = total / distinctSchools.Count();

            return Ok(averageNoDaysToRecord);
        }
    }
}
