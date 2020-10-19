using System;
using BursaCalculator.Core.Infrastructure;
using ReactiveUI;
using static BursaCalculator.Core.Infrastructure.MoneyExtensions;

namespace BursaCalculator.Wpf.TypeConverters
{
    public class StringToMoneyTypeConverter : IBindingTypeConverter
    {
        public int GetAffinityForObjects(Type fromType, Type toType) =>
            fromType == typeof(string) && toType == typeof(Money)
                ? 10
                : 0;

        public bool TryConvert(object from, Type toType, object conversionHint, out object result)
        {
            var isSuccess = decimal.TryParse((string) from, out var money);
            result = Money(money);
            return isSuccess;
        }
    }
}