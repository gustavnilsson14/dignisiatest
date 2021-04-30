using NUnit.Framework;

namespace UnitTest
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
            Assert.AreNotEqual(DignisiaTest.MockDatabase.creditDataPoints.Count, 399);
        }
    }
}