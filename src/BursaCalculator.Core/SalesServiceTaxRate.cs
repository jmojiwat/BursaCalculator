using BursaCalculator.Core.Infrastructure;

namespace BursaCalculator.Core
{
    public class SalesServiceTaxRate
    {
        public SalesServiceTaxRate(Percent brokerage, Percent clearingFee, Percent stampDuty)
        {
            Brokerage = brokerage;
            ClearingFee = clearingFee;
            StampDuty = stampDuty;
        }

        public Percent Brokerage { get; }
        public Percent ClearingFee { get; }
        public Percent StampDuty { get; }
    }
}