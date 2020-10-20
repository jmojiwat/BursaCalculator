using System;
using System.Globalization;
using BursaCalculator.Core.Infrastructure;
using LanguageExt;
using ReactiveUI;

namespace BursaCalculator.Wpf.TypeConverters
{
    public class LotToStringTypeConverter : IBindingTypeConverter
    {
        public int GetAffinityForObjects(Type fromType, Type toType) =>
            fromType == typeof(Option<Lot>) && toType == typeof(string)
                ? 10
                : 0;

        public bool TryConvert(object? from, Type toType, object? conversionHint, out object? result)
        {
            result = from != null
                ? ((Option<Lot>) from).Map(ToString).IfNone(() => string.Empty)
                : string.Empty;

            return from != null && ((Option<Lot>) from).IsSome;
        }

        private static string ToString(Lot l) => ((int) l).ToString("N0", CultureInfo.CurrentCulture);
    }
}