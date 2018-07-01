using GP.Kernels;
using GP.Result;
using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GP
{
    internal class GaussianProcess
    {
        private readonly Kernel _kernel;
        public readonly List<DataPoint> Data;
        private Matrix<double> _covariance;

        public GaussianProcess(Kernel kernel)
        {
            _kernel = kernel;
            Data = new List<DataPoint>();
            _covariance = Matrix<double>.Build.Dense(1, 1);
        }

        public void AddDataPoint(DataPoint dataPoint)
        {
            if (Data.Any(q => q.X == dataPoint.X))
            {
                throw new InvalidOperationException(Consts.ModelAlreadyContainsDataPoint);
            }

            Data.Add(dataPoint);

            int size = Data.Count;
            var updated = Matrix<double>.Build.Dense(size, size);
            _covariance.ForEach((i, j, q) => updated[i, j] = q);

            for (int i = 0; i < size - 1; i++)
            {
                var value = _kernel.Compute(Data[i].X, dataPoint.X);
                updated[i, size - 1] = value;
                updated[size - 1, i] = value;
            }

            updated[size - 1, size - 1] = _kernel.Compute(dataPoint.X, dataPoint.X);
            updated.MapInplace(q => Math.Round(q, 5));
            _covariance = updated;
        }

        public List<EstimationResult> EstimateAtRange(double[] Xs)
        {
            List<EstimationResult> res = new List<EstimationResult>();
            Xs.ForEach((i, q) => res.Add(EstimateAtPoint(q)));
            return res;
        }

        public EstimationResult EstimateAtPoint(double x)
        {
            var kStar = Data.Select((q, i) => _kernel.Compute(x, q.X)).ToArray();
            var fValues = Data.Select(q => q.FX).ToArray();

            var ks = Vector<double>.Build.Dense(kStar);
            var f = Vector<double>.Build.Dense(fValues);

            var common = GetCovariance()
                .Inverse()
                .Multiply(ks);

            var mu = common.DotProduct(f);
            var confidence = -common.DotProduct(ks) + _kernel.Compute(x, x);

            return new EstimationResult(mu, confidence, x);
        }

        public Matrix<double> GetCovariance()
        {
            return _covariance;
        }
    }
}
