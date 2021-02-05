using System;

namespace BursaCalculator.Core
{
    public record StampDutyRate
    {
        public StampDutyRate(decimal amount, decimal forEvery, decimal maxAmount)
        {
            if (amount < 0) throw new ArgumentOutOfRangeException(nameof(amount));
            if (forEvery <= 0) throw new ArgumentOutOfRangeException(nameof(forEvery));
            Amount = amount;
            ForEvery = forEvery;
            MaxAmount = maxAmount;
        }

        public decimal Amount { get; private init; }
        public decimal ForEvery { get; private init; }
        public decimal MaxAmount { get; private init; }
    }
}