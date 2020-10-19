using System;
using BursaCalculator.Core.Infrastructure;

namespace BursaCalculator.Core
{
    public class BrokeragePercentageRate
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
}