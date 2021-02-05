using System;
using System.Linq;
using BursaCalculator.Core.Infrastructure;

namespace BursaCalculator.Core
{
    public static class BrokerageFeeRateExtensions
    {
        public static BrokerageFeeRate BrokerageFeeRate(IOrderedEnumerable<BrokeragePercentageRate> percentageRates,
            Percent onlineTradingRebate,
            decimal minimumFee,
            Percent intraDayFee) =>
            new BrokerageFeeRate(percentageRates, onlineTradingRebate,  minimumFee, intraDayFee);
           
    }
}