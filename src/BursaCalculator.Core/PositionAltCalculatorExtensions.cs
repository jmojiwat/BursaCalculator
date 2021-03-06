using BursaCalculator.Core.Infrastructure;
using LanguageExt;
using static BursaCalculator.Core.Infrastructure.MoneyExtensions;

namespace BursaCalculator.Core
{
    public static class PositionAltCalculatorExtensions
    {
        public static Option<Percent> Risk(Money capital, Money accountRisk) =>
            Prelude.Try(() => accountRisk / capital)
                .Map(PercentExtensions.ToPercent)
                .ToOption();

        public static Option<Money> StopLossPrice(Money accountRisk, Money entryPrice, Quantity shares) =>
            Prelude.Try(() => (shares * entryPrice - accountRisk) / shares)
                .Map(pq => pq.PerShare)
                .Map(Money)
                .ToOption();


        public static Money StopLossPrice(Money entryPrice, Percent stopLossPercent) => 
            entryPrice - entryPrice * stopLossPercent;
        
        public static Money StopLossPrice(Money entryPrice, Tick stopLossTick) => 
            entryPrice - stopLossTick * ToTickSize(entryPrice);

        public static Money TargetPrice(Money entryPrice, Percent targetPercent) => 
            targetPercent * entryPrice + entryPrice;
        
        public static Money TargetPrice(Money entryPrice, Tick targetTick) => 
            targetTick * ToTickSize(entryPrice) + entryPrice;
    }
}