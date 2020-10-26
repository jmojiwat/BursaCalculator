using System.Reflection;
using System.Windows;
using BursaCalculator.Wpf.TypeConverters;
using ReactiveUI;
using Splat;
using static Splat.Locator;

namespace BursaCalculator.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            CurrentMutable.RegisterViewsForViewModels(Assembly.GetCallingAssembly());
            
            CurrentMutable.RegisterConstant(new MoneyToStringTypeConverter(), typeof(IBindingTypeConverter));
            CurrentMutable.RegisterConstant(new StringToMoneyTypeConverter(), typeof(IBindingTypeConverter));
            CurrentMutable.RegisterConstant(new PercentToStringTypeConverter(), typeof(IBindingTypeConverter));
            CurrentMutable.RegisterConstant(new StringToPercentTypeConverter(), typeof(IBindingTypeConverter));
            CurrentMutable.RegisterConstant(new IntToStringTypeConverter(), typeof(IBindingTypeConverter));
            CurrentMutable.RegisterConstant(new StringToIntTypeConverter(), typeof(IBindingTypeConverter));
            CurrentMutable.RegisterConstant(new TickToStringTypeConverter(), typeof(IBindingTypeConverter));
            CurrentMutable.RegisterConstant(new StringToTickTypeConverter(), typeof(IBindingTypeConverter));
        }
    }
}
