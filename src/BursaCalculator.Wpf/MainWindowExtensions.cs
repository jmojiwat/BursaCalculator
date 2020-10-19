using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Media;
using BursaCalculator.Core.Infrastructure;
using BursaCalculator.ViewModel;
using static BursaCalculator.ViewModel.MainWindowViewModelExtensions;
using static LanguageExt.Prelude;
using Unit = System.Reactive.Unit;

namespace BursaCalculator.Wpf
{
    public static class MainWindowExtensions
    {
        private static readonly SolidColorBrush ValidForegroundBrush = Brushes.Black;
        private static readonly SolidColorBrush InvalidForegroundBrush = Brushes.Red;

        public static SolidColorBrush ChooseBrushColor(bool value) =>
            value
                ? ValidForegroundBrush
                : InvalidForegroundBrush;

        public static SolidColorBrush ChooseBrushColor(bool left, bool right) =>
            left && right
                ? ValidForegroundBrush
                : InvalidForegroundBrush;

        public static void DecrementInt(TextBox textBox) =>
            parseInt(textBox.Text)
                .Map(i => i - 1)
                .IfSome(s => textBox.Text = s.ToString());

        public static void DecrementPriceForProperty(MainWindowViewModel viewModel,
            Expression<Func<MainWindowViewModel, Money>> property)
        {
            if (property.Body is MemberExpression memberSelectorExpression)
            {
                var pi = memberSelectorExpression.Member as PropertyInfo;
                var amount = (Money) pi.GetValue(viewModel);

                pi.SetValue(viewModel, DecrementPrice(amount));
            }
        }

        public static void DecrementTickForProperty(MainWindowViewModel viewModel,
            Expression<Func<MainWindowViewModel, Tick>> property)
        {
            if (property.Body is MemberExpression memberSelectorExpression)
            {
                var pi = memberSelectorExpression.Member as PropertyInfo;
                var ticks = (Tick) pi.GetValue(viewModel);

                pi.SetValue(viewModel, DecrementTick(ticks));
            }
        }

        public static void IncrementInt(TextBox textBox) =>
            parseInt(textBox.Text)
                .Map(i => i + 1)
                .IfSome(s => textBox.Text = s.ToString());

        public static void IncrementPriceForProperty(MainWindowViewModel viewModel,
            Expression<Func<MainWindowViewModel, Money>> property)
        {
            if (property.Body is MemberExpression memberSelectorExpression)
            {
                var pi = memberSelectorExpression.Member as PropertyInfo;
                var amount = (Money) pi.GetValue(viewModel);
                var newAmount = IncrementPrice(amount);
                object newAmountBoxed = newAmount;
                object boxed = viewModel;
                pi.SetValue(boxed, newAmountBoxed, null);
            }
        }

        public static void IncrementTickForProperty(MainWindowViewModel viewModel,
            Expression<Func<MainWindowViewModel, Tick>> property)
        {
            if (property.Body is MemberExpression memberSelectorExpression)
            {
                var pi = memberSelectorExpression.Member as PropertyInfo;
                var ticks = (Tick) pi.GetValue(viewModel);

                pi.SetValue(viewModel, IncrementTick(ticks));
            }
        }

        public static bool IsGreaterThanZero(Money amount) => amount > MoneyExtensions.Money(0);
        public static bool IsGreaterThanZero(Percent amount) => amount > PercentExtensions.Percent(0);
        public static bool IsGreaterThanZero(Tick amount) => amount > TickExtensions.Tick(0);
        public static bool IsGreaterThanZero(Share amount) => amount > ShareExtensions.Share(0);
        public static bool IsGreaterThanZero(Lot amount) => amount > LotExtensions.Lot(0);
        public static bool IsGreaterThanZero(decimal amount) => amount > 0;

        public static bool IsValidDecimal(string s)
        {
            return decimal.TryParse(s, out _);
        }

        public static void KeepTextBoxState(TextBox textBox, Action action)
        {
            var state = RememberTextBoxState(textBox);
            action();
            RestoreTextBoxState(state, textBox);
        }


        private static (int caretIndex, int textLength) RememberTextBoxState(TextBox textBox) =>
            (textBox.CaretIndex, textBox.Text.Length);

        private static Unit RestoreTextBoxState((int caretIndex, int textLength) state, TextBox textBox)
        {
            if (state.caretIndex == state.textLength - 1)
            {
                textBox.CaretIndex = textBox.Text.Length - 1;
            }
            else
            {
                textBox.CaretIndex = state.caretIndex;
            }

            return Unit.Default;
        }
    }
}