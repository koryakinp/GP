namespace GP.Result
{
    public class EstimationResult
    {
        public readonly double Mean;
        public readonly double LowerBound;
        public readonly double UpperBound;
        public readonly double X;

        internal EstimationResult(double mean, double confidence, double x)
        {
            Mean = mean;
            LowerBound = Mean - confidence;
            UpperBound = Mean + confidence;
            X = x;
        }
    }
}
