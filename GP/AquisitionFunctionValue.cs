using System;

namespace GP
{
    public class AquisitionFunctionValue : IComparable<AquisitionFunctionValue>
    {
        public readonly double X;
        public readonly double FX;

        public AquisitionFunctionValue(double x, double fx)
        {
            X = x;
            FX = fx;
        }

        public int CompareTo(AquisitionFunctionValue other)
        {
            return FX.CompareTo(other.FX);
        }
    }
}
