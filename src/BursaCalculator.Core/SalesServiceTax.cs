namespace BursaCalculator.Core
{
    public record SalesServiceTax
    {
        public decimal Brokerage { get; init; }
        public decimal ClearingFee { get; init; }
        public decimal StampDuty { get; init; }
    }
}