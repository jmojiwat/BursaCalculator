using System;
using BursaCalculator.Core.Infrastructure;
using ReactiveUI;
using static BursaCalculator.Core.Infrastructure.PercentExtensions;

namespace BursaCalculator.Wpf.TypeConverters
{
    public class StringToPercentTypeConverter : IBindingTypeConverter
    {
        public int GetAffinityForObjects(Type fromType, Type toType) =>
            fromType == typeof(string) && toType == typeof(Percent) 
                ? 10 
                : 0;

        public bool TryConvert(object @from, Type toType, object conversionHint, out object result)
        {
            var isSuccess = decimal.TryParse((string) @from, out var percent);
            result = Percent(percent);
            return isSuccess;
        }
    }
}