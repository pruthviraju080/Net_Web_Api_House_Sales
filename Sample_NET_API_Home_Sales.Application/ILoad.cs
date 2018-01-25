using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sample_NET_API_Home_Sales.Application;

namespace Sample_NET_API_Home_Sales.Application
{
    interface ILoad
    {
        Task<List<HomeSale>> LoadFromCsv();
    }
}
