using System;
using System.Globalization;
using LanguageExt;
using ReactiveUI;

namespace BursaCalculator.Wpf.TypeConverters
{
    public class IntToStringTypeConverter : IBindingTypeConverter
    {
        public int GetAffinityForObjects(Type fromType, Type toType) =>
            fromType == typeof(Option<int>) && toType == typeof(string)
                ? 10
                : 0;

        public bool TryConvert(object? from, Type toType, object? conversionHint, out object? result)
        {
            result = @from != null
                ? ((Option<int>) @from).Map(ToString).IfNone(() => string.Empty)
                : string.Empty;

            return @from != null && ((Option<int>) @from).IsSome;
        }

        private static string ToString(int i) => i.ToString("N0", CultureInfo.CurrentCulture);
    }
}