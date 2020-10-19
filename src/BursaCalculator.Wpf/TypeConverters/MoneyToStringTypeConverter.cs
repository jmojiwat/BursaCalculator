using System;
using System.Globalization;
using BursaCalculator.Core.Infrastructure;
using ReactiveUI;

namespace BursaCalculator.Wpf.TypeConverters
{
    public class MoneyToStringTypeConverter : IBindingTypeConverter
    {
        public int GetAffinityForObjects(Type fromType, Type toType) =>
            fromType == typeof(Money) && toType == typeof(string) 
                ? 10 
                : 0;

        public bool TryConvert(object @from, Type toType, object conversionHint, out object result)
        {
            var format = conversionHint == null
                ? "N3"
                : (string) conversionHint;
            
            var money = (decimal)(Money) @from;
            var fractional = money - Math.Truncate(money);
            result = fractional == decimal.Zero
                ? money.ToString("N0", CultureInfo.CurrentCulture)
                : money.ToString(format, CultureInfo.CurrentCulture);

            return true;
        }
    }
}