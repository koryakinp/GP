namespace GP
{
    public class EstimationResult
    {
        public readonly double Mean;
        public readonly double LowerBound;
        public readonly double UpperBound;

        internal EstimationResult(double mean, double confidence)
        {
            Mean = mean;
            LowerBound = Mean - confidence;
            UpperBound = Mean + confidence;
        }
    }
}
