using BursaCalculator.Core.Infrastructure;

namespace BursaCalculator.Core
{
    public static class SalesServiceTaxRateExtensions
    {
        public static SalesServiceTaxRate SalesServiceTaxRate(Percent brokerage, Percent clearingFee, Percent stampDuty) =>
            new SalesServiceTaxRate(brokerage, clearingFee, stampDuty);
    }
}