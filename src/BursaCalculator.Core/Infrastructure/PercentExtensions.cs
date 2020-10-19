namespace BursaCalculator.Core.Infrastructure
{
    public static class PercentExtensions
    {
        public static Percent Percent(decimal value) => new Percent(value);

        public static Percent ToPercent(decimal value) => new Percent(value * 100);
    }
}