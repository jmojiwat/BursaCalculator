namespace BursaCalculator.Core
{
    public static class SalesServiceTaxExtensions
    {
        public static SalesServiceTax CalculateSalesServiceTax(SalesServiceTaxRate taxRate, decimal amount) =>
            new SalesServiceTax
            {
                Brokerage = taxRate.Brokerage * amount,
                ClearingFee = taxRate.ClearingFee * amount,
                StampDuty = taxRate.StampDuty * amount
            };

        public static decimal ToTotal(SalesServiceTax tax) =>
            tax.Brokerage + tax.ClearingFee + tax.StampDuty;
    }
}