using System;

namespace BursaCalculator.Core
{
    public static class StampDutyExtensions
    {
        public static decimal CalculateStampDuty(StampDutyRate stampDutyRate, decimal amount)
        {
            var duty = MultipleCount(stampDutyRate.ForEvery, amount) * stampDutyRate.Amount +
                       Modulus(stampDutyRate.ForEvery, amount) * stampDutyRate.Amount;

            return Math.Min(duty, stampDutyRate.MaxAmount);
        }

        private static int Modulus(decimal forEvery, decimal amount) =>
            amount % forEvery > 0 ? 1 : 0;

        private static int MultipleCount(decimal forEvery, decimal amount) =>
            (int) Math.Floor(amount / forEvery);
    }
}