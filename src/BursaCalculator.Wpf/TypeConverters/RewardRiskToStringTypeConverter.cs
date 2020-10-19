using System;
using System.Globalization;
using ReactiveUI;

namespace BursaCalculator.Wpf.TypeConverters
{
    public class RewardRiskToStringTypeConverter : IBindingTypeConverter
    {
        public int GetAffinityForObjects(Type fromType, Type toType) =>
            fromType == typeof(decimal) && toType == typeof(string) 
                ? 10 
                : 0;

        public bool TryConvert(object @from, Type toType, object conversionHint, out object result)
        {
            var percent = (decimal) @from;
            var fractional = percent - Math.Truncate(percent);
            result = fractional == decimal.Zero
                ? percent.ToString("N0", CultureInfo.CurrentCulture)
                : percent.ToString("N2", CultureInfo.CurrentCulture);

            return true;
        }
    }
}