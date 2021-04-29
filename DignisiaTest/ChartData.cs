using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DignisiaTest
{
    public class ChartData
    {
        public List<string> categories { get; set; }
        public List<Series> series { get; set; }
    }
    public class Series
    {
        public string name { get; set; }
        public string unit { get; set; }
        public List<Decimal> data { get; set; }
    }
    public class CreditDataPoint {
        public bool closed { get; set; }
        public Decimal value { get; set; }
        public int segmentId { get; set; }
        public DateTime timeStamp { get; set; }
    }
}
