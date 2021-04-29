using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DignisiaTest.Controllers
{
    [ApiController]
    public class ChartDataController : Controller
    {
        private readonly ILogger<ChartData> _logger;

        public ChartDataController(ILogger<ChartData> logger)
        {
            _logger = logger;
        }
        [Route("chart-data")]
        public int Index(int? id)
        {
            return 1;// ControllerContext.MyDisplayRouteInfo(id);
        }
        [Route("chart-data/{widgetId}")]
        public ChartData GetDataForWidget(int widgetId, [FromQuery] int segmentId, [FromQuery] int caseAge)
        {
            if (!MockDatabase.GetAllSegmentsBelow(out List<int> segmentIds, segmentId))
                return null;
            switch (widgetId)
            {
                case 1:
                    return DebtSizeData(segmentIds, caseAge);
                case 2:
                    return OpenStatusData(segmentIds, caseAge);
            }
            return null;
        }

        private ChartData OpenStatusData(List<int> segmentIds, int caseAge)
        {
            Series closedCases = new Series
            {
                name = "Closed cases",
                unit = "",
                data = new List<decimal>()
            };
            Series openCases = new Series
            {
                name = "Open cases",
                unit = "",
                data = new List<decimal>()
            };
            ChartData result = new ChartData
            {
                categories = new List<string>(),
                series = new List<Series>
                {
                    closedCases,
                    openCases,
                }
            };
            DateTime baseDateValue = DateTime.Parse("2020-02-01");
            DateTime minMonth = baseDateValue;
            DateTime maxMonth = baseDateValue.AddMonths(1);
            for (int month = 0; month < caseAge; month++)
            {
                List<CreditDataPoint> closedPoints = MockDatabase.creditDataPoints.FindAll(dataPoint => (segmentIds.Contains(dataPoint.segmentId) && dataPoint.timeStamp > minMonth && dataPoint.timeStamp <= maxMonth && dataPoint.closed));
                List<CreditDataPoint> openPoints = MockDatabase.creditDataPoints.FindAll(dataPoint => (segmentIds.Contains(dataPoint.segmentId) && dataPoint.timeStamp > minMonth && dataPoint.timeStamp <= maxMonth && !dataPoint.closed));
                closedCases.data.Add(closedPoints.Count());
                openCases.data.Add(openPoints.Count());
                result.categories.Add(minMonth.ToString("yyyy-MM-dd"));
                minMonth = minMonth.AddMonths(1);
                maxMonth = maxMonth.AddMonths(1);
            }
            return result;
        }

        private ChartData DebtSizeData(List<int> segmentIds, int caseAge)
        {
            Series debtSize = new Series
            {
                name = "Debt size",
                unit = "EUR",
                data = new List<Decimal>()
            };
            ChartData result = new ChartData {
                categories = new List<string>(),
                series = new List<Series>
                {
                    debtSize
                }
            };
            DateTime baseDateValue = DateTime.Parse("2020-02-01");
            DateTime minMonth = baseDateValue;
            DateTime maxMonth = baseDateValue.AddMonths(1);
            for (int month = 0; month < caseAge; month++)
            {
                IEnumerable<Decimal> totalValue = MockDatabase.creditDataPoints.FindAll(dataPoint => (segmentIds.Contains(dataPoint.segmentId) && dataPoint.timeStamp > minMonth && dataPoint.timeStamp <= maxMonth)).Select(data => data.value);
                debtSize.data.Add(totalValue.Sum());
                result.categories.Add(minMonth.ToString("yyyy-MM-dd"));
                minMonth = minMonth.AddMonths(1);
                maxMonth = maxMonth.AddMonths(1);
            }
            return result;
        }
    }
}
/*
 {
"categories":["2020-03-01","2020-04-01","2020-05-01","2020-06-01","2020-07-01","2020-08-01","2020-09-01","2020-10-01","2020-11-01","2020-12-01","2021-01-01"],
"series":[{"name":"Debt size","unit":"EUR","data":[962121.0,987424.0,976782.0,999987.0,1000000.0,1070801.0,1220020.0,1340210.0,1412030.0,1460021.0,1377625.0]}]}
 */

/*
 Raw response for one of the widgets above looks like this (caseAge=5, meaning we need to add 5 months to the base value which is 2020-02-01 and take timestamps >= than this): {"categories":["2020-07-01","2020-08-01","2020-09-01","2020-10- 01","2020-11-01","2020-12-01","2021-01-01"],"series":[{"name":"Debt size","unit":"EUR","data":[1000000.0,1070801.0,1220020.0,1340210.0,1412030.0,1460021.0,1377625.0]}]} 
 */