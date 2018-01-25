using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample_NET_API_Home_Sales.Application
{
    public class HomeSale
    {
        public string PropertyZip { get; set; }

        public string SchoolCode { get; set; }

        public string SchoolDesc { get; set; }

        public DateTime RecordDate { get; set; }

        public DateTime SaleDate { get; set; }

        public double Price { get; set; }
    }
}
