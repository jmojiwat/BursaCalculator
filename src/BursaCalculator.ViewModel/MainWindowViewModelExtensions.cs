using System.Linq;
using BursaCalculator.Core;
using BursaCalculator.Core.Infrastructure;
using BursaCalculator.Persistence;
using LanguageExt;
using static BursaCalculator.Core.Infrastructure.MoneyExtensions;
using static BursaCalculator.Core.Infrastructure.PercentExtensions;
using static BursaCalculator.Core.Infrastructure.TickExtensions;
using static LanguageExt.Prelude;

namespace BursaCalculator.ViewModel
{
    public static class MainWindowViewModelExtensions
    {
        public static Option<Money> CalculateAccountRisk(Option<Money> capital, Option<Percent> risk) =>
            from c in capital
            from r in risk
            select PositionCalculatorExtensions.AccountRisk(c, r);

        public static Money DecrementPrice(Money amount) => amount - ToTickSize(amount);
        public static Tick DecrementTick(Tick ticks) => ticks - Tick(1);

        public static Option<Money> EntryAmount(Option<Money> entryPrice, Option<int> lots) =>
            from e in entryPrice
            from l in lots
            select PositionCalculatorExtensions.EntryAmount(e, l.Lots());

        public static Money IncrementPrice(Money amount) => amount + ToTickSize(amount);

        public static Tick IncrementTick(Tick ticks) => ticks + Tick(1);

        public static bool IsGreaterThanZero(Option<Money> value) =>
            value.Map(IsGreaterThanZero).IfNone(() => false);

        public static bool IsGreaterThanZero(Money value) =>
            value > Money(0);

        public static bool IsGreaterThanZero(Option<Percent> vaule) =>
            vaule.Map(m => m > Percent(0)).IfNone(() => false);

        public static bool IsGreaterThanZero(Option<Tick> value) =>
            value.Map(m => m > Tick(0)).IfNone(() => false);

        public static bool IsGreaterThanZero(Option<decimal> value) =>
            value.Map(m => m > 0).IfNone(() => false);

        public static bool IsGreaterThanZero(Option<int> value) =>
            value.Map(m => m > 0).IfNone(() => false);  

        public static bool IsValidEntryPriceStopLossPrice(Option<Money> entryPrice, Option<Money> stopLossPrice) =>
            (from e in entryPrice
             from sl in stopLossPrice
             let isEntryPriceGreaterThanZero = IsGreaterThanZero(e)
             let isStopLossPriceGreaterThanZero = IsGreaterThanZero(sl)
             let isEntryPriceGreaterThanStopLoss = PositionCalculatorExtensions.IsValidStopLossPrice(e, sl)
             select 
                 isEntryPriceGreaterThanZero && 
                 isStopLossPriceGreaterThanZero && 
                 isEntryPriceGreaterThanStopLoss)
            .IfNone(() => false);

        public static bool IsValidTargetPrice(Option<Money> entryPrice, Option<Money> targetPrice) =>
            (from e in entryPrice
                from sl in targetPrice
                select PositionCalculatorExtensions.IsValidTargetPrice(e, sl))
            .IfNone(() => false);

        // flatten is a monadic join https://github.com/louthy/language-ext/issues/573
        public static Option<int> Lots(Option<Money> entryPrice, Option<Money> stopLossPrice, Option<Money> accountRisk) =>
            from shares in flatten(from e in entryPrice
                from sl in stopLossPrice
                from ar in accountRisk
                select PositionCalculatorExtensions.Shares(e, sl, ar)) 
            select  shares.Lots;

        public static Option<Percent> Risk(Option<Money> capital, Option<Money> accountRisk) =>
            flatten(from c in capital
                from ar in accountRisk
                select PositionAltCalculatorExtensions.Risk(c, ar));

        public static Option<decimal> RiskReward(Option<Money> entryPrice, Option<Money> stopLossPrice,
            Option<Money> targetPrice) =>
            flatten(from e in entryPrice
                from sl in stopLossPrice
                from t in targetPrice
                select PositionCalculatorExtensions.RiskReward(e, sl, t));

        public static Option<int> Shares(Option<Money> entryPrice, Option<Money> stopLossPrice, Option<Money> accountRisk) =>
            from shares in flatten(from e in entryPrice
                from sl in stopLossPrice
                from ar in accountRisk
                select PositionCalculatorExtensions.Shares(e, sl, ar)) 
            select  shares.Shares;

        public static Option<Money> StopLossAmount(Option<Money> entryPrice, Option<Money> stopLossPrice, Option<int> lots) =>
            from e in entryPrice
            from sl in stopLossPrice
            from l in lots
            select PositionCalculatorExtensions.StopLossAmount(e, sl, l.Lots());

        public static Option<Percent> StopLossPercentage(Option<Money> entryPice, Option<Money> stopLossPrice) =>
            flatten(from e in entryPice
                from sl in stopLossPrice
                select PositionCalculatorExtensions.StopLossPercentage(e, sl));

        public static Option<Money>
            StopLossPrice(Option<Money> accountRisk, Option<Money> entryPrice, Option<Quantity> shares) =>
            flatten(from ar in accountRisk
                from e in entryPrice
                from s1 in shares
                select PositionAltCalculatorExtensions.StopLossPrice(ar, e, s1));

        public static Option<Money> StopLossPrice(Option<Money> entryPrice, Option<Percent> stopLossPercent) =>
            from e in entryPrice
            from slp in stopLossPercent
            select PositionAltCalculatorExtensions.StopLossPrice(e, slp);

        public static Option<Money> StopLossPrice(Option<Money> entryPrice, Option<Tick> stopLossTick) =>
            from e in entryPrice
            from slt in stopLossTick
            select PositionAltCalculatorExtensions.StopLossPrice(e, slt);

        public static Option<Tick> StopLossTick(Option<Money> entryPrice, Option<Money> stopLossPrice) =>
            from e in entryPrice
            from sl in stopLossPrice
            select PositionCalculatorExtensions.StopLossTick(e, sl);

        public static Option<Money>
            TargetAmount(Option<Money> entryPrice, Option<Money> targetPrice, Option<int> lots) =>
            from e in entryPrice
            from t in targetPrice
            from l in lots
            select PositionCalculatorExtensions.TargetAmount(e, t, l.Lots());

        public static Option<Percent> TargetPercentage(Option<Money> entryPice, Option<Money> targetPrice) =>
            flatten(from e in entryPice
                from t in targetPrice
                select PositionCalculatorExtensions.TargetPercentage(e, t));

        public static Option<Money> TargetPrice(Option<Money> entryPrice, Option<Percent> targetPercent) =>
            from e in entryPrice
            from tp in targetPercent
            select PositionAltCalculatorExtensions.TargetPrice(e, tp);

        public static Option<Money> TargetPrice(Option<Money> entryPrice, Option<Tick> targetTick) =>
            from e in entryPrice
            from tt in targetTick
            select PositionAltCalculatorExtensions.TargetPrice(e, tt);

        public static Option<Tick> TargetTick(Option<Money> entryPrice, Option<Money> targetPrice) =>
            from e in entryPrice
            from t in targetPrice
            select PositionCalculatorExtensions.TargetTick(e, t);

        public static Settings ToSettings(MainWindowViewModel model) =>
            new Settings
            {
                Capital = model.Capital.Match(c => (decimal) c, () => decimal.MinValue),
                Risk = model.Risk.Match(r => (decimal) r, () => decimal.MinValue),
                EntryPrice = model.EntryPrice.Match(ep => (decimal) ep, () => decimal.MinValue),
                StopLossPrice = model.StopLossPrice.Match(slp => (decimal) slp, () => decimal.MinValue),
                TargetPrice = model.TargetPrice.Match(tp => (decimal) tp, () => decimal.MinValue)
            };

        public static MainWindowViewModel ToViewModel(Settings settings) =>
            new()
            {
                Capital = settings.Capital == decimal.MinValue
                    ? None
                    : Some(Money(settings.Capital)),
                Risk = settings.Risk == decimal.MinValue
                    ? None
                    : Some(Percent(settings.Risk)),
                EntryPrice = settings.EntryPrice == decimal.MinValue
                    ? None
                    : Some(Money(settings.EntryPrice)),
                StopLossPrice = settings.StopLossPrice == decimal.MinValue
                    ? None
                    : Some(Money(settings.StopLossPrice)),
                TargetPrice = settings.TargetPrice == decimal.MinValue
                    ? None
                    : Some(Money(settings.TargetPrice))
            };

        public static Fee ToFee(FeeSettings settings)
        {
            var brokeragePercentageRate = settings.BrokeragePercentageRates
                .Map(r => new BrokeragePercentageRate(r.AmountFrom, Percent(r.Rate)))
                .OrderBy(r => r.AmountFrom);

            var broker = new BrokerageFeeRate(
                brokeragePercentageRate, 
                Percent(settings.BrokerageOnlineTradingRebate), 
                settings.BrokerageMinimumFee, 
                Percent(settings.BrokerageIntraDayFee));

            var clearing = new ClearingFeeRate(
                Percent(settings.ClearingClearingRate),
                settings.ClearingMaximumFee);

            var stampDuty = new StampDutyRate(
                settings.StampDutyAmount,
                settings.StampDutyForEvery,
                settings.StampDutyMaxAmount);

            var salesServiceTax = new SalesServiceTaxRate(
                Percent(settings.SalesServiceTaxRateBrokerage),
                Percent(settings.SalesServiceTaxRateClearingFee),
                Percent(settings.SalesServiceTaxRateStampDuty));

            return new Fee(broker, clearing, stampDuty, salesServiceTax);
        }

        public static FeeSettings ToSettings(Fee fee)
        {
            return new()
            {
                BrokeragePercentageRates = fee.BrokerageFeeRate.PercentageRates
                    .Map(r => new BrokeragePercentageRateSettings
                    {
                        AmountFrom = r.AmountFrom,
                        Rate = (decimal) r.Rate
                    }),
                BrokerageOnlineTradingRebate = (decimal) fee.BrokerageFeeRate.OnlineTradingRebate,
                BrokerageMinimumFee = fee.BrokerageFeeRate.MinimumFee,
                BrokerageIntraDayFee = (decimal) fee.BrokerageFeeRate.IntraDayFee,

                ClearingClearingRate = (decimal) fee.ClearingFeeRate.ClearingRate,
                ClearingMaximumFee = fee.ClearingFeeRate.MaximumFee,

                StampDutyAmount = fee.StampDutyRate.Amount,
                StampDutyForEvery = fee.StampDutyRate.ForEvery,
                StampDutyMaxAmount = fee.StampDutyRate.MaxAmount,

                SalesServiceTaxRateBrokerage = (decimal) fee.SalesServiceTaxRate.Brokerage,
                SalesServiceTaxRateClearingFee = (decimal) fee.SalesServiceTaxRate.ClearingFee,
                SalesServiceTaxRateStampDuty = (decimal) fee.SalesServiceTaxRate.StampDuty
            };
        }
    }
}
