using System;

namespace GP.Kernels
{
    public class GaussianKernel : Kernel
    {
        private readonly double _lengthscale;
        private readonly double _variance;

        public GaussianKernel(double lengthscale, double variance)
        {
            _lengthscale = lengthscale * 2;
            _variance = variance;
        }

        internal override double Compute(double left, double right)
        {
            if(left == right)
            {
                return _variance;
            }

            var sqdist = Math.Pow((left - right), 2);
            return _variance * Math.Exp(-sqdist / _lengthscale);
        }
    }
}
