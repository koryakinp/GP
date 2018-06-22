using GP.Kernels;
using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GP
{
    public class Model
    {
        private readonly Kernel _kernel;
        private readonly List<DataPoint> _data;
        private Matrix<double> _covariance;

        public Model(Kernel kernel)
        {
            _kernel = kernel;
            _data = new List<DataPoint>();
            _covariance = Matrix<double>.Build.Dense(1, 1);
        }

        public void AddDataPoint(DataPoint dataPoint)
        {
            if (_data.Any(q => q.X == dataPoint.X))
            {
                throw new InvalidOperationException(Consts.ModelAlreadyContainsDataPoint);
            }

            _data.Add(dataPoint);

            int size = _data.Count;
            var updated = Matrix<double>.Build.Dense(size, size);
            _covariance.ForEach((i, j, q) => updated[i, j] = q);

            for (int i = 0; i < size - 1; i++)
            {
                var value = _kernel.Compute(_data[i].X, dataPoint.X);
                updated[i, size - 1] = value;
                updated[size - 1, i] = value;
            }

            updated[size - 1, size - 1] = _kernel.Compute(dataPoint.X, dataPoint.X);
            _covariance = updated;
        }

        public List<EstimationResult> EstimateAtRange(double min, double max, int numberOfPoints)
        {
            if(!_data.Any(q => q.X == min))
            {
                throw new InvalidOperationException(Consts.DataPointStartMissing);
            }

            if(!_data.Any(q => q.X == max))
            {
                throw new InvalidOperationException(Consts.DataPointEndMissing);
            }

            double[] xs = new double[numberOfPoints];
            xs.ForEach((i, q) => xs[i] = min + i*(max - min) / (numberOfPoints - 1));

            List<EstimationResult> res = new List<EstimationResult>();
            xs.ForEach((i, q) => res.Add(EstimateAtPoint(q)));
            return res;
        }

        public EstimationResult EstimateAtPoint(double x)
        {
            var kStar = _data.Select((q, i) => _kernel.Compute(x, q.X)).ToArray();
            var fValues = _data.Select(q => q.FX).ToArray();

            var ks = Vector<double>.Build.Dense(kStar);
            var f = Vector<double>.Build.Dense(fValues);

            var common = GetCovariance()
                .Inverse()
                .Multiply(ks);

            var mu = common.DotProduct(f);
            var confidence = -common.DotProduct(ks) + _kernel.Compute(x, x);

            return new EstimationResult(mu, confidence);
        }

        internal Matrix<double> GetCovariance()
        {
            return _covariance;
        }
    }
}
