using System;
using BursaCalculator.Core.Infrastructure;
using LanguageExt;
using ReactiveUI;
using static BursaCalculator.Core.Infrastructure.TickExtensions;
using static LanguageExt.Prelude;

namespace BursaCalculator.Wpf.TypeConverters
{
    public class StringToTickTypeConverter : IBindingTypeConverter
    {
        public int GetAffinityForObjects(Type fromType, Type toType) =>
            fromType == typeof(string) && toType == typeof(Option<Tick>) 
                ? 10 
                : 0;

        public bool TryConvert(object? from, Type toType, object? conversionHint, out object? result)
        {
            result = parseInt((string) from)
                .Map(Tick);
            return true;
        }
    }
}