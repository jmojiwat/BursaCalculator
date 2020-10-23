using System;
using System.Reactive.Linq;
using BursaCalculator.Core.Infrastructure;
using LanguageExt;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using static BursaCalculator.ViewModel.MainWindowViewModelExtensions;

namespace BursaCalculator.ViewModel
{
    public class MainWindowViewModel : ReactiveObject
    {
        public MainWindowViewModel()
        {
            this.WhenAnyValue(
                    vm => vm.Capital,
                    vm => vm.Risk,
                    MainWindowViewModelExtensions.AccountRisk)
                .Subscribe(o => AccountRisk = o);

            this.WhenAnyValue(
                    vm => vm.EntryPrice,
                    vm => vm.StopLossPrice,
                    vm => vm.AccountRisk,
                    MainWindowViewModelExtensions.Lots)
                .Subscribe(o => Lots = o);

            this.WhenAnyValue(
                    vm => vm.EntryPrice,
                    vm => vm.StopLossPrice,
                    vm => vm.AccountRisk,
                    MainWindowViewModelExtensions.Shares)
                .ToPropertyEx(this, vm => vm.Shares);

            this.WhenAnyValue(
                    vm => vm.EntryPrice,
                    vm => vm.Lots,
                    MainWindowViewModelExtensions.EntryAmount)
                .ToPropertyEx(this, vm => vm.EntryAmount);

            this.WhenAnyValue(
                    vm => vm.EntryPrice,
                    vm => vm.StopLossPrice,
                    StopLossPercentage)
                .Subscribe(o => StopLossPercent = o);

            this.WhenAnyValue(
                    vm => vm.EntryPrice,
                    vm => vm.StopLossPrice,
                    StopLossTick)
                .Subscribe(o => StopLossTicks = o);

            this.WhenAnyValue(
                    vm => vm.EntryPrice,
                    vm => vm.StopLossPrice,
                    vm => vm.Lots,
                    MainWindowViewModelExtensions.StopLossAmount)
                .ToPropertyEx(this, vm => vm.StopLossAmount);

            this.WhenAnyValue(
                    vm => vm.EntryPrice,
                    vm => vm.TargetPrice,
                    TargetPercentage)
                .Subscribe(o => TargetPercent = o);

            this.WhenAnyValue(
                    vm => vm.EntryPrice,
                    vm => vm.TargetPrice,
                    TargetTick)
                .Subscribe(o => TargetTicks = o);

            this.WhenAnyValue(
                    vm => vm.EntryPrice,
                    vm => vm.TargetPrice,
                    vm => vm.Lots,
                    MainWindowViewModelExtensions.TargetAmount)
                .ToPropertyEx(this, vm => vm.TargetAmount);

            this.WhenAnyValue(
                    vm => vm.EntryPrice,
                    vm => vm.StopLossPrice,
                    vm => vm.TargetPrice,
                    MainWindowViewModelExtensions.RiskReward)
                .ToPropertyEx(this, vm => vm.RiskReward);

            this.WhenAnyValue(vm => vm.AccountRisk)
                .Where(ar => IsFocusedAccountRisk)
                .Select(ar => Risk(Capital, ar))
                .Subscribe(o => Risk = o);

            this.WhenAnyValue(vm => vm.Lots)
                .Where(l => IsFocusedLots)
                .Select(l => StopLossPrice(AccountRisk, EntryPrice, l))
                .Subscribe(o => StopLossPrice = o);


            this.WhenAnyValue(vm => vm.StopLossPercent)
                .Where(slp => IsFocusedStopLossPercent)
                .Select(slp => StopLossPrice(EntryPrice, slp))
                .Subscribe(o => StopLossPrice = o);

            this.WhenAnyValue(vm => vm.StopLossTicks)
                .Where(slt => IsFocusedStopLossTick)
                .Select(slt => StopLossPrice(EntryPrice, slt))
                .Subscribe(o => StopLossPrice = o);

            this.WhenAnyValue(vm => vm.TargetPercent)
                .Where(tp => IsFocusedTargetPercent)
                .Select(tp => TargetPrice(EntryPrice, tp))
                .Subscribe(o => TargetPrice = o);

            this.WhenAnyValue(vm => vm.TargetTicks)
                .Where(tt => IsFocusedTargetTick)
                .Select(tt => TargetPrice(EntryPrice, tt))
                .Subscribe(o => StopLossPrice = o);

            ObserveViewModelProperties();
        }

        private void ObserveViewModelProperties()
        {
            this.WhenAnyValue(vm => vm.Capital, IsGreaterThanZero)
                .BindTo(this, vm => vm.IsValidCapital);

            this.WhenAnyValue(vm => vm.Risk, IsGreaterThanZero)
                .BindTo(this, vm => vm.IsValidRisk);

            this.WhenAnyValue(vm => vm.AccountRisk, IsGreaterThanZero)
                .BindTo(this, vm => vm.IsValidAccountRisk);

            this.WhenAnyValue(vm => vm.EntryPrice, IsGreaterThanZero)
                .BindTo(this, vm => vm.IsValidEntryPrice);

            this.WhenAnyValue(vm => vm.Lots, IsGreaterThanZero)
                .BindTo(this, vm => vm.IsValidLots);

            this.WhenAnyValue(vm => vm.Shares, IsGreaterThanZero)
                .BindTo(this, vm => vm.IsValidShares);

            this.WhenAnyValue(vm => vm.EntryAmount, IsGreaterThanZero)
                .BindTo(this, vm => vm.IsValidEntryAmount);

            this.WhenAnyValue(vm => vm.StopLossPrice, IsGreaterThanZero)
                .BindTo(this, vm => vm.IsValidStopLossPrice);

            this.WhenAnyValue(vm => vm.StopLossPercent, IsGreaterThanZero)
                .BindTo(this, vm => vm.IsValidStopLossPercent);

            this.WhenAnyValue(vm => vm.StopLossTicks, IsGreaterThanZero)
                .BindTo(this, vm => vm.IsValidStopLossTicks);

            this.WhenAnyValue(vm => vm.StopLossAmount, IsGreaterThanZero)
                .BindTo(this, vm => vm.IsValidStopLossAmount);

            this.WhenAnyValue(vm => vm.TargetPrice, IsGreaterThanZero)
                .BindTo(this, vm => vm.IsValidTargetPrice);

            this.WhenAnyValue(vm => vm.TargetPercent, IsGreaterThanZero)
                .BindTo(this, vm => vm.IsValidTargetPercent);

            this.WhenAnyValue(vm => vm.TargetTicks, IsGreaterThanZero)
                .BindTo(this, vm => vm.IsValidTargetTicks);

            this.WhenAnyValue(vm => vm.TargetAmount, IsGreaterThanZero)
                .BindTo(this, vm => vm.IsValidTargetAmount);

            this.WhenAnyValue(vm => vm.RiskReward, IsGreaterThanZero)
                .BindTo(this, vm => vm.IsValidRiskReward);

            this.WhenAnyValue(vm => vm.EntryPrice, vm => vm.StopLossPrice,
                    MainWindowViewModelExtensions.IsValidStopLossPrice)
                .BindTo(this, vm => vm.IsStopLossPriceLessThanEntryPrice);

            this.WhenAnyValue(vm => vm.EntryPrice, vm => vm.TargetPrice,
                    MainWindowViewModelExtensions.IsValidTargetPrice)
                .BindTo(this, vm => vm.IsTargetPriceMoreThanEntryPrice);
        }

        [Reactive] public bool IsValidCapital { get; set; }
        [Reactive] public bool IsValidRisk { get; set; }
        [Reactive] public bool IsValidAccountRisk { get; set; }
        [Reactive] public bool IsValidEntryPrice { get; set; }
        [Reactive] public bool IsValidLots { get; set; }
        [Reactive] public bool IsValidShares { get; set; }
        [Reactive] public bool IsValidEntryAmount { get; set; }
        [Reactive] public bool IsValidStopLossPrice { get; set; }
        [Reactive] public bool IsValidStopLossPercent { get; set; }
        [Reactive] public bool IsValidStopLossTicks { get; set; }
        [Reactive] public bool IsValidStopLossAmount { get; set; }
        [Reactive] public bool IsValidTargetPrice { get; set; }
        [Reactive] public bool IsValidTargetPercent { get; set; }
        [Reactive] public bool IsValidTargetTicks { get; set; }
        [Reactive] public bool IsValidTargetAmount { get; set; }
        [Reactive] public bool IsValidRiskReward { get; set; }

        [Reactive] public bool IsStopLossPriceLessThanEntryPrice { get; set; }
        [Reactive] public bool IsTargetPriceMoreThanEntryPrice { get; set; }

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