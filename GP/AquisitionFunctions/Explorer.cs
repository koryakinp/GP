using GP.Result;
using System;

namespace GP.AquisitionFunctions
{
    internal class Explorer : AquisitionFunction
    {
        public Explorer(GaussianProcess gp, double[] Xs) : base(gp, Xs) { }

        public override AquisitionFunctionValue GetAquistionValue(EstimationResult er)
        {
            return new AquisitionFunctionValue(er.X, er.UpperBound - er.LowerBound);
        }
    }
}
