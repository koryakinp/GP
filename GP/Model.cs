using GP.AquisitionFunctions;
using GP.Kernels;
using GP.Result;
using System;
using System.Linq;

namespace GP
{
    public class Model
    {
        private readonly GaussianProcess _gp;
        private readonly Func<double, double> Query;
        private readonly double[] Xs;

        public Model(
            Kernel kernel, 
            double min, 
            double max, 
            int resolution, 
            Func<double, double> query)
        {
            _gp = new GaussianProcess(kernel);
            Query = query;
            Xs = new double[resolution];
            Xs.ForEach((i, q) => Xs[i] = min + i * (max - min) / (resolution - 1));
        }

        public ModelResult Explore(int queries)
        {
            var af = new Explorer(_gp, Xs);
            return ComputeResult(af, queries);
        }

        public ModelResult FindExtrema(Goal goal, int queries)
        {
            var af = new ProbabilityOfImprovement(goal, _gp, Xs);
            return ComputeResult(af, queries);
        }

        private ModelResult ComputeResult(AquisitionFunction af, int queries)
        {
            for (int i = 0; i < Xs.Length; i++)
            {
                var nextX = af.GetNextQueryPoint();
                if (!_gp.Data.Any(q => q.X == nextX))
                {
                    _gp.AddDataPoint(new DataPoint(nextX, Query(nextX)));
                }
            }

            return new ModelResult(
                _gp.EstimateAtRange(Xs),
                _gp.Data,
                af.GetAquisitionFunctionValues());
        }
    }
}
