namespace BursaCalculator.Core.Infrastructure
{
    public static class MoneyExtensions
    {
        public static Money Money(decimal value) => new Money(value);
        
        public static Money ToTickSize(Money price) =>
            price switch
            {
                _ when price < Money(1m) => Money(0.005m),
                _ when price < Money(9.99m) => Money(0.01m),
                _ when price < Money(99.99m) => Money(0.02m),
                _ => Money(0.1m)
            };

    }
}