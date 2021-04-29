using System;
using System.Collections;
using System.Collections.Generic;

namespace DignisiaTest
{
    public class Segment
    {
        public int segmentId { get; set; }
        public int parentId { get; set; }
        public string segmentName { get; set; }
        public List<Segment> children { get; set; }

    }
}
