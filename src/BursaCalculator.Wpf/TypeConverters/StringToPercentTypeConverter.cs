using System;
using BursaCalculator.Core.Infrastructure;
using LanguageExt;
using ReactiveUI;
using static BursaCalculator.Core.Infrastructure.PercentExtensions;
using static LanguageExt.Prelude;

namespace BursaCalculator.Wpf.TypeConverters
{
    public class StringToPercentTypeConverter : IBindingTypeConverter
    {
        public int GetAffinityForObjects(Type fromType, Type toType) =>
            fromType == typeof(string) && toType == typeof(Option<Percent>) 
                ? 10 
                : 0;

        public bool TryConvert(object @from, Type toType, object conversionHint, out object result)
        {
            result = parseDecimal((string) from)
                .Map(Percent);
            return true;
        }
    }
}