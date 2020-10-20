using System;
using System.Globalization;
using BursaCalculator.Core.Infrastructure;
using LanguageExt;
using ReactiveUI;

namespace BursaCalculator.Wpf.TypeConverters
{
    public class PercentToStringTypeConverter : IBindingTypeConverter
    {
        public int GetAffinityForObjects(Type fromType, Type toType) =>
            fromType == typeof(Option<Percent>) && toType == typeof(string) 
                ? 10 
                : 0;

        public bool TryConvert(object? @from, Type toType, object? conversionHint, out object? result)
        {
            result = from != null
                ? ((Option<Percent>) from).Map(ToString).IfNone(() => string.Empty)
                : string.Empty;

            return from != null && ((Option<Percent>) from).IsSome;        }

        private static string ToString(Percent percent)
        {
            var p = (decimal) percent;
            var fractional = p - Math.Truncate(p);
            return fractional == decimal.Zero
                ? p.ToString("N0", CultureInfo.CurrentCulture)
                : p.ToString("N2", CultureInfo.CurrentCulture);
        }
    }
}