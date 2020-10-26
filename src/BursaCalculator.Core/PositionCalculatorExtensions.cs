using BursaCalculator.Core.Infrastructure;
using LanguageExt;
using static BursaCalculator.Core.Infrastructure.PercentExtensions;
using static BursaCalculator.Core.Infrastructure.TickExtensions;
using static LanguageExt.Prelude;

namespace BursaCalculator.Core
{
    public static class PositionCalculatorExtensions
    {
        public static Money AccountRisk(Money capital, Percent risk) => capital * risk;

        public static Option<Quantity> Shares(Money entryPrice, Money stopLossPrice, Money accountRisk) =>
            SharesToLong(entryPrice, stopLossPrice, accountRisk);

        public static Money StopLossAmount(Money entryPrice, Money stopLossPrice, Quantity shares) =>
            (entryPrice - stopLossPrice) * shares;

        public static Option<Percent> StopLossPercentage(Money entryPice, Money stopLossPrice) =>
            Try(() => (entryPice - stopLossPrice) / entryPice)
                .Map(ToPercent)
                .ToOption();

        public static Tick StopLossTick(Money entryPice, Money stopLossPrice) => 
            ToTick(entryPice, stopLossPrice);

        public static Money TargetAmount(Money entryPrice, Money targetPrice, Quantity shares) =>
            (targetPrice - entryPrice) * shares;

        public static Option<Percent> TargetPercentage(Money entryPice, Money targetPrice) =>
            Try(() => (targetPrice - entryPice) / entryPice)
                .Map(ToPercent)
                .ToOption();

        public static Tick TargetTick(Money entryPice, Money targetPrice) =>
            ToTick(targetPrice, entryPice);
        
        public static Option<decimal> RiskReward(Money entryPrice, Money stopLossPrice, Money targetPrice) => 
            RiskRewardLong(entryPrice, stopLossPrice, targetPrice);
        
        private static Option<decimal> RiskRewardLong(Money entryPrice, Money stopLossPrice, Money targetPrice)
        {
            var dividend = (decimal)(targetPrice - entryPrice);
            var divisor = (decimal)(entryPrice - stopLossPrice);
            
            if(dividend < decimal.Zero) return None;
            if(divisor < decimal.Zero) return None;

            return Try(() => dividend / divisor)
                .ToOption();
        }

        private static Option<Quantity> SharesToLong(Money entryPrice, Money stopLossPrice, Money accountRisk) =>
            Try(() => (int) (accountRisk / (entryPrice - stopLossPrice)))
                .Map(i => i.Shares())
                .ToOption();

        public static Money EntryAmount(Money entryPrice, Quantity shares) =>
            entryPrice * shares;

        public static bool IsValidStopLossPrice(Money entryPrice, Money stopLossPrice) =>
            entryPrice > stopLossPrice;

        public static bool IsValidTargetPrice(Money entryPrice, Money targetPrice) =>
            entryPrice < targetPrice;
    }
}