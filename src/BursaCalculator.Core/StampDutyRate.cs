using System;

namespace BursaCalculator.Core
{
    public class StampDutyRate
    {
        public StampDutyRate(decimal amount, decimal forEvery, decimal maxAmount)
        {
            if (amount < 0) throw new ArgumentOutOfRangeException(nameof(amount));
            if (forEvery <= 0) throw new ArgumentOutOfRangeException(nameof(forEvery));
            Amount = amount;
            ForEvery = forEvery;
            MaxAmount = maxAmount;
        }

        public decimal Amount { get; }
        public decimal ForEvery { get; }
        public decimal MaxAmount { get; }
    }
}