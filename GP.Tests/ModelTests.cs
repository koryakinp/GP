using GP.Kernels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GP.Tests
{
    [TestClass]
    public class ModelTests
    {
        private Model _model;
        private const double _delta = 0.001;

        [TestInitialize]
        public void SetUp()
        {
            var kernel = new GaussianKernel(1, 1);
            _model = new Model(kernel);
            _model.AddDataPoint(new DataPoint(1.02, 0.79));
            _model.AddDataPoint(new DataPoint(1.99, 0.94));
            _model.AddDataPoint(new DataPoint(4.04, 0.65));
        }

        [TestMethod]
        public void TestAddDataPoint()
        {
            var actual = _model.GetCovariance();
            var expected = new double[3, 3]
            {
                {1.0000,  0.6247,  0.0105 },
                {0.6247,  1.0000,  0.1223 },
                {0.0105,  0.1223,  1.0000 }                
            };

            Assert.AreEqual(expected.Length, actual.ColumnCount * actual.RowCount);
            expected.ForEach((i, j, q) => Assert.AreEqual(q, actual[i, j], _delta));
        }

        [TestMethod]
        public void EstimateAtPointTest()
        {
            var actual = _model.EstimateAtPoint(3);
            Assert.AreEqual(0.762, actual.Mean, _delta);
            Assert.AreEqual(1.072, actual.UpperBound, _delta);
            Assert.AreEqual(0.451, actual.LowerBound, _delta);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), Consts.ModelAlreadyContainsDataPoint)]
        public void ShouldThrowExceptionOnDuplicateEntries()
        {
            _model.AddDataPoint(new DataPoint(1.02, 0.79));
        }

        [TestMethod]
        public void EstimateAtRangeTest()
        {
            var actual = _model.EstimateAtRange(1.02, 4.04, 5);
            var expected = new double[5, 3]
            {
                { 0.790, 0.790, 0.790 },
                { 0.939, 0.950, 0.962 },
                { 0.711, 0.848, 0.984 },
                { 0.447, 0.728, 1.010 },
                { 0.650, 0.650, 0.650 }
            };

            Assert.AreEqual(expected.GetLength(0), actual.Count);

            actual.ToArray().ForEach((i, q) =>
            {
                Assert.AreEqual(expected[i,0], q.LowerBound, _delta);
                Assert.AreEqual(expected[i,1], q.Mean, _delta);
                Assert.AreEqual(expected[i,2], q.UpperBound, _delta);
            });
        }
    }
}
