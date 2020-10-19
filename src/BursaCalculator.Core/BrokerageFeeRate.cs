using System;
using System.Linq;
using BursaCalculator.Core.Infrastructure;

namespace BursaCalculator.Core
{
    public class BrokerageFeeRate
    {
        public BrokerageFeeRate(IOrderedEnumerable<BrokeragePercentageRate> percentageRates,
            Percent onlineTradingRebate,
            decimal minimumFee,
            Percent intraDayFee)
        {
            if (minimumFee < 0) throw new ArgumentOutOfRangeException(nameof(minimumFee));
//            InitialPercentageRate = initialPercentageRate;
            PercentageRates = percentageRates ?? throw new ArgumentNullException(nameof(percentageRates));
            OnlineTradingRebate = onlineTradingRebate;
            MinimumFee = minimumFee;
            IntraDayFee = intraDayFee;
        }
//        public Percent InitialPercentageRate { get; }

        public IOrderedEnumerable<BrokeragePercentageRate> PercentageRates { get; }
        public Percent OnlineTradingRebate { get; }
        public decimal MinimumFee { get; }
        public Percent IntraDayFee { get; }
    }
}