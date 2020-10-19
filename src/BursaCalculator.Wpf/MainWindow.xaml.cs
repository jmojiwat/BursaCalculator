using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using BursaCalculator.Core.Infrastructure;
using BursaCalculator.ViewModel;
using BursaCalculator.Wpf.TypeConverters;
using ReactiveUI;
using static BursaCalculator.Persistence.SettingsExtensions;
using static BursaCalculator.ViewModel.MainWindowViewModelExtensions;
using static BursaCalculator.Wpf.MainWindowExtensions;

// ReSharper disable PossibleNullReferenceException

namespace BursaCalculator.Wpf
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();

            ViewModel = ToViewModel(Retrieve());

            this.WhenActivated(disposableRegistration =>
            {
                BindViewModelToView(disposableRegistration);

                BindEvents(disposableRegistration);

                ObserveViewInputControls(disposableRegistration);

                ObserveViewModelProperties(disposableRegistration);

                WhenAnyValid(disposableRegistration);
            });
        }

        protected override void OnClosed(EventArgs e)
        {
            Persist(ToSettings(ViewModel));
            base.OnClosed(e);
        }

        private void BindEvents(CompositeDisposable disposableRegistration)
        {
            this.Events().GotKeyboardFocus
                .Where(e => e.Source is TextBox
                            && (e.KeyboardDevice.IsKeyDown(Key.Tab)
                                || e.KeyboardDevice.IsKeyDown(Key.LeftAlt)
                                || e.KeyboardDevice.IsKeyDown(Key.RightAlt)))
                .Subscribe(e => ((TextBox) e.Source).SelectAll())
                .DisposeWith(disposableRegistration);

            EntryPriceTextBox.Events().PreviewKeyDown
                .Where(e => e.Key == Key.Up)
                .Subscribe(e => KeepTextBoxState((TextBox) e.Source,
                    () => IncrementPriceForProperty(ViewModel, vm => vm.EntryPrice)))
                .DisposeWith(disposableRegistration);

            EntryPriceTextBox.Events().PreviewKeyDown
                .Where(e => e.Key == Key.Down)
                .Subscribe(e => KeepTextBoxState((TextBox) e.Source,
                    () => DecrementPriceForProperty(ViewModel, vm => vm.EntryPrice)))
                .DisposeWith(disposableRegistration);

            StopLossPriceTextBox.Events().PreviewKeyDown
                .Where(e => e.Key == Key.Up)
                .Subscribe(e => KeepTextBoxState((TextBox) e.Source,
                    () => IncrementPriceForProperty(ViewModel, vm => vm.StopLossPrice)))
                .DisposeWith(disposableRegistration);

            StopLossPriceTextBox.Events().PreviewKeyDown
                .Where(e => e.Key == Key.Down)
                .Subscribe(e => KeepTextBoxState((TextBox) e.Source,
                    () => DecrementPriceForProperty(ViewModel, vm => vm.StopLossPrice)))
                .DisposeWith(disposableRegistration);

            TargetPriceTextBox.Events().PreviewKeyDown
                .Where(e => e.Key == Key.Up)
                .Subscribe(e => KeepTextBoxState((TextBox) e.Source,
                    () => IncrementPriceForProperty(ViewModel, vm => vm.TargetPrice)))
                .DisposeWith(disposableRegistration);

            TargetPriceTextBox.Events().PreviewKeyDown
                .Where(e => e.Key == Key.Down)
                .Subscribe(e => KeepTextBoxState((TextBox) e.Source,
                    () => DecrementPriceForProperty(ViewModel, vm => vm.TargetPrice)))
                .DisposeWith(disposableRegistration);

            StopLossTicksTextBox.Events().PreviewKeyDown
                .Where(e => e.Key == Key.Up)
                .Subscribe(e => KeepTextBoxState((TextBox) e.Source,
                    () => IncrementTickForProperty(ViewModel, vm => vm.StopLossTicks)))
                .DisposeWith(disposableRegistration);

            StopLossTicksTextBox.Events().PreviewKeyDown
                .Where(e => e.Key == Key.Down)
                .Subscribe(e => KeepTextBoxState((TextBox) e.Source,
                    () => DecrementTickForProperty(ViewModel, vm => vm.StopLossTicks)))
                .DisposeWith(disposableRegistration);

            TargetTicksTextBox.Events().PreviewKeyDown
                .Where(e => e.Key == Key.Up)
                .Subscribe(e => KeepTextBoxState((TextBox) e.Source,
                    () => IncrementTickForProperty(ViewModel, vm => vm.TargetTicks)))
                .DisposeWith(disposableRegistration);

            TargetTicksTextBox.Events().PreviewKeyDown
                .Where(e => e.Key == Key.Down)
                .Subscribe(e => KeepTextBoxState((TextBox) e.Source,
                    () => DecrementTickForProperty(ViewModel, vm => vm.TargetTicks)))
                .DisposeWith(disposableRegistration);
        }

        private void BindViewModelToView(CompositeDisposable disposableRegistration)
        {
            this.Bind(ViewModel,
                    vm => vm.Capital,
                    v => v.CapitalTextBox.Text,
                    "N0")
                .DisposeWith(disposableRegistration);

            this.Bind(ViewModel,
                    vm => vm.Risk,
                    v => v.RiskTextBox.Text)
                .DisposeWith(disposableRegistration);

            this.Bind(ViewModel,
                    vm => vm.AccountRisk,
                    v => v.AccountRiskTextBox.Text,
                    "N0")
                .DisposeWith(disposableRegistration);

            this.Bind(ViewModel,
                    vm => vm.IsFocusedAccountRisk,
                    v => v.AccountRiskTextBox.IsFocused)
                .DisposeWith(disposableRegistration);

            this.Bind(ViewModel,
                    vm => vm.Lots,
                    v => v.LotsTextBox.Text)
                .DisposeWith(disposableRegistration);

            this.Bind(ViewModel,
                    vm => vm.IsFocusedLots,
                    v => v.LotsTextBox.IsFocused)
                .DisposeWith(disposableRegistration);

            this.Bind(ViewModel,
                    vm => vm.EntryPrice,
                    v => v.EntryPriceTextBox.Text)
                .DisposeWith(disposableRegistration);

            this.OneWayBind(ViewModel,
                    vm => vm.Shares,
                    v => v.SharesTextBlock.Text)
                .DisposeWith(disposableRegistration);

            this.OneWayBind(ViewModel,
                    vm => vm.EntryAmount,
                    v => v.EntryAmountTextBlock.Text,
                    "N0")
                .DisposeWith(disposableRegistration);

            this.Bind(ViewModel,
                    vm => vm.StopLossPrice,
                    v => v.StopLossPriceTextBox.Text)
                .DisposeWith(disposableRegistration);

            this.Bind(ViewModel,
                    vm => vm.StopLossPercent,
                    v => v.StopLossPercentTextBox.Text)
                .DisposeWith(disposableRegistration);

            this.Bind(ViewModel,
                    vm => vm.IsFocusedStopLossPercent,
                    v => v.StopLossPercentTextBox.IsFocused)
                .DisposeWith(disposableRegistration);

            this.Bind(ViewModel,
                    vm => vm.StopLossTicks,
                    v => v.StopLossTicksTextBox.Text)
                .DisposeWith(disposableRegistration);

            this.Bind(ViewModel,
                    vm => vm.IsFocusedStopLossTick,
                    v => v.StopLossTicksTextBox.IsFocused)
                .DisposeWith(disposableRegistration);


            this.OneWayBind(ViewModel,
                    vm => vm.StopLossAmount,
                    v => v.StopLossAmountTextBlock.Text,
                    "N0")
                .DisposeWith(disposableRegistration);

            this.Bind(ViewModel,
                    vm => vm.TargetPrice,
                    v => v.TargetPriceTextBox.Text)
                .DisposeWith(disposableRegistration);

            this.Bind(ViewModel,
                    vm => vm.TargetPercent,
                    v => v.TargetPercentTextBox.Text)
                .DisposeWith(disposableRegistration);

            this.Bind(ViewModel,
                    vm => vm.IsFocusedTargetPercent,
                    v => v.TargetPercentTextBox.IsFocused)
                .DisposeWith(disposableRegistration);

            this.Bind(ViewModel,
                    vm => vm.TargetTicks,
                    v => v.TargetTicksTextBox.Text)
                .DisposeWith(disposableRegistration);

            this.Bind(ViewModel,
                    vm => vm.IsFocusedTargetTick,
                    v => v.TargetTicksTextBox.IsFocused)
                .DisposeWith(disposableRegistration);

            this.OneWayBind(ViewModel,
                    vm => vm.TargetAmount,
                    v => v.TargetAmountTextBlock.Text,
                    "N0")
                .DisposeWith(disposableRegistration);

            this.OneWayBind(ViewModel,
                    vm => vm.RiskReward,
                    v => v.RiskRewardTextBlock.Text, null, 
                    new RewardRiskToStringTypeConverter())
                .DisposeWith(disposableRegistration);
        }

        private Unit ObserveViewInputControls(CompositeDisposable disposableRegistration)
        {
            this.WhenAnyValue(view => view.CapitalTextBox.Text, IsValidDecimal)
                .BindTo(this, view => view.ViewModel.IsValidInputCapital)
                .DisposeWith(disposableRegistration);

            this.WhenAnyValue(view => view.RiskTextBox.Text, IsValidDecimal)
                .BindTo(this, view => view.ViewModel.IsValidInputRisk)
                .DisposeWith(disposableRegistration);

            this.WhenAnyValue(view => view.AccountRiskTextBox.Text, IsValidDecimal)
                .BindTo(this, view => view.ViewModel.IsValidInputAccountRisk)
                .DisposeWith(disposableRegistration);

            this.WhenAnyValue(view => view.EntryPriceTextBox.Text, IsValidDecimal)
                .BindTo(this, view => view.ViewModel.IsValidInputEntryPrice)
                .DisposeWith(disposableRegistration);

            this.WhenAnyValue(view => view.LotsTextBox.Text, IsValidDecimal)
                .BindTo(this, view => view.ViewModel.IsValidInputLots)
                .DisposeWith(disposableRegistration);

            this.WhenAnyValue(view => view.StopLossPriceTextBox.Text, IsValidDecimal)
                .BindTo(this, view => view.ViewModel.IsValidInputStopLossPrice)
                .DisposeWith(disposableRegistration);

            this.WhenAnyValue(view => view.StopLossPercentTextBox.Text, IsValidDecimal)
                .BindTo(this, view => view.ViewModel.IsValidInputStopLossPercent)
                .DisposeWith(disposableRegistration);

            this.WhenAnyValue(view => view.StopLossTicksTextBox.Text, IsValidDecimal)
                .BindTo(this, view => view.ViewModel.IsValidInputStopLossTicks)
                .DisposeWith(disposableRegistration);

            this.WhenAnyValue(view => view.TargetPriceTextBox.Text, IsValidDecimal)
                .BindTo(this, view => view.ViewModel.IsValidInputTargetPrice)
                .DisposeWith(disposableRegistration);

            this.WhenAnyValue(view => view.TargetPercentTextBox.Text, IsValidDecimal)
                .BindTo(this, view => view.ViewModel.IsValidInputTargetPercent)
                .DisposeWith(disposableRegistration);

            this.WhenAnyValue(view => view.TargetTicksTextBox.Text, IsValidDecimal)
                .BindTo(this, view => view.ViewModel.IsValidInputTargetTicks)
                .DisposeWith(disposableRegistration);

            return Unit.Default;
        }

        private Unit ObserveViewModelProperties(CompositeDisposable disposableRegistration)
        {
            this.WhenAnyValue(view => view.ViewModel.Capital, IsGreaterThanZero)
                .BindTo(this, view => view.ViewModel.IsValidCapital)
                .DisposeWith(disposableRegistration);

            this.WhenAnyValue(view => view.ViewModel.Risk, IsGreaterThanZero)
                .BindTo(this, view => view.ViewModel.IsValidRisk)
                .DisposeWith(disposableRegistration);

            this.WhenAnyValue(view => view.ViewModel.AccountRisk, IsGreaterThanZero)
                .BindTo(this, view => view.ViewModel.IsValidAccountRisk)
                .DisposeWith(disposableRegistration);


            this.WhenAnyValue(view => view.ViewModel.EntryPrice, IsGreaterThanZero)
                .BindTo(this, view => view.ViewModel.IsValidEntryPrice)
                .DisposeWith(disposableRegistration);

            this.WhenAnyValue(view => view.ViewModel.Lots, IsGreaterThanZero)
                .BindTo(this, view => view.ViewModel.IsValidLots)
                .DisposeWith(disposableRegistration);

            this.WhenAnyValue(view => view.ViewModel.StopLossPrice, IsGreaterThanZero)
                .BindTo(this, view => view.ViewModel.IsValidStopLossPrice)
                .DisposeWith(disposableRegistration);

            this.WhenAnyValue(view => view.ViewModel.StopLossPercent, IsGreaterThanZero)
                .BindTo(this, view => view.ViewModel.IsValidStopLossPercent)
                .DisposeWith(disposableRegistration);

            this.WhenAnyValue(view => view.ViewModel.StopLossTicks, IsGreaterThanZero)
                .BindTo(this, view => view.ViewModel.IsValidStopLossTicks)
                .DisposeWith(disposableRegistration);

            this.WhenAnyValue(view => view.ViewModel.TargetPrice, IsGreaterThanZero)
                .BindTo(this, view => view.ViewModel.IsValidTargetPrice)
                .DisposeWith(disposableRegistration);

            this.WhenAnyValue(view => view.ViewModel.TargetPercent, IsGreaterThanZero)
                .BindTo(this, view => view.ViewModel.IsValidTargetPercent)
                .DisposeWith(disposableRegistration);

            this.WhenAnyValue(view => view.ViewModel.TargetTicks, IsGreaterThanZero)
                .BindTo(this, view => view.ViewModel.IsValidTargetTicks)
                .DisposeWith(disposableRegistration);

            return Unit.Default;
        }

        private Unit WhenAnyValid(CompositeDisposable disposableRegistration)
        {
            this.WhenAnyValue(view => view.ViewModel.IsValidInputCapital, view => view.ViewModel.IsValidCapital,
                    ChooseBrushColor)
                .BindTo(this, view => view.CapitalTextBox.Foreground)
                .DisposeWith(disposableRegistration);

            this.WhenAnyValue(view => view.ViewModel.IsValidInputRisk, view => view.ViewModel.IsValidRisk,
                    ChooseBrushColor)
                .BindTo(this, view => view.RiskTextBox.Foreground)
                .DisposeWith(disposableRegistration);

            this.WhenAnyValue(view => view.ViewModel.IsValidInputAccountRisk, view => view.ViewModel.IsValidAccountRisk,
                    ChooseBrushColor)
                .BindTo(this, view => view.AccountRiskTextBox.Foreground)
                .DisposeWith(disposableRegistration);

            this.WhenAnyValue(view => view.ViewModel.IsValidInputEntryPrice, view => view.ViewModel.IsValidEntryPrice,
                    ChooseBrushColor)
                .BindTo(this, view => view.EntryPriceTextBox.Foreground)
                .DisposeWith(disposableRegistration);

            this.WhenAnyValue(view => view.ViewModel.IsValidInputLots, view => view.ViewModel.IsValidLots,
                    ChooseBrushColor)
                .BindTo(this, view => view.LotsTextBox.Foreground)
                .DisposeWith(disposableRegistration);

            this.WhenAnyValue<MainWindow, SolidColorBrush, Share>(view => view.ViewModel.Shares,
                    share => ChooseBrushColor(IsGreaterThanZero(share)))
                .BindTo(this, view => view.SharesTextBlock.Foreground)
                .DisposeWith(disposableRegistration);

            this.WhenAnyValue<MainWindow, SolidColorBrush, Money>(view => view.ViewModel.EntryAmount,
                    amount => ChooseBrushColor(IsGreaterThanZero(amount)))
                .BindTo(this, view => view.EntryAmountTextBlock.Foreground)
                .DisposeWith(disposableRegistration);

            this.WhenAnyValue(view => view.ViewModel.IsValidInputStopLossPrice,
                    view => view.ViewModel.IsValidStopLossPrice, ChooseBrushColor)
                .BindTo(this, view => view.StopLossPriceTextBox.Foreground)
                .DisposeWith(disposableRegistration);

            this.WhenAnyValue(view => view.ViewModel.IsValidInputStopLossPercent,
                    view => view.ViewModel.IsValidStopLossPercent, ChooseBrushColor)
                .BindTo(this, view => view.StopLossPercentTextBox.Foreground)
                .DisposeWith(disposableRegistration);

            this.WhenAnyValue(view => view.ViewModel.IsValidInputStopLossTicks,
                    view => view.ViewModel.IsValidStopLossTicks, ChooseBrushColor)
                .BindTo(this, view => view.StopLossTicksTextBox.Foreground)
                .DisposeWith(disposableRegistration);

            this.WhenAnyValue<MainWindow, SolidColorBrush, Money>(view => view.ViewModel.StopLossAmount,
                    amount => ChooseBrushColor(IsGreaterThanZero(amount)))
                .BindTo(this, view => view.StopLossAmountTextBlock.Foreground)
                .DisposeWith(disposableRegistration);

            this.WhenAnyValue(view => view.ViewModel.IsValidInputTargetPrice, view => view.ViewModel.IsValidTargetPrice,
                    ChooseBrushColor)
                .BindTo(this, view => view.TargetPriceTextBox.Foreground)
                .DisposeWith(disposableRegistration);

            this.WhenAnyValue(view => view.ViewModel.IsValidInputTargetPercent,
                    view => view.ViewModel.IsValidTargetPercent, ChooseBrushColor)
                .BindTo(this, view => view.TargetPercentTextBox.Foreground)
                .DisposeWith(disposableRegistration);

            this.WhenAnyValue(view => view.ViewModel.IsValidInputTargetTicks, view => view.ViewModel.IsValidTargetTicks,
                    ChooseBrushColor)
                .BindTo(this, view => view.TargetTicksTextBox.Foreground)
                .DisposeWith(disposableRegistration);

            this.WhenAnyValue<MainWindow, SolidColorBrush, Money>(view => view.ViewModel.TargetAmount,
                    amount => ChooseBrushColor(IsGreaterThanZero(amount)))
                .BindTo(this, view => view.TargetAmountTextBlock.Foreground)
                .DisposeWith(disposableRegistration);

            this.WhenAnyValue<MainWindow, SolidColorBrush, decimal>(view => view.ViewModel.RiskReward,
                    amount => ChooseBrushColor(IsGreaterThanZero(amount)))
                .BindTo(this, view => view.RiskRewardTextBlock.Foreground)
                .DisposeWith(disposableRegistration);

            return Unit.Default;
        }
    }
}