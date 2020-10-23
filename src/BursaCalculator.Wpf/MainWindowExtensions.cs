using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Media;
using BursaCalculator.Core.Infrastructure;
using BursaCalculator.ViewModel;
using LanguageExt;
using static BursaCalculator.ViewModel.MainWindowViewModelExtensions;
using static LanguageExt.Prelude;
// ReSharper disable PossibleNullReferenceException

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

        public static void DecrementPriceForProperty(MainWindowViewModel viewModel,
            Expression<Func<MainWindowViewModel, Option<Money>>> property)
        {
            if (property.Body is MemberExpression memberSelectorExpression)
            {
                var pi = memberSelectorExpression.Member as PropertyInfo;
                var amount = (Option<Money>) pi.GetValue(viewModel);

                pi.SetValue(viewModel, amount.Map(DecrementPrice));
            }
        }

        public static void DecrementTickForProperty(MainWindowViewModel viewModel,
            Expression<Func<MainWindowViewModel, Option<Tick>>> property)
        {
            if (property.Body is MemberExpression memberSelectorExpression)
            {
                var pi = memberSelectorExpression.Member as PropertyInfo;
                var ticks = (Option<Tick>) pi.GetValue(viewModel);

                pi.SetValue(viewModel, map(ticks, DecrementTick));
            }
        }

        public static void IncrementPriceForProperty(MainWindowViewModel viewModel,
            Expression<Func<MainWindowViewModel, Option<Money>>> property)
        {
            if (property.Body is MemberExpression memberSelectorExpression)
            {
                var pi = memberSelectorExpression.Member as PropertyInfo;
                var amount = (Option<Money>) pi.GetValue(viewModel);

                pi.SetValue(viewModel, amount.Map(IncrementPrice));
            }
        }

        public static void IncrementTickForProperty(MainWindowViewModel viewModel,
            Expression<Func<MainWindowViewModel, Option<Tick>>> property)
        {
            if (property.Body is MemberExpression memberSelectorExpression)
            {
                var pi = memberSelectorExpression.Member as PropertyInfo;
                var ticks = (Option<Tick>) pi.GetValue(viewModel);

                pi.SetValue(viewModel, ticks.Map(IncrementTick));
            }
        }

        /*
        public static bool IsGreaterThanZero(Option<Money> amount) =>
            amount.Map(m => m > Money(0)).IfNone(() => false);

        public static bool IsGreaterThanZero(Option<Percent> amount) =>
            amount.Map(m => m > Percent(0)).IfNone(() => false);

        public static bool IsGreaterThanZero(Option<Tick> amount) =>
            amount.Map(m => m > Tick(0)).IfNone(() => false);

        public static bool IsGreaterThanZero(Option<Share> amount) =>
            amount.Map(m => m > Share(0)).IfNone(() => false);

        public static bool IsGreaterThanZero(Option<Lot> amount) =>
            amount.Map(m => m > Lot(0)).IfNone(() => false);

        public static bool IsGreaterThanZero(Option<decimal> amount) =>
            amount.Map(m => m > 0).IfNone(() => false);

        public static bool IsValidDecimal(string s) => decimal.TryParse(s, out _);
        */

        public static (int caretIndex, int textLength) KeepTextBoxState(TextBox textBox, Action action)
        {
            var state = RememberTextBoxState(textBox);
            action();
            return RestoreTextBoxState(state, textBox);
        }


        private static (int caretIndex, int textLength) RememberTextBoxState(TextBox textBox) =>
            (textBox.CaretIndex, textBox.Text.Length);

        private static (int caretIndex, int textLength) RestoreTextBoxState((int caretIndex, int textLength) state, TextBox textBox)
        {
            if (state.caretIndex == state.textLength - 1)
            {
                textBox.CaretIndex = textBox.Text.Length - 1;
            }
            else
            {
                textBox.CaretIndex = state.caretIndex;
            }

            return (textBox.CaretIndex, textBox.Text.Length);
        }
    }
}