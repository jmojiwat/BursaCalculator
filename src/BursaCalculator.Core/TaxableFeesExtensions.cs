namespace BursaCalculator.Core
{
    public static class TaxableFeesExtensions
    {
        public static TaxableFees TaxableFees(decimal brokerage, decimal clearingFee, decimal stampDuty) =>
            new TaxableFees(brokerage, clearingFee, stampDuty);
    }
}