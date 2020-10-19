using BursaCalculator.Core.Infrastructure;

namespace BursaCalculator.Core
{
    public class ClearingFeeRate
    {
        public ClearingFeeRate(Percent clearingRate, decimal maximumFee)
        {
            ClearingRate = clearingRate;
            MaximumFee = maximumFee;
        }

        public Percent ClearingRate { get; }
        public decimal MaximumFee { get; }
    }
}