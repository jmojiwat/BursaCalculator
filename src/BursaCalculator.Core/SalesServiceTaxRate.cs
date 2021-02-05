using BursaCalculator.Core.Infrastructure;

namespace BursaCalculator.Core
{
    public record SalesServiceTaxRate
    {
        public SalesServiceTaxRate(Percent brokerage, Percent clearingFee, Percent stampDuty)
        {
            Brokerage = brokerage;
            ClearingFee = clearingFee;
            StampDuty = stampDuty;
        }

        public Percent Brokerage { get; private init; }
        public Percent ClearingFee { get; private init; }
        public Percent StampDuty { get; private init; }
    }
}