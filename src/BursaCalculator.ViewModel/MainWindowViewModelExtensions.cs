using BursaCalculator.Core;
using BursaCalculator.Core.Infrastructure;
using BursaCalculator.Persistence;
using static BursaCalculator.Core.Infrastructure.LotExtensions;
using static BursaCalculator.Core.Infrastructure.MoneyExtensions;
using static BursaCalculator.Core.Infrastructure.PercentExtensions;
using static BursaCalculator.Core.Infrastructure.ShareExtensions;
using static BursaCalculator.Core.Infrastructure.TickExtensions;

namespace BursaCalculator.ViewModel
{
    public static class MainWindowViewModelExtensions
    {
        public static Money StopLossPrice(Money accountRisk, Money entryPrice, Lot lots) =>
            PositionAltCalculatorExtensions.StopLossPrice(accountRisk, entryPrice, lots)
                .IfNone(() => Money(0));
        public static Lot Lots(Money entryPrice, Money stopLossPrice, Money accountRisk) =>
            PositionCalculatorExtensions.Lots(entryPrice, stopLossPrice, accountRisk)
                .IfNone(() => Lot(0));

        public static Percent Risk(Money capital, Money accountRisk) =>
            PositionAltCalculatorExtensions.Risk(capital, accountRisk)
                .IfNone(() => Percent(0));

        public static decimal RiskReward(Money entryPrice, Money stopLossPrice, Money targetPrice) =>
            PositionCalculatorExtensions.RiskReward(entryPrice, stopLossPrice, targetPrice)
                .IfNone(() => decimal.Zero);

        public static Share Shares(Money entryPrice, Money stopLossPrice, Money accountRisk) =>
            PositionCalculatorExtensions.Shares(entryPrice, stopLossPrice, accountRisk)
                .IfNone(() => Share(0));

        public static Percent StopLossPercentage(Money entryPice, Money stopLossPrice) =>
            PositionCalculatorExtensions.StopLossPercentage(entryPice, stopLossPrice)
                .IfNone(() => ToPercent(0));

        public static Tick StopLossTick(Money entryPrice, Money stopLossPrice) =>
            PositionCalculatorExtensions.StopLossTick(entryPrice, stopLossPrice);

        public static Percent TargetPercentage(Money entryPice, Money targetPrice) =>
            PositionCalculatorExtensions.TargetPercentage(entryPice, targetPrice)
                .IfNone(() => ToPercent(0));

        public static Tick TargetTick(Money entryPrice, Money targetPrice) =>
            PositionCalculatorExtensions.TargetTick(entryPrice, targetPrice);

        public static MainWindowViewModel ToViewModel(Settings settings) =>
            new MainWindowViewModel
            {
                Capital = Money(settings.Capital),
                Risk = Percent(settings.Risk),
                EntryPrice = Money(settings.EntryPrice),
                StopLossPrice = Money(settings.StopLossPrice),
                TargetPrice = Money(settings.TargetPrice)
            };

        public static Settings ToSettings(MainWindowViewModel model) =>
            new Settings
            {
                Capital = (decimal) model.Capital,
                Risk = (decimal) model.Risk,
                EntryPrice = (decimal) model.EntryPrice,
                StopLossPrice = (decimal) model.StopLossPrice,
                TargetPrice = (decimal) model.TargetPrice
            };

        public static Money EntryAmount(Money entryPrice, Lot lots) => 
            PositionCalculatorExtensions.EntryAmount(entryPrice, lots);
        
        public static Money StopLossPrice(Money entryPrice, Percent stopLossPercent) =>
            PositionAltCalculatorExtensions.StopLossPrice(entryPrice, stopLossPercent);
        
        public static Money StopLossPrice(Money entryPrice, Tick stopLossTick) =>
            PositionAltCalculatorExtensions.StopLossPrice(entryPrice, stopLossTick);
        
        public static Money TargetPrice(Money entryPrice, Percent targetPercent) =>
            PositionAltCalculatorExtensions.TargetPrice(entryPrice, targetPercent);
        public static Money TargetPrice(Money entryPrice, Tick targetTick) =>
            PositionAltCalculatorExtensions.TargetPrice(entryPrice, targetTick);

        public static Tick IncrementTick(Tick ticks) => ticks + Tick(1);
        public static Tick DecrementTick(Tick ticks) => ticks - Tick(1);

        public static Money IncrementPrice(Money amount) => amount + ToTickSize(amount);
        public static Money DecrementPrice(Money amount) => amount - ToTickSize(amount);
    }
}