using GP.Result;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GP.AquisitionFunctions
{
    internal abstract class AquisitionFunction
    {
        protected readonly GaussianProcess Gp;
        protected readonly double[] Xs;

        public AquisitionFunction(GaussianProcess gp, double[] xs)
        {
            Gp = gp;
            Xs = xs;
        }

        protected double GetRandomQueryPoint()
        {
            return Xs[new Random().Next(0, Xs.Length - 1)];
        }

        public double GetNextQueryPoint()
        {
            if (!Gp.Data.Any())
            {
                return GetRandomQueryPoint();
            }

            var best = Gp
                .EstimateAtRange(Xs)
                .Max(q => GetAquistionValue(q));

            return best.X;
        }

        public List<AquisitionFunctionValue> GetAquisitionFunctionValues()
        {
            return Gp.EstimateAtRange(Xs)
                .Select(GetAquistionValue)
                .ToList();
        }

        public abstract AquisitionFunctionValue GetAquistionValue(EstimationResult er);

    }
}
