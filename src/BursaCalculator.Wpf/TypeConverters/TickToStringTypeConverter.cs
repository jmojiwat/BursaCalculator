using System;
using System.Globalization;
using BursaCalculator.Core.Infrastructure;
using LanguageExt;
using ReactiveUI;

namespace BursaCalculator.Wpf.TypeConverters
{
    public class TickToStringTypeConverter : IBindingTypeConverter
    {
        public int GetAffinityForObjects(Type fromType, Type toType) =>
            fromType == typeof(Option<Tick>) && toType == typeof(string) 
                ? 10 
                : 0;

        public bool TryConvert(object? @from, Type toType, object? conversionHint, out object? result)
        {
            result = from != null
                ? ((Option<Tick>) from).Map(ToString).IfNone(() => string.Empty)
                : string.Empty;

            return from != null && ((Option<Tick>) from).IsSome;
        }

        private static string ToString(Tick t) => ((int) t).ToString("N0", CultureInfo.CurrentCulture);

    }
}