using System;
using System.Linq;
using BursaCalculator.Core.Infrastructure;

namespace BursaCalculator.Core
{
    public record BrokerageFeeRate
    {
        public BrokerageFeeRate(
            IOrderedEnumerable<BrokeragePercentageRate> percentageRates, 
            Percent onlineTradingRebate, 
            decimal minimumFee, 
            Percent intraDayFee)
        {
            if(minimumFee < 0) throw new ArgumentOutOfRangeException(nameof(minimumFee));

            PercentageRates = percentageRates ?? throw new ArgumentNullException(nameof(percentageRates));
            OnlineTradingRebate = onlineTradingRebate;
            MinimumFee = minimumFee;
            IntraDayFee = intraDayFee;
        }

        public IOrderedEnumerable<BrokeragePercentageRate> PercentageRates { get; init; }
        public Percent OnlineTradingRebate { get; private init; }
        public decimal MinimumFee { get; private init; }
        public Percent IntraDayFee { get; private init; }
    }
}