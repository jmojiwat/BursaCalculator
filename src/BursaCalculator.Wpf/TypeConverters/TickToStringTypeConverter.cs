using System;
using System.Globalization;
using BursaCalculator.Core.Infrastructure;
using ReactiveUI;

namespace BursaCalculator.Wpf.TypeConverters
{
    public class TickToStringTypeConverter : IBindingTypeConverter
    {
        public int GetAffinityForObjects(Type fromType, Type toType) =>
            fromType == typeof(Tick) && toType == typeof(string) 
                ? 10 
                : 0;

        public bool TryConvert(object @from, Type toType, object conversionHint, out object result)
        {
            result = ((int)(Tick) @from).ToString("N0", CultureInfo.CurrentUICulture);
            return true;
        }
    }
}