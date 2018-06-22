using System;
using System.Collections.Generic;
using System.Text;

namespace GP
{
    public class DataPoint
    {
        public readonly double X;
        public readonly double FX;

        public DataPoint(double x, double fx)
        {
            X = x;
            FX = fx;
        }
    }
}
