using System;
using BursaCalculator.Core.Infrastructure;
using ReactiveUI;
using static BursaCalculator.Core.Infrastructure.ShareExtensions;

namespace BursaCalculator.Wpf.TypeConverters
{
    public class StringToShareTypeConverter : IBindingTypeConverter
    {
        public int GetAffinityForObjects(Type fromType, Type toType) =>
            fromType == typeof(string) && toType == typeof(Share) 
                ? 10 
                : 0;

        public bool TryConvert(object @from, Type toType, object conversionHint, out object result)
        {
            var isSuccess = int.TryParse((string) @from, out var share);
            result = Share(share);
            return isSuccess;
        }
    }
}