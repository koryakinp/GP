using System;
using System.Linq;
using GP.Result;
using MathNet.Numerics.Distributions;

namespace GP.AquisitionFunctions
{
    internal class ProbabilityOfImprovement : AquisitionFunction
    {
        private readonly Goal Goal;

        public ProbabilityOfImprovement(Goal goal, GaussianProcess gp, double[] xs) : base(gp, xs)
        {
            Goal = goal;
        }

        public override AquisitionFunctionValue GetAquistionValue(EstimationResult er)
        {
            if(Goal == Goal.Max && er.UpperBound < Gp.Data.Max(q => q.FX) || 
                Goal == Goal.Min && er.LowerBound > Gp.Data.Min(q => q.FX))
            {
                return new AquisitionFunctionValue(er.X, 0);
            }

            if(er.Mean == er.UpperBound)
            {
                return new AquisitionFunctionValue(er.X, 0);
            }

            double best = Goal == Goal.Max ? Gp.Data.Max(q => q.FX) : Gp.Data.Min(q => q.FX);
            var sigma = Math.Abs(er.UpperBound - er.LowerBound);
            var next = Normal.CDF(er.Mean, sigma, (er.Mean - best) / sigma);
            return new AquisitionFunctionValue(er.X, Math.Max(next, 0));
        }
    }
}
