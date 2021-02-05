using System;
using BursaCalculator.Core.Infrastructure;

namespace BursaCalculator.Core
{
    public record BrokeragePercentageRate
    {
        public BrokeragePercentageRate(decimal amountFrom, Percent rate)
        {
            if (amountFrom < 0) throw new ArgumentOutOfRangeException(nameof(amountFrom));
            AmountFrom = amountFrom;
            Rate = rate;
        }

        public decimal AmountFrom { get; }
        public Percent Rate { get; }
    }

    public static class BrokeragePercentageRateExtensions
    {
        public static BrokeragePercentageRate BrokeragePercentageRate(decimal amountFrom, Percent rate) =>
            new BrokeragePercentageRate(amountFrom, rate);
    }
}