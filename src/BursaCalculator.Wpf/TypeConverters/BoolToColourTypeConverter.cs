using System;
using System.Windows.Media;
using ReactiveUI;

namespace BursaCalculator.Wpf.TypeConverters
{
    public class BoolToColourTypeConverter : IBindingTypeConverter
    {
        private static readonly SolidColorBrush ValidForegroundBrush = Brushes.Black;
        private static readonly SolidColorBrush InvalidForegroundBrush = Brushes.Red;

        public int GetAffinityForObjects(Type fromType, Type toType) =>
            fromType == typeof(bool) && toType == typeof(SolidColorBrush)
                ? 10
                : 0;

        public bool TryConvert(object from, Type toType, object conversionHint, out object result)
        {
            result = (bool) @from;

            result = (bool) @from ? ValidForegroundBrush : InvalidForegroundBrush;
            return true;
        }
    }
}