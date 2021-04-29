using NUnit.Framework;

namespace NUnitTestProject1
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            DignisiaTest.MockDatabase.GenerateCreditDataPoints();
        }

        [Test]
        public void TestDataPointsAmount()
        {
            Assert.AreEqual(DignisiaTest.MockDatabase.creditDataPoints.Count, 400);
        }
    }
}