using System;
using LanguageExt;
using ReactiveUI;

namespace BursaCalculator.Wpf.TypeConverters
{
    public class StringToIntTypeConverter : IBindingTypeConverter
    {
        public int GetAffinityForObjects(Type fromType, Type toType) =>
            fromType == typeof(string) && toType == typeof(Option<int>)
                ? 10
                : 0;

        public bool TryConvert(object? from, Type toType, object? conversionHint, out object? result)
        {
            result = Prelude.parseInt((string) @from);
            return true;
        }
    }
}