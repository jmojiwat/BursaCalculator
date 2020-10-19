using System;
using BursaCalculator.Core.Infrastructure;
using ReactiveUI;
using static BursaCalculator.Core.Infrastructure.TickExtensions;

namespace BursaCalculator.Wpf.TypeConverters
{
    public class StringToTickTypeConverter : IBindingTypeConverter
    {
        public int GetAffinityForObjects(Type fromType, Type toType) =>
            fromType == typeof(string) && toType == typeof(Tick) 
                ? 10 
                : 0;

        public bool TryConvert(object @from, Type toType, object conversionHint, out object result)
        {
            var isSuccess = int.TryParse((string) @from, out var tick);
            result = Tick(tick);
            return isSuccess;
        }
    }
}