using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Sample_NET_API_Home_Sales.Application
{
    public class CsvProvide
    {
        public IEnumerable<string> OpenFile(string filePath)
        {
            return File.ReadAllLines(filePath);
        }
    }
}
