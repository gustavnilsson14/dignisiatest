using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DignisiaTest.Controllers
{
    [ApiController]
    public class SegmentsController : ControllerBase
    {
        private readonly ILogger<SegmentsController> _logger;

        public SegmentsController(ILogger<SegmentsController> logger)
        {
            _logger = logger;
        }

        [Route("[controller]")]
        public Segment GetTree()
        {
            return MockDatabase.GetSegmentsTree();
        }

        [Route("[controller]/flat")]
        public List<Segment> GetFlat()
        {
            return MockDatabase.segments;
        }
    }
}
