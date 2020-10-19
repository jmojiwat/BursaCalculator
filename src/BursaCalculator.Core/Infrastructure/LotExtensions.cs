namespace BursaCalculator.Core.Infrastructure
{
    public static class LotExtensions
    {
        public static Share ToShare(Lot lot) => new Share(lot * 100);
        
        public static Lot Lot(int value) => new Lot(value);
    }
}