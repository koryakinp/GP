﻿using System;
using System.Linq;
using GP.Result;
using MathNet.Numerics.Distributions;

namespace GP.AquisitionFunctions
{
    internal class ExpectedfImprovement : AquisitionFunction
    {
        private readonly Goal Goal;

        public ExpectedfImprovement(Goal goal, GaussianProcess gp, double[] xs) : base(gp, xs)
        {
            Goal = goal;
        }

        public override AquisitionFunctionValue GetAquistionValue(EstimationResult er)
        {
            double best = Goal == Goal.Max
                ? Gp.Data.Max(q => q.FX)
                : Gp.Data.Min(q => q.FX);

            var delta = er.Mean - best;
            var sigma = er.UpperBound - er.LowerBound;

            var firstTerm = sigma * Normal.PDF(0, 1, delta / sigma);
            var secondTerm = Math.Abs(delta) * Normal.CDF(0, 1, -1 * Math.Abs(delta) / sigma);

            var next = delta + firstTerm - secondTerm;
            return new AquisitionFunctionValue(er.X, Math.Max(next, 0));
        }
    }
}
