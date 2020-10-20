using System;
using System.Globalization;
using BursaCalculator.Core.Infrastructure;
using LanguageExt;
using ReactiveUI;

namespace BursaCalculator.Wpf.TypeConverters
{
    public class ShareToStringTypeConverter : IBindingTypeConverter
    {
        public int GetAffinityForObjects(Type fromType, Type toType) =>
            fromType == typeof(Option<Share>) && toType == typeof(string) 
                ? 10 
                : 0;

        public bool TryConvert(object? @from, Type toType, object? conversionHint, out object? result)
        {
            result = from != null
                ? ((Option<Share>) from).Map(ToString).IfNone(() => string.Empty)
                : string.Empty;

            return from != null && ((Option<Share>) from).IsSome;
        }

        private static string ToString(Share s) => ((int) s).ToString("N0", CultureInfo.CurrentCulture);

    }
}