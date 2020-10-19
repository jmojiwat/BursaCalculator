using BursaCalculator.Core.Infrastructure;
using LanguageExt;
using static BursaCalculator.Core.Infrastructure.PercentExtensions;
using static BursaCalculator.Core.Infrastructure.ShareExtensions;
using static BursaCalculator.Core.Infrastructure.TickExtensions;
using static LanguageExt.Prelude;

namespace BursaCalculator.Core
{
    public static class PositionCalculatorExtensions
    {
        public static Money AccountRisk(Money capital, Percent risk) =>
            capital * risk;

        public static Option<Share> Shares(Money entryPrice, Money stopLossPrice, Money accountRisk) =>
            SharesToLong(entryPrice, stopLossPrice, accountRisk);

        public static Option<Lot> Lots(Money entryPrice, Money stopLossPrice, Money accountRisk) =>
            SharesToLong(entryPrice, stopLossPrice, accountRisk)
                .Map(ToLot);

        public static Money StopLossAmount(Money entryPrice, Money stopLossPrice, Lot lots) =>
            (entryPrice - stopLossPrice) * lots;

        public static Option<Percent> StopLossPercentage(Money entryPice, Money stopLossPrice) =>
            Try(() => (entryPice - stopLossPrice) / entryPice)
                .Map(ToPercent)
                .ToOption();

        public static Tick StopLossTick(Money entryPice, Money stopLossPrice) => 
            ToTick(entryPice, stopLossPrice);

        public static Money TargetAmount(Money entryPrice, Money targetPrice, Lot lots) =>
            (targetPrice - entryPrice) * lots;

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

        private static Option<Share> SharesToLong(Money entryPrice, Money stopLossPrice, Money accountRisk) =>
            Try(() => (int) (accountRisk / (entryPrice - stopLossPrice)))
                .Map(Share)
                .ToOption();

        public static Money EntryAmount(Money entryPrice, Lot lots) =>
            entryPrice * lots;
    }
}