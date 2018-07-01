using System;
using System.Collections.Generic;
using System.Text;

namespace GP
{
    internal class Consts
    {
        public const string ModelAlreadyContainsDataPoint = "Model already contain that data point";
        public const string DataPointStartMissing = "Can not estimate at range. A data point must be provided at the begining of the range.";
        public const string DataPointEndMissing = "Can not estimate at range. A data point must be provided at the end of the range.";
        public const string InvalidMinMax = "Max parameter must be greater than Min parameter";
        public const string InvalidOutputResolution = "Output Resolution parameter must be greater than Number of Queries parameter";
    }
}
