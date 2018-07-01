using GP.Kernels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using MathNet.Numerics;

namespace GP.Tests
{
    [TestClass]
    public class ModelTests
    {
        private Model Model;

        [TestInitialize]
        public void SetUp()
        {
            var kernel = new GaussianKernel(0.1, 1);
            Model = new Model(kernel, 1, 3, 300, Trig.Sin);
        }

        [TestMethod]
        public void ExploreTest()
        {
            var res = Model.Explore(10);
        }
    }
}
