using System;
using BursaCalculator.Core.Infrastructure;
using LanguageExt;
using ReactiveUI;
using static BursaCalculator.Core.Infrastructure.MoneyExtensions;
using static LanguageExt.Prelude;

namespace BursaCalculator.Wpf.TypeConverters
{
    public class StringToMoneyTypeConverter : IBindingTypeConverter
    {
        public int GetAffinityForObjects(Type fromType, Type toType) =>
            fromType == typeof(string) && toType == typeof(Option<Money>)
                ? 10
                : 0;

        public bool TryConvert(object? from, Type toType, object? conversionHint, out object? result)
        {
            result = parseDecimal((string) from)
                .Map(Money);
            return true;
        }
    }
}