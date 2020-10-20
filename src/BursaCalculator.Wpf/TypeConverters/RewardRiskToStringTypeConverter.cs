using System;
using System.Globalization;
using LanguageExt;
using ReactiveUI;

namespace BursaCalculator.Wpf.TypeConverters
{
    public class RewardRiskToStringTypeConverter : IBindingTypeConverter
    {
        public int GetAffinityForObjects(Type fromType, Type toType) =>
            fromType == typeof(Option<decimal>) && toType == typeof(string) 
                ? 10 
                : 0;

        public bool TryConvert(object? @from, Type toType, object? conversionHint, out object? result)
        {
            result = from != null
                ? ((Option<decimal>) from).Map(ToString).IfNone(() => string.Empty)
                : string.Empty;

            return from != null && ((Option<decimal>) from).IsSome;
        }

        private static string ToString(decimal d)
        {
            var fractional = d - Math.Truncate(d);
            return fractional == decimal.Zero
                ? d.ToString("N0", CultureInfo.CurrentCulture)
                : d.ToString("N2", CultureInfo.CurrentCulture);
        }

    }
}