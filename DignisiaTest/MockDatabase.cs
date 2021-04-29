using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DignisiaTest
{
    public class MockDatabase
    {
        public static List<Segment> segments = new List<Segment> {
            new Segment{
                segmentId = 1,
                parentId = -1,
                segmentName = "All countries",
            },
            new Segment{
                segmentId = 2,
                parentId = 1,
                segmentName = "Sweden",
            },
            new Segment{
                segmentId = 3,
                parentId = 1,
                segmentName = "Germany",
            },
            new Segment{
                segmentId = 4,
                parentId = 2,
                segmentName = "Invoice",
            },
            new Segment{
                segmentId = 5,
                parentId = 2,
                segmentName = "Credit card",
            },
            new Segment{
                segmentId = 6,
                parentId = 2,
                segmentName = "Loan",
            },
            new Segment{
                segmentId = 7,
                parentId = 3,
                segmentName = "Invoice",
            }
        };

        public static Segment GetSegmentsTree()
        {
            Segment root = MockDatabase.segments.Find(segment => segment.parentId == -1);
            return GetChildSegmentsRecursive(root);

        }
        public static bool GetAllSegmentsBelow(out List<int> result, int segmentId)
        {
            result = new List<int>();
            Segment root = MockDatabase.segments.Find(segment => segment.segmentId == segmentId);
            if (root == null)
                return false;
            root = GetChildSegmentsRecursive(root);
            result = GetSegmentIdsRecursive(root);
            return true;
        }
        public static Segment GetChildSegmentsRecursive(Segment currentParent) {
            currentParent.children = new List<Segment>();
            currentParent.children.AddRange(MockDatabase.segments.FindAll(segment => segment.parentId == currentParent.segmentId));
            foreach (Segment child in currentParent.children)
            {
                MockDatabase.GetChildSegmentsRecursive(child);
            }
            return currentParent;
        }
        public static List<int> GetSegmentIdsRecursive(Segment currentParent) {
            List<int> result = new List<int>();
            foreach (Segment child in currentParent.children)
            {
                result.AddRange(GetSegmentIdsRecursive(child));
            }
            result.Add(currentParent.segmentId);
            return result;
        }

        public static List<CreditDataPoint> creditDataPoints;

        public static void GenerateCreditDataPoints() {
            Random random = new Random();

            MockDatabase.creditDataPoints = new List<CreditDataPoint>();
            List<int> segmentIds = new List<int> { 4, 5, 6, 7 };
            for (int i = 0; i < 400; i++)
            {
                int segmentId = segmentIds[random.Next(0, segmentIds.Count)];
                DateTime timeStamp = DateTime.Parse("2020-02-01").AddMonths(random.Next(0,14));
                CreditDataPoint segment = new CreditDataPoint
                {
                    closed = random.Next() > (Int32.MaxValue / 2),
                    value = Convert.ToDecimal(random.Next(10000,200000)),
                    segmentId = segmentId,
                    timeStamp = timeStamp
                };
                MockDatabase.creditDataPoints.Add(segment);
            }
        }
    }
}