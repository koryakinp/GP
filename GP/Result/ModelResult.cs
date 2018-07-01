using System.Collections.Generic;

namespace GP.Result
{
    public class ModelResult
    {
        public readonly IReadOnlyList<EstimationResult> EstimationValues;
        public readonly IReadOnlyList<DataPoint> QueryValues;
        public readonly IReadOnlyList<AquisitionFunctionValue> AquisitionValues;

        internal ModelResult(
            List<EstimationResult> er, 
            List<DataPoint> qr,
            List<AquisitionFunctionValue> af)
        {
            EstimationValues = er;
            QueryValues = qr;
            AquisitionValues = af;
        }
    }
}
