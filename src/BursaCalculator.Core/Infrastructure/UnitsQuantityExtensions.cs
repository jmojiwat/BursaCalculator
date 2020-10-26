namespace BursaCalculator.Core.Infrastructure
{
    public static class UnitsQuantityExtensions
    {
        public static Quantity Shares(this int @this) => new Quantity(@this);
        public static Quantity Lots(this int @this) => new Quantity(@this * 100);

        public static readonly Quantity s = 1.Shares();
        public static readonly Quantity bl = 1.Lots();
    }
}