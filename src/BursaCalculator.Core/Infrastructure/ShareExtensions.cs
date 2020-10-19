namespace BursaCalculator.Core.Infrastructure
{
    public static class ShareExtensions
    {
        public static Share Share(int value) => new Share(value);

        public static Lot ToLot(Share share) => new Lot(share / 100);
    }
}