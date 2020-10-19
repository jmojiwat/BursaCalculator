using System;
using System.Globalization;
using BursaCalculator.Core.Infrastructure;
using ReactiveUI;

namespace BursaCalculator.Wpf.TypeConverters
{
    public class LotToStringTypeConverter : IBindingTypeConverter
    {
        public int GetAffinityForObjects(Type fromType, Type toType) =>
            fromType == typeof(Lot) && toType == typeof(string) 
                ? 10 
                : 0;

        public bool TryConvert(object @from, Type toType, object conversionHint, out object result)
        {
            result = ((int)(Lot) @from).ToString("N0", CultureInfo.CurrentUICulture);
            return true;
        }
    }
}