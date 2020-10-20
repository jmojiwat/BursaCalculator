using System;
using System.Globalization;
using BursaCalculator.Core.Infrastructure;
using LanguageExt;
using ReactiveUI;

namespace BursaCalculator.Wpf.TypeConverters
{
    public class MoneyToStringTypeConverter : IBindingTypeConverter
    {
        public int GetAffinityForObjects(Type fromType, Type toType) =>
            fromType == typeof(Option<Money>) && toType == typeof(string) 
                ? 10 
                : 0;

        public bool TryConvert(object? from, Type toType, object? conversionHint, out object? result)
        {
            result = from != null
                ? ((Option<Money>) from).Map(m => ToString(conversionHint, m)).IfNone(() => string.Empty)
                : string.Empty;

            return from != null && ((Option<Money>) from).IsSome;
        }

        private static string ToString(object conversionHint, Money amount)
        {
            var format = conversionHint == null
                ? "N3"
                : (string) conversionHint;

            var m = (decimal) amount;
            var fractional = m - Math.Truncate(m);
            return fractional == decimal.Zero
                ? m.ToString("N0", CultureInfo.CurrentCulture)
                : m.ToString(format, CultureInfo.CurrentCulture);
        }
    }
}