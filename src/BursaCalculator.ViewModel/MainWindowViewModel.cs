using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using BursaCalculator.Core.Infrastructure;
using LanguageExt;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using static BursaCalculator.ViewModel.MainWindowViewModelExtensions;

namespace BursaCalculator.ViewModel
{
    public sealed class MainWindowViewModel : ReactiveObject, IDisposable
    {
        private readonly CompositeDisposable disposables = new();
//        private Fee fee;

        public MainWindowViewModel()
        {
            /*
            fee = ToFee(RetrieveFeeSettings());
            PersistFeeSettings(ToSettings(fee));
            */

            this.WhenAnyValue(
                    vm => vm.Capital,
                    vm => vm.Risk,
                    CalculateAccountRisk)
                .Subscribe(o => AccountRisk = o)
                .DisposeWith(disposables);

            this.WhenAnyValue(
                    vm => vm.EntryPrice,
                    vm => vm.StopLossPrice,
                    vm => vm.AccountRisk,
                    MainWindowViewModelExtensions.Lots)
                .Subscribe(o => Lots = o)
                .DisposeWith(disposables);

            this.WhenAnyValue(
                    vm => vm.EntryPrice,
                    vm => vm.StopLossPrice,
                    vm => vm.AccountRisk,
                    MainWindowViewModelExtensions.Shares)
                .ToPropertyEx(this, vm => vm.Shares)
                .DisposeWith(disposables);

            this.WhenAnyValue(
                    vm => vm.EntryPrice,
                    vm => vm.Lots,
                    MainWindowViewModelExtensions.EntryAmount)
                .ToPropertyEx(this, vm => vm.EntryAmount)
                .DisposeWith(disposables);

            this.WhenAnyValue(
                    vm => vm.EntryPrice,
                    vm => vm.StopLossPrice,
                    StopLossPercentage)
                .Subscribe(o => StopLossPercent = o)
                .DisposeWith(disposables);

            this.WhenAnyValue(
                    vm => vm.EntryPrice,
                    vm => vm.StopLossPrice,
                    StopLossTick)
                .Subscribe(o => StopLossTicks = o)
                .DisposeWith(disposables);

            this.WhenAnyValue(
                    vm => vm.EntryPrice,
                    vm => vm.StopLossPrice,
                    vm => vm.Lots,
                    MainWindowViewModelExtensions.StopLossAmount)
                .ToPropertyEx(this, vm => vm.StopLossAmount)
                .DisposeWith(disposables);

            this.WhenAnyValue(
                    vm => vm.EntryPrice,
                    vm => vm.TargetPrice,
                    TargetPercentage)
                .Subscribe(o => TargetPercent = o)
                .DisposeWith(disposables);

            this.WhenAnyValue(
                    vm => vm.EntryPrice,
                    vm => vm.TargetPrice,
                    TargetTick)
                .Subscribe(o => TargetTicks = o)
                .DisposeWith(disposables);

            this.WhenAnyValue(
                    vm => vm.EntryPrice,
                    vm => vm.TargetPrice,
                    vm => vm.Lots,
                    MainWindowViewModelExtensions.TargetAmount)
                .ToPropertyEx(this, vm => vm.TargetAmount)
                .DisposeWith(disposables);

            this.WhenAnyValue(
                    vm => vm.EntryPrice,
                    vm => vm.StopLossPrice,
                    vm => vm.TargetPrice,
                    MainWindowViewModelExtensions.RiskReward)
                .ToPropertyEx(this, vm => vm.RiskReward)
                .DisposeWith(disposables);

            this.WhenAnyValue(vm => vm.AccountRisk)
                .Where(ar => IsFocusedAccountRisk)
                .Select(ar => Risk(Capital, ar))
                .Subscribe(o => Risk = o)
                .DisposeWith(disposables);

            this.WhenAnyValue(vm => vm.Lots)
                .Where(l => IsFocusedLots)
                .Select(l => StopLossPrice(AccountRisk, EntryPrice, l.Map(x => x.Lots())))
                .Subscribe(o => StopLossPrice = o)
                .DisposeWith(disposables);


            this.WhenAnyValue(vm => vm.StopLossPercent)
                .Where(slp => IsFocusedStopLossPercent)
                .Select(slp => StopLossPrice(EntryPrice, slp))
                .Subscribe(o => StopLossPrice = o)
                .DisposeWith(disposables);

            this.WhenAnyValue(vm => vm.StopLossTicks)
                .Where(slt => IsFocusedStopLossTick)
                .Select(slt => StopLossPrice(EntryPrice, slt))
                .Subscribe(o => StopLossPrice = o)
                .DisposeWith(disposables);

            this.WhenAnyValue(vm => vm.TargetPercent)
                .Where(tp => IsFocusedTargetPercent)
                .Select(tp => TargetPrice(EntryPrice, tp))
                .Subscribe(o => TargetPrice = o)
                .DisposeWith(disposables);

            this.WhenAnyValue(vm => vm.TargetTicks)
                .Where(tt => IsFocusedTargetTick)
                .Select(tt => TargetPrice(EntryPrice, tt))
                .Subscribe(o => StopLossPrice = o)
                .DisposeWith(disposables);

            ObserveViewModelProperties();
        }

        [ObservableAsProperty] public bool IsValidCapital { get; }
        [ObservableAsProperty] public bool IsValidRisk { get; }
        [ObservableAsProperty] public bool IsValidAccountRisk { get; }
        [ObservableAsProperty] public bool IsValidEntryPriceStopLossPrice { get; }
        [ObservableAsProperty] public bool IsValidLots { get; }
        [ObservableAsProperty] public bool IsValidShares { get; }
        [ObservableAsProperty] public bool IsValidEntryAmount { get; }
        [ObservableAsProperty] public bool IsValidStopLossPercent { get; }
        [ObservableAsProperty] public bool IsValidStopLossTicks { get; }
        [ObservableAsProperty] public bool IsValidStopLossAmount { get; }
        [ObservableAsProperty] public bool IsValidTargetPrice { get; }
        [ObservableAsProperty] public bool IsValidTargetPercent { get; }
        [ObservableAsProperty] public bool IsValidTargetTicks { get; }
        [ObservableAsProperty] public bool IsValidTargetAmount { get; }
        [ObservableAsProperty] public bool IsValidRiskReward { get; }

        [Reactive] public Option<Money> Capital { get; set; }
        [Reactive] public Option<Percent> Risk { get; set; }
        [Reactive] public Option<Money> AccountRisk { get; set; }

        [Reactive] public Option<Money> EntryPrice { get; set; }
        [Reactive] public Option<int> Lots { get; set; }
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

        [ObservableAsProperty] public Option<int> Shares { get; }
        [ObservableAsProperty] public Option<Money> EntryAmount { get; }
        [ObservableAsProperty] public Option<Money> StopLossAmount { get; }
        [ObservableAsProperty] public Option<decimal> RiskReward { get; }
        [ObservableAsProperty] public Option<Money> TargetAmount { get; }


        public void Dispose()
        {
            disposables?.Dispose();
        }

        private void ObserveViewModelProperties()
        {
            this.WhenAnyValue(vm => vm.Capital, IsGreaterThanZero)
                .ToPropertyEx(this, vm => vm.IsValidCapital)
                .DisposeWith(disposables);


            this.WhenAnyValue(vm => vm.Risk, IsGreaterThanZero)
                .ToPropertyEx(this, vm => vm.IsValidRisk)
                .DisposeWith(disposables);

            this.WhenAnyValue(vm => vm.AccountRisk, IsGreaterThanZero)
                .ToPropertyEx(this, vm => vm.IsValidAccountRisk)
                .DisposeWith(disposables);

            this.WhenAnyValue(
                    vm => vm.EntryPrice,
                    vm => vm.StopLossPrice,
                    MainWindowViewModelExtensions.IsValidEntryPriceStopLossPrice)
                .ToPropertyEx(this, vm => vm.IsValidEntryPriceStopLossPrice)
                .DisposeWith(disposables);

            this.WhenAnyValue(
                    vm => vm.EntryPrice,
                    vm => vm.TargetPrice,
                    MainWindowViewModelExtensions.IsValidTargetPrice)
                .ToPropertyEx(this, vm => vm.IsValidTargetPrice)
                .DisposeWith(disposables);

            this.WhenAnyValue(vm => vm.Lots, IsGreaterThanZero)
                .ToPropertyEx(this, vm => vm.IsValidLots)
                .DisposeWith(disposables);

            this.WhenAnyValue(vm => vm.Shares, IsGreaterThanZero)
                .ToPropertyEx(this, vm => vm.IsValidShares)
                .DisposeWith(disposables);

            this.WhenAnyValue(vm => vm.EntryAmount, IsGreaterThanZero)
                .ToPropertyEx(this, vm => vm.IsValidEntryAmount)
                .DisposeWith(disposables);

            this.WhenAnyValue(vm => vm.StopLossPercent, IsGreaterThanZero)
                .ToPropertyEx(this, vm => vm.IsValidStopLossPercent)
                .DisposeWith(disposables);

            this.WhenAnyValue(vm => vm.StopLossTicks, IsGreaterThanZero)
                .ToPropertyEx(this, vm => vm.IsValidStopLossTicks)
                .DisposeWith(disposables);

            this.WhenAnyValue(vm => vm.StopLossAmount, IsGreaterThanZero)
                .ToPropertyEx(this, vm => vm.IsValidStopLossAmount)
                .DisposeWith(disposables);

            this.WhenAnyValue(vm => vm.TargetPercent, IsGreaterThanZero)
                .ToPropertyEx(this, vm => vm.IsValidTargetPercent)
                .DisposeWith(disposables);

            this.WhenAnyValue(vm => vm.TargetTicks, IsGreaterThanZero)
                .ToPropertyEx(this, vm => vm.IsValidTargetTicks)
                .DisposeWith(disposables);

            this.WhenAnyValue(vm => vm.TargetAmount, IsGreaterThanZero)
                .ToPropertyEx(this, vm => vm.IsValidTargetAmount)
                .DisposeWith(disposables);

            this.WhenAnyValue(vm => vm.RiskReward, IsGreaterThanZero)
                .ToPropertyEx(this, vm => vm.IsValidRiskReward)
                .DisposeWith(disposables);
        }
    }
}