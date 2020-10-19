using System;

namespace BursaCalculator.Core
{
    public static class ClearingFeeExtensions
    {
        public static decimal ClearingFee(ClearingFeeRate feeRate, decimal amount) =>
            Math.Min(feeRate.ClearingRate * amount, feeRate.MaximumFee);
    }
}