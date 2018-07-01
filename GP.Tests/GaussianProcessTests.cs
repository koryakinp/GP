using GP.Kernels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GP.Tests
{
    [TestClass]
    public class GaussianProcessTests
    {
        private GaussianProcess _gp;
        private const double _delta = 0.001;

        [TestInitialize]
        public void SetUp()
        {
            var kernel = new GaussianKernel(1, 1);
            _gp = new GaussianProcess(kernel);
            _gp.AddDataPoint(new DataPoint(1.02, 0.79));
            _gp.AddDataPoint(new DataPoint(1.99, 0.94));
            _gp.AddDataPoint(new DataPoint(4.04, 0.65));
        }

        [TestMethod]
        public void TestAddDataPoint()
        {
            var actual = _gp.GetCovariance();
            var expected = new double[3, 3]
            {
                {1.0000, 0.6247, 0.0105 },
                {0.6247, 1.0000, 0.1223 },
                {0.0105, 0.1223, 1.0000 }                
            };

            Assert.AreEqual(expected.Length, actual.ColumnCount * actual.RowCount);
            expected.ForEach((i, j, q) => Assert.AreEqual(q, actual[i, j], _delta));
        }

        [TestMethod]
        public void EstimateAtPointTest()
        {
            var actual = _gp.EstimateAtPoint(3);
            Assert.AreEqual(0.762, actual.Mean, _delta);
            Assert.AreEqual(1.072, actual.UpperBound, _delta);
            Assert.AreEqual(0.451, actual.LowerBound, _delta);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), Consts.ModelAlreadyContainsDataPoint)]
        public void ShouldThrowExceptionOnDuplicateEntries()
        {
            _gp.AddDataPoint(new DataPoint(1.02, 0.79));
        }

        [TestMethod]
        public void EstimateAtRangeTest()
        {
            var actual = _gp.EstimateAtRange(new double[] { 1.02, 1.775, 2.530, 3.285, 4.04 });
            var expected = new double[5, 4]
            {
                { 0.790, 0.790, 0.790, 1.02 },
                { 0.939, 0.950, 0.962, 1.775 },
                { 0.711, 0.848, 0.984, 2.530 },
                { 0.447, 0.728, 1.010, 3.285 },
                { 0.650, 0.650, 0.650, 4.04 }
            };

            Assert.AreEqual(expected.GetLength(0), actual.Count);

            actual.ToArray().ForEach((i, q) =>
            {
                Assert.AreEqual(expected[i,0], q.LowerBound, _delta);
                Assert.AreEqual(expected[i,1], q.Mean, _delta);
                Assert.AreEqual(expected[i,2], q.UpperBound, _delta);
                Assert.AreEqual(expected[i,3], q.X, _delta);
            });
        }
    }
}
