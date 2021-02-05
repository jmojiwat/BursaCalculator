namespace BursaCalculator.Core
{
    public record TaxableFees
    {
        public TaxableFees(decimal brokerage, decimal clearingFee, decimal stampDuty)
        {
            Brokerage = brokerage;
            ClearingFee = clearingFee;
            StampDuty = stampDuty;
        }

        public decimal Brokerage { get; private init; }
        public decimal ClearingFee { get; private init; }
        public decimal StampDuty { get; private init; }
    }
}