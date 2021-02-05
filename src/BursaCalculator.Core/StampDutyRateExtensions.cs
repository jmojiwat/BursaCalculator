namespace BursaCalculator.Core
{
    public static class StampDutyRateExtensions
    {
        public static StampDutyRate StampDutyRate(decimal amount, decimal forEvery, decimal maxAmount) =>
            new StampDutyRate(amount, forEvery, maxAmount);
    }
}