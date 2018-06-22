using System;
using System.Collections.Generic;
using System.Text;

namespace GP
{
    internal class Consts
    {
        public const string MatrixMustBeSquare = "Can not perform Cholesky Decomposistion. A matrix must be N by N.";
        public const string MatrixMustBeSimmetrical = "Can not perform Cholesky Decomposistion. A matrix must be symmetrical.";
        public const string ModelAlreadyContainsDataPoint = "Model already contain that data point";
        public const string DataPointStartMissing = "Can not estimate at range. A data point must be provided at the begining of the range.";
        public const string DataPointEndMissing = "Can not estimate at range. A data point must be provided at the end of the range.";
    }
}
