using System;
using System.Globalization;
using BursaCalculator.Core.Infrastructure;
using ReactiveUI;

namespace BursaCalculator.Wpf.TypeConverters
{
    public class PercentToStringTypeConverter : IBindingTypeConverter
    {
        public int GetAffinityForObjects(Type fromType, Type toType) =>
            fromType == typeof(Percent) && toType == typeof(string) 
                ? 10 
                : 0;

        public bool TryConvert(object @from, Type toType, object conversionHint, out object result)
        {
            var percent = (decimal)(Percent) @from;
            var fractional = percent - Math.Truncate(percent);
            result = fractional == decimal.Zero
                ? percent.ToString("N0", CultureInfo.CurrentCulture)
                : percent.ToString("N2", CultureInfo.CurrentCulture);

            return true;
        }
    }
}