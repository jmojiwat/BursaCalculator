using System;
using System.Reactive.Linq;
using BursaCalculator.Core.Infrastructure;
using LanguageExt;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace BursaCalculator.ViewModel
{
    public class MainWindowViewModel : ReactiveObject
    {
        public MainWindowViewModel()
        {
            this.WhenAnyValue(
                    model => model.Capital,
                    model => model.Risk,
                    MainWindowViewModelExtensions.AccountRisk)
                .Subscribe(o => AccountRisk = o);

            this.WhenAnyValue(
                    model => model.EntryPrice,
                    model => model.StopLossPrice,
                    model => model.AccountRisk,
                    MainWindowViewModelExtensions.Lots)
                .Subscribe(o => Lots = o);

            this.WhenAnyValue(
                    model => model.EntryPrice,
                    model => model.StopLossPrice,
                    model => model.AccountRisk,
                    MainWindowViewModelExtensions.Shares)
                .ToPropertyEx(this, model => model.Shares);

            this.WhenAnyValue(
                    model => model.EntryPrice,
                    model => model.Lots,
                    MainWindowViewModelExtensions.EntryAmount)
                .ToPropertyEx(this, model => model.EntryAmount);

            this.WhenAnyValue(
                    model => model.EntryPrice,
                    model => model.StopLossPrice,
                    MainWindowViewModelExtensions.StopLossPercentage)
                .Subscribe(o => StopLossPercent = o);

            this.WhenAnyValue(
                    model => model.EntryPrice,
                    model => model.StopLossPrice,
                    MainWindowViewModelExtensions.StopLossTick)
                .Subscribe(o => StopLossTicks = o);

            this.WhenAnyValue(
                    model => model.EntryPrice,
                    model => model.StopLossPrice,
                    model => model.Lots,
                    MainWindowViewModelExtensions.StopLossAmount)
                .ToPropertyEx(this, model => model.StopLossAmount);

            this.WhenAnyValue(
                    model => model.EntryPrice,
                    model => model.TargetPrice,
                    MainWindowViewModelExtensions.TargetPercentage)
                .Subscribe(o => TargetPercent = o);

            this.WhenAnyValue(
                    model => model.EntryPrice,
                    model => model.TargetPrice,
                    MainWindowViewModelExtensions.TargetTick)
                .Subscribe(o => TargetTicks = o);

            this.WhenAnyValue(
                    model => model.EntryPrice,
                    model => model.TargetPrice,
                    model => model.Lots,
                    MainWindowViewModelExtensions.TargetAmount)
                .ToPropertyEx(this, model => model.TargetAmount);

            this.WhenAnyValue(
                    model => model.EntryPrice,
                    model => model.StopLossPrice,
                    model => model.TargetPrice,
                    MainWindowViewModelExtensions.RiskReward)
                .ToPropertyEx(this, model => model.RiskReward);

            this.WhenAnyValue(model => model.AccountRisk)
                .Where(ar => IsFocusedAccountRisk)
                .Select(ar => MainWindowViewModelExtensions.Risk(Capital, ar))
                .Subscribe(o => Risk = o);

            this.WhenAnyValue(model => model.Lots)
                .Where(l => IsFocusedLots)
                .Select(l => MainWindowViewModelExtensions.StopLossPrice(AccountRisk, EntryPrice, l))
                .Subscribe(o => StopLossPrice = o);


            this.WhenAnyValue(model => model.StopLossPercent)
                .Where(slp => IsFocusedStopLossPercent)
                .Select(slp => MainWindowViewModelExtensions.StopLossPrice(EntryPrice, slp))
                .Subscribe(o => StopLossPrice = o);

            this.WhenAnyValue(model => model.StopLossTicks)
                .Where(slt => IsFocusedStopLossTick)
                .Select(slt => MainWindowViewModelExtensions.StopLossPrice(EntryPrice, slt))
                .Subscribe(o => StopLossPrice = o);

            this.WhenAnyValue(model => model.TargetPercent)
                .Where(tp => IsFocusedTargetPercent)
                .Select(tp => MainWindowViewModelExtensions.TargetPrice(EntryPrice, tp))
                .Subscribe(o => TargetPrice = o);

            this.WhenAnyValue(model => model.TargetTicks)
                .Where(tt => IsFocusedTargetTick)
                .Select(tt => MainWindowViewModelExtensions.TargetPrice(EntryPrice, tt))
                .Subscribe(o => StopLossPrice = o);
        }

        [Reactive] public bool IsValidCapital { get; set; }
        [Reactive] public bool IsValidRisk { get; set; }
        [Reactive] public bool IsValidAccountRisk { get; set; }
        [Reactive] public bool IsValidEntryPrice { get; set; }
        [Reactive] public bool IsValidLots { get; set; }
        [Reactive] public bool IsValidStopLossPrice { get; set; }
        [Reactive] public bool IsValidStopLossPercent { get; set; }
        [Reactive] public bool IsValidStopLossTicks { get; set; }
        [Reactive] public bool IsValidTargetPrice { get; set; }
        [Reactive] public bool IsValidTargetPercent { get; set; }
        [Reactive] public bool IsValidTargetTicks { get; set; }

        [Reactive] public Option<Money> Capital { get; set; }
        [Reactive] public Option<Percent> Risk { get; set; }
        [Reactive] public Option<Money> AccountRisk { get; set; }

        [Reactive] public Option<Money> EntryPrice { get; set; }
        [Reactive] public Option<Lot> Lots { get; set; }
        [Reactive] public Option<Money> StopLossPrice { get; set; }
        [Reactive] public Option<Percent> StopLossPercent { get; set; }
        [Reactive] public Option<Tick> StopLossTicks { get; set; }
        [Reactive] public Option<Money> TargetPrice { get; set; }
        [Reactive] public Option<Percent> TargetPercent { get; set; }
        [Reactive] public Option<Tick> TargetTicks { get; set; }

        [Reactive] public bool IsFocusedLots { get; set; }
        [Reactive] public bool IsFocusedStopLossPercent { get; set; }
        [Reactive] public bool IsFocusedStopLossTick { get; set; }
        [Reactive] public bool IsFocusedTargetPercent { get; set; }
        [Reactive] public bool IsFocusedTargetTick { get; set; }
        [Reactive] public bool IsFocusedAccountRisk { get; set; }

        [ObservableAsProperty] public Option<Share> Shares { get; }
        [ObservableAsProperty] public Option<Money> EntryAmount { get; }
        [ObservableAsProperty] public Option<Money> StopLossAmount { get; }
        [ObservableAsProperty] public Option<decimal> RiskReward { get; }
        [ObservableAsProperty] public Option<Money> TargetAmount { get; }

        
       
    }
}