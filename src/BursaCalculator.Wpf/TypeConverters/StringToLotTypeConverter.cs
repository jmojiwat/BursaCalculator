using System;
using BursaCalculator.Core.Infrastructure;
using ReactiveUI;
using static BursaCalculator.Core.Infrastructure.LotExtensions;

namespace BursaCalculator.Wpf.TypeConverters
{
    public class StringToLotTypeConverter : IBindingTypeConverter
    {
        public int GetAffinityForObjects(Type fromType, Type toType) =>
            fromType == typeof(string) && toType == typeof(Lot) 
                ? 10 
                : 0;

        public bool TryConvert(object @from, Type toType, object conversionHint, out object result)
        {
            var isSuccess = int.TryParse((string) @from, out var lot);
            result = Lot(lot);
            return isSuccess;
        }
    }
}