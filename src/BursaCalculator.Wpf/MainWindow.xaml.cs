using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
    // ReSharper disable once RedundantExtendsListEntry
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

//                WhenAnyValid(disposableRegistration);
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

            this.OneWayBind(ViewModel,
                    vm => vm.IsValidCapital,
                    v => v.CapitalTextBox.Foreground, 
                    null,
                    new BoolToColourTypeConverter())
                .DisposeWith(disposableRegistration);

            this.OneWayBind(ViewModel,
                    vm => vm.IsValidRisk,
                    v => v.RiskTextBox.Foreground, 
                    null,
                    new BoolToColourTypeConverter())
                .DisposeWith(disposableRegistration);

            this.OneWayBind(ViewModel,
                    vm => vm.IsValidAccountRisk,
                    v => v.AccountRiskTextBox.Foreground,
                    null,
                    new BoolToColourTypeConverter())
                .DisposeWith(disposableRegistration);

            this.OneWayBind(ViewModel,
                    vm => vm.IsValidEntryPriceStopLossPrice,
                    v => v.EntryPriceTextBox.Foreground,
                    null,
                    new BoolToColourTypeConverter())
                .DisposeWith(disposableRegistration);

            this.OneWayBind(ViewModel,
                    vm => vm.IsValidLots,
                    v => v.LotsTextBox.Foreground,
                    null,
                    new BoolToColourTypeConverter())
                .DisposeWith(disposableRegistration);

            this.OneWayBind(ViewModel,
                    vm => vm.IsValidShares, 
                    v => v.SharesTextBlock.Foreground,
                    null,
                        new BoolToColourTypeConverter())
                .DisposeWith(disposableRegistration);

            this.OneWayBind(ViewModel,
                    vm => vm.IsValidEntryAmount, 
                    v => v.EntryAmountTextBlock.Foreground,
                    null,
                    new BoolToColourTypeConverter())
                .DisposeWith(disposableRegistration);

            // todo: figure out why this isn't working
            this.OneWayBind(ViewModel,
                    vm => vm.IsValidEntryPriceStopLossPrice,
                    v => v.StopLossPriceTextBox.Foreground,
                    null,
                    new BoolToColourTypeConverter())
                .DisposeWith(disposableRegistration);

            this.OneWayBind(ViewModel,
                    vm => vm.IsValidStopLossPercent, 
                    v => v.StopLossPercentTextBox.Foreground,
                    null,
                    new BoolToColourTypeConverter())
                .DisposeWith(disposableRegistration);

            this.OneWayBind(ViewModel,
                    vm => vm.IsValidStopLossTicks, 
                    v => v.StopLossTicksTextBox.Foreground,
                    null,
                    new BoolToColourTypeConverter())
                .DisposeWith(disposableRegistration);

            this.OneWayBind(ViewModel,
                    vm => vm.IsValidStopLossAmount, 
                    v => v.StopLossAmountTextBlock.Foreground,
                    null,
                    new BoolToColourTypeConverter())
                .DisposeWith(disposableRegistration);

            // todo: figure out why this isn't working
            this.OneWayBind(ViewModel,
                    vm => vm.IsValidTargetPrice, 
                    v => v.TargetPriceTextBox.Foreground,
                    null,
                    new BoolToColourTypeConverter())
                .DisposeWith(disposableRegistration);

            this.OneWayBind(ViewModel,
                    vm => vm.IsValidTargetPercent, 
                    view => view.TargetPercentTextBox.Foreground,
                    null,
                    new BoolToColourTypeConverter())
                .DisposeWith(disposableRegistration);

            this.OneWayBind(ViewModel,
                    vm => vm.IsValidTargetTicks, 
                    v => v.TargetTicksTextBox.Foreground,
                    null,
                    new BoolToColourTypeConverter())
                .DisposeWith(disposableRegistration);

            this.OneWayBind(ViewModel,
                    vm => vm.IsValidTargetAmount, 
                    v => v.TargetAmountTextBlock.Foreground,
                    null,
                    new BoolToColourTypeConverter())
                .DisposeWith(disposableRegistration);

            this.OneWayBind(ViewModel,
                    vm => vm.IsValidRiskReward, 
                    v => v.RiskRewardTextBlock.Foreground,
                    null,
                    new BoolToColourTypeConverter())
                .DisposeWith(disposableRegistration);

        }
    }
}