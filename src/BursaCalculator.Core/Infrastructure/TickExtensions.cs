using System.Linq;
using LanguageExt;
using static BursaCalculator.Core.Infrastructure.MoneyExtensions;

namespace BursaCalculator.Core.Infrastructure
{
    public static class TickExtensions
    {
        public static Tick ToTick(Money fromPrice, Money toPrice)
        {
            var tickSize = new[] { fromPrice, toPrice }
                .Min()
                .Apply(ToTickSize);
            
            return Tick((int)((fromPrice - toPrice) / tickSize));
        }

        public static Tick Tick(int value) => new Tick(value);
    }
}